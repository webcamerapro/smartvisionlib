using System;

namespace eLibNet4VideoSurveillanceApi.Constants
{
    /// <summary>
    ///     Класс API констант.
    /// </summary>
    public static class ApiFieldConstants
    {
        /// <summary>
        ///     JWT адрес.
        /// </summary>
        public static readonly Uri JwtAddress = new Uri("https://jwt.videosurveillance.cloud/Token/");

        /// <summary>
        ///     API адрес.
        /// </summary>
        public static readonly Uri ApiAddress = new Uri("https://api.videosurveillance.cloud/en/api/");
    }
}