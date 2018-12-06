using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Team3_ACS_Project2{
    public struct CartItem{
        public DataRowView item;
        public int quantity;

        public CartItem(DataRowView item, int quantity){
            this.item = item;
            this.quantity = quantity;
        }
    }

    public class ShopingCart{
        public static List<CartItem> cartItems = new List<CartItem>();
        public static void Test(){ }
    }
}
