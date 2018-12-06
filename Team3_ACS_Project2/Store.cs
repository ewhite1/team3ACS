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
using System.Resources;

namespace Team3_ACS_Project2{
    public partial class Store: Form{
        SqlConnection connection = new SqlConnection("Server=cstnt.tstc.edu; Database=VidGames_23531031; User Id=ITSE2353Group3; Password=3G310313");
        SqlDataAdapter adapter;
        DataTable table = new DataTable();
        CurrencyManager currencyManager;
        DataRowView dataRowView;

        public Store(){
            InitializeComponent();
            connection.Open();

            adapter = new SqlDataAdapter(new SqlCommand(
                "SELECT Video_Title AS Title, Video_PricePerUnit AS Price, Video_QuantityOnHand AS Stock, Video_Genre AS Genre, " +
                "ESRB_Rating AS Rating, SystemType AS System, Video_Description AS Description, Video_ImageID AS Image " +
                "From Video_Games_Table", connection));
            adapter.Fill(table);
            dgvGames.DataSource = table;

            currencyManager = (CurrencyManager)BindingContext[table];
            txtDescription.DataBindings.Add("Text", table, "Description");
            lblImage.DataBindings.Add("Text", table, "Image");
        }

        private void lblImage_TextChanged(object sender, EventArgs e){
            dataRowView = (DataRowView) currencyManager.Current;
            switch(dataRowView["Image"]){
                case "Wraframe.jpg": pbxVid.Image = Properties.Resources.Wraframe; break;
                case "dota2.jpg": pbxVid.Image = Properties.Resources.dota2; break;
                case "Ring_of_Elysium.jpg": pbxVid.Image = Properties.Resources.Ring_Of_Elysium; break;
                case "GTA5.jpg": pbxVid.Image = Properties.Resources.GTA5; break;
                case "PubG.jpg": pbxVid.Image = Properties.Resources.PUBG; break;
                case "Rainbow6.jpg": pbxVid.Image = Properties.Resources.Rainbow6; break;
                case "TF2.jpg": pbxVid.Image = Properties.Resources.TF2; break;
                case "CSGO.jpg": pbxVid.Image = Properties.Resources.CSGO; break;
                case "Football_Manager_2019.jpg": pbxVid.Image = Properties.Resources.Football_Manager_2019; break;
                case "ARK.jpg": pbxVid.Image = Properties.Resources.ARK; break;
            }

            cbxStock.SelectedIndex = -1;
            cbxStock.Items.Clear();
            for(int i = 0; i < (int)dataRowView["Stock"]+1; i++){
                cbxStock.Items.Add(i);
            }
        }

        private void dgvGames_CellContentClick(object sender, DataGridViewCellEventArgs e){}
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e){ Application.Exit(); }
        private void btnHome_Click(object sender, EventArgs e){ connection.Close(); Close(); }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e){ connection.Close(); Close(); }

        private void btnAdd_Click(object sender, EventArgs e){
            ShopingCart.cartItems.Add(new CartItem(dataRowView, (int)cbxStock.SelectedItem));
        }

        private void btnCart_Click(object sender, EventArgs e) {
            Cart frmCart = new Cart();
            frmCart.ShowDialog();

            if(frmCart.orderDone == true)
                Close();
        }
    }
}
