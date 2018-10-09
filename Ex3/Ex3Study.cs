using System;
using System.Collections.Generic;
using System.IO;
/*
Переделать программу Пример использования коллекций для решения следующих задач:
а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся (*частотный
массив);
в) отсортировать список по возрасту студента;
г) *отсортировать список по курсу и возрасту студента;
д) **разработать единый метод подсчета количества студентов по различным параметрам
выбора с помощью делегата и методов предикатов.

Выполнил Волков Кирилл
*/
namespace Ex3
{
    public class Ex3Study
    {
        /// <summary>
        /// метод сортировки по имени студентов
        /// </summary>
        /// <param name="st1"></param>
        /// <param name="st2"></param>
        /// <returns>Сравнивает два указанных объекта System.String и возвращает целое число, которое
        ///     показывает их относительное положение в порядке сортировки.</returns>
        static int StudName(Student st1, Student st2)
        {
            return String.Compare(st1.firstName, st2.firstName); 
        }

        /// <summary>
        /// метод сортировки по возрасту студентов
        /// </summary>
        /// <param name="st1"></param>
        /// <param name="st2"></param>
        /// <returns>Сравнивает два указанных объекта System.String и возвращает целое число, которое
        ///     показывает их относительное положение в порядке сортировки.</returns>
        static int StudAge(Student st1, Student st2) 
        {
            return String.Compare(st1.age.ToString(), st2.age.ToString()); 
        }

        /// <summary>
        /// метод сортировки по курсу и возрасту студентов
        /// </summary>
        /// <param name="st1"></param>
        /// <param name="st2"></param>
        /// <returns>Сравнивает два указанных объекта System.String и возвращает целое число, которое
        ///     показывает их относительное положение в порядке сортировки.</returns>
        static int StudCourseAge(Student st1, Student st2) 
        {
            return String.Compare((100*st1.course + st1.age).ToString(), (100*st2.course + st2.age).ToString());
        }

        public static void Main()
        {
            int bakalavr = 0;
            int magistr = 0;
            int MaxCourses = 7;
            int[] courses = new int[MaxCourses];
            int[] courses18_20 = new int[MaxCourses];

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
                    // и делаем частотный массив для возрастов 18-20 
                    switch (int.Parse(s[6]))
                    {
                        case 1: courses[1]++; if (int.Parse(s[5]) >= 18 && int.Parse(s[5]) <= 20) courses18_20[1]++; break;
                        case 2: courses[2]++; if (int.Parse(s[5]) >= 18 && int.Parse(s[5]) <= 20) courses18_20[2]++; break;
                        case 3: courses[3]++; if (int.Parse(s[5]) >= 18 && int.Parse(s[5]) <= 20) courses18_20[3]++; break;
                        case 4: courses[4]++; if (int.Parse(s[5]) >= 18 && int.Parse(s[5]) <= 20) courses18_20[4]++; break;
                        case 5: courses[5]++; if (int.Parse(s[5]) >= 18 && int.Parse(s[5]) <= 20) courses18_20[5]++; break;
                        case 6: courses[6]++; if (int.Parse(s[5]) >= 18 && int.Parse(s[5]) <= 20) courses18_20[6]++; break;
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
                Console.Write("Количество стдентов на {0} курсе = {1} ", i, courses[i]);
                Console.WriteLine("из них в возрасте 18 - 20 лет = {0}", courses18_20[i]);
            }

            //list.Sort(new Comparison<Student>(StudName));
            //foreach (var v in list) Console.WriteLine(v.firstName);

            //list.Sort(new Comparison<Student>(StudAge));
            //foreach (var v in list) Console.WriteLine(v.firstName + " " + v.age + " лет ");

            list.Sort(new Comparison<Student>(StudCourseAge));
            #region формирование метода с предикатами для подсчета количества студентов по параметрам
            //эти параметры можно получать от ввода с клавиатуры
            int AgeMin = 18, AgeMax = 20, Course = 1;
            string department = "Военная";

            //Определение предикатов
            Predicate<Student> isOlder = x => x.age >= AgeMin;
            Predicate<Student> isYounger = x => x.age <= AgeMax;
            Predicate<Student> isCourse = x => x.course == Course;
            Predicate<Student> isCathedral = x => String.Equals(x.department, department);

            //формирование списка параметров поиска, можно использовать add();    
            List<Predicate<Student>> PredyList = new List<Predicate<Student>>
            {
                isOlder,
                isYounger,
                isCathedral
            };          
            
            Console.WriteLine("Cстудентов по параметрам = " + CountStudents(list, PredyList));
            #endregion формирование метода с предикатами для подсчета количества студентов по параметрам

            //foreach (var v in list) Console.WriteLine(v.firstName + " " + v.course + " Курс " + v.age + " Лет");
            //Console.WriteLine(v.firstName + " " + v.course + " Курс " + v.age + " Лет");
            Console.WriteLine("\n");
            Console.WriteLine(DateTime.Now - dt);
            Console.ReadKey();
        }

        public delegate int DeleCount(List<Student> LS, List<Predicate<Student>> LPS);
        /// <summary>
        /// Метод посчета количества студентов по заданным параметрам
        /// </summary>
        /// <param name="StudList">Список студентов</param>
        /// <param name="PredyList">Список параметров</param>
        /// <returns>Возвращает количество студентов удовлетворяющим условиям параметров</returns>
        public static int CountStudents(List<Student>StudList, List<Predicate<Student>> PredyList)
        {
            int countStudents = 0;
            bool Bcheck = false;

            foreach (var v in StudList)
            {
                Bcheck = true;
                for (int i = 0; i < PredyList.Count; i++)
                    if (!PredyList[i](v))
                        Bcheck = false;
                if (Bcheck)
                    countStudents++;             
            }

            return countStudents;
        }
    }
}
