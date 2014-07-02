using System.Collections.Generic;
using Newtonsoft.Json;

namespace LordCommander.Client
{
    public class ModelValidationError : WebError
    {
        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("ModelState")]
        public Dictionary<string, List<string>> ModelState { get; set; } 
    }

    public abstract class WebError
    {
    }
}