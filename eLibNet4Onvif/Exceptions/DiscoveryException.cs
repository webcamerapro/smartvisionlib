using System;

namespace eLibNet4Onvif.Exceptions
{
    /// <summary>
    ///     Исключение, возникающее при ошибках обнаружения.
    /// </summary>
    public class DiscoveryException : Exception
    {
        internal DiscoveryException() { }

        internal DiscoveryException(string message) : base(message) { }

        internal DiscoveryException(string message, Exception inner) : base(message, inner) { }
    }
}