using FlowerShopManagmentSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShopManagmentSystem
{
    public partial class Stock : Form
    {
        private string Item_Number, Flower_Name, Quantity, Available_Quantity, Expire_Date;

        DBConnection con;

        private BindingSource BindingSource = new BindingSource();
        private bool isEditing = false; 
        public Stock()
        {
            InitializeComponent();
            try
            {
                con = DBConnection.getDBConnection();
                dGVStock.DataSource = BindingSource;
                refreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool isDataValid()
        {
            Item_Number = txtItemNo.Text;
            Flower_Name= txtFlowerName.Text;
            Quantity= txtQuantity.Text;
            Available_Quantity = txtAvQu.Text;
            Expire_Date = txtExpireD.Text;


            if (Item_Number == string.Empty || Flower_Name == string.Empty)
            {
                MessageBox.Show("Enter the Item Number and Flower Name");
                return false;
            }
            return true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAvQu.Clear();
            txtExpireD.Clear();
            txtFlowerName.Clear();
            txtItemNo.Clear();
            txtQuantity.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (isEditing)
                {
                    int selectedRowIndex = dGVStock.SelectedCells[0].RowIndex;

                    dGVStock.Rows[selectedRowIndex].Cells["Item_Number"].Value = txtItemNo.Text;
                    dGVStock.Rows[selectedRowIndex].Cells["Flower_Name"].Value = txtFlowerName.Text;
                    dGVStock.Rows[selectedRowIndex].Cells["Quantity"].Value = txtQuantity.Text;
                    dGVStock.Rows[selectedRowIndex].Cells["Available_Quantity"].Value = txtAvQu.Text;
                    dGVStock.Rows[selectedRowIndex].Cells["Expire_Date"].Value = txtExpireD.Text;

                    isEditing = false;
                }
                else if (isDataValid())
                {
                    string query3 = "INSERT INTO Stock_TBL(Item_Number, Flower_Name, Quantity, Available_Quantity, Expire_Date) VALUES('{0}','{1}','{2}','{3}','{4}')";
                    query3 = string.Format(query3, Item_Number, Flower_Name, Quantity, Available_Quantity, Expire_Date);
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
            string sqlForSelectingAll = "select * from Stock_TBL";
            BindingSource.DataSource = con.getDataTable(sqlForSelectingAll);
            dGVStock.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dGVStock.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dGVStock.SelectedCells[0].RowIndex;
                txtItemNo.Text = dGVStock.Rows[selectedRowIndex].Cells["Item_Number"].Value.ToString();
                txtFlowerName.Text = dGVStock.Rows[selectedRowIndex].Cells["Flower_Name"].Value.ToString();
                txtQuantity.Text = dGVStock.Rows[selectedRowIndex].Cells["Quantity"].Value.ToString();
                txtAvQu.Text = dGVStock.Rows[selectedRowIndex].Cells["Available_Quantity"].Value.ToString();
                txtExpireD.Text = dGVStock.Rows[selectedRowIndex].Cells["Expire_Date"].Value.ToString();
                isEditing = true;
            }
            else
            {
                MessageBox.Show("Please select a row to edit.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dGVStock.SelectedRows.Count > 0)
                {
                    string selectedCustomerID = dGVStock.SelectedRows[0].Cells["Item_Number"].Value.ToString();
                    string query = $"DELETE FROM Stock_TBL WHERE Item_Number = '{selectedCustomerID}'";
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
    }
    
}
