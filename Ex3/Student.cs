namespace Ex3
{
    public class Student
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string lastName;
        /// <summary>
        /// Имя
        /// </summary>
        public string firstName;
        /// <summary>
        /// Наименования ВУЗа
        /// </summary>
        public string university;
        /// <summary>
        /// Наименование факультета
        /// </summary>
        public string faculty;
        /// <summary>
        /// Номер Курса
        /// </summary>
        public int course;
        /// <summary>
        /// Наименование кафедры
        /// </summary>
        public string department;
        /// <summary>
        /// Номер группы
        /// </summary>
        public int group;
        /// <summary>
        /// Город прописки студента
        /// </summary>
        public string city;
        /// <summary>
        /// Возраст студента
        /// </summary>
        public int age;
        
        // Создаем конструктор
        public Student(string firstName, string lastName, string university,
        string faculty, string department, int age, int course, int group, string city)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.university = university;
            this.faculty = faculty;
            this.department = department;
            this.course = course;
            this.age = age;
            this.group = group;
            this.city = city;
        }
    }
}
