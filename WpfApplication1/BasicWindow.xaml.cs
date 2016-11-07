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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for BasicWindow.xaml
    /// </summary>
    public partial class BasicWindow : Window
    {
        public BasicWindow()
        {
            InitializeComponent();
            this.Loaded += BasicWindow_Loaded;
        }

        private void BasicWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Basic.Equal();
            Basic.Boxing();
        }
    }
}
