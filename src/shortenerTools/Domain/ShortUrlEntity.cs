using System.Linq;
using Microsoft.Azure.Cosmos.Table;

namespace Cloud5mins.domain
{
    public class ShortUrlEntity : TableEntity
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public string ShortUrl { get; set; }

        public string Vanity { get; set; }
        
        /// <summary>
        /// The user principal name (UPN) of the owner.
        /// </summary>
        /// <value>User Principal Name (UPN)</value>
        public string OwnerUpn { get; set; }

        public int Clicks { get; set; }

        public ShortUrlEntity() { }

        public ShortUrlEntity(string ownerUpn, string longUrl, string endUrl)
        {
            initialize(ownerUpn, longUrl, endUrl, string.Empty);
        }

        public ShortUrlEntity(string ownerUpn, string longUrl, string endUrl, string title)
        {
            initialize(ownerUpn, longUrl, endUrl, title);
        }

        private void initialize(string ownerUpn, string longUrl, string endUrl, string title)
        {
            PartitionKey = endUrl.First().ToString().ToLower();
            RowKey = endUrl.ToLower();
            Vanity = endUrl; // case-sensitive!
            Url = longUrl;
            Title = title;
            OwnerUpn = ownerUpn;
            Clicks = 0;
        }

        public static ShortUrlEntity GetEntity(string ownerUpn, string longUrl, string endUrl)
        {
            return new ShortUrlEntity
            {
                PartitionKey = endUrl.First().ToString().ToLower(),
                RowKey = endUrl.ToLower(),
                Url = longUrl,
                OwnerUpn = ownerUpn
            };
        }
    }
}
