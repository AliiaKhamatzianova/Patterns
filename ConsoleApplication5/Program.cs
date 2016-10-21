using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication5
{

    public abstract class Developer
    {
        public string Name { get; }

        public Developer(string n)
        {
            Name = n;
        }

        public abstract House Create();
    }

    public interface House
    {

    }

    class BrickDeveloper : Developer
    {
        public BrickDeveloper(string n) : base(n)
        {

        }

        public override House Create()
        {
            return new BrickHouse();
        }
    }

    public class BrickHouse : House
    {
        public BrickHouse()
        {
            Console.WriteLine("Brick house");
        }
    }


    class WoodDeveloper : Developer
    {
        public WoodDeveloper(string n) : base(n)
        {

        }

        public override House Create()
        {
            return new WoodHouse();
        }
    }

    public class WoodHouse : House
    {
        public WoodHouse()
        {
            Console.WriteLine("Wood house");
        }
    }

    public class Developers
    {
        private Dictionary<string, Developer> _developers = new Dictionary<string, Developer>();

        public Developer this[string name]
        {
            get
            {
                if (!_developers.ContainsKey(name))
                    //throw new ArgumentException(nameof(name));
                    return null;

                return _developers[name];
            }
        }

        public void RegisterDeveloper(Developer dev)
        {
            _developers[dev.Name] = dev;
        }

        public void UnRegisterDeveloper(Developer dev)
        {
            _developers.Remove(dev.Name);
        }
    }

    public abstract class Writer
    {
        public void Write(string message)
        {
            WriteImpl(message);
        }

        protected abstract void WriteImpl(string message);
        
    }

    

    public class StreamMessageWriter
    {
        private Stream _stream;

        public StreamMessageWriter(Stream s)
        {
            _stream = s;
        }

        public void Write(string message)
        {
            using (var sw = new StreamWriter(_stream))
            {
                sw.WriteLine(message);
            }
        }
    }

    public class ConsoleWriter:Writer
    {
        protected override void WriteImpl (string message)
        {
            Console.WriteLine(message);
        }
    }

    public class FileWriter : Writer
    {
        protected override void WriteImpl(string message)
        {
            using (var sw = new StreamWriter("message.txt"))
            {
                sw.WriteLine(message);
            }
        }
    }

    public class ExtandableWriter
    {
        public void Write(string message)
        {
            //WriteImpl(message);this.WriteImpl(message) - abstract static class static method
        }

        //public void WriteImpl(string message) { }
    }

    public class DelegateWriter
    {
        private Action<string> _writer;

        public DelegateWriter(Action<string> writer)
        {
            _writer = writer;
        }
        public void Write(string message)
        {
            _writer(message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            #region Factory Method

            //TimeSpan ts = TimeSpan.FromHours(1000.250);
            /*
            var dev1 = new WoodDeveloper("LLC Brickers");
            House wooden = dev1.Create();

            var dev2 = new BrickDeveloper("LLC Private Constructs");
            House brick = dev2.Create();

            var dev3 = new BrickDeveloper("LLC Wood");
            House wood= dev3.Create();*/

            /*
            var developers = new Developers();
            developers.RegisterDeveloper(new WoodDeveloper("LLC Brickers"));
            developers.RegisterDeveloper(new BrickDeveloper("LLC Private Constructs"));
            developers.RegisterDeveloper(new WoodDeveloper("LLC Wood"));

            House h1 = developers["LLC Brickers"]?.Create(); // предотвращение дальнейшего вызова null объекта
            */

            #endregion

            /*Writer w = new ConsoleWriter();
            w.Write("Hello world");

            //Writer w2 = new FileWriter();
            //w2.Write("Hello");

            var smw = new StreamMessageWriter(Console.OpenStandardOutput());
            smw.Write("hello");

            smw = new StreamMessageWriter(new FileStream("message.txt", FileMode.Append));
            smw.Write("Hello");*/

            var dw = new DelegateWriter(Console.WriteLine);
            dw.Write("Hello");
        }
    }
}
