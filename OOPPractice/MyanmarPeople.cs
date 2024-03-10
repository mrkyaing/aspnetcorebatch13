using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPPractice
{
    internal class MyanmarPeople : SayHello
    {
        public override void Greeting()
        {
            Console.WriteLine("ဟုတ်ကဲ့ မင်္ဂလာပါ");
        }

        public override void MySelf()
        {
            Console.WriteLine("i am Myanmar .  currently , i live in YGN" + base.Name);
        }
    }
}
