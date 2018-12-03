using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static int Gcd(int x, int y)
        {
            while (y != 0)
            {
                int tmp = x % y;
                x = y;
                y = tmp;
            }

            return x;
        }

        static bool IsInteger(double value)
        {
            return value % 1 == 0;
        }

        static double RoundUp(double number, int digits)
        {
            return Math.Ceiling(number * Math.Pow(10, digits)) / Math.Pow(10, digits);
        }

        static Tuple<int, int> FormulaSheetUpgrade(int pageTotals, int imposeFirst, int imposeSecond)
        {
            double repeatLarge = 0, repeatSmall = 0;
            int imposeLarge = imposeFirst >= imposeSecond ? imposeFirst : imposeSecond;
            int imposeSmall = imposeFirst < imposeSecond ? imposeFirst : imposeSecond;

            double gcd = Gcd(imposeLarge, imposeSmall);
            double dividend = RoundUp(pageTotals / gcd, 0) * gcd;

            while (true)
            {
                double oddLarge = 0d;
                oddLarge = RoundUp(dividend / imposeLarge, 0);

                double step = 0, temptLarge = 0d, temptSmall = 0d;
                List<Tuple<double, double>> restult = new List<Tuple<double, double>>();
                while (temptLarge >= 0 && temptLarge < oddLarge)
                {
                    temptLarge = gcd * step;
                    temptSmall = (dividend - gcd * imposeLarge * step) / imposeSmall;

                    if (IsInteger(temptLarge)
                        && temptLarge >= 0
                        && temptSmall >= 0
                        && IsInteger(temptSmall))
                    {
                        restult.Add(Tuple.Create(temptLarge, temptSmall));
                    }

                    step += (1 / gcd);
                }

                if (restult != null && restult.Count > 0)
                {
                    var last = restult.Last();

                    repeatLarge = last.Item1;
                    repeatSmall = last.Item2;

                    double blankPages = Math.Abs(pageTotals - ((repeatLarge * imposeLarge) + (repeatSmall * imposeSmall)));
                    Console.WriteLine("blank pages: " + blankPages);

                    break;
                }

                dividend += gcd;
            }

            return Tuple.Create((int)repeatLarge, (int)repeatSmall);
        }

        public static void Main()
        {
            var result = FormulaSheetUpgrade(238, 24, 16);

            System.Console.WriteLine("impose large: " + result.Item1);
            System.Console.WriteLine("impose small:" + result.Item2);
            Console.ReadKey();
        }
}