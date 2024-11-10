using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRG282Project.BusinessProcess;
using PRG282Project.DataHandler;
using PRG282Project.Presentation;

namespace PRG282Project.Presentation
{
    public partial class frmMain : Form
    {
        public string CurrentUser { get;private  set; }
        private readonly StudentManager studentManager;
        public frmMain(string user)
        {
            
            InitializeComponent();
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "students.txt");
            
            studentManager = new StudentManager(filePath);
            CurrentUser = user;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogIn login = new frmLogIn();
            login.Show();
            Hide();
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            try
            {
                List<Student> students = studentManager.GetStudents();
                dgvStudents.DataSource = students;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading students: {ex.Message}");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string name = txtStudentName.Text;
            string course = txtCourse.Text;

            try
            {
                int ID = int.Parse(txtStudentID.Text);
                int age = int.Parse(txtAge.Text);


                studentManager.UpdateStudent(ID, name, age, course);

                Log log = new Log(CurrentUser, $"Updated student ID: {ID} @ ");

                List<Student> students = studentManager.GetStudents();
                dgvStudents.DataSource = students;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating: {ex.Message}");
            }

        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            lblCurrentUser.Text += $" {CurrentUser}";
            lblDetail.Text = "Please select a category to seach first";
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            string summaryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "summary.txt");
            lblAverageAge.Text = $"Average Age: {studentManager.getAverageAge()}";
            lblTotalStudents.Text = $"Total students: {studentManager.getTotalStudents()}";
            StudentsDatahandler studentsDatahandler = new StudentsDatahandler(summaryPath);

            studentsDatahandler.WriteSummery(Convert.ToString(studentManager.getTotalStudents()), Convert.ToString(studentManager.getAverageAge()),summaryPath);

        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStudents.Rows[e.RowIndex];
                txtStudentID.Text = row.Cells[0].Value.ToString();
                txtStudentName.Text = row.Cells[1].Value.ToString();
                txtAge.Text = row.Cells[2].Value.ToString();
                txtCourse.Text = row.Cells[3].Value.ToString();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = int.Parse(txtStudentID.Text);
                
                studentManager.DeleteStudent(ID);

                Log log = new Log(CurrentUser, $"Deleted student ID: {ID} @ ");

                List<Student> students = studentManager.GetStudents();
                dgvStudents.DataSource = students;
               
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error deleting student:{ex.Message}");
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = int.Parse(txtStudentID.Text);
                string Name = txtStudentName.Text;
                int age = int.Parse(txtAge.Text);
                string course = txtCourse.Text;

                Log log = new Log(CurrentUser, $"Inserted student ID: {ID} @ ");
                studentManager.InsertStudent(ID, Name, age, course);
                List<Student> students = studentManager.GetStudents();
                dgvStudents.DataSource = students;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error inserting student:{ex.Message}");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int category = cbCategory.SelectedIndex;
                string detail = txtDetail.Text;

                if (category == -1)
                {
                    MessageBox.Show("Please select a category to search");
                }
                else if (category == 0)
                {
                    List<Student> searchID = studentManager.SearchID(int.Parse(detail));
                    if (searchID.Count > 0)
                    {
                        dgvStudents.DataSource = searchID;
                    }
                    else
                    {
                        MessageBox.Show("No students found with the specified ID.");
                    }
                   
                }
                else if (category == 1)
                {
                    List<Student> searchName = studentManager.SearchName(detail);
                    if (searchName.Count > 0)
                    {
                        dgvStudents.DataSource = searchName;
                    }
                    else
                    {
                        MessageBox.Show("No students found with the specified Name.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategory.SelectedIndex == 0)
            {
                lblDetail.Text = "Enter ID of student: ";
            } else if (cbCategory.SelectedIndex == 1) 
            {
                lblDetail.Text = "Please enter students name: ";
            }
            else
            {
                lblDetail.Text = "Please select a category to seach first";
            }
        }
    }
}