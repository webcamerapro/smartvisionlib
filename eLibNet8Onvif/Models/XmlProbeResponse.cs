using System.Xml.Serialization;

namespace eLibNet8Onvif.Models;

[XmlRoot("Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
internal class XmlProbeResponse
{
    [XmlElement(Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public Header Header { get; init; } = new();

    [XmlElement(Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public Body Body { get; init; } = new();
}

internal class Header
{
    [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2004/08/addressing")]
    public string MessageId { get; init; } = string.Empty;

    [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2004/08/addressing")]
    public string RelatesTo { get; init; } = string.Empty;

    [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2004/08/addressing")]
    public string To { get; init; } = string.Empty;

    [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
    public string AppSequence { get; init; } = string.Empty;
}

internal class Body
{
    [XmlArray(Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
    public ProbeMatch[] ProbeMatches { get; init; } = [];
}

internal class ProbeMatch
{
    [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2004/08/addressing")]
    public EndpointReference EndpointReference { get; init; } = new();

    [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
    public string Types { get; init; } = string.Empty;

    [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
    public string Scopes { get; init; } = string.Empty;

    [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
    public string XAddrs { get; init; } = string.Empty;

    [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
    public string MetadataVersion { get; init; } = string.Empty;
}

internal class EndpointReference
{
    [XmlElement(Namespace = "http://schemas.xmlsoap.org/ws/2004/08/addressing")]
    public string Address { get; init; } = string.Empty;
}