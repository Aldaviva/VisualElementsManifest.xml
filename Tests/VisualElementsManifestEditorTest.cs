using System.Drawing;
using System.IO;
using VisualElementsManifest;
using VisualElementsManifest.Data;
using Xunit;
using Xunit.Abstractions;
using ColorTranslator = VisualElementsManifest.Marshal.ColorTranslator;

namespace Tests {

    public class VisualElementsManifestEditorTest {

        private readonly VisualElementsManifestEditor _editor = new VisualElementsManifestEditorImpl();
        private readonly ITestOutputHelper _outputHelper;

        public VisualElementsManifestEditorTest(ITestOutputHelper outputHelper) {
            this._outputHelper = outputHelper;
        }

        [Fact]
        public void LoadExample() {
            const string xml = @"<Application xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
    <VisualElements
        BackgroundColor=""#FF0000""
        ShowNameOnSquare150x150Logo=""on""
        ForegroundText=""light""
        Square150x150Logo=""Assets\150x150Logo.png""
        Square70x70Logo=""Assets\70x70Logo.png""/>
</Application>";

            LoadAndAssert(xmlText: xml,
                expectedShowNameOnSquare150X150Logo: NameVisibility.On,
                expectedSquare150X150Logo: @"Assets\150x150Logo.png",
                expectedSquare70X70Logo: @"Assets\70x70Logo.png",
                expectedForegroundText: TextBrightness.Light,
                expectedBackgroundColor: "#FF0000");
        }

        [Fact]
        public void LoadVivaldi() {
            const string xml = @"<Application xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
  <VisualElements
      ShowNameOnSquare150x150Logo='on'
      Square150x150Logo='3.2.1967.41\VisualElements\Logo.png'
      Square70x70Logo='3.2.1967.41\VisualElements\SmallLogo.png'
      Square44x44Logo='3.2.1967.41\VisualElements\SmallLogo.png'
      ForegroundText='light'
      BackgroundColor='#EF3939'/>
</Application>";

            LoadAndAssert(xmlText: xml,
                expectedShowNameOnSquare150X150Logo: NameVisibility.On,
                expectedSquare150X150Logo: @"3.2.1967.41\VisualElements\Logo.png",
                expectedSquare70X70Logo: @"3.2.1967.41\VisualElements\SmallLogo.png",
                expectedForegroundText: TextBrightness.Light,
                expectedBackgroundColor: "#EF3939");
        }

        [Fact]
        public void LoadOneDrive() {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Application xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <VisualElements ForegroundText=""light"" BackgroundColor=""#484644"" ShowNameOnSquare150x150Logo=""on"" Square150x150Logo=""LogoImages\OneDriveMedTile.png"" Square70x70Logo=""LogoImages\OneDriveSmallTile.png""></VisualElements>
</Application>";

            LoadAndAssert(xmlText: xml,
                expectedShowNameOnSquare150X150Logo: NameVisibility.On,
                expectedSquare150X150Logo: @"LogoImages\OneDriveMedTile.png",
                expectedSquare70X70Logo: @"LogoImages\OneDriveSmallTile.png",
                expectedForegroundText: TextBrightness.Light,
                expectedBackgroundColor: "#484644");
        }

        [Fact]
        public void LoadVisualStudio() {
            const string xml = @"<Application xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <VisualElements
      BackgroundColor=""#2D2D30""
      ShowNameOnSquare150x150Logo=""on""
      ForegroundText=""light""
      Square150x150Logo=""Assets\VisualStudio.150x150.png""
      Square70x70Logo=""Assets\VisualStudio.70x70.png"" />
</Application>";

            LoadAndAssert(xmlText: xml,
                expectedShowNameOnSquare150X150Logo: NameVisibility.On,
                expectedSquare150X150Logo: @"Assets\VisualStudio.150x150.png",
                expectedSquare70X70Logo: @"Assets\VisualStudio.70x70.png",
                expectedForegroundText: TextBrightness.Light,
                expectedBackgroundColor: "#2D2D30");
        }

