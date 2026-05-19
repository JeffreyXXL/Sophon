using Common;
using DryIoc;
using Newtonsoft.Json;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;
using Sophon.Application;
using Sophon.Infrastructure;
using Sophon.Core;
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
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterCommon();
            containerRegistry.RegisterInfrastructure();
            containerRegistry.RegisterCore();
            containerRegistry.RegisterApplication();

            containerRegistry.RegisterForNavigation<UserView>("UserView");
            containerRegistry.RegisterForNavigation<AxisInfrastructureView>("AxisInfrastructureView");
            containerRegistry.RegisterForNavigation<IOInfrastructureView>("IOInfrastructureView");
            containerRegistry.RegisterForNavigation<ProtocolInfrastructureView>("ProtocolInfrastructureView");
            containerRegistry.RegisterForNavigation<ParamView>("ParamView");
            containerRegistry.RegisterForNavigation<HomeView>("HomeView");
            containerRegistry.RegisterForNavigation<StationView>("StationView");
            containerRegistry.RegisterForNavigation<AlarmRegisterView>("AlarmRegisterView");
            containerRegistry.RegisterForNavigation<AlarmHistoryView>("AlarmHistoryView");

            containerRegistry.RegisterDialog<AddParamView, AddParamViewModel>();
        }

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            // 初始化数据库
            var dbInitializer = Container.Resolve<IDatabaseInitializer>();
            dbInitializer.Initialize();

            // 加载配置
            var axisService = Container.Resolve<ICardRepository>();
            var protocolService = Container.Resolve<IProtocolRepository>();
            var paramService = Container.Resolve<IParamRepository>();
            axisService.LoadAllConfigs();
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