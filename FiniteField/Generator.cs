using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteField
{
    public class Generator
    {
        public static Element Generate(Field f)
        {
            string s = "";
            Random rand = new Random();
            for(int i = 0; i < Field.dimension; i++)
            {
                var k = rand.NextDouble();
                if( k < 0.5) 
                {
                    s += '1';
                }
                else
                {
                    s += '0';
                }
            }
            return Convertor.FromBinary(s, f);
        }
    }
}
