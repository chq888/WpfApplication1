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
    /// Interaction logic for InheritWindow.xaml
    /// </summary>
    public partial class InheritWindow : Window
    {
        public InheritWindow()
        {
            InitializeComponent();
            this.Loaded += InheritWindow_Loaded;
        }

        private void InheritWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Inherite a = new Inherite();
            a.Show();

            Base a1 = new Inherite();
            a1.Show();

            Interface1 b = a;
            b.Show();

            Singleton3 sf= new Singleton3("");
            Singleton3.Instance.Test();
        }
    }

    public sealed class Singleton3
    {
        private static readonly Singleton3 _Instance = new Singleton3();
        public Singleton3()
        {

        }


        public Singleton3(string aa)
        {

        }


        static Singleton3()
        {
            
        }

        public static Singleton3 Instance
        {
            get
            {
                return _Instance;
            }
        }

        public void Test()
        {

        }
    }



}
