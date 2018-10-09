using System;
using System.Collections.Generic;
using System.IO;
/*
 Модифицировать программу нахождения минимума функции так, чтобы можно было
передавать функцию в виде делегата.
а) Сделать меню с различными функциями и представить пользователю выбор, для какой
функции и на каком отрезке находить минимум.
б) Использовать массив (или список) делегатов, в котором хранятся различные функции.
в) *Переделать функцию Load, чтобы она возвращала массив считанных значений. Пусть она
возвращает минимум через параметр.

Выполнил Волков Кирилл
*/
namespace Ex2
{
    public class Ex2Fun
    {
        //Список функций
        public static double F(double x)
        {
            return x * x - 50 * x + 10;
        }
        public static double Sin(double x)
        {
            return Math.Sin(x);
        }
        public static double Cos(double x)
        {
            return Math.Cos(x);
        }
        public static double Tan(double x)
        {
            return Math.Tan(x);
        }
        public static double Exp(double x)
        {
            return Math.Exp(x);
        }
        public static double Pow2(double x)
        {
            return x*x;
        }
        public static double Pow3(double x)
        {
            return Math.Pow(x,3);
        }
        public static double PowDev2(double x)
        {
            return 1 / x;
        }
        public static double XSin(double x)
        {
            return Math.Sin(x)*x;
        }
        public static double XCos(double x)
        {
            return Math.Cos(x) * x;
        }
        public static double ExpCos(double x)
        {
            return Math.Cos(x) * Math.Exp(x);
        }
        public static double ExpSin(double x)
        {
            return Math.Sin(x) * Math.Exp(x);
        }

        public delegate double Function(double x);


//        public List<Function> DelegaList1 { get => DelegaList; set => DelegaList = value; }

        /// <summary>
        /// Процедура инициализация коллекции делегатов с функциями
        /// </summary>
        /// <param name="DelegaList">Коллекция делегатов которую нужно инициализировать</param>
        public static void InitDelegate(out List<Function> DelegaList)
        {
            DelegaList = new List<Function>
            {
                F,
                Sin,
                Cos,
                Tan,
                Exp,
                Pow2,
                Pow3,
                PowDev2,
                XSin,
                XCos,
                ExpCos,
                ExpSin
            };
        }

        /// <summary>
        /// Процедура сохранения значений функции выбранной из списка функций
        /// </summary>
        /// <param name="fileName">Имя файла куда необходима записать данные</param>
        /// <param name="F">Функция по которой будет производится расчет</param>
        /// <param name="a">Нижняя граница интервалла</param>
        /// <param name="b">Верхняя граница интервалла</param>
        /// <param name="h">Шаг интервалла</param>
        public static void SaveFunc(string fileName, Function F, double a, double b, double h)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create,
            FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            double x = a;
            while (x <= b)
            {
                bw.Write(F(x));
                x += h; // x=x+h;
            }
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// Функция загрузки из файла минимального значения 
        /// </summary>
        /// <param name="fileName">Имя файла где хранятся данные функции</param>
        /// <param name="min">Передаваемый параментр, которому будет присвоено минимальное значение из массива данных в файле</param>
        /// <returns></returns>
        public static double[] Load(string fileName, ref double min)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            min = double.MaxValue;
            double[] d = new double[fs.Length / sizeof(double)];
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d[i] = bw.ReadDouble();
                if (d[i] < min) min = d[i];
            }
            bw.Close();
            fs.Close();
            return d;
        }


        public static void Main()
        {
            InitDelegate(out List<Function> DelegaList);
            string Filename = "data.bin";
            double Minimum = 0;
            double choise = 0;
            double min = 0, max = 0;
            double step = 0;
            Console.WriteLine("Выбирете функцию и введите ее порядковый номер\n" +
            "1) F = x * x - 50 * x + 10\n" +
            "2) F = Sin(x)\n" +
            "3) F = Cos(x)\n" +
            "4) F = Tan(x)\n" +
            "5) F = Exp(x)\n" +
            "6) F = x * х\n" +
            "7) F = x ^ 3\n" +
            "8) F = 1 / x\n" +
            "9) F = x * Sin(x)\n" +
            "10) F = x * Cos(x)\n" +
            "11) F = Exp(x) * Cos(x)\n" +
            "12) F = Exp(x) * Sin(x)\n");            
            InputCheck(ref choise);

            Console.WriteLine("Задайте интервал");
            Console.Write("\nВведите минимальное значение ");
            InputCheck(ref min);
            
            Console.Write("\nВведите максимальное значение ");
            InputCheck(ref max);

            Console.Write("\nВведите шаг ");
            InputCheck(ref step);

            SaveFunc(Filename, DelegaList[Convert.ToInt32(choise-1)], min, max, step);

            double[] Array = Load(Filename, ref Minimum);

            for (int i = 0; i < Array.Length; i++)
            {
                Console.Write("  {0,8:0.000}", Array[i]);
                if (i != 0 && i % 5 == 0)
                {
                    Console.WriteLine("\n");
                }
            }

            Console.WriteLine("\nМинимум равен = " + Minimum);
            Console.ReadKey();
        }

        /// <summary>
        /// метод проверки правильности вводимых данных для типа double
        /// </summary>
        /// <param name="min"></param>
        public static void InputCheck(ref double min)
        {
            string temp;
            while (true)
            {
                try
                {
                    temp = Console.ReadLine();
                    if ("q" == Convert.ToString(temp))
                    {
                        Console.WriteLine("До свидания! Всего доброго!");
                        Console.Read();
                        break;
                    }
                    else
                        min = Convert.ToInt32(temp);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Неверный формат ввода. Введите число соответствующее номеру задачи");
                }
            }          
        }
    }
}


