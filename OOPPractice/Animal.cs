using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPPractice
{
    public class Animal
    {
        public string Name { get; set; }
       public  int LifeSpan {  get; set; }

        public string Color { get; set; }

        public virtual void Eat()
        {
            Console.WriteLine("Eat somethings");
        }

        public virtual void Speak()
        {
            Console.WriteLine("speak somethings");
        }

        public void ShowInfo()
        {
            Console.WriteLine("name:" + Name);
            Console.WriteLine("color:" + Color);
            Console.WriteLine("Life Span:" + LifeSpan+" in months");
        }
    }
}
