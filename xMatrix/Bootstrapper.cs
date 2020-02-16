using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using xMatrix.Core.Interfaces;
using xMatrix.Core.Services;
using xMatrix.ViewModels;

namespace xMatrix
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();
        }

        //protected override void OnStartup(object sender, StartupEventArgs e)
        //{
        //    DisplayRootViewFor<MainWindowViewModel>();
        //}

        protected override void Configure()
        {
            _container.Instance(_container);
            _container.PerRequest<MainWindowViewModel>()
                .PerRequest<Snackbar>()
                .PerRequest<IidService, IdService>()

                ;
            //_container.PerRequest<Snackbar>();
            //_container
            //    .Singleton<IWindowManager, WindowManager>()
            //    .Singleton<IEventAggregator, EventAggregator>()
            //    .Singleton<IWindowsLogger, ConsoleLogger>()
            //    .PerRequest<IStockListService, StockListService>()

        }

        protected override object GetInstance(Type service, string key)
        {
            var result = _container.GetInstance(service, key);
            return result;
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
