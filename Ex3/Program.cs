using System;
using System.Collections.Generic;
using System.IO;

namespace Ex1
{
    class Program
    {

        static int StudName(Student st1, Student st2)
        {
            return String.Compare(st1.firstName, st2.firstName); 
        }

        static int StudAge(Student st1, Student st2) 
        {
            return String.Compare(st1.age.ToString(), st2.age.ToString()); 
        }

        static int StudCourseAge(Student st1, Student st2) 
        {
            return String.Compare((100*st1.course + st1.age).ToString(), (100*st2.course + st2.age).ToString());
        }

        static void Main(string[] args)
        {
            int bakalavr = 0;
            int magistr = 0;
            int MaxCourses = 7;
            int[] courses = new int[MaxCourses];

            bool firstRead = true;
            List<Student> list = new List<Student>();
            // Создаем список студентов
            DateTime dt = DateTime.Now;
            StreamReader sr = new StreamReader("..\\..\\Students.csv");
            while (!sr.EndOfStream)
            {
                try
                {
                    if (firstRead)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            string[] temp = sr.ReadLine().Split(';');
                        }
                        firstRead = false;
                    }   
                    string[] s = sr.ReadLine().Split(';');
                    // Добавляем в список новый экземпляр класса Student
                    list.Add(new
                    Student(s[0], s[1], s[2], s[3], s[4], int.Parse(s[5]), int.Parse(s[6]), int.Parse(s[7])
                    , s[8]));
                    // Одновременно подсчитываем количество студентов на различных курсах
                    switch (int.Parse(s[6]))
                    {
                        case 1: courses[1]++; break;
                        case 2: courses[2]++; break;
                        case 3: courses[3]++; break;
                        case 4: courses[4]++; break;
                        case 5: courses[5]++; break;
                        case 6: courses[6]++; break;
                        default: break;
                    }


                    if (int.Parse(s[6]) < 5) bakalavr++; else magistr++;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Ошибка!ESC - прекратить выполнение программы");
                    // Выход из Main
                    if (Console.ReadKey().Key == ConsoleKey.Escape) return;
                }
            }
            sr.Close();            
            Console.WriteLine("Всего студентов : " + list.Count);
            Console.WriteLine("Бакалавров : {0}\n", bakalavr);
            Console.WriteLine("Магистров : {0}", magistr);
            
            for (int i = 1; i < MaxCourses; i++)
            {
                Console.WriteLine("Количество стдентов на {0} курсе = {1}", i, courses[i]);
            }

            //list.Sort(new Comparison<Student>(StudAge));
            //foreach (var v in list) Console.WriteLine(v.firstName + " " + v.age + " лет ");

            list.Sort(new Comparison<Student>(StudCourseAge));
            foreach (var v in list) Console.WriteLine(v.firstName + " " + v.course + " Курс " + v.age + " Лет");

            Console.WriteLine(DateTime.Now - dt);
            Console.ReadKey();
        }
    }
}
