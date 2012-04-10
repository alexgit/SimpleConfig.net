using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DynamicConfig.Web
{
    [Serializable]
    [XmlRoot(ElementName = "clientAccess")]
    public class ClientAccess
    {
        [XmlElement("directorate")]
        public DirectorateSettings Directorate { get; set; }

        [XmlElement("team")]
        public TeamSettings Team { get; set; }

        [XmlElement("dialogue")]
        public DialogueSettings Dialogue { get; set; }

        [XmlElement("file")]
        public FileSettings File { get; set; }        
    }

    [Serializable]
    public class DirectorateSettings
    {
        [XmlAttribute("security")]
        public bool Security { get; set; }

        [XmlAttribute("preview")]
        public bool Preview { get; set; }
    }

    [Serializable]
    public class TeamSettings
    {
        [XmlAttribute("security")]
        public bool Security { get; set; }

        [XmlAttribute("detailed")]
        public bool Detailed { get; set; }
    }

    [Serializable]
    public class DialogueSettings
    {
        [XmlAttribute("teamSecurity")]
        public bool TeamSecurity { get; set; }
    }

    [Serializable]
    public class FileSettings
    {
        [XmlAttribute("teamSecurity")]
        public bool TeamSecurity { get; set; }
    }    
}
