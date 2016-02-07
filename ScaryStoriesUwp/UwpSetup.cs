using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Microsoft.ApplicationInsights;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Platform.Platform;
using MvvmCross.WindowsUWP.Platform;
using ScaryStoriesUwp.ServicesImpl;
using ScaryStoriesUwp.Shared;
using ScaryStoriesUwp.Shared.Dependencies;
using ScaryStoriesUwp.Shared.Services;

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
            var settingsProvider = Mvx.Resolve<ISettingsProvider>();
            settingsProvider.IsOffline = false;
            Mvx.ConstructAndRegisterSingleton<ISpeechSyntizerService, SpeechSyntizerService>();
            Mvx.RegisterType<IStoryDatabaseLoader, StoryDatabaseLoader>();
            Mvx.RegisterSingleton(typeof(IGlobalProgressProvider),new GlobalProgressProvider());
          
            Mvx.RegisterType<TelemetryClient,TelemetryClient>();
        }


        protected override IMvxIoCProvider CreateIocProvider()
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
