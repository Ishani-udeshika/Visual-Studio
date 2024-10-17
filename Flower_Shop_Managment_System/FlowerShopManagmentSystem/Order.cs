using FlowerShopManagmentSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShopManagmentSystem
{
    public partial class Order : Form
    {
        private string Order_ID;
        private string Customer_ID;
        private string Order_Date;
        private string Total_Amount;

        DBConnection con;

        private BindingSource BindingSource = new BindingSource();
        private bool isEditing = false; 

        public Order()
        {
            InitializeComponent();

            try
            {
                con = DBConnection.getDBConnection();
                dGVFOrder.DataSource = BindingSource;
                refreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool isDataValid()
        {
            Order_ID = txtOrderID.Text;
            Customer_ID = txtCutomerID.Text;
            Order_Date = txtOrderDate.Text;
            Total_Amount = txtTotalPrice.Text;

            if (Order_ID == string.Empty || Customer_ID == string.Empty)
            {
                MessageBox.Show("Order_ID and Customer_ID must be entered");
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (isDataValid())
                {
                    string query1 = "INSERT INTO  Order_TBL(Order_ID,Customer_ID,Order_Date,Total_Amount) VALUES('{0}','{1}','{2}','{3}')";
                    query1 = string.Format(query1, Order_ID, Customer_ID, Order_Date, Total_Amount);
                    con.executeInsertUpdateQuery(query1);
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
            string sqlForSelectingAll = "select * from Order_TBL";
            BindingSource.DataSource = con.getDataTable(sqlForSelectingAll);
            dGVFOrder.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dGVFOrder.SelectedRows.Count > 0)
                {
                    string selectedOrderID = dGVFOrder.SelectedRows[0].Cells["Order_ID"].Value.ToString();
                    string query = $"DELETE FROM Order_TBL WHERE Order_ID = '{selectedOrderID}'";
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

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application", "Conform", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dGVFOrder.SelectedCells.Count > 0)
                {
                    int selectedRowIndex =dGVFOrder.SelectedCells[0].RowIndex;
                    txtOrderID.Text = dGVFOrder.Rows[selectedRowIndex].Cells["Order_ID"].Value.ToString();
                    txtCutomerID.Text = dGVFOrder.Rows[selectedRowIndex].Cells["Customer_ID"].Value.ToString();
                    txtOrderDate.Text = dGVFOrder.Rows[selectedRowIndex].Cells["Order_Date"].Value.ToString();
                    txtTotalPrice.Text = dGVFOrder.Rows[selectedRowIndex].Cells["Total_Amount"].Value.ToString();

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

        private void btnBill_Click(object sender, EventArgs e)
        {
            Bill Bill = new Bill(this);
            Bill.ShowDialog();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCutomerID.Clear();
            txtFlowerID.Clear();
            txtOrderDate.Clear();
            txtOrderID.Clear();
            txtQuantity.Clear();
            txtTotalPrice.Clear();
        }
    }
}
