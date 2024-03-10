using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPPractice
{
    public class Person
    {
        private string name;
        private int age;

        public void SetName(string n)
        {
            if (string.IsNullOrEmpty(n))
            {
                Console.WriteLine("invalid name");
            }
            else
            {
                name = n;
            }
        }
    }
}
