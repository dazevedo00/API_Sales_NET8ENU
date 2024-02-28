using Newtonsoft.Json;
using System.Text;

namespace API_Sales_NET8.Tests
{
    internal static class ContentHelper
    {
        /// <summary>
        /// Converts an object to JSON and creates a StringContent with the specified encoding and content type.
        /// </summary>
        /// <param name="obj">The object to be converted to JSON.</param>
        /// <returns>StringContent with JSON representation of the object.</returns>
        internal static StringContent GetStringContent(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
