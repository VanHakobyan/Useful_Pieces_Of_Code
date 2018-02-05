using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomComplexType
{
    public class ComplexNumber : IComparable
    {
        public ComplexNumber(string complexPart, string realPart)
        {
            this.realPart = realPart;
            this.complexPart = complexPart;
        }
        private string complexCoefficient = "i";
        public string complexPart;
        public string realPart;
        public ComplexNumber Sum(ComplexNumber c1)
        {
            DataTable dataTable = new DataTable();
            string c = $"{dataTable.Compute($"{c1.complexPart}+{complexPart}", "").ToString()}{complexCoefficient}";
            string r = $"{dataTable.Compute($"{c1.realPart}+{realPart}", "").ToString()}";

            return new ComplexNumber(c, r) { complexPart = c, realPart = r };
        }
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
