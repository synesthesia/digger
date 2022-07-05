﻿using Ardalis.GuardClauses;
using DiigoSharp.ApiClient.Exceptions;
using DiigoSharp.ApiClient.Helpers;
using DiigoSharp.ApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static DiigoSharp.ApiClient.ApiEndPoints;

namespace DiigoSharp.ApiClient
{
    public class DiigoClient : IDiigoClient
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey;

        public DiigoClient(HttpClient httpClient, string apiKey)
        {
            Guard.Against.Null(httpClient, nameof(httpClient));
            Guard.Against.NullOrEmpty(apiKey, nameof(apiKey));

            this.httpClient = httpClient;
            this.apiKey = apiKey;
        }

        public async Task<BookmarksCollection> GetBookmarks(SearchParameters parameters)
        {
            string requestUri = QueryStringHelpers.AddQueryString(BookmarksApiUrls.Query, this.GetQueryParameters(parameters));
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
           
            var response = await this.httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<IEnumerable<BookmarkItem>>();
                var bookmarksCollection = new BookmarksCollection();
                bookmarksCollection.Bookmarks = content;
                bookmarksCollection.Count = content.LongCount();
                return bookmarksCollection;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new DiigoClientException(response.StatusCode, message);
            }
        }

        public async Task<SaveBookmarkResponse> SaveBookmark(Bookmark bookmark)
        {
            string requestUri = QueryStringHelpers.AddQueryString(BookmarksApiUrls.Create, this.GetQueryParameters(bookmark));

            var response = await this.httpClient.PostAsync(requestUri, null);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<SaveBookmarkResponse>();
                return content;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new DiigoClientException(response.StatusCode, message);
            }
        }

        private Dictionary<string, string> GetQueryParameters(object obj)
        {
            var queryStrings = new Dictionary<string, string>();
            queryStrings.Add(Constants.API_KEY_PARAM, this.apiKey);

            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(obj, null);

                if (propertyValue != null)
                {
                    queryStrings.Add(property.Name.ToLower(), propertyValue.ToString().ToLower());
                }
            }

            return queryStrings;
        }
    }
}
