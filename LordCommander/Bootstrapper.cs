﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using LordCommander.ViewModels;
using LordCommander.Views;

namespace LordCommander
{
    public class Bootstrapper : BootstrapperBase
    {
        #region Fields

        private CompositionContainer _container;

        #endregion Fields

        public Bootstrapper()
        {
            Initialize();
        }

        #region Protected Methods

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }

        protected override void Configure()
        {
            var catalog =
                new AggregateCatalog(
                    new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                    new DirectoryCatalog("."));

            _container = new CompositionContainer(catalog);
            ContainerSingleton.Instance = _container;

            var batch = new CompositionBatch();
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_container);
            _container.Compose(batch);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            ViewLocator.LocateForModelType(typeof (MainViewModel), _container.GetExportedValue<MainView>(), null);
            DisplayRootViewFor<MainViewModel>();
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = _container.GetExportedValues<object>(contract).ToList();

            if (exports.Any())
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = new DirectoryCatalog(".").LoadedFiles.Select(Assembly.LoadFrom).ToList();
            assemblies.Add(Assembly.GetExecutingAssembly());
            return assemblies.ToArray();
        }

        #endregion Protected Methods
    }
}
