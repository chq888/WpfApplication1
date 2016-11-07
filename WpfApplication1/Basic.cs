using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class Basic
    {
        public static void Boxing()
        {
            int i = 25;
            object o = i;
            Console.WriteLine(o.ToString());

            object first = 5;
            object o1 = first;
            i = (int)o1; // unbox;

            Console.WriteLine(i.ToString());

            // raise casting expcetion 
            string aa = (string) o1;

            Console.WriteLine(aa);
        }

        public static void Equal()
        {
            string name1 = "a";
            string name2 = "a";
            Console.WriteLine($"==: {name1 == name2}");
            Console.WriteLine($"==: {name1.Equals(name2)}");

            MyClass1 c1 = new MyClass1() { Name = "a"};
            MyClass1 c2 = c1;
            Console.WriteLine($"==: {c1 == c2}");
            Console.WriteLine($"==: {c1.Equals(c2)}");

        }

        public class MyClass1
        {
            public string Name { get; set; }
        }
    }
}
