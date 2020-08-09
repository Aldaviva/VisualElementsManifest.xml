using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace VisualElementsManifest.Data {

    public interface Application {

        VisualElements VisualElements { get; set; }

    }

    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [XmlRoot("Application", Namespace = "", IsNullable = false)]
    public class ApplicationImpl: Application {

        VisualElements Application.VisualElements {
            get => VisualElements;
            set => VisualElements = (VisualElementsImpl) value;
        }

        public VisualElementsImpl VisualElements { get; set; } = new VisualElementsImpl();

        protected bool Equals(ApplicationImpl other) {
            return VisualElements.Equals(other.VisualElements);
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }

            if (ReferenceEquals(this, obj)) {
                return true;
            }

            return obj.GetType() == GetType() && Equals((ApplicationImpl) obj);

        }

        public override int GetHashCode() {
            return VisualElements.GetHashCode();
        }

    }

}