using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RivestShamirAdlemanRSA;

namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var myRSA = new RSA();
            myRSA.GeneratePublicAndPrivateKeyData("Carlos");            

            Console.ReadKey();
        }
    }
}
