using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ApotekHjartat.Api.IntegrationTest.Extentions
{
    public static class HttpExtensions
    {
        public static HttpContent EmptyBody => new StringContent("", Encoding.UTF8, "application/json");
        public static T SerializeResponseContent<T>(this HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        public static StringContent ToStringContent(this object body)
        {
            var json = JsonConvert.SerializeObject(body);
            return new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        }
    }
}
