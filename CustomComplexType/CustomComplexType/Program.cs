using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomComplexType
{
    class Program
    {
        static void Main(string[] args)
        {
            ComplexNumber complexNumber1 = new ComplexNumber("1", "12");
            ComplexNumber complexNumber2 = new ComplexNumber("32", "16");
            ComplexNumber cSum = complexNumber1.Sum(complexNumber2);
        }
    }
}
