using System.Linq;

namespace  adminBlazorWebsite.Data
{
    public class ShortUrlEntity
    {
        public string PartitionKey { get; set; }
        
        public string RowKey { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Vanity { get; set; }

        public string ShortUrl { get; set; }

        public string OwnerUpn { get; set; }
        
        public int Clicks { get; set; }

        public ShortUrlEntity(){}

        public static ShortUrlEntity GetEntity(string longUrl, string endUrl){
            return new ShortUrlEntity
            {
                PartitionKey = endUrl.First().ToString().ToLower(),
                RowKey = endUrl.ToLower(),
                Url = longUrl
            };
        }

        public string GetDisplayableUrl(){

            var length = Url.ToString().Length;
            if (length >= 64){
                return Url.Substring(0, 63) + "...";
            }
            return Url;
        }
    }


}
