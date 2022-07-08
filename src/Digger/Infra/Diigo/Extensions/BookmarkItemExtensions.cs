namespace Digger.Infra.Diigo.Models
{
    public static class BookmarkExtensions
    {
        public static BookmarkItem RemoveTag(this BookmarkItem bmk, string tagToRemove)
        {
            var tags = bmk.Tags == null ? new string[0] : ((string)(bmk.Tags)).Split(',');
            var interim = tags.Where(t => t != tagToRemove).ToArray();
            bmk.Tags = string.Join(',', interim);
            return bmk;
        }

        public static BookmarkItem AddTag(this BookmarkItem bmk, string tagToAdd)
        {
            var tags = bmk.Tags == null ? new string[0] : ((string)(bmk.Tags)).Split(',');
            var interim = tags.ToList();
            interim.Add(tagToAdd);
            bmk.Tags = string.Join(',', interim);
            return bmk;
        }
    }
}
