using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NX595Interface
{
    public class AppSettings
    {
        public class NX595ESettings
        {
            public string Host { get; set; }

            public string LoginName { get; set; }

            public string Password { get; set; }
        }

        public NX595ESettings NX595E { get; set; }
    }
}
