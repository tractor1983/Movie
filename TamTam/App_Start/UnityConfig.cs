using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Web.Http;
using TamTam.Service;
using Unity.WebApi;


namespace TamTam
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IVideoRepository, YoutubeRepository>("a");
            container.RegisterType<IVideoRepository, VimeoRepository>("b");
            container.RegisterType<IEnumerable<IVideoRepository>, IVideoRepository[]>();

            container.RegisterType<IMovieRepository, ImdbRepository>("c");
            container.RegisterType<IMovieRepository, RotenTomatoesRepository>("b");
            container.RegisterType<IEnumerable<IMovieRepository>, IMovieRepository[]>();

            container.RegisterType<ISearchService, SearchService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}