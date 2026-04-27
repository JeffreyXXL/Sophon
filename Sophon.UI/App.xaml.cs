using Autofac;
using Common;
using DryIoc;
using Newtonsoft.Json;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;
using Sophon.Application;
using Sophon.Infrastructure;
using Sophon.UI.Views;
using Sophon.UI.Views.SubViews;
using System.Collections.Generic;
using System.Windows;

namespace Sophon.UI
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        private Autofac.IContainer _autofacContainer;

        public App()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAllModuleExt();
            _autofacContainer = builder.Build();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<UserView>("UserView");
            containerRegistry.RegisterForNavigation<InfrastructureView>("InfrastructureView");
            containerRegistry.RegisterForNavigation<ParamView>("ParamView");
            containerRegistry.RegisterForNavigation<HomeView>("HomeView");
            containerRegistry.RegisterForNavigation<StationView>("StationView");
            containerRegistry.RegisterForNavigation<AlarmView>("AlarmView");

            containerRegistry.RegisterDialog<AddParamView, AddParamViewModel>();
        }

        protected override Rules CreateContainerRules()
        {
            //桥接DryIoc和Autofac
            return base.CreateContainerRules().WithUnknownServiceResolvers(request =>
            {
                if (_autofacContainer != null && _autofacContainer.IsRegistered(request.ServiceType))
                {
                    var serviceType = request.ServiceType;
                    return new DelegateFactory(_ => _autofacContainer.Resolve(serviceType));
                }
                return null;
            });
        }

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            // 初始化数据库
            var dbInitializer = Container.Resolve<IDatabaseInitializer>();
            dbInitializer.Initialize();

            // 加载配置
            var hardwareService = Container.Resolve<IHardwareService>();
            var protocolService = Container.Resolve<IProtocolService>();
            var paramService = Container.Resolve<IParamService>();
            hardwareService.LoadAllConfigs();
            protocolService.LoadAllConfigs();
            paramService.LoadAllConfigs();

            // 导航到用户界面
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate("ContentRegion", "UserView");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new ProtocolConfigConverter()
                }
            };
        }
    }
}