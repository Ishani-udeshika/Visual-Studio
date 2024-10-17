using FlowerShopManagmentSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShopManagmentSystem
{
    public partial class Customer : Form
    {
        private string Customer_ID;
        private string Customer_Name;
        private string Address;
        private string Contact_Number;

        DBConnection con;
            
        private BindingSource BindingSource = new BindingSource();
        private bool isEditing = false;

        public Customer()
        {
            InitializeComponent();

            try
            {
                con = DBConnection.getDBConnection();
                dGVCustomer.DataSource = BindingSource;
                refreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       public bool isDataValid()
        {
            Customer_ID = txtCustomerID.Text;
            Customer_Name = txtCustomerName.Text;
            Address = txtAddress.Text;
            Contact_Number = txtContactNo.Text;

            
            if(Customer_ID == string.Empty || Customer_Name == string.Empty || Address== string.Empty || Contact_Number == string.Empty)
            {
                MessageBox.Show("Please Fill the All Values");
                return false;
            }
            return true;
        }

       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (isEditing)
                {
                    int selectedRowIndex = dGVCustomer.SelectedCells[0].RowIndex;

                    dGVCustomer.Rows[selectedRowIndex].Cells["Customer_ID"].Value = txtCustomerID.Text;
                    dGVCustomer.Rows[selectedRowIndex].Cells["Customer_Name"].Value = txtCustomerName.Text;
                    dGVCustomer.Rows[selectedRowIndex].Cells["Address"].Value = txtAddress.Text;
                    dGVCustomer.Rows[selectedRowIndex].Cells["Contact_Number"].Value = txtContactNo.Text;

                    isEditing = false;
                }
                else if (isDataValid())
                {
                    string query3 = "INSERT INTO Customer_TBL(Customer_ID,Customer_Name,Address,Contact_Number) VALUES('{0}','{1}','{2}','{3}')";
                    query3 = string.Format(query3, Customer_ID, Customer_Name, Address, Contact_Number);
                    con.executeInsertUpdateQuery(query3);
                    refreshGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void refreshGrid()
        {
            string sqlForSelectingAll = "select * from Customer_TBL";
            BindingSource.DataSource = con.getDataTable(sqlForSelectingAll);
            dGVCustomer.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dGVCustomer.SelectedRows.Count > 0)
                {
                    string selectedCustomerID = dGVCustomer.SelectedRows[0].Cells["Customer_ID"].Value.ToString();
                    string query = $"DELETE FROM Customer_TBL WHERE Customer_ID = '{selectedCustomerID}'";
                    con.executeInsertUpdateQuery(query);
                    refreshGrid();
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dGVCustomer.SelectedCells.Count > 0)
                {
                    int selectedRowIndex = dGVCustomer.SelectedCells[0].RowIndex;
                    txtCustomerID.Text = dGVCustomer.Rows[selectedRowIndex].Cells["Customer_ID"].Value.ToString();
                    txtCustomerName.Text = dGVCustomer.Rows[selectedRowIndex].Cells["Customer_Name"].Value.ToString();
                    txtAddress.Text = dGVCustomer.Rows[selectedRowIndex].Cells["Address"].Value.ToString();
                    txtContactNo.Text = dGVCustomer.Rows[selectedRowIndex].Cells["Contact_Number"].Value.ToString();

                    isEditing = true;
                }
                else
                {
                    MessageBox.Show("Please select a row to edit.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
               txtAddress.Clear();
               txtContactNo.Clear();
               txtCustomerID.Clear();
               txtCustomerName.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }
    }
}
