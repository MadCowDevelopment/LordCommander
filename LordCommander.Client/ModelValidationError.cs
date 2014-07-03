using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LordCommander.Client
{
    public class ModelValidationError : WebError
    {
        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("ModelState")]
        public Dictionary<string, List<string>> ModelState { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Message);
            builder.AppendLine();
            if (ModelState != null)
            {
                foreach (var values in ModelState.Values)
                {
                    foreach (var value in values)
                    {
                        builder.AppendLine(value);
                    }

                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }
    }

    public abstract class WebError
    {
    }
}