        [Fact]
        public void LoadVisualStudioInstaller() {
            const string xml = @"<Application xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <VisualElements
      BackgroundColor=""#2D2D30""
      ShowNameOnSquare150x150Logo=""on""
      ForegroundText=""light""
      Square150x150Logo=""Assets\Installer.150x150.png""
      Square70x70Logo=""Assets\Installer.70x70.png"" />
</Application>
";

            LoadAndAssert(xmlText: xml,
                expectedShowNameOnSquare150X150Logo: NameVisibility.On,
                expectedSquare150X150Logo: @"Assets\Installer.150x150.png",
                expectedSquare70X70Logo: @"Assets\Installer.70x70.png",
                expectedForegroundText: TextBrightness.Light,
                expectedBackgroundColor: "#2D2D30");
        }

        [Fact]
        public void LoadExcel() {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Application xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <VisualElements ForegroundText=""light"" BackgroundColor=""#404040"" ShowNameOnSquare150x150Logo=""on"" Square150x150Logo=""LogoImages\ExcelLogo.png"" Square70x70Logo=""LogoImages\ExcelLogoSmall.png""></VisualElements>
</Application>";

            LoadAndAssert(xmlText: xml,
                expectedShowNameOnSquare150X150Logo: NameVisibility.On,
                expectedSquare150X150Logo: @"LogoImages\ExcelLogo.png",
                expectedSquare70X70Logo: @"LogoImages\ExcelLogoSmall.png",
                expectedForegroundText: TextBrightness.Light,
                expectedBackgroundColor: "#404040");
        }

        [Fact]
        public void LoadOutlook() {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Application xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <VisualElements ForegroundText=""light"" BackgroundColor=""#404040"" ShowNameOnSquare150x150Logo=""on"" Square150x150Logo=""LogoImages\OutlookLogo.png"" Square70x70Logo=""LogoImages\OutlookLogoSmall.png""></VisualElements>
</Application>";

            LoadAndAssert(xmlText: xml,
                expectedShowNameOnSquare150X150Logo: NameVisibility.On,
                expectedSquare150X150Logo: @"LogoImages\OutlookLogo.png",
                expectedSquare70X70Logo: @"LogoImages\OutlookLogoSmall.png",
                expectedForegroundText: TextBrightness.Light,
                expectedBackgroundColor: "#404040");
        }

        [Fact]
        public void LoadWord() {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Application xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <VisualElements ForegroundText=""light"" BackgroundColor=""#404040"" ShowNameOnSquare150x150Logo=""on"" Square150x150Logo=""LogoImages\WinWordLogo.png"" Square70x70Logo=""LogoImages\WinWordLogoSmall.png""></VisualElements>
</Application>";

            LoadAndAssert(xmlText: xml,
                expectedShowNameOnSquare150X150Logo: NameVisibility.On,
                expectedSquare150X150Logo: @"LogoImages\WinWordLogo.png",
                expectedSquare70X70Logo: @"LogoImages\WinWordLogoSmall.png",
                expectedForegroundText: TextBrightness.Light,
                expectedBackgroundColor: "#404040");
        }

        private void LoadAndAssert(
            string         xmlText,
            NameVisibility expectedShowNameOnSquare150X150Logo,
            string?        expectedSquare150X150Logo,
            string?        expectedSquare70X70Logo,
            TextBrightness expectedForegroundText,
            string         expectedBackgroundColor) {

            Application application = _editor.LoadString(xmlText);
            Assert.NotNull(application);

            VisualElements visualElements = application.VisualElements;
            Assert.NotNull(visualElements);

            Assert.Equal(expectedShowNameOnSquare150X150Logo, visualElements.ShowNameOnSquare150X150Logo);
            Assert.Equal(expectedSquare150X150Logo, visualElements.Square150X150Logo);
            Assert.Equal(expectedSquare70X70Logo, visualElements.Square70X70Logo);
            Assert.Equal(expectedForegroundText, visualElements.ForegroundText);
            Assert.Equal(expectedBackgroundColor, ColorTranslator.ToHtml(visualElements.BackgroundColor));
        }

