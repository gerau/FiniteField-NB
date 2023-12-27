using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiniteFieldNormal.Field;

namespace FiniteFieldNormal
{
    internal class Convertor
    {
        public static Element fromBinary(string bin) 
        {
            var result = new Element();
            var str = bin.PadLeft(Field.dimension, '0');
            for(int i = 0; i < Field.dimension; i++)
            {
                if (str.ElementAt(i) == '1') {
                    result.array[i / 64] += (ulong)1 << (i % 64); 
                }
            }
            return result;
        }
        public static string toBinary(Element element) 
        {
            var result = "";
            for (int i = 0; i < Field.dimension; i++)
            {
                ulong temp = element.array[i / 64] >> (i % 64);
                if((temp & 1) == 1)
                {
                    result += "1";
                }
                else
                {
                    result += "0";
                }
            }
            return result;
        }
    }
}
