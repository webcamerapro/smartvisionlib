namespace eLibNet4Onvif.Constants
{
    /// <summary>
    ///     Класс констант.
    /// </summary>
    internal class FieldConstants
    {
        /// <summary>
        ///     Адрес многоадресной рассылки
        /// </summary>
        internal const string MulticastAddress = "239.255.255.250";

        /// <summary>
        ///     Порт многоадресной рассылки
        /// </summary>
        internal const int MulticastPort = 3702;

        /// <summary>
        ///     Сообщение для поиска Onvif устройства
        /// </summary>
        internal const string ProbeMessage =
            "<e:Envelope xmlns:e=\"http://www.w3.org/2003/05/soap-envelope\" "
            + "xmlns:w=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\" "
            + "xmlns:d=\"http://schemas.xmlsoap.org/ws/2005/04/discovery\"> "
            + "<e:Header> "
            + "<w:MessageID> uuid:{0} </w:MessageID> "
            + "<w:ReplyTo> <w:Address> http://schemas.xmlsoap.org/ws/2004/08/addressing/role/anonymous </w:Address> </w:ReplyTo> "
            + "<w:To e:mustUnderstand=\"1\"> urn:schemas-xmlsoap-org:ws:2005:04:discovery </w:To> "
            + "<w:Action e:mustUnderstand=\"1\"> http://schemas.xmlsoap.org/ws/2005/04/discovery/Probe </w:Action> "
            + "</e:Header> "
            + "<e:Body> "
            + "<d:Probe> "
            + "<d:Types xmlns:dn=\"http://www.onvif.org/ver10/network/wsdl\"> dn:NetworkVideoTransmitter </d:Types> "
            + "<d:Probe> "
            + "</e:Body> "
            + "</e:Envelope>";
    }
}