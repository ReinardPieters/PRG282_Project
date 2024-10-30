using PRG282Project.DataHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        
    }

}
