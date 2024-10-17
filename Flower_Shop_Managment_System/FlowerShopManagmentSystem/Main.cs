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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application", "Conform", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void lblProduct_Click(object sender, EventArgs e)
        {
            Flower Flower = new Flower();
            Flower.Show();
            this.Hide();
        }

        private void lblOrder_Click(object sender, EventArgs e)
        { 
            Order Order = new Order();
            Order.Show();
            this.Hide();
        }

        private void lblCustomer_Click(object sender, EventArgs e)
        {
            Customer Customer = new Customer();
            Customer.Show();
            this.Hide();
        }

        private void lblStock_Click(object sender, EventArgs e)
        {
            Stock Stock = new Stock();
            Stock.Show();
            this.Hide();
        }
    }
}
