using Autofac;
using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;
using Sophon.Infrastructure;
using Sophon.UI.Views;
using System.Windows;
using Common;

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
            //桥接DryIoc和Autofac
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


        }

        protected override Rules CreateContainerRules()
        {
            return base.CreateContainerRules().WithUnknownServiceResolvers(request =>
            {
                if (_autofacContainer != null && _autofacContainer.IsRegistered(request.ServiceType))
                {
                    return new DelegateFactory(_ => _autofacContainer.Resolve(request.ServiceType));
                }
                return null;
            });
        }

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            var dbInitializer = Container.Resolve<IDatabaseInitializer>();
            dbInitializer.Initialize();


            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate("ContentRegion", "UserView", result =>
            {
                if (result.Error != null)
                {
                    // 把具体的错误弹窗显示出来
                    MessageBox.Show($"导航失败！\n错误信息: {result.Error.Message}\n内部错误: {result.Error.InnerException?.Message}");
                }
            });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
