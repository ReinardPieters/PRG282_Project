using PRG282Project.BusinessProcess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PRG282Project.DataHandler
{
    internal class StudentsDatahandler
    {
        private readonly string filePath;

        public StudentsDatahandler(string filePath)
        {
            this.filePath = filePath;
        }
        public void WriteSummery(string total,string averageAge,string path)
        {
            string summery = $"Total Students: {total}. Average Age of students : {averageAge} {DateTime.Now}{Environment.NewLine}";
            File.AppendAllText(path, summery);

            MessageBox.Show("Added summary to summary.txt");   
        }
        public List<Student> LoadStudents() 
        {
            var students = new List<Student>();

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Student file not found.");
            }

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(',');

                if (parts.Length == 4 &&
                    int.TryParse(parts[0], out int studentID) &&
                    int.TryParse(parts[2], out int age))
                {
                    students.Add(new Student
                    {
                        StudentID = studentID,
                        Name = parts[1],
                        Age = age,
                        Course = parts[3]
                    });
                }
            }
            return students;
        }

        public void WriteStudents(List<Student> students)
        {
            //Check if the text file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Student file not found.");
            }

            List<string> details = new List<string>();
            //Adding all students in list using their ToString method
            foreach (Student student in students)
            {
                details.Add(student.ToString());
            }
            
            //Writing to the text file
            File.WriteAllLines(filePath, details);
        }

    }
}
