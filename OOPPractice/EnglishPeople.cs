using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPPractice
{
    public class EnglishPeople : SayHello
    {
        public override void Greeting()
        {
            Console.WriteLine("Hi,Nice to meet  you");
        }

        public override void MySelf()
        {
            Console.WriteLine("i am english .  currently , i live in USA" + Name);
        }
    }
}
