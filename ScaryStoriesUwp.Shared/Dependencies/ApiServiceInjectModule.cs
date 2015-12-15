using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using ScaryStoriesUniversal.Api;
using ScaryStoriesUwp.Shared.Services.OnlineOfflineStoryService;

namespace ScaryStoriesUwp.Shared.Dependencies
{
    public class ApiServiceInjectModule:INinjectModule
    {
        public ApiServiceInjectModule()
        {
            Name = "Апи";
        }

        public IKernel Kernel { get; }
        public void OnLoad(IKernel kernel)
        {
            kernel.Bind<IApiService>().To<ApiService>().WhenInjectedInto<StoriesStorage>().InTransientScope().WithConstructorArgument("url", "http://storiesmobileservice.azure-mobile.net/").WithConstructorArgument("key", "eOxFathFeOfvquGBFoAZmDsGJuifQH42");
            kernel.Bind<IApiService>().To<StoriesStorage>().InTransientScope().WithConstructorArgument("url", "http://storiesmobileservice.azure-mobile.net/").WithConstructorArgument("key", "eOxFathFeOfvquGBFoAZmDsGJuifQH42");

        }

        public void OnUnload(IKernel kernel)
        {
           
        }

        public void OnVerifyRequiredModules()
        {
            
        }

        public string Name { get; }
    }
}
