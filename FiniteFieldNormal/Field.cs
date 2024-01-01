namespace FiniteFieldNormal
{
    public class Field
    {
        public const int dimension = 491;
        public int[][] multiplicativeMatrix;

        private static int Modulo(int number, int modulo)
        {
            int result = number % modulo;
            if (result < 0)
            {
                result += modulo;
            }
            return result;
        }
        public Field()
        {
            int[] powerOfTwos = new int[dimension];
            powerOfTwos[0] = 1;
            bool[,] multMatrix = new bool[dimension, dimension];

            multiplicativeMatrix = new int[dimension][];

            var p = dimension * 2 + 1;
            for (int i = 1; i < dimension; i++)
            {
                powerOfTwos[i] = powerOfTwos[i - 1] * 2 % p;
            }
            for (int i = 0; i < dimension; i++)
            {
                for (int j = i; j < dimension; j++)
                {
                    if (Modulo(powerOfTwos[i] + powerOfTwos[j], p) == 1)
                    {
                        multMatrix[i, j] = multMatrix[j, i] = true;
                        continue;
                    }
                    else if (Modulo(powerOfTwos[i] - powerOfTwos[j], p) == 1)
                    {
                        multMatrix[i, j] = multMatrix[j, i] = true;
                        continue;
                    }
                    else if (Modulo(-powerOfTwos[i] + powerOfTwos[j], p) == 1)
                    {
                        multMatrix[i, j] = multMatrix[j, i] = true;
                        continue;
                    }
                    else if (Modulo(-powerOfTwos[i] - powerOfTwos[j], p) == 1)
                    {
                        multMatrix[i, j] = multMatrix[j, i] = true;
                        continue;
                    }
                }
            }
            List<int> res = new List<int>();
            for (int i = 0; i < dimension; i++)
            {

                for (int j = 0; j < dimension; j++)
                {
                    if (multMatrix[i, j])
                    {
                        res.Add(j);
                    }
                }
                multiplicativeMatrix[i] = new int[res.Count];
                for (int j = 0; j < res.Count; j++)
                {
                    multiplicativeMatrix[i][j] = res[j];
                }
                res.Clear();
            }
        }
        public byte[] MultByMatrix(byte[] vector)
        {
            var result = new byte[dimension];
            for (int i = 0; i < dimension; i++)
            {
                int count = 0;
                for (int j = 0; j < multiplicativeMatrix[i].Length; j++)
                {
                    count += vector[multiplicativeMatrix[i][j]];
                }
                count &= 1;
                result[i] = (byte)count;
            }
            return result;
        }
        private static byte MultByVector(byte[] left, byte[] right)
        {
            byte result = 0;
            for (int i = 0; i < dimension; i++)
            {
                result += (byte)(left[i] & right[i]);
            }
            return (byte)(result & 1);
        }

        public Element Mult(Element left, Element right)
        {
            ulong[] result = new ulong[left.array.Length];


            var tempLeft = GetPositionOfOnes(left);
            var tempRight = GetPositionOfOnes(right);
            for (int i = 0; i < dimension; i++)
            {
                var temp = MultByMatrix(tempLeft[i]);
                var count = MultByVector(temp, tempRight[i]);

                result[i / 64] |= (ulong)count << (i % 64);
            }
            return new Element(result, left.field);
        }
        private static byte[][] GetPositionOfOnes(Element element)
        {
            byte[][] result = new byte[dimension][];
            for (int i = 0; i < dimension; i++)
            {
                result[i] = new byte[dimension];
                if (element.GetBit(i) == 1)
                {
                    result[0][i] = 1;
                }
            }
            for (int i = 1; i < dimension; i++)
            {
                result[i] = new byte[dimension];
                for (int j = 0; j < dimension - 1; j++)
                {
                    result[i][j] = result[i - 1][j + 1];
                }
                result[i][^1] = result[i - 1][0];
            }
            return result;
        }
        public Element One()
        {
            Element result = new Element(this);
            for (int i = 0; i < result.array.Length - 1; i++)
            {
                result.array[i] = ulong.MaxValue;
            }
            result.array[^1] = ulong.MaxValue & (ulong.MaxValue >> (64 - dimension % 64));
            return result;
        }
        public Element Zero()
        {
            return new Element(this);
        }
    }
}
