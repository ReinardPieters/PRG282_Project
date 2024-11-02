using PRG282Project.DataHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Windows.Forms;

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
        
    }

}
