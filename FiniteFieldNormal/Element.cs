namespace FiniteFieldNormal
{
    public class Element
    {
        public ulong[] array = new ulong[Field.dimension / 64 + 1];
        public Field field;

        public Element(Field field)
        {
            this.field = field;
        }
        public Element(Element element, Field field)
        {
            Array.Copy(element.array, array, array.Length);
            this.field = field;
        }
        public Element(Element element)
        {
            Array.Copy(element.array, array, array.Length);
            this.field = element.field;
        }
        public Element(ulong[] array, Field field)
        {
            this.array = array;
            this.field = field;
        }

        public static Element operator + (Element left, Element right)
        {
            Element result = new Element(left.field);
            for (int i = 0; i < left.array.Length; i++)
            {
                result.array[i] = left.array[i] ^ right.array[i];
            }
            return result;
        }
        
        public Element Power(Element element)
        {
            var result = field.One();
            for (int i = 0; i < Field.dimension - 1; i++)
            {
                if (element.GetBit(i) == 1)
                {
                    result = field.Mult(result, this);
                }
                result = result.CycleShiftToRight();
            }
            if (element.GetBit(Field.dimension - 1) == 1)
            {
                result = field.Mult(result, this);
            }
            return result;

        }
        public Element CycleShiftToRight()
        {
            Element result = new Element(field);
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
        public Element CycleShiftToRight(int shift)
        {
            Element result = new Element(field);
            for(int i = 0; i < shift; i++)
            {
                result = result.CycleShiftToRight();
            }
            return result;
        }
        
        public Element CycleShiftToLeft()
        {
            Element result = new Element(field);
            for (int i = 0; i < array.Length - 1; i++)
            {
                result.array[i] = array[i] >> 1 | array[i + 1] << 63;
            }
            int l = (Field.dimension % 64);
            var temp = array[0] & 1;

            result.array[^1] = (array[^1] >> 1) + (temp << l - 1);
            return result;
        }

        public Element Inversed()
        {
            var binary = Convert.ToString(Field.dimension - 1, 2);
            var result = new Element(this);
            var k = 1;           
            for(int i = 1; i < binary.Length; i++)
            {
                var temp = new Element(result);
                for(int j = 0; j < k; j++)
                {
                    result = result.CycleShiftToRight();
                }
                k = 2 * k;
                result = field.Mult(result, temp);
                if(binary.ElementAt(i) == '1')
                {
                    result = field.Mult(result.CycleShiftToRight(),this);
                    k++;
                }
            }
            result = result.CycleShiftToRight();
            return result;
        }
        public Element Trace()
        {
            int k = BitCount();
            k &= 1;
            if(k == 1)
            {
                return field.One();
            }
            else
            {
                return field.Zero();
            }
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
        public static int BitCount(ulong u)
        {
            int count = 0;
            while (u > 0)
            {
                count++;
                u = u & u - 1;
            }
            return count;
        }
        public int GetBit(int position)
        {
            if (position >= Field.dimension | position < 0)
            {
                return 0;
            }
            int count = position / 64;
            int shift = position % 64;
            return (int)((array[count] >> shift) & 1);
        }
        public override string ToString()
        {
            return Convertor.ToBinary(this);
        }
    }
}
