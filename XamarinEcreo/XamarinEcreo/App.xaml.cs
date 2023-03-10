using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using XamarinEcreo.Services;
using XamarinEcreo.Views;

namespace XamarinEcreo
{
[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //DependencyResolver.ResolveUsing(type=>container.IsRegistered(type)? container.Resolve(type):null);
            DependencyService.Register<IUserDbService, UserDbService>();
            DependencyService.Register<IAuthenticationService, AuthenticationService>();
            DependencyService.Register<IImageDbService, ImageDbService>();
            DependencyService.Register<IAdminstratorService, AdminstratorService>();
            //MainPage = new NavigationPage(new LoginPage());
            //RegisterType<IUserDbService, UserDbService>();
            //BuildContainer();
            MainPage = new AppShell();
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        static IContainer container;
        
        static readonly ContainerBuilder builder = new ContainerBuilder();
        public static void RegisterType<T>() where T : class
        {
            builder.RegisterType<T>();
        }
        public static void RegisterType<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            builder.RegisterType<T>().As<TInterface>();
        }
        public static void RegisterTypeWithParameters<T>(Type param1Type, object param1Value, Type Param2Type, string param2Name) where T : class
        {
            builder.RegisterType<T>()
                .WithParameters(new List<Parameter>()
                {
                    new TypedParameter(param1Type, param1Value),
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType==Param2Type && pi.Name==param2Name,
                        (pi, ctx)=>ctx.Resolve(Param2Type))
                });
        }
        public static void RegisterTypeWithParameters<TInterface, T>(Type param1Type, object param1Value, Type Param2Type, string param2Name) where TInterface : class where T : class, TInterface
        {
            builder.RegisterType<T>()
                .WithParameters(new List<Parameter>()
                {
                    new TypedParameter(param1Type, param1Value),
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType==Param2Type && pi.Name==param2Name,
                        (pi, ctx)=>ctx.Resolve(Param2Type))
                });
        }
        public static void BuildContainer()
        {
            container = builder.Build();
        }
    }
}
