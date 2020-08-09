using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using VisualElementsManifest.Data;

namespace VisualElementsManifest {

    public interface VisualElementsManifestEditor {

        Application LoadFile(string filePath);

        Application LoadString(string xmlContents);

        void Save(Application application, string filePath);

        string Save(Application application);

        void Save(Application application, XmlWriter xmlWriter);

        void RelativizeUris(Application application, string pathsRelativeTo);

        Application CreateDefault();

    }

    public class VisualElementsManifestEditorImpl: VisualElementsManifestEditor {

        private readonly XmlSerializer _applicationSerializer = new XmlSerializer(typeof(ApplicationImpl));

        private static readonly XmlWriterSettings DefaultXmlWriterSettings = new XmlWriterSettings {
            OmitXmlDeclaration  = true,
            Indent              = true,
            NewLineOnAttributes = true,
            Encoding            = new UTF8Encoding(false)
        };

        public Application LoadFile(string filePath) {
            using Stream fileStream = new FileStream(filePath, FileMode.Open);
            return (Application) _applicationSerializer.Deserialize(fileStream);
        }

        public Application LoadString(string xmlContents) {
            using TextReader reader = new StringReader(xmlContents);
            return (Application) _applicationSerializer.Deserialize(reader);
        }

        public void Save(Application application, string filePath) {
            using var xmlWriter = XmlWriter.Create(filePath, DefaultXmlWriterSettings);
            Save(application, xmlWriter);
        }

        public string Save(Application application) {
            StringBuilder xmlText = new StringBuilder();
            Save(application, XmlWriter.Create(xmlText, DefaultXmlWriterSettings));
            return xmlText.ToString();
        }

        public void Save(Application application, XmlWriter xmlWriter) {
            _applicationSerializer.Serialize(xmlWriter, application, new XmlSerializerNamespaces(new[] {
                new XmlQualifiedName("xsi", XmlSchema.InstanceNamespace)
            }));
        }

        public void RelativizeUris(Application application, string pathsRelativeTo) {
            string? RelativizeUri(string? originalUri) => originalUri != null ? Path.Combine(pathsRelativeTo, originalUri) : null;

            application.VisualElements.Square150X150Logo = RelativizeUri(application.VisualElements.Square150X150Logo);
            application.VisualElements.Square70X70Logo   = RelativizeUri(application.VisualElements.Square70X70Logo);
        }

        public Application CreateDefault() {
            return new ApplicationImpl();
        }

    }

}