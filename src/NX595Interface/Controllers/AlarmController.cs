using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NX595Interface.Controllers
{
    public class AlarmController : BaseNXController
    {
        public AlarmController(IOptions<AppSettings> optionsAccessor) :
            base(optionsAccessor) { }

        // GET: zone/
        [HttpGet]
        [Route("Status")]
        public async Task<ActionResult> Status()
        {
            using (var client = new HttpClient())
            {
                var settings = AppSettings.NX595E;
                client.BaseAddress = new Uri(settings.Host);

                var sessionID = await GetSessionID(client);

                return await JsonStatusResult(client, sessionID);
            }
        }

        [HttpPost]
        [Route("Arm/{armType:regex((off|away|stay))}")]
        public async Task<ActionResult> Arm(string armType)
        {
            var armTypeCodeMap = new Dictionary<string, string>
            {
                { "off", "16" } ,
                { "away", "17" } ,
                { "stay", "18" }
            };

            return await Keyfunction(armTypeCodeMap[armType]);
        }

        [HttpPost]
        [Route("Chime")]
        public async Task<ActionResult> Chime()
        {
            return await Keyfunction("1");
        }

        private async Task<ActionResult> Keyfunction(string data2)
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
                        { "comm", "80" } ,
                        { "data0", "2" } ,
                        { "data1", "1" } ,
                        { "data2", data2 }
                    }
                );

                var response = await client.PostAsync("/user/keyfunction.cgi", httpContent);
                response.EnsureSuccessStatusCode();

                return await JsonStatusResult(client, sessionID);
            }
        }
    }
}
