namespace FiniteField
{
    internal class Convertor
    {
        public static Element FromBinary(string bin, Field f)
        {
            var result = new Element(f);
            var str = bin.PadLeft(Field.dimension, '0');
            for (int i = 0; i < Field.dimension; i++)
            {
                if (str.ElementAt(i) == '1')
                {
                    result.array[i / 64] += (ulong)1 << i % 64;
                }
            }
            return result;
        }
        public static string ToBinary(Element element)
        {
            var result = "";
            for (int i = 0; i < Field.dimension; i++)
            {
                if (element.GetBit(i) == 1)
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
