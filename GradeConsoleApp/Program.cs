using System;
using System.Collections.Generic;
using System.IO;

namespace GradeConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Teacher> teacherList = new List<Teacher>();
            List<string> studentsList = new List<string>();

            GradesProgram(teacherList, studentsList);
        }

        static void GradesProgram(List<Teacher> listOfTeachers, List<string> listOfStudents)
        {
            try
            {
                Console.Write("Enter Teacher name : ");
                string teacherName = Console.ReadLine();

                Console.WriteLine($"Hello {teacherName}\n1. Add Student\n2. Get Avrage\n3. Get Second Student\n4. Search By ID\n");
                int teacherPick = int.Parse(Console.ReadLine());

                switch (teacherPick)
                {
                    case 1:
                        AddStudent(teacherName, listOfTeachers, listOfStudents);
                        break;
                    case 2:
                        GetAvrage(teacherName);
                        break;
                    case 3:
                        GetSecondStudent(teacherName);
                        break;
                    case 4:
                        SearchById(teacherName);
                        break;

                    default:
                        Console.WriteLine($"Thank you {teacherName} and have a good day");
                        break;
                }
            }
            catch(FormatException e)
            {
                Console.WriteLine($"{e.Message}, Try Again");
                GradesProgram(listOfTeachers, listOfStudents);
            }
            catch
            {
                Console.WriteLine("Wrong input field. try again.");
                GradesProgram(listOfTeachers, listOfStudents);
            }

        }

        static void AddStudent(string teacherName, List<Teacher> listOfTeachers, List<string> listOfStudents)
        {
            int counter = 0;
            try
            {
                Console.Write($"{teacherName}, How many student ? : ");
                int studentCount = int.Parse(Console.ReadLine());

                for (int i = 0; i < studentCount; i++)
                {
                
                    Console.Write("Student Name : ");
                    string studentName = Console.ReadLine();
                    Console.Write("Student ID : ");
                    int studentId = int.Parse(Console.ReadLine());
                    List<int> grades = new List<int>();
                    for(int j = 0; j < 3; j++)
                    {
                        Console.Write("Enter Grade : ");
                        int grade = int.Parse(Console.ReadLine());
                        grades.Add(grade);
                    }

                    Teacher newTeacher = new Teacher(teacherName, studentName, studentId, grades);
                    listOfTeachers.Add(newTeacher);

                    SaveToFile(counter, teacherName, studentName, studentId, grades);
                    counter += 1;

                    AddToStudentList(listOfStudents, newTeacher);
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"{e.Message}, Try Again");
                AddStudent(teacherName, listOfTeachers, listOfStudents);
            }
            catch (StackOverflowException e)
            {
                Console.WriteLine($"{e.Message}, Try Again");
                AddStudent(teacherName, listOfTeachers, listOfStudents);
            }
            catch(IndexOutOfRangeException e)
            {
                Console.WriteLine($"{e.Message}, Try Again");
                AddStudent(teacherName, listOfTeachers, listOfStudents);
            }
            catch
            {
                Console.WriteLine("Wrong input field. try again.");
                AddStudent(teacherName, listOfTeachers, listOfStudents);
            }
            
        }

        static void SaveToFile(int counter,string teacherName, string studentName, int studentId, List<int> grades)
        {
            FileStream fs = new FileStream($@"C:\Users\Jacob\Desktop\GradesFiles\{teacherName}.txt", FileMode.Append);
            using(StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine($"ID:{counter}. Student Name : {studentName} Student # : {studentId} Grades : a.{grades[0]} b. {grades[1]} c. {grades[2]},");
            }
        }

        static void AddToStudentList(List<string> listOfStudents, Teacher obj)
        {
            listOfStudents.Add($"Student Name : {obj.studentName}, Student ID : {obj.studentId}, Grades : a.{obj.grades[0]} b. {obj.grades[1]} c. {obj.grades[2]}");
        }

        static void GetAvrage(string teacherName)
        {
            try
            {
                FileStream fs = new FileStream($@"C:\Users\Jacob\Desktop\GradesFiles\{teacherName}.txt", FileMode.Open);
                using (StreamReader sr = new StreamReader(fs))
                {
                    string text = sr.ReadLine();
                    int indexOfName = text.IndexOf("Name :");
                    int indexOfGrade1 = text.IndexOf("a.");
                    int indexOfGrade2 = text.IndexOf("b.");
                    int indexOfGrade3 = text.IndexOf("c.");

                    int grade1 = int.Parse(text.Substring(indexOfGrade1 + 2, 2));
                    int grade2 = int.Parse(text.Substring(indexOfGrade2 + 3, 2));
                    int grade3 = int.Parse(text.Substring(indexOfGrade3 + 3, 2));

                    int avg = (grade1 + grade2 + grade3) / 3;

                    Console.WriteLine($"Student Name : {text.Substring(indexOfName + 7, 5)}, Grades Avrege : {avg}");
                }
            }
            catch(InvalidCastException e)
            {
                Console.WriteLine($"{e.Message}, Try Again");
                GetAvrage(teacherName);
            }
            catch
            {
                Console.WriteLine("Unknown Error, Try Again");
                GetAvrage(teacherName);
            }
        }

        static void GetSecondStudent(string teacherName)
        {
            FileStream fs = new FileStream($@"C:\Users\Jacob\Desktop\GradesFiles\{teacherName}.txt", FileMode.Open);
            using (StreamReader sr = new StreamReader(fs))
            {
                string text = sr.ReadToEnd();
                int indexOf2ndStu = text.IndexOf("ID:1");

                Console.WriteLine(text.Substring(indexOf2ndStu));
            }
        }

        static void SearchById(string teacherName)
        {
            try
            {
                Console.Write("Enter Student ID : ");
                int teacherPick = int.Parse(Console.ReadLine());
                FileStream fs = new FileStream($@"C:\Users\Jacob\Desktop\GradesFiles\{teacherName}.txt", FileMode.Open);
                using (StreamReader sr = new StreamReader(fs))
                {
                    string text = sr.ReadToEnd();

                    int indexOfStu = text.IndexOf($"ID:{teacherPick}");
                    int endIndex = text.IndexOf(",");

                    Console.WriteLine(text.Substring(indexOfStu, endIndex));
                }
            }
            catch(FormatException e)
            {
                Console.WriteLine($"{e.Message}, Try Again");
                SearchById(teacherName);
            }
            catch
            {
                Console.WriteLine("Wrong input field. try again.");
                SearchById(teacherName);
            }
            
        }
    }
}
