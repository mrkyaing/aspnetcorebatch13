using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPPractice
{
    public abstract class SayHello
    {
        public string Name { get; set; }
        
        public void Hi()//Non-abstract method
        {
            Console.WriteLine("Hi");
        }

        //abstract method 
        public abstract void  Greeting();
        public abstract void MySelf();
    }
}
