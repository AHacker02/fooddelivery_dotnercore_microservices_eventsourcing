using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OMF.Common.Abstractions;
using OMF.Common.Models;

namespace OMF.Common.Helpers
{
    public class HttpWrapper:IHttpWrapper
    {
        private readonly IConfiguration _configuration;

        public HttpWrapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<TResponse> Get<TResponse>(string url)
        {
            using (var client = new HttpClient())
            {
                var user=new User()
                {
                    Id = 0,
                    Email = "System"
                };
                client.DefaultRequestHeaders.Add("Authorization","Bearer " + user.GenerateJwtToken(_configuration["Token"]));
                var httpResponse = await client.GetAsync(new Uri(url));

                if (httpResponse.IsSuccessStatusCode)
                {
                    var response = await httpResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResponse>(response);
                }

                return default;
            }
        }
    }
}
