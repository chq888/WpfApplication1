using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class Base
    {
        public virtual void Show()
        {
            System.Console.WriteLine("Base");
        }
    }

    public class Inherite : Base, Interface1, Interface2
    {
        //void Interface2.Show()
        //{
        //    System.Console.WriteLine("Interface2.Show(");
        //}

        public new void Show()
        {
            System.Console.WriteLine("11");
        }
        //void Interface1.Show()
        //{
        //    System.Console.WriteLine("Interface1.Show");
        //}
    }

    public interface Interface1
    {
        void Show();
    }

    public interface Interface2
    {
        void Show();
    }

}
