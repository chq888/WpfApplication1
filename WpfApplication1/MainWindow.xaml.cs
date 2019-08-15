using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml;

namespace WpfApplication1
{
    public class AAA
    {
        public string aa;

        public AAA()
        {
            Debug.WriteLine("public AAA()");
        }

        static AAA()
        {
            Debug.WriteLine("static AAA()");
        }

        public static void Write()
        {
            Debug.WriteLine("AAA.Write()");
        }

        public static void Read()
        {
            Debug.WriteLine("AAA.Read()");
        }

    }

    public class BBB : AAA
    {

        public BBB()
        {
            Debug.WriteLine("public BBB()");
        }

        static BBB()
        {
            Debug.WriteLine("static BBB()");
        }

        public static new void Write()
        {
            Debug.WriteLine("BBB.Write()");
        }

        public static new void Read()
        {
            Debug.WriteLine("BBB.Read()");
        }

    }

    public static class SAAA
    {

        static SAAA()
        {
            Debug.WriteLine("SAAA()");
        }

        public static void Write()
        {
            Debug.WriteLine("SAAA.Write()");
        }

        public static void Read()
        {
            Debug.WriteLine("SAAA.Read()");
        }

    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String aa = null;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;

            Debug.WriteLine("Start test");

            AAA.Write();
            BBB.Write();
            SAAA.Write();

            var aaa2 = new AAA();

            AAA.Read();
            BBB.Read();
            SAAA.Read();

            var bbb2 = new BBB();

            Debug.WriteLine("End test");

            Derived1 ddd = new Derived1();
            Base1 bbb1 = new Base1();
            //ddd = bbb1;
            bbb1 = ddd;

            IEnumerable<Derived1> d = new List<Derived1>();
            IEnumerable<Base1> b = d;
            //d = b;
            Action aa333 = () => { };
            
            Action<Base1> b1 = (target) => { Console.WriteLine(target.GetType().Name); };
            Action<Derived1> d1 = (target) => { Console.WriteLine(target.GetType().Name); };//b1;
            d1(new Derived1());


            StringWriter writer = new StringWriter();
            XmlTextWriter xmlWriter = new XmlTextWriter(writer);
            xmlWriter.Dispose();
            xmlWriter = null;
            writer.Dispose();
            writer = null;
            XmlReader xmlReader = XmlReader.Create(new StringReader(""));
            xmlReader.Dispose();



            int Value = 12;

            Console.WriteLine("Techniques of incrementing a value");

            Console.Write("Value = ");
            Console.WriteLine(Value);

            Console.Write("Value = ");
            Console.WriteLine(Value);

            Console.Write("Value = ");
            Console.WriteLine(Value);


            for (int u = 0; u < 3; u++)
            {
                Console.Write(++u);
            }

            MyClass aa = new MyClass();
            int aaa = 1 + 2 / 2;
            int bbb = (1 + 2) / 2;
            int ccc = 0;
            string base64Encoded = "YmFzZTY0IGVuY29kZWQgc3RyaW5n";
            string base64Decoded;
            byte[] data = System.Convert.FromBase64String(base64Encoded);
            base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
            ccc = 0;
            MyClass2 my = new MyClass2();
            my.AAA1();
            MyClass1 my1 = my;
            my1.AAA1();
            MyClass my2 = my;
            my2.AAA1();

            my1 = new MyClass1();
            my1.AAA1();
            my2 = my1;
            my2.AAA1();

            string str = "this is a test that is a cat.";
            int astr = str.IndexOf('a', 0);
            astr = str.IndexOf('a', 1);
            astr = str.IndexOf('a', 8);
            astr = str.IndexOf('a', 9);
            astr = "this is a test that".Length;
            string str1 = SubToken(str, 3, "a");



            // Display powers of 2 up to the exponent of 8:
            foreach (int i in Power(2, 8))
            {
                Console.Write("{0} ", i);
            }


            str = "aabaa";
            //FirstMethod(str);
            //lastLetter(str);

        }




