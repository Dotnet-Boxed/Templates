namespace GraphQLTemplate.Options
{
    using System.ComponentModel.DataAnnotations;
    using HotChocolate.Execution.Options;
    using HotChocolate.Types.Pagination;

    public class GraphQLOptions
    {
        [Required]
        public int MaxAllowedComplexity { get; set; }

        [Required]
        public int MaxAllowedExecutionDepth { get; set; }

        [Required]
        public PagingOptions Paging { get; set; } = default!;

        [Required]
        public RequestExecutorOptions Request { get; set; } = default!;
    }
}
