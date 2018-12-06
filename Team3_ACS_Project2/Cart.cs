using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Team3_ACS_Project2{
    public partial class Cart: Form{
        public bool orderDone = false;

        public Cart(){
            InitializeComponent();
            DrawCart();
        }

        private void btnClear_Click(object sender, EventArgs e) {
            ShopingCart.cartItems.Clear();
            DrawCart();
        }

        private void DrawCart(){
            float subTotal = 0.0f;

            lbxCart.Items.Clear();
            foreach (CartItem item in ShopingCart.cartItems) {
                float total = float.Parse(item.item["price"].ToString()) * item.quantity;
                lbxCart.Items.Add($"{total} -- { (string)item.item["Title"]}: " +
                    $"{float.Parse(item.item["Price"].ToString()).ToString("c")}  [x{item.quantity}]");

                subTotal += total;
            }

            txtSubTotal.Text = subTotal.ToString("c");
            txtTotalCost.Text = (subTotal + (subTotal * 0.0825)).ToString("c");
        }

        private void btnContinue_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnRemove_Click(object sender, EventArgs e) {
            ShopingCart.cartItems.RemoveAt(lbxCart.SelectedIndex);
            DrawCart();
        }

        private void btnCheckout_Click(object sender, EventArgs e){
            float grandTotal = 0.0f;

            string message = "Order: \n";
            foreach(CartItem item in ShopingCart.cartItems){
                float total = float.Parse(item.item["price"].ToString()) * item.quantity;
                message += $"{(string)item.item["Title"]}: " +
                    $"{float.Parse(item.item["Price"].ToString()).ToString("c")}  [x{item.quantity}]\n";
                grandTotal += total;
            }
            message += $"Total: {(grandTotal + (grandTotal * 0.0825)).ToString("c")}" +
                $"\nShipped to User, Name";
            MessageBox.Show(message);

            ShopingCart.cartItems.Clear();
            orderDone = true;
            Close();
        }
    }
}
