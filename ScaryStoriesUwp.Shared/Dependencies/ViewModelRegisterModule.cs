using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Ninject;
using Ninject.Infrastructure.Language;
using Ninject.Modules;

namespace ScaryStoriesUwp.Shared.Dependencies
{
    public class ViewModelRegisterModule:INinjectModule
    {
        public ViewModelRegisterModule()
        {
            Name = "Регистратор вью-моделей";
        }
        public IKernel Kernel { get; }
        public void OnLoad(IKernel kernel)
        {
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var viewModels=assembly.GetTypes().Where(x => x.GetAllBaseTypes().Contains(typeof (MvxViewModel))).ToList();
            foreach (var viewModel in viewModels)
            {
                kernel.Bind(viewModel).To(viewModel).InTransientScope();
            }

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
