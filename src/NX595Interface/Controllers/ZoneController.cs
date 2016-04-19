using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NX595Interface.Controllers
{
    public class ZoneController : BaseNXController
    {
        public ZoneController(IOptions<AppSettings> optionsAccessor) :
            base(optionsAccessor) { }

        [HttpPost]
        [Route("bypass/{zoneIndex:int}")]
        public async Task<ActionResult> Bypass(int zoneIndex)
        {
            using (var client = new HttpClient())
            {
                var settings = AppSettings.NX595E;
                client.BaseAddress = new Uri(settings.Host);

                var sessionID = await GetSessionID(client);
                var httpContent = new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        { "sess", sessionID } ,
                        { "comm", "82" } ,
                        { "data0", zoneIndex.ToString() }
                    }
                );

                var response = await client.PostAsync("/user/zonefunction.cgi", httpContent);
                response.EnsureSuccessStatusCode();

                return await JsonStatusResult(client, sessionID);
            }
        }
    }
}
