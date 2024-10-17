using FlowerShopManagmentSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShopManagmentSystem
{
    public partial class Flower : Form
    {
        private string Flower_ID;
        private string Flower_Name;
        private string Price;

       DBConnection con;

        private BindingSource BindingSource = new BindingSource();
        private bool isEditing = false;

        public Flower()
        {
            InitializeComponent();

            try
            {
                con = DBConnection.getDBConnection();
                dGVFlower.DataSource = BindingSource;
                refreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool isDataValid()
        {
            Flower_ID = txtFlowerID.Text;
            Flower_Name = txtFlowerName.Text;
            Price = txtPrice.Text;

            if (Flower_ID == string.Empty || Flower_Name == string.Empty)
            {
                MessageBox.Show("Both Code and Name of the categoey must be entered");
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
                    string query1 = "INSERT INTO  Flower_TBL(Flower_ID,Flower_Name,Price) VALUES('{0}','{1}','{2}')";
                    query1 = string.Format(query1, Flower_ID, Flower_Name, Price);
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
            string sqlForSelectingAll = "select * from Flower_TBL";
            BindingSource.DataSource = con.getDataTable(sqlForSelectingAll);
            dGVFlower.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application", "Conform", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dGVFlower.SelectedRows.Count > 0)
                {
                    string selectedFlowerID =dGVFlower.SelectedRows[0].Cells["Flower_ID"].Value.ToString();
                    string query = $"DELETE FROM Flower_TBL WHERE Flower_ID = '{selectedFlowerID}'";
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
            {
                try
                {
                    if (dGVFlower.SelectedCells.Count > 0)
                    {
                        int selectedRowIndex = dGVFlower.SelectedCells[0].RowIndex;
                        txtFlowerID.Text = dGVFlower.Rows[selectedRowIndex].Cells["Flower_ID"].Value.ToString();
                        txtFlowerName.Text = dGVFlower.Rows[selectedRowIndex].Cells["Flower_Name"].Value.ToString();
                        txtPrice.Text = dGVFlower.Rows[selectedRowIndex].Cells["Price"].Value.ToString();
                        

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
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFlowerID.Clear();
            txtFlowerName.Clear();
            txtPrice.Clear();
        }
    }
}
