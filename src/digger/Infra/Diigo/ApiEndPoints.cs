using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiigoSharp.ApiClient
{
    internal static class ApiEndPoints
    {
        internal static class BookmarksApiUrls
        {
            public static string Query => $"/api/v2/bookmarks";
            public static string Create => "/api/v2/bookmarks";
        }
    }
}
