using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteFieldNormal.Field
{
    public class Field
    {
        public const int dimension = 491;
        public List<List<int>> multiplicativeMatrix;

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

            multiplicativeMatrix = new List<List<int>>();

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

            for (int i = 0; i < dimension; i++)
            {
                multiplicativeMatrix.Add(new List<int>());
                for (int j = 0; j < dimension; j++)
                {
                    if (multMatrix[i, j])
                    {
                        multiplicativeMatrix[i].Add(j);
                    }
                }
            }
        }
    }
}
