using PRG282Project.DataHandler;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.IO;

namespace PRG282Project.BusinessProcess
{
    internal class StudentManager
    {

        private StudentsDatahandler dataHandler;
        public StudentManager(string filePath)
        {
            dataHandler = new StudentsDatahandler(filePath);
        }
        public List<Student> SearchID(int ID)
        {
            List<Student> allStudents = GetStudents();

            List<Student> searchedStudents = allStudents.Where(Student => Student.StudentID == ID).ToList();

            return searchedStudents;

        }
        public List<Student> SearchName(string name) 
        {
            List<Student> allStudents = GetStudents();

            List<Student> searchedStudents = allStudents
            .Where(student => student.Name.Contains(name))
            .ToList();

            return searchedStudents;
        }

        public List<Student> GetStudents()
        {
            return dataHandler.LoadStudents();
        }

        public float getAverageAge()
        {
            int totalAge = 0;
            int count = 0;
            foreach (Student student in GetStudents())
                { 
                    count++;
                    totalAge = totalAge + student.Age;
                }

            float averageAge = count > 0 ? (float)Math.Round((float)totalAge / count, 2) : 0;
            return (averageAge);

        }
        public int getTotalStudents() 
        {
            return GetStudents().Count; 
        }
        public void UpdateStudent(int id, string name, int age, string course)
        {
            try
            {
                List<Student> students = GetStudents();
                bool found = false;
                bool nameOnlyLetters = name.All(char.IsLetter);
                bool courseOnlyLetters = course.All(char.IsLetter);

                if (id < 1)
                {
                    MessageBox.Show("ID cannot be 0 or negative. Please try again.");
                    return;
                }

                if (!nameOnlyLetters || !courseOnlyLetters)
                {
                    MessageBox.Show("Name or course cannot contain special characters or numbers.");
                    return;

                }

                foreach (Student student in students)
                {
                    if (student.StudentID == id)
                     {
                        student.Name = name;
                        student.Course = course;
                        student.Age = age;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    MessageBox.Show("Student ID does not exist");
                } else
                {
                    dataHandler.WriteStudents(students);
                    MessageBox.Show("Student information succesfully updated");
                        
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void DeleteStudent(int id)
        {
            try
            {
                if(id <= 0)
                {
                    MessageBox.Show("Please enter a valid and positive Student ID!");
                    return;
                }
                List<Student> students = GetStudents();
                Student deleteStudent = students.FirstOrDefault(student => student.StudentID == id);

                if(deleteStudent != null)
                {
                    
                    students.Remove(deleteStudent);
                    dataHandler.WriteStudents(students);
                    MessageBox.Show("Student was deleted succesfully!");
                }
                else
                {
                    MessageBox.Show("Student with that ID not found!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error could not delete that student!");
            }
        }
        public void InsertStudent(int id, string name, int age, string course)
        {
            try
            {
                if(id <= 0)
                {
                    MessageBox.Show("Please enter a valid, positive Student ID!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Please enter a name that is valid!");
                    return;
                }
                if (age <= 0)
                {
                    MessageBox.Show("Please enter a age that is valid!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(course))
                {
                    MessageBox.Show("Please enter a valid course!");
                    return;
                }
                List<Student> students = GetStudents();
                if(students.Any(student => student.StudentID== id))
                {
                    MessageBox.Show("The Student with that ID already exist!");
                    return;
                }
                Student newstudent = new Student
                {
                    StudentID = id,
                    Name = name,
                    Age = age,
                    Course = course
                };
                students.Add(newstudent);
                dataHandler.WriteStudents(students);
                MessageBox.Show("New Student was added successfully!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error, could not add the student!");
            }
        }
    }
}
