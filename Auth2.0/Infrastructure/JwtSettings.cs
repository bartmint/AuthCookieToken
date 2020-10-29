using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth2._0.Infrastructure
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int MinutesToExpiration { get; set; }

        public TimeSpan Expire => TimeSpan.FromMinutes(MinutesToExpiration);
    }
}