        public async void asdf ()
        {
            TaskCompletionSource<int> tcs1 = new TaskCompletionSource<int>();
            Task<int> t1 = tcs1.Task;
            Debug.WriteLine("00");

            // Start a background task that will complete tcs1.Task
            Task.Factory.StartNew(() =>
            {
                Debug.WriteLine("11");
                Thread.Sleep(5000);
                Debug.WriteLine("22");
                tcs1.SetResult(15);
                Debug.WriteLine("333");
            });
            Debug.WriteLine("44");

            // The attempt to get the result of t1 blocks the current thread until the completion source gets signaled.
            // It should be a wait of ~1000 ms.
            Stopwatch sw = Stopwatch.StartNew();
            int result = t1.Result;
            sw.Stop();

            Debug.WriteLine("(ElapsedTime={0}): t1.Result={1} (expected 15) ", sw.ElapsedMilliseconds, result);

            // ------------------------------------------------------------------

            // Alternatively, an exception can be manually set on a TaskCompletionSource.Task
            TaskCompletionSource<int> tcs2 = new TaskCompletionSource<int>();
            Task<int> t2 = tcs2.Task;

            // Start a background Task that will complete tcs2.Task with an exception
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                tcs2.SetException(new InvalidOperationException("SIMULATED EXCEPTION"));
            });

            // The attempt to get the result of t2 blocks the current thread until the completion source gets signaled with either a result or an exception.
            // In either case it should be a wait of ~1000 ms.
            sw = Stopwatch.StartNew();
            try
            {
                result = t2.Result;

                Debug.WriteLine("t2.Result succeeded. THIS WAS NOT EXPECTED.");
            }
            catch (AggregateException e)
            {
                Debug.Write("(ElapsedTime={0}): " + sw.ElapsedMilliseconds);
                Debug.WriteLine("The following exceptions have been thrown by t2.Result: (THIS WAS EXPECTED)");
                for (int j = 0; j < e.InnerExceptions.Count; j++)
                {
                    Debug.WriteLine("\n-------------------------------------------------\n{0}", e.InnerExceptions[j].ToString());
                }
            }

        }

        public static System.Collections.Generic.IEnumerable<int> Power(int number, int exponent)
        {
            int result = 1;

            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }
        }


        public static void FirstMethod(string str)
        {
            for (int u = 0; u < str.Length; u++)
            {
                String aa = str.Substring(u, (str.Length - 1));
                for (int i = 0, j = aa.Length - 1; i < j; i++, j--)
                {
                    if (aa[i] != aa[j])
                    {
                        continue;

                    }
                }
            }

        }
        void lastLetter(string word)
        {

            int len = word.Length;
            if (len >= 1 && len <= 100)
            {
                for (int i = 0; i < 2; i++) {
                    String aa = word.Substring(word.Length - (i + 1), 1);
                }
            }
        }

        // Virtoga.Core.Extensions.StringExtension
        public string SubToken(string src, int n, string delimiter)
        {
            if (n < 0 || src == null)
            {
                return null;
            }
            if (n == 0 && src.IndexOf(delimiter) == -1)
            {
                return src;
            }
            int num = -delimiter.Length;
            for (int i = 0; i < n; i++)
            {
                num = src.IndexOf(delimiter, delimiter.Length);
                /*
                if ((num = src.IndexOf(delimiter, num + delimiter.Length)) == -1)
                {
                    return null;
                }
                */
            }
            string text = src.Substring(num + delimiter.Length);
            if ((num = text.IndexOf(delimiter)) != -1)
            {
                text = text.Substring(0, num);
            }

            return text;
        }


        // Virtoga.Core.Extensions.StringExtension
        public string SubToken(string src, string start, string end, int fromIndex, bool sensitive)
        {
            if (src == null)
            {
                return string.Empty;
            }
            string text = src;
            if (!sensitive)
            {
                src = src.ToUpper();
                start = start.ToUpper();
                end = end.ToUpper();
            }
            int num = src.IndexOf(start, fromIndex);
            if (num == -1)
            {
                return null;
            }
            num += start.Length;
            int num2 = src.IndexOf(end, num);
            if (num2 == -1)
            {
                return null;
            }
            int num3 = num;
            while ((num3 = src.IndexOf(start, num3)) != -1 && num3 < num2)
            {
                num2 = src.IndexOf(end, num2 + end.Length);
                num3 += start.Length;
            }

            string aaa = text.Substring(num, num2 - num);
            return aaa;
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            asdf();

            return;

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            tokenSource.CancelAfter(TimeSpan.FromSeconds(10));

            Action action = () =>
            {
                try
                {
                    //token.ThrowIfCancellationRequested();

                    while (true)
                    {

                        if (token.IsCancellationRequested)
                        {
                            Console.WriteLine("Canceled!");
                            //break;
                        }

                        Console.Write(".");
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                }
                catch (Exception ex)
                {
                }
            
            };

            try {
                var task = Task.Run(action, token);

                //
                // 3秒後にキャンセル
                //
                task.Wait();

                tokenSource.Dispose();
            }
            catch (Exception exxx)
            {

            }


            //CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
            CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.FrameworkCultures);
            foreach (CultureInfo cul in cinfo)
            {
                RegionInfo ri = null;
                try
                {
                    ri = new RegionInfo(cul.Name);
                    string TwoLetterISORegionName = ri.TwoLetterISORegionName;
                    string ThreeLetterISORegionName = ri.ThreeLetterISORegionName;
                    string TwoLetterISOLanguageName = cul.TwoLetterISOLanguageName;
                    string ThreeLetterISOLanguageName = cul.ThreeLetterISOLanguageName;
                    string RegionName = cul.Name;
                }
                catch
                {
                    continue;
                }
            }


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(".\\XMLFile1.xml");
            XmlNamespaceManager xmlNsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            xmlNsMgr.AddNamespace("airsync", "AirSync");
            xmlNsMgr.AddNamespace("airsyncbase", "AirSyncBase");
            XmlNode appDataNode = xmlDoc.SelectSingleNode(String.Format(".//{0}:ApplicationData", "airsync"), xmlNsMgr);
            XmlNodeList attachmentsNode = appDataNode.SelectNodes(String.Format(".//{0}:Attachments", "airsyncbase"), xmlNsMgr);
            int jcount = attachmentsNode.Count;
            for (int j = 0; j < jcount; j++)
            {
                XmlNode attachmentNode = attachmentsNode[j];
                XmlNode ddd = attachmentNode.SelectSingleNode(String.Format(".//{0}:DisplayName", "airsyncbase"), xmlNsMgr);
                string aa = ddd.InnerText;
                attachmentNode.SelectNodes(String.Format(".//{0}:FileReference", "airsyncbase"), xmlNsMgr);
                attachmentNode.SelectNodes(String.Format(".//{0}:EstimatedDataSize", "airsyncbase"), xmlNsMgr);
            } // for

            IList<string> result = new List<string>();
            using (var reader = new StreamReader("TextFile1.txt"))
            {
                string currentLine;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    if (currentLine.StartsWith("message-id", StringComparison.CurrentCultureIgnoreCase) || currentLine.StartsWith("references", StringComparison.CurrentCultureIgnoreCase))
                    {
                        // if you do not need multiple lines and just the first one
                        // just break from the loop (break;)            
                        result.Add(currentLine);
                    }
                }
            }

            /*
            xmlNsMgr.AddNamespace("Settings", "Settings");
            XmlNode userInformation = xmlDoc.SelectSingleNode(String.Format(".//{0}:UserInformation//{0}:Get//{0}:EmailAddresses", "Settings"), xmlNsMgr);
            String value = "";
            XmlNodeList childNodes = userInformation.ChildNodes;
            int count = childNodes.Count;
            for (int i = 0; i < count; i++)
            {
                XmlNode xmlnode = childNodes[i];
                value = xmlnode.InnerText;
            }
            */
        }
    }
    public class MyClass
    {
        // Name of the instance:
        private string name;


        // Needed to satisfy the new() constraint.
        public MyClass()
        {
        }


        public MyClass(int initialCount = 0, string name = "")
        {
            this.name = name;
        }


        public virtual void AAA1() {
            Console.Write("MyClass");
        }
    
    }




    public class MyClass1 : MyClass
    {
        // Name of the instance:
        private readonly string name;

        // Needed to satisfy the new() constraint.
        public MyClass1()
        {
        }


        public MyClass1(int initialCount = 0, string name = "")
        {
            this.name = name;
        }


        public override void AAA1()
        {
            Console.Write("MyClass1");
        }


    }

    public class MyClass2 : MyClass1
    {

        public new void AAA1()
        {
            Console.Write("MyClass2");
            MyClass3 s = new MyClass3();
        }

    }

    public class MyClass3
    {
        private static string aaa = "";
        static MyClass3() {
        }
        private static void AAA1()
        {
            aaa = "";
            Console.Write("MyClass2");
        }

    }

    class Base1
    {
        public static void PrintBases(IEnumerable<Base1> bases)
        {
            foreach (Base1 b in bases)
            {
                Console.WriteLine(b);
            }
        }
    }

    class Derived1 : Base1
    {
        public static void Main1()
        {
            List<Derived1> dlist = new List<Derived1>();

            Derived1.PrintBases(dlist);
            IEnumerable<Base1> bIEnum = dlist;
        }
    }

}
