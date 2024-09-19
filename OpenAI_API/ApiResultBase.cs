using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenAI_API.Models;

namespace OpenAI_API
{
    /// <summary>
    /// Convert the meta data JSON to a string
    /// </summary>
    public class MetadataJsonConverter : JsonConverter
    {
        /// <summary>
        /// Can it convert the object type
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        /// <summary>
        /// Read the JSON and convert it to a string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            return token.ToString(Formatting.None);
        }

        /// <summary>
        /// Write the JSON
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Added OpenRouterError class to handle errors from OpenAI API
    /// see https://openrouter.ai/docs#errors
    /// </summary>
    public class OpenRouterError
    {
        /// <summary>
        /// error code
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }
        /// <summary>
        /// error message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
        /// <summary>
        /// metadata    
        /// </summary>
        [JsonProperty("metadata")]
        [JsonConverter(typeof(MetadataJsonConverter))]
        public string MetaData { get; set; }
    }

    /// <summary>
    /// Represents a result from calling the OpenAI API, with all the common metadata returned from every endpoint
    /// </summary>
    abstract public class ApiResultBase
    {

        /// The time when the result was generated
        [JsonIgnore]
        public DateTime? Created => CreatedUnixTime.HasValue ? (DateTime?)DateTimeOffset.FromUnixTimeSeconds(CreatedUnixTime.Value).DateTime : null;

        /// <summary>
        /// The time when the result was generated in unix epoch format
        /// </summary>
        [JsonProperty("created")]
        public long? CreatedUnixTime { get; set; }

        /// <summary>
        /// Which model was used to generate this result.
        /// </summary>
        [JsonProperty("model")]
        public Model Model { get; set; }

        /// <summary>
        /// Object type, ie: text_completion, file, fine-tune, list, etc
        /// </summary>
        [JsonProperty("object")]
        public string Object { get; set; }

        /// <summary>
        /// The organization associated with the API request, as reported by the API.
        /// </summary>
        [JsonIgnore]
        public string Organization { get; internal set; }

        /// <summary>
        /// The server-side processing time as reported by the API.  This can be useful for debugging where a delay occurs.
        /// </summary>
        [JsonIgnore]
        public TimeSpan ProcessingTime { get; internal set; }

        /// <summary>
        /// The request id of this API call, as reported in the response headers.  This may be useful for troubleshooting or when contacting OpenAI support in reference to a specific request.
        /// </summary>
        [JsonIgnore]
        public string RequestId { get; internal set; }

        /// <summary>
        /// The Openai-Version used to generate this response, as reported in the response headers.  This may be useful for troubleshooting or when contacting OpenAI support in reference to a specific request.
        /// </summary>
        [JsonIgnore]
        public string OpenaiVersion { get; internal set; }

        /// <summary>
        /// The error object returned from the API, if any
        /// </summary>
        [JsonProperty("error")]
        public OpenRouterError Error { get; set; }
    }
}