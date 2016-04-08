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
using  System.Linq.Expressions;
using Expression = System.Linq.Expressions.Expression;
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.Loaded += Window1_Loaded;
            IList<String> bbb = new List<String>();
            string aa = String.Join(",", bbb);
            string aa1 = String.Join(",", bbb);
            Expression firstArg = Expression.Constant(2);
            Expression secondArg = Expression.Constant(3);
            Expression add = Expression.Add(firstArg, secondArg);
            Expression<Func<int>> dd =  Expression.Lambda<Func<int>>(add);
            Console.WriteLine(dd.Compile()());
        }

        // declare delegate
        delegate int DoWork(ref string work);
        // have a method to create an instance of and call the delegate
        public void WorkItOut()
        {
            // declare instance
            DoWork dw = (ref string s) =>
            {
                Console.WriteLine(s);
                return s.GetHashCode();
            };
            // invoke delegate
            string aa = "Do some inline work";
            int i = dw(ref aa);
        }

        int[] arr = new int[] { 1, 55, 23, 78, 6 };

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            //InsertSort();
            //SelectSort();
            BubbleSort();
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }

        }

        public void BubbleSort()
        {
            int n = arr.Length;
            int temp;

            for (int i = 0; i < n; i++)
            {
                for (int j = n - 1; j > i; j--)
                {
                    if (arr[j] < arr[j - 1])
                    {
                        temp = arr[j];
                        arr[j] = arr[j-1];
                        arr[j - 1] = temp;
                    }
                }
            }
        }

        public void InsertSort()
        {
            int i,j,newValue = 0;
            for (i = 1; i < arr.Length; i++)
            {
                newValue = arr[i];
                j = i;
                while (j > 0 && arr[j - 1] > newValue)
                {
                    arr[j] = arr[j - 1];
                    j--;
                }
                arr[j] = newValue;
            }
        }


        public void SelectSort()
        {
            int n = arr.Length;
            int minIndex, temp;

            for (int i = 0; i < n - 1; i++)
            {
                minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    temp = arr[i];
                    arr[i] = arr[minIndex];
                    arr[minIndex] = temp;
                }

            }
        }
    }
}
