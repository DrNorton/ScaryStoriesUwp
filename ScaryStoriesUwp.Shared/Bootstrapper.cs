using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ScaryStoriesUwp.Shared.Database.DataAccess;
using ScaryStoriesUwp.Shared.Database.Repositories;
using ScaryStoriesUwp.Shared.Services;
using ScaryStoriesUwp.Shared.ViewModels;
using ScaryStoriesUwp.Shared.ViewModels.Shell;

namespace ScaryStoriesUwp.Shared
{
    public class Bootstrapper:MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MainViewModel>();
            RegisterServices();
            RegisterDatabaseServices();
            CreateMenu();
        }

        private void CreateMenu()
        {
            Mvx.RegisterSingleton(typeof(MenuConstructor), new MenuConstructor());
        }

        private void RegisterDatabaseServices()
        {
            InitLocalStore();
        }

        private void InitLocalStore()
        {
            var settingsProvider=Mvx.Resolve<ISettingsProvider>();
         
            var dbConnection = new DbConnectionToLocalStorage("localstore.db",String.Format("{0}_localdatabase.db", settingsProvider.DatabaseVersion));
            
            Mvx.RegisterSingleton(typeof(IDbConnection), dbConnection);
    
            Mvx.RegisterType<Database.Repositories.Base.IFavoriteStoriesRepository, FavoriteStoriesRepository>();
            Mvx.RegisterType<Database.Repositories.Base.IStoriesLocalRepository, StoriesLocalRepository>();

        }
        private void RegisterServices()
        {
            Mvx.RegisterSingleton(typeof(StoryListIdsContainer), new StoryListIdsContainer());

        }
    }
}
