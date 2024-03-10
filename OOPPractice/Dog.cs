using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPPractice
{
    public class Dog:Animal
    {
        public override void Eat()
        {
            Console.WriteLine("eat meal");
        }
        public override void Speak()
        {
            Console.WriteLine("Woak Woak");
        }
    }
}
