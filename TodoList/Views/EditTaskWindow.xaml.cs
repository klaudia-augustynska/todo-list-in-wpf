using Microsoft.Practices.Unity;
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
using System.Windows.Shapes;
using TodoList.Helpers;
using TodoList.Helpers.EventAggregator;
using TodoList.Models;
using TodoList.ViewModels;

namespace TodoList.Views
{
    /// <summary>
    /// Interaction logic for EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window, ISubscriber<TodoEditorShouldCloseItself>
    {
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;

        public EditTaskWindow(IUnityContainer container, IEventAggregator eventAggregator, TaskItem item)
        {
            InitializeComponent();
            _container = container;
            _eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
            DataContext = _container.Resolve<EditTaskWindowViewModel>(new ParameterOverride("item", item));
        }

        public void OnEvent(TodoEditorShouldCloseItself e)
        {
            Close();
        }
    }
}
