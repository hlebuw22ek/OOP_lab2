using System;
using System.Text;

namespace OOPLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("=========================================");
                Console.WriteLine("1 - Автоматичний тест усіх завдань");
                Console.WriteLine("2 - Ручне введення: MyMatrix (Завдання 1)");
                Console.WriteLine("3 - Ручне введення: MyTime (Завдання 2)");
                Console.WriteLine("0 - Вихід");
                Console.Write("Оберіть пункт меню: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            RunAutoTests();
                            break;
                        case "2":
                            TestMatrixManual();
                            break;
                        case "3":
                            TestTimeManual();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Невірний пункт меню, спробуйте ще раз.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: " + ex.Message);
                }

                Console.WriteLine();
            }
        }

        // Зчитування матриці з клавіатури по рядках
        static MyMatrix ReadMatrixFromConsole(string name)
        {
            Console.Write($"Введіть кількість рядків матриці {name}: ");
            int rows = int.Parse(Console.ReadLine());

            string[] lines = new string[rows];
            Console.WriteLine($"Введіть {rows} рядків(а) чисел через пробіл:");
            for (int i = 0; i < rows; i++)
            {
                Console.Write($"Рядок {i + 1}: ");
                lines[i] = Console.ReadLine();
            }

            return new MyMatrix(lines);
        }

        static void TestMatrixManual()
        {
            Console.WriteLine("--- Ручна перевірка MyMatrix ---");
            MyMatrix a = ReadMatrixFromConsole("A");
            Console.WriteLine("\nВи ввели матрицю A:");
            Console.WriteLine(a);

            Console.WriteLine("\nТранспонована копія A:");
            Console.WriteLine(a.GetTransponedCopy());

            if (a.Height == a.Width)
            {
                Console.WriteLine("Детермінант A (1-й виклик): " + a.CalcDeterminant());
                Console.WriteLine("Детермінант A (2-й виклик із кешу): " + a.CalcDeterminant());

                Console.Write("Змінити елемент матриці для перевірки скидання кешу? (y/n): ");
                string ans = Console.ReadLine();
                if (ans == "y" || ans == "Y")
                {
                    Console.Write("Індекс рядка (з 0): ");
                    int r = int.Parse(Console.ReadLine());
                    Console.Write("Індекс стовпчика (з 0): ");
                    int c = int.Parse(Console.ReadLine());
                    Console.Write("Нове значення: ");
                    double val = double.Parse(Console.ReadLine());

                    a[r, c] = val;
                    Console.WriteLine("Нова матриця A:");
                    Console.WriteLine(a);
                    Console.WriteLine("Новий детермінант A: " + a.CalcDeterminant());
                }
            }

            Console.Write("\nВвести другу матрицю B для додавання та множення? (y/n): ");
            string secondAns = Console.ReadLine();
            if (secondAns == "y" || secondAns == "Y")
            {
                MyMatrix b = ReadMatrixFromConsole("B");
                Console.WriteLine("\nМатриця B:");
                Console.WriteLine(b);

                if (a.Height == b.Height && a.Width == b.Width)
                {
                    Console.WriteLine("\nСума A + B:");
                    Console.WriteLine(a + b);
                }
                else
                {
                    Console.WriteLine("\nДодати A + B не можна (різні розміри).");
                }

                if (a.Width == b.Height)
                {
                    Console.WriteLine("\nДобуток A * B:");
                    Console.WriteLine(a * b);
                }
                else
                {
                    Console.WriteLine("\nПомножити A * B не можна (ширина A != висоті B).");
                }
            }
        }

        static void TestTimeManual()
        {
            Console.WriteLine("--- Ручна перевірка MyTime ---");
            Console.Write("Введіть години (0-23): ");
            int h = int.Parse(Console.ReadLine());
            Console.Write("Введіть хвилини (0-59): ");
            int m = int.Parse(Console.ReadLine());
            Console.Write("Введіть секунди (0-59): ");
            int s = int.Parse(Console.ReadLine());

            MyTime t = new MyTime(h, m, s);
            Console.WriteLine("\nВведений час: " + t);
            Console.WriteLine("Секунд від початку доби: " + t.TimeSinceMidnight());
            Console.WriteLine("Зараз за розкладом: " + t.WhatLesson());
            Console.WriteLine("+ 1 секунда: " + t.AddOneSecond());
            Console.WriteLine("+ 1 хвилина: " + t.AddOneMinute());
            Console.WriteLine("+ 1 година:  " + t.AddOneHour());

            Console.Write("\nСкільки секунд додати (можна від'ємне число)? ");
            int addSec = int.Parse(Console.ReadLine());
            Console.WriteLine("Новий час: " + t.AddSeconds(addSec));
        }

        static void RunAutoTests()
        {
            Console.WriteLine("=== АВТОТЕСТ ЗАВДАННЯ 1: MyMatrix ===");
            double[,] rawArray = {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };
            MyMatrix m1 = new MyMatrix(rawArray);
            Console.WriteLine("Матриця m1 (2x3):");
            Console.WriteLine(m1);

            string matrixText = m1.ToString();
            MyMatrix m2 = new MyMatrix(matrixText);
            Console.WriteLine("\nСума m1 + m2:");
            Console.WriteLine(m1 + m2);

            MyMatrix mTransp = m1.GetTransponedCopy();
            Console.WriteLine("\nТранспонована копія m1 (3x2):");
            Console.WriteLine(mTransp);

            Console.WriteLine("\nДобуток m1 * mTransp:");
            Console.WriteLine(m1 * mTransp);

            double[,] detArr = {
                { 2, 4, 1 },
                { 0, 2, 1 },
                { 3, 1, 2 }
            };
            MyMatrix mDet = new MyMatrix(detArr);
            Console.WriteLine("\nДетермінант (1-й виклик): " + mDet.CalcDeterminant());
            Console.WriteLine("Детермінант (2-й виклик із кешу): " + mDet.CalcDeterminant());
            mDet[0, 0] = 5;
            Console.WriteLine("Детермінант після зміни mDet[0,0] = 5: " + mDet.CalcDeterminant());

            Console.WriteLine("\n=== АВТОТЕСТ ЗАВДАННЯ 2: MyTime ===");
            MyTime t1 = new MyTime(8, 5, 9);
            Console.WriteLine("Час t1: " + t1);
            Console.WriteLine("Зараз за розкладом: " + t1.WhatLesson());
            Console.WriteLine("Плюс 1 година і 20 хвилин: " + t1.AddOneHour().AddSeconds(20 * 60));
        }
    }
}
