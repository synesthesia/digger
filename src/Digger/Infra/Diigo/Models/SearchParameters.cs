namespace Digger.Infra.Diigo.Models
{
    public class SearchParameters
    {
        public string? User { get; set; }

        public long Start { get; set; }

        public long Count { get; set; }

        public SortBy SortOrder { get; set; }

        public Visibility Filter { get; set; }

        public string? ListName { get; set; }

        public TagCollection Tags {get; set;}


        public SearchParameters()
        {
            Tags = new TagCollection();

        }


    }
}
