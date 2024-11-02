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
using PRG282Project.Presentation;

namespace PRG282Project.Presentation
{
    public partial class frmMain : Form
    {
        private readonly StudentManager studentManager;
        public frmMain()
        {
            
            InitializeComponent();
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "students.txt");
            studentManager = new StudentManager(filePath);
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
            int ID = int.Parse(txtStudentID.Text);
            int age = int.Parse(txtAge.Text);
            string name = txtStudentName.Text;
            string course = txtCourse.Text;

            studentManager.UpdateStudent(ID, name, age, course);

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
        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            var result = studentManager.getAverageMarkAndCount();
            lblAverageAge.Text = $"Average Age: {result.average}";
            lblTotalStudents.Text = $"Total students: {result.count}";
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
    }
}