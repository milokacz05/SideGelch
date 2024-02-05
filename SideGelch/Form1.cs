using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SideGelch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //SqlConnection conn = new SqlConnection(@"Data Source=LC21204XX\SQLEXPRESS;Initial Catalog=""SideQuest Logins"";Persist Security Info=True;User ID=sa;Password=sa2023");
        string connectionString = @"Data Source=LC21204XX\SQLEXPRESS;Initial Catalog=""SideQuest Logins"";Persist Security Info=True;User ID=sa;Password=sa2023";
        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void submitBTN_Click(object sender, EventArgs e)
        {


            string username, user_password;

            username = usernameBOX.Text;
            user_password = passwordBOX.Text;

            try
            {
                string query = "SELECT * FROM UserLogin WHERE Username = @username AND Password = @password";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", user_password);

                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        
                        username = usernameBOX.Text;
                        user_password = passwordBOX.Text;

                        this.Hide();
                        var Form2 = new Form2();
                        Form2.Closed += (s, args) => this.Close();
                        Form2.Show();
                    }
                    else
                    {
                        // Authentication failed
                        MessageBox.Show("Invalid login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        usernameBOX.Clear();
                        passwordBOX.Clear();
                        usernameBOX.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
            
        }
    }
}
