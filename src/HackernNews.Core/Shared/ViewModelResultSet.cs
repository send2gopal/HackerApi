using System.Text.Json.Serialization;

namespace HackernNews.Core.Shared
{
    /// <inheritdoc />
    /// <summary>
    /// Response wrapper that represents a collection result.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ViewModelResultSet<T> : ViewModelResult
    {
        /// <summary>
        /// The number of elements in the result set.
        /// </summary>
        [JsonPropertyName("count")]
        public int Count => Results.Count;

        /// <summary>
        /// The result set.
        /// </summary>
        [JsonPropertyName("results")]
        public List<T> Results { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelResultSet{T}"/> class.
        /// </summary>
        /// <param name="viewModels">The view models.</param>
        public ViewModelResultSet(IEnumerable<T> viewModels) => Results = viewModels.ToList();
    }
}
