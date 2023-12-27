using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FiniteFieldNormal.Field.Field
{
    public class Element()
    {
        public ulong[] array = new ulong[Field.dimension / 64 + 1];
        //public Field field = f;

        public static Element operator +(Element left, Element right)
        {
            Element result = new Element();
            for (int i = 0; i < left.array.Length; i++)
            {
                result.array[i] = left.array[i] ^ right.array[i];
            }
            return result;
        }
        public Element CycleShiftToRight()
        {
            Element result = new Element();
            int l = Field.dimension % 64;

            var k = array[^1] >> l - 1 & 1;
            result.array[0] = (array[0] << 1) + k;
            for (int i = 1; i < array.Length - 1; i++)
            {
                result.array[i] = array[i] << 1 | array[i - 1] >> 63;
            }
            result.array[^1] = (array[^1] << 1) + (array[^2] >> 63);
            result.array[^1] = result.array[^1] - ((ulong)1 << l);
            return result;
        }
        public Element CycleShiftToLeft()
        {
            Element result = new Element();
            ulong mask = (ulong)1 << 63;
            for (int i = 0; i < array.Length - 1; i++)
            {
                result.array[i] = array[i] >> 1 | array[i + 1] << 63;
            }
            int l = Field.dimension % 64;
            var temp = array[0] & 1;

            result.array[^1] = (array[^1] >> 1) + (temp << l);
            return result;
        }


        public int BitCount()
        {
            int count = 0;
            foreach (var number in array)
            {
                count += BitCount(number);
            }
            return count;

        }
        public int BitCount(ulong u)
        {
            int count = 0;
            while (u > 0)
            {
                count++;
                u = u & u - 1;
            }
            return count;
        }

    }
}
