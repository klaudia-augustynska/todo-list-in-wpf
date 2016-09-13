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
using TodoList.Models;
using TodoList.ViewModels;

namespace TodoList.Views
{
    /// <summary>
    /// Interaction logic for EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        private readonly IUnityContainer _container;

        public EditTaskWindow(IUnityContainer container, TaskItem item)
        {
            InitializeComponent();
            _container = container;
            DataContext = _container.Resolve<EditTaskWindowViewModel>(new ParameterOverride("item", item));
        }
    }
}
