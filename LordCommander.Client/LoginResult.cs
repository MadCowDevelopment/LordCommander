using System;
using System.Net;
using Newtonsoft.Json;

namespace LordCommander.Client
{
    public class LoginResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("userName")]
        public string EMail { get; set; }

        [JsonProperty(".issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(".expires")]
        public DateTime Expires { get; set; }

        public Cookie AuthCookie { get; set; }
    }
}