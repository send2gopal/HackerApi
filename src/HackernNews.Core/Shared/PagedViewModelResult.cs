using System.Text.Json.Serialization;

namespace HackernNews.Core.Shared
{
    /// <summary>
    /// Represents a paged result set for a view model.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the result set.</typeparam>
    public class PagedViewModelResult<T> : ViewModelResultSet<T>
    {
        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        [JsonPropertyName("size")]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total count of items.
        /// </summary>
        [JsonPropertyName("total")]
        public int TotalCount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedViewModelResult{T}"/> class.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="totalCount">The total count of items.</param>
        public PagedViewModelResult(
            List<T> results,
            int page,
            int pageSize,
            int totalCount)
            : base(results)
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
