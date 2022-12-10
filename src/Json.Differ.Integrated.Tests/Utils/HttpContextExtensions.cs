using Newtonsoft.Json;
using System.Text;

namespace Json.Differ.Integrated.Tests.Utils
{
    public static class HttpContentExtensions
    {
        public static HttpContent ToHttpStringContent(this object value)
        {
            return new StringContent(
                JsonConvert.SerializeObject(value),
                Encoding.UTF8,
                "application/json");
        }

        public static async Task<T> ToObjType<T>(this HttpContent value) where T : class
        {
            var str = await value.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<T>(str);
            return obj;
        }
    }
}
