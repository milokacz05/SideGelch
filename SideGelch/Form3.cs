using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SideGelch
{
    public partial class Form3 : Form
    {

        private SqlConnection con;
        private string connectionString = @"Data Source=LC21204XX\SQLEXPRESS;Initial Catalog=SideQuest Logins;Persist Security Info=True;User ID=sa;Password=sa2023";


        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e) 
        {
            con.Close();
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            con.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password cannot be empty.");
                return;
            }

            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM UserLogin WHERE Username = @Username", con);
            checkCmd.Parameters.AddWithValue("@Username", username);
            int userCount = (int)checkCmd.ExecuteScalar();

            if (userCount > 0)
            {
                MessageBox.Show("Username already exists. Please choose a different one.");
                return;
            }

            SqlCommand insertCmd = new SqlCommand("INSERT INTO UserLogin (Username, Password) VALUES (@Username, @Password)", con);
            insertCmd.Parameters.AddWithValue("@Username", username);
            insertCmd.Parameters.AddWithValue("@Password", password);

            int rowsAffected = insertCmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Registration successful!");
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
            else
            {
                MessageBox.Show("Registration failed. Please try again.");
            }
        }
    }
}
