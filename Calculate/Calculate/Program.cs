using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculate
{
    class Program
    {
        static void Main(string[] args)
        {
            //string input = "14+5*4";
            //string input = "14+5*4/2";
            //string input = "-14+5*2";
            //string input = "-14+5*4/2";
            string input = "1-14+5*4/2";

            Calculator Calculator = new Calculator(input);
            Calculator.GetResult();
        }
    }
}
