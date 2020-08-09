using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Serialization;
using VisualElementsManifest.Marshal;

namespace VisualElementsManifest.Data {

    public interface VisualElements {

        string? Square150X150Logo { get; set; }
        string? Square70X70Logo { get; set; }
        TextBrightness ForegroundText { get; set; }
        NameVisibility ShowNameOnSquare150X150Logo { get; set; }
        Color BackgroundColor { get; set; }

    }

    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [GeneratedCode("xsd", "4.8.3928.0")]
    [XmlType("VisualElements")]
    public class VisualElementsImpl: VisualElements {

        [XmlAttribute("Square150x150Logo")]
        public string? Square150X150Logo { get; set; }

        [XmlAttribute("Square70x70Logo")]
        public string? Square70X70Logo { get; set; }

        [XmlAttribute]
        public TextBrightness ForegroundText { get; set; } = TextBrightness.Light;

        [XmlAttribute("ShowNameOnSquare150x150Logo")]
        public NameVisibility ShowNameOnSquare150X150Logo { get; set; } = NameVisibility.On;

        [XmlAttribute("BackgroundColor")]
        public string BackgroundColorString { get; set; } = string.Empty;

        [XmlIgnore]
        public Color BackgroundColor {
            get => ColorTranslator.FromHtml(BackgroundColorString);
            set => BackgroundColorString = ColorTranslator.ToHtml(value);
        }

        protected bool Equals(VisualElementsImpl other) {
            return Square150X150Logo == other.Square150X150Logo && Square70X70Logo == other.Square70X70Logo && ForegroundText == other.ForegroundText &&
                   BackgroundColorString == other.BackgroundColorString && ShowNameOnSquare150X150Logo == other.ShowNameOnSquare150X150Logo;
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }

            if (ReferenceEquals(this, obj)) {
                return true;
            }

            return obj.GetType() == GetType() && Equals((VisualElementsImpl) obj);
        }

        public override int GetHashCode() {
            unchecked {
                int hashCode = (Square150X150Logo != null ? Square150X150Logo.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Square70X70Logo != null ? Square70X70Logo.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) ForegroundText;
                hashCode = (hashCode * 397) ^ BackgroundColorString.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) ShowNameOnSquare150X150Logo;
                return hashCode;
            }
        }

    }

}