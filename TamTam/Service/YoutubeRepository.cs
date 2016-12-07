using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TamTam.Dto;

namespace TamTam.Service
{
    public class YoutubeRepository : IVideoRepository
    {
        public IEnumerable<VideoEntry> Search(SearchQuery query)
        {
            var movieList = new List<VideoEntry>();
            var apiKey = ConfigurationSettings.AppSettings["YoutubeApiKey"];

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = query.Title; // Replace with your search term.
            searchListRequest.MaxResults = 3;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.Execute();

            foreach (var item in searchListResponse.Items)
            {
                if (item.Id.Kind == "youtube#video")
                {                 
                    var videoUrl = $"http://www.youtube.com/embed/{item.Id.VideoId}";
                    var iframe = $"<iframe width='320' height='240' src='https://www.youtube.com/embed/{item.Id.VideoId}' frameborder='0' allowfullscreen></iframe>";

                    movieList.Add(new VideoEntry
                    {
                        Url = videoUrl,
                        Iframe = iframe
                    });
                }
            } 

            return movieList;
        }
    }
}