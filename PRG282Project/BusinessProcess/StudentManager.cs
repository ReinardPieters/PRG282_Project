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

            public List<Student> GetStudents()
            {
                return dataHandler.LoadStudents();
            }

            public float getAverageMark()
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
                    }
                        else
                    {
                        dataHandler.WriteStudents(students);
                        MessageBox.Show("Student information succesfully updated");
                        
                    }
                }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not update student information");
                    }
            }
        public void DeleteStudent(int id)
        {
            try
            {
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


        
    }

}
