using System;
using System.Xml.Serialization;

namespace VisualElementsManifest.Data {

    [Serializable]
    public enum TextBrightness {

        [XmlEnum("light")]
        Light,

        [XmlEnum("dark")]
        Dark

    }

    [Serializable]
    public enum NameVisibility {

        [XmlEnum("on")]
        On,

        [XmlEnum("off")]
        Off

    }

}