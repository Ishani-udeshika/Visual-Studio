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

namespace FlowerShopManagmentSystem
{
    public partial class LoginForm : Form
    {
        private const string connectionString = "Data Source=Dell\\SQLEXPRESS01;Initial Catalog=UserDB;Integrated Security=True";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxPass.Checked == false)
            {
                txtPass.UseSystemPasswordChar =true;
            }
            else
            {
                txtPass.UseSystemPasswordChar = false;
            }
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            txtPass.Clear();
            txtUName.Clear();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Exit Application","Conform",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUName.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Enter the User Name and password");
            }
            else if (string.IsNullOrEmpty(txtUName.Text))
            {
                MessageBox.Show("Enter the User Name");
            }
            else if (string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Enter the Password");
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM User_TBL WHERE UserName = @username AND Password = @password", con))
                        {
                            cmd.Parameters.AddWithValue("@username", txtUName.Text);
                            cmd.Parameters.AddWithValue("@password", txtPass.Text);

                            object result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                Main main = new Main();
                                main.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("UserName or Password invalid");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
              
            }

        }
    }
}
