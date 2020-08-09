using System;
using System.Drawing;
using FluentAssertions;
using Xunit;
using ColorTranslator = VisualElementsManifest.Marshal.ColorTranslator;

namespace Tests {

    public class ColorTranslatorTest {

        [Fact]
        public void Serialize() {
            Assert.Equal("#FFAA00", ColorTranslator.ToHtml(Color.FromArgb(0x00, 0xFF, 0xAA, 0x00)));
            Assert.Equal("#FFFFFF", ColorTranslator.ToHtml(Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF)));
            Assert.Equal("#000000", ColorTranslator.ToHtml(Color.FromArgb(0x00, 0x00, 0x00, 0x00)));
        }

        [Fact]
        public void Deserialize() {
            const string input = "#FFAA00";
            Color actual = ColorTranslator.FromHtml(input);
            Assert.Equal(0xFF, actual.R);
            Assert.Equal(0xAA, actual.G);
            Assert.Equal(0x00, actual.B);
        }

        [Fact]
        public void DeserializeShort() {
            const string input = "#FA0";
            Color actual = ColorTranslator.FromHtml(input);
            Assert.Equal(0xFF, actual.R);
            Assert.Equal(0xAA, actual.G);
            Assert.Equal(0x00, actual.B);
        }

        [Fact]
        public void RequiresLeadingPoundSign() {
            Func<Color> thrower = () => ColorTranslator.FromHtml("123456");
            thrower.Should().Throw<ArgumentException>("input must start with #");
        }

        [Fact]
        public void RequiresLength() {
            Func<Color> thrower = () => ColorTranslator.FromHtml("#12345");
            thrower.Should().Throw<ArgumentException>("input must be 4 or 7 characters (including leading #)");
        }

        [Fact]
        public void RequiresHexCharacters() {
            Func<Color> thrower = () => ColorTranslator.FromHtml("#12345z");
            thrower.Should().Throw<ArgumentException>("input must be 4 or 7 characters (including leading #)");
        }

    }

}