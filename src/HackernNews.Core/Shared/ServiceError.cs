using System.Text.Json.Serialization;

namespace HackernNews.Core.Shared
{
    /// <summary>
    /// Represents an error that occurs during service operations.
    /// </summary>
    public class ServiceError
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the type of the error.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
