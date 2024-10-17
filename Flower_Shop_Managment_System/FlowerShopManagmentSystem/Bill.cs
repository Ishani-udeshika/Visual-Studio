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
    public partial class Bill : Form
    {
        Order Order1;
        public Bill(Order order)
        {
            InitializeComponent();
            this.Order1 = order;
        }

        private void Bill_Load(object sender, EventArgs e)
        {
            txtCutomerID.Text = Order1.txtCutomerID.Text;
            txtOrderID.Text= Order1.txtOrderID.Text;
            txtOrderDate.Text = Order1.txtOrderDate.Text;
            txtQuantity.Text = Order1.txtQuantity.Text;
            txtPrice.Text = Order1.txtTotalPrice.Text;
            txtFlowerID.Text = Order1.txtFlowerID.Text;

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                double totalPrice,Price;
                int quantity;

                quantity = Convert.ToInt32(txtQuantity.Text);
                Price = Convert.ToDouble(txtPrice.Text);

                totalPrice = Price*quantity;
                lblTotalPrice.Text = totalPrice.ToString();
                MessageBox.Show("Print Successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.Show();
            this.Hide();

        }
    }
}
