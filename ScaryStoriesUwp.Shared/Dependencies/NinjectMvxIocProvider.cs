using System;
using System.Collections.Generic;
using System.Reflection;
using Cirrious.CrossCore.Core;
using Cirrious.CrossCore.IoC;
using Ninject;
using Ninject.Modules;

namespace ScaryStoriesUwp.Shared.Dependencies
{
    public class NinjectMvxIocProvider : MvxSingleton<IMvxIoCProvider>, IMvxIoCProvider
    {
        private StandardKernel _kernel = new StandardKernel();


        public NinjectMvxIocProvider()
        {
            var assembly = this.GetType().GetTypeInfo().Assembly;
            _kernel = new StandardKernel();
            _kernel.Load(assembly);
        }

        public NinjectMvxIocProvider(INinjectSettings settings, params INinjectModule[] modules)
        {
            _kernel = new StandardKernel(settings, modules);
        }

        public void CallbackWhenRegistered(Type type, Action action)
        {
        }

        public void CallbackWhenRegistered<T>(Action action)
        {
        }

        public bool CanResolve(Type type)
        {
            return (bool)_kernel.CanResolve(type);
        }

        public bool CanResolve<T>() where T : class
        {
            return _kernel.CanResolve<T>();
        }

        public object Create(Type type)
        {
            return _kernel.Get(type);
        }

        public T Create<T>() where T : class
        {
            return _kernel.Get<T>();
        }

        public object GetSingleton(Type type)
        {
            return _kernel.Get(type);
        }

        public T GetSingleton<T>() where T : class
        {
            return _kernel.Get<T>();
        }

        public object IoCConstruct(Type type)
        {
            return _kernel.Get(type);
        }

        public T IoCConstruct<T>() where T : class
        {
            return (T)IoCConstruct(typeof(T));
        }

        public void RegisterSingleton(Type tInterface, Func<object> theConstructor)
        {
            _kernel.Bind(tInterface).ToMethod(context => theConstructor()).InSingletonScope();
        }

        public void RegisterSingleton(Type tInterface, object theObject)
        {
            _kernel.Bind(tInterface).ToConstant(theObject).InSingletonScope();
        }

        public void RegisterSingleton<TInterface>(Func<TInterface> theConstructor) where TInterface : class
        {
            _kernel.Bind<TInterface>().ToMethod(context => theConstructor()).InSingletonScope();
        }

        public void RegisterSingleton<TInterface>(TInterface theObject) where TInterface : class
        {
            _kernel.Bind<TInterface>().ToConstant(theObject).InSingletonScope();
        }

        public void RegisterType(Type tFrom, Type tTo)
        {
            _kernel.Bind(tFrom).To(tTo);
        }

        public void RegisterType(Type t, Func<object> constructor)
        {
            _kernel.Bind(t).ToMethod(context => constructor());
        }

        public void RegisterType<TInterface>(Func<TInterface> constructor) where TInterface : class
        {
            _kernel.Bind<TInterface>().ToMethod(context => constructor());
        }

        public object Resolve(Type type)
        {
            return _kernel.Get(type);
        }

        public T Resolve<T>() where T : class
        {
            return _kernel.Get<T>();
        }

        public bool TryResolve(Type type, out object resolved)
        {
            resolved = _kernel.TryGet(type);
            return (resolved != null);
        }

        public bool TryResolve<T>(out T resolved) where T : class
        {
            resolved = _kernel.TryGet<T>();
            return (resolved != null);
        }

        void IMvxIoCProvider.RegisterType<TFrom, TTo>()
        {
            _kernel.Bind<TFrom>().To<TTo>();
        }
    }
}
