﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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
            string filePath = @"C:\Users\reina\OneDrive\Desktop\PRG282_Project\PRG282_Project\PRG282Project\students.txt";
            studentManager = new StudentManager(filePath);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogIn login = new frmLogIn();
            login.Show();
            this.Hide();
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
    }
}