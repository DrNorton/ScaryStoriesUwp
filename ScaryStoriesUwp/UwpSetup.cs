using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsCommon.Platform;
using ScaryStoriesUwp.ServicesImpl;
using ScaryStoriesUwp.Shared;
using ScaryStoriesUwp.Shared.Dependencies;
using ScaryStoriesUwp.Shared.Services;
using ScaryStoriesUwp.Shared.ViewModels;
using ScaryStoriesUwp.Shared.ViewModels.Shell;
using ScaryStoriesUwp.Views;

namespace ScaryStoriesUwp
{
    public class UwpSetup: MvxWindowsSetup
    {
        public UwpSetup(Frame rootFrame) : base(rootFrame)
        {

        }

        protected override IMvxApplication CreateApp()
        {
            var mainSetup = new Bootstrapper();
            RegisterPlatformDependServices();
            
            return mainSetup;
        }

       

        private void RegisterPlatformDependServices()
        {
            Mvx.RegisterType<IMessageProvider, MessageProvider>();
            Mvx.ConstructAndRegisterSingleton<ISettingsProvider,SettingsProvider>();
            Mvx.ConstructAndRegisterSingleton<ISpeechSyntizerService, SpeechSyntizerService>();
            Mvx.RegisterType<IStoryDatabaseLoader, StoryDatabaseLoader>();
            Mvx.RegisterSingleton(typeof(IGlobalProgressProvider),new GlobalProgressProvider());
        }


        protected override Cirrious.CrossCore.IoC.IMvxIoCProvider CreateIocProvider()
        {
            return new NinjectMvxIocProvider();
        }

        

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

       
    }

    public class DebugTrace : IMvxTrace
    {
        public void Trace(MvxTraceLevel level, string tag, Func<string> message)
        {
            Debug.WriteLine(tag + ":" + level + ":" + message());
        }

        public void Trace(MvxTraceLevel level, string tag, string message)
        {
            Debug.WriteLine(tag + ":" + level + ":" + message);
        }

        public void Trace(MvxTraceLevel level, string tag, string message, params object[] args)
        {
            try
            {
                Debug.WriteLine(string.Format(tag + ":" + level + ":" + message, args));
            }
            catch (FormatException)
            {
                Trace(MvxTraceLevel.Error, tag, "Exception during trace of {0} {1} {2}", level, message);
            }
        }
    }
}
