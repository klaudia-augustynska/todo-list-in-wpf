﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using TodoList.Helpers.EventAggregator;
using TodoList.Models;
using TodoList.ViewModels;
using TodoList.Views;

namespace TodoList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IUnityContainer container = new UnityContainer();
            
            container.RegisterType<MainWindowViewModel, MainWindowViewModel>();
            container.RegisterType<EditTaskWindowViewModel, EditTaskWindowViewModel>();
            container.RegisterType<IEventAggregator, SimpleEventAggregator>(new ContainerControlledLifetimeManager());
           // container.RegisterType<EditTaskWindow, EditTaskWindow>();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
