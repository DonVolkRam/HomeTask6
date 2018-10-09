using System;
/*
 Изменить программу вывода таблицы функции так, чтобы можно было передавать функции
типа double (double, double). Продемонстрировать работу на функции с функцией a*x^2 и
функцией a*sin(x).
Выполнил Волков Кирилл
*/

namespace Ex1
{
    public delegate double Fun(double a, double x);
    public class Ex1Table
    {
        /// <summary>
        /// Вывод значений функции
        /// </summary>
        /// <param name="F">Выражение вынкции</param>
        /// <param name="a">Параметр</param>
        /// <param name="x">Переменная</param>
        /// <param name="b">Максимальное значение переменной</param>
        public static void Table(Fun F, double a, double x, double b)
        {            
            Console.WriteLine("----- X ----- Y -----");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", x, F(a, x));
                x += 1;
            }
            Console.WriteLine("---------------------");
        }

        // Создаем метод для передачи его в качестве параметра в Table
        public static double FuncAx2(double a, double x)
        {
            return a * x * x;
        }

        public static void Main()
        {
            Console.WriteLine("Таблица функции a*x^2:");
            Table(FuncAx2, 5, -2, 2);
            
            Console.WriteLine("Таблица функции a*sin(x):");
            Table(delegate (double a, double x) { return a * Math.Sin(x); }, 6, 0, 3);
            Console.Read();
        }
    }
}
