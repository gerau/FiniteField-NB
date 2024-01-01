using System.Diagnostics;
using System.Reflection.Emit;
using System.Text;

namespace FiniteField
{
    internal class Program
    {
        
        static void MeasureTime(int numOfIterations)
        {
            long[] ticks = new long[8];
            Stopwatch st = new();
            for (int i = 0; i < numOfIterations; i++)
            {
                st.Start();
                var field = new Field();
                st.Stop();
                Console.WriteLine($"Field multiplicative matrix = {st.ElapsedMilliseconds}");
                ticks[0] += st.ElapsedTicks;
                st.Restart();

                var num1 = Generator.Generate(field);
                var num2 = Generator.Generate(field);
                st.Start();
                _ = num1 + num2;
                st.Stop();
                Console.WriteLine($"Addition ticks = {st.ElapsedTicks}");
                ticks[1] += st.ElapsedTicks;
                st.Restart();

                num1 = Generator.Generate(field);
                num2 = Generator.Generate(field);
                st.Start();
                _ = num1 * num2;
                st.Stop();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Multiplication ticks = {st.ElapsedTicks}");
                ticks[2] += st.ElapsedTicks;
                st.Restart();

                num1 = Generator.Generate(field);
                st.Start();
                _ = num1.ToSquare();
                st.Stop();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"To square ticks = {st.ElapsedTicks}");
                ticks[3] += st.ElapsedTicks;
                st.Restart();

                num1 = Generator.Generate(field);
                st.Start();
                _ = num1.Trace();
                st.Stop();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Trace ticks = {st.ElapsedTicks}");
                ticks[4] += st.ElapsedTicks;
                st.Restart();

                num1 = Generator.Generate(field);
                num2 = Generator.Generate(field);
                st.Start();
                _ = num1.Pow(num2);
                st.Stop();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Power ticks = {st.ElapsedTicks}");
                ticks[5] += st.ElapsedTicks;
                st.Restart();

                num1 = Generator.Generate(field);
                st.Start();
                _ = num1.InverseElement();
                st.Stop();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Inverse Element ticks = {st.ElapsedTicks}");
                ticks[6] += st.ElapsedTicks;
                st.Restart();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Total amount of ticks(in average):");
            Console.WriteLine($"Field multiplication matrix founding: {ticks[0] / numOfIterations}");
            Console.WriteLine($"Addition: {ticks[1] / numOfIterations}");
            Console.WriteLine($"Multiplication: {ticks[2] / numOfIterations}");
            Console.WriteLine($"Square: {ticks[3] / numOfIterations}");
            Console.WriteLine($"Trace: {ticks[4] / numOfIterations}");
            Console.WriteLine($"Power: {ticks[5] / numOfIterations}");
            Console.WriteLine($"Inverse: {ticks[6] / numOfIterations}");
            Console.WriteLine($"Quadratic equation: {ticks[7] / numOfIterations}");
            
        }


        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            var str1 = "10101011100001101101000000111011010110110001110101101001101110001111010011100000110010010011101110100101010100100111101110011110000001011001100111001110001010001101000110100111011010111101001100110100000001100101110011100110000010110110010100100001011100100100011101000100111000001011011101111011111110110101101111101111101000100111000100000010010110001001011110110011111010110101001101001001100110000001011110000111100001101000010111010011100111010011010000000010001101100111100100000101000";
            var str2 = "00011001111111011010010110110111001101111111010011110010011001011101101011111010100101010101011111010010111111010110001111001001101110001001011010011011100001110100100101100111100010010000101101110010000110000000101011011001100100111110011001111110101011110010101011001101110001010110011101110010011100101110110001010000011101001111001100100111000011101101011111100000110101111011100101000011100010001010000100010011010001100000000110000010001111100101111010101111000011000101101001000000010";
            var str3 = "00010011101111111011000010111010110111010101010010101101110001100001010111111101111010101101010111010001000110011011111101111110110000001001011001110111001001010001000110100111010000010011000010100111101011010011100111111001100100001111100101100001000111010011101101110000100010001000011011001011011000110001100101110111000100011011001100100110001010001101000001011001010000101001110010110101100111110000001011101010010110001010100001100001011010111000111011110101001011011110010100000001100";
                        
            var Field = new Field();

            var a = Convertor.FromBinary(str1, Field);
            var b = Convertor.FromBinary(str2, Field);
            var n = Convertor.FromBinary(str3, Field);


            var add = a + b;
            var mult = a * b;
            var square = a.ToSquare();
            var inversed = a.InverseElement();
            var pow = a.Pow(n);


            var str = new StringBuilder();
            str.AppendLine($"A = {a}");
            str.AppendLine($"B = {b}");
            str.AppendLine($"N = {n}");
            str.AppendLine($"A + B = {add}");
            str.AppendLine($"A * B = {mult}");
            str.AppendLine($"A ^ 2 = {square}");
            str.AppendLine($"A ^ -1 = {inversed}");
            str.AppendLine($"A ^ N = {pow}");

            Console.WriteLine(str);

            for(int i = 0; i < 100; i++)
            {
                Console.WriteLine();
            }
            MeasureTime(100);

        }
    }
}
