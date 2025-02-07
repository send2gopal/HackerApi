using System.Text.Json.Serialization;

namespace HackernNews.Core.Shared
{
    /// <summary>
    /// Represents the result of a view model operation.
    /// </summary>
    public class ViewModelResult
    {
        /// <summary>
        /// Gets or sets the metadata associated with the result.
        /// </summary>
        [JsonPropertyName("metadata")]
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets the list of errors associated with the result.
        /// </summary>
        [JsonPropertyName("errors")]
        public List<ServiceError> Errors { get; set; } = new List<ServiceError>();

        /// <summary>
        /// Gets or sets a value indicating whether the operation was a partial success.
        /// </summary>
        [JsonPropertyName("isPartialSuccess")]
        public bool IsPartialSuccess { get; set; }
    }

    /// <summary>
    /// Represents the result of a view model operation with a specific type.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    public class ViewModelResult<T> : ViewModelResult
    {
        /// <summary>
        /// Gets or sets the result of the operation.
        /// </summary>
        [JsonPropertyName("result")]
        public T Result { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelResult{T}"/> class.
        /// </summary>
        /// <param name="result">The result of the operation.</param>
        public ViewModelResult(T result) => Result = result;
    }
}
