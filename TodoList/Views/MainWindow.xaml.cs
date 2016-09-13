using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.Unity;
using TodoList.Helpers;
using TodoList.Helpers.EventAggregator;
using TodoList.ViewModels;
using TodoList.Views;

namespace TodoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISubscriber<EnableUserToEditTask>
    {
        private readonly IUnityContainer _container;
        private IEventAggregator _eventAggregator;

        public MainWindow(IUnityContainer container, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _container = container;
            _eventAggregator = eventAggregator;
            DataContext = _container.Resolve<MainWindowViewModel>();
            _eventAggregator.Subscribe(this);
        }

        public void OnEvent(EnableUserToEditTask e)
        {
           // var window = new EditTaskWindow(e.Task);
            var window = _container.Resolve<EditTaskWindow>(new ParameterOverride("item", e.Task));
            window.ShowDialog();
        }
    }
}
