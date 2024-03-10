using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPPractice
{
    public class JapanesePeople : SayHello
    {
        public override void Greeting()
        {
            Console.WriteLine("こんにちは、よろしくお願いします");
        }

        public override void MySelf()
        {
            Console.WriteLine("i am Japanese .  currently , i live in TOKYO"+base.Name);
        }
    }
}
