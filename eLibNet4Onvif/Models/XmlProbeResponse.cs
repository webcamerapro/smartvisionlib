using System.Xml.Serialization;

namespace eLibNet4Onvif.Models
{
    [XmlRoot("Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    internal class XmlProbeResponse
    {
        [XmlElement(Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public Header Header { get; set; } = new Header();

        [XmlElement(Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public Body Body { get; set; } = new Body();
    }

    internal class Header
    {
        [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2004/08/addressing")]
        public string MessageId { get; set; } = string.Empty;

        [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2004/08/addressing")]
        public string RelatesTo { get; set; } = string.Empty;

        [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2004/08/addressing")]
        public string To { get; set; } = string.Empty;

        [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
        public string AppSequence { get; set; } = string.Empty;
    }

    internal class Body
    {
        [XmlArray(Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
        public ProbeMatch[] ProbeMatches { get; set; } = System.Array.Empty<ProbeMatch>();
    }

    internal class ProbeMatch
    {
        [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2004/08/addressing")]
        public EndpointReference EndpointReference { get; set; } = new EndpointReference();

        [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
        public string Types { get; set; } = string.Empty;

        [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
        public string Scopes { get; set; } = string.Empty;

        [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
        public string XAddrs { get; set; } = string.Empty;

        [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
        public string MetadataVersion { get; set; } = string.Empty;
    }

    internal class EndpointReference
    {
        [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2004/08/addressing")]
        public string Address { get; set; } = string.Empty;
    }
}