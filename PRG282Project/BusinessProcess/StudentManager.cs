﻿using PRG282Project.DataHandler;
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

        //Searches students based on their ID
        public List<Student> SearchID(int ID)
        {
            List<Student> allStudents = GetStudents();

            List<Student> searchedStudents = allStudents.Where(Student => Student.StudentID == ID).ToList();

            return searchedStudents;

        }
        //Searches students based on their name
        public List<Student> SearchName(string name) 
        {
            List<Student> allStudents = GetStudents();

            List<Student> searchedStudents = allStudents
            .Where(student => student.Name.Contains(name))
            .ToList();

            return searchedStudents;
        }

        public List<Student> FilterCourse(string course)
        {
            List<Student> allStudents = GetStudents();

            List<Student> filteredStudents = new List<Student> ();

            if (course == "All")
            {
                return allStudents;
            }
            else
            {
                //Only getting the students with the specified course
                foreach (Student student in allStudents)
                {
                    if (student.Course == course)
                    {
                        filteredStudents.Add(student);
                    }
                }

                return filteredStudents;
            }
            
        }

        public List<Student> FilterAge(int index)
        {
            List<Student> allStudents = GetStudents();

            List<Student> filteredStudents = new List<Student>();

            //Only getting students with the specified age
            switch (index)
            {
                case 0:
                    return allStudents;
                    //Age less than 20
                case 1:
                    foreach (Student student in allStudents)
                    {
                        if (student.Age > 18 && student.Age < 21)
                        {
                            filteredStudents.Add(student);
                        }
                    }
                    return filteredStudents;
                    //Age between 21 and 25
                case 2:
                    foreach (Student student in allStudents)
                    {
                        if (student.Age > 20 && student.Age < 26)
                        {
                            filteredStudents.Add(student);
                        }
                    }
                    return filteredStudents;
                    //Age between 26 and 30
                case 3:
                    foreach (Student student in allStudents)
                    {
                        if (student.Age > 25 && student.Age < 31)
                        {
                            filteredStudents.Add(student);
                        }
                    }
                    return filteredStudents;
                default:
                    return allStudents;
            }
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

                //Input validation
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

                if (name == "" || course == "")
                {
                    MessageBox.Show("All fields must be filled in.");
                    return;
                }

                if (age < 19)
                {
                    MessageBox.Show("Please enter a valid age.");
                    return;
                }

                //Finding the student with the correct ID
                foreach (Student student in students)
                {
                    if (student.StudentID == id)
                     {
                        //Changing values to new values
                        student.Name = name;
                        student.Course = course;
                        student.Age = age;
                        found = true;
                        break;
                    }
                }
                //Displaying message if ID does not exist
                if (!found)
                {
                    MessageBox.Show("Student ID does not exist");
                //Writing the updated student information to the textfile
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
        public void DeleteStudent(int id) // method for when you want to delete a student
        {
            try
            {
                if(id <= 0) // input validation
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
        public void InsertStudent(int id, string name, int age, string course) //method for when inserting a new student
        {
            try
            {    //input validation for the textboxes
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
