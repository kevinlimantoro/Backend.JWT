using System;
using System.Net.Http;
using System.Text;

namespace Backend.SDK
{
    public static partial class BackendSDK
    {
        private static readonly HttpClient _client = new HttpClient();
        private static bool APIValidate()
        {
            try
            {
                var res = _client.PostAsync(SDK.Properties.Settings.Default.ConnectionString,
                    new StringContent(_Serializer.Serialize(_BaseResult), Encoding.UTF8, "application/json")).Result;
                var mod = _Serializer.Deserialize<APIResult>(res.Content.ReadAsStringAsync().Result);
                return mod.Result == 1;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