        [Fact]
        public void Save() {
            Application application = new ApplicationImpl();
            application.VisualElements.ShowNameOnSquare150X150Logo = NameVisibility.On;
            application.VisualElements.Square150X150Logo           = @"Application\3.2.1967.41\VisualElements\Logo.png";
            application.VisualElements.Square70X70Logo             = @"Application\3.2.1967.41\VisualElements\SmallLogo.png";
            application.VisualElements.ForegroundText              = TextBrightness.Light;
            application.VisualElements.BackgroundColor             = Color.FromArgb(0xEF, 0x39, 0x39);

            string actual = _editor.Save(application);

            const string expected = @"<Application xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <VisualElements
    Square150x150Logo=""Application\3.2.1967.41\VisualElements\Logo.png""
    Square70x70Logo=""Application\3.2.1967.41\VisualElements\SmallLogo.png""
    ForegroundText=""light""
    ShowNameOnSquare150x150Logo=""on""
    BackgroundColor=""#EF3939"" />
</Application>";

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void SaveFile() {
            Application application = new ApplicationImpl();
            application.VisualElements.ShowNameOnSquare150X150Logo = NameVisibility.On;
            application.VisualElements.Square150X150Logo           = @"Application\3.2.1967.41\VisualElements\Logo.png";
            application.VisualElements.Square70X70Logo             = @"Application\3.2.1967.41\VisualElements\SmallLogo.png";
            application.VisualElements.ForegroundText              = TextBrightness.Light;
            application.VisualElements.BackgroundColor             = Color.FromArgb(0xEF, 0x39, 0x39);

            string filename = Path.GetTempFileName();
            _editor.Save(application, filename);

            const string expected = @"<Application xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <VisualElements
    Square150x150Logo=""Application\3.2.1967.41\VisualElements\Logo.png""
    Square70x70Logo=""Application\3.2.1967.41\VisualElements\SmallLogo.png""
    ForegroundText=""light""
    ShowNameOnSquare150x150Logo=""on""
    BackgroundColor=""#EF3939"" />
</Application>";

            string actual = File.ReadAllText(filename);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RelativizeUris() {
            const string input = @"<Application xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
  <VisualElements
      ShowNameOnSquare150x150Logo='on'
      Square150x150Logo='3.2.1967.41\VisualElements\Logo.png'
      Square70x70Logo='3.2.1967.41\VisualElements\SmallLogo.png'
      Square44x44Logo='3.2.1967.41\VisualElements\SmallLogo.png'
      ForegroundText='light'
      BackgroundColor='#EF3939'/>
</Application>";

            Application application = _editor.LoadString(input);
            _editor.RelativizeUris(application, "Application");

            Assert.Equal(@"Application\3.2.1967.41\VisualElements\Logo.png", application.VisualElements.Square150X150Logo);
            Assert.Equal(@"Application\3.2.1967.41\VisualElements\SmallLogo.png", application.VisualElements.Square70X70Logo);
        }

        [Fact]
        public void LoadFile() {
            const string fileContents = @"<Application xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
  <VisualElements
      ShowNameOnSquare150x150Logo='on'
      Square150x150Logo='3.2.1967.41\VisualElements\Logo.png'
      Square70x70Logo='3.2.1967.41\VisualElements\SmallLogo.png'
      Square44x44Logo='3.2.1967.41\VisualElements\SmallLogo.png'
      ForegroundText='light'
      BackgroundColor='#EF3939'/>
</Application>
";

            string filename = Path.GetTempFileName();
            File.WriteAllText(filename, fileContents);

            Application application = _editor.LoadFile(filename);

            Assert.Equal(@"3.2.1967.41\VisualElements\Logo.png", application.VisualElements.Square150X150Logo);
            Assert.Equal(@"3.2.1967.41\VisualElements\SmallLogo.png", application.VisualElements.Square70X70Logo);
            Assert.Equal(NameVisibility.On, application.VisualElements.ShowNameOnSquare150X150Logo);
            Assert.Equal(TextBrightness.Light, application.VisualElements.ForegroundText);
            Assert.Equal(ColorTranslator.FromHtml("#EF3939"), application.VisualElements.BackgroundColor);
            
            File.Delete(filename);
        }


    }

}