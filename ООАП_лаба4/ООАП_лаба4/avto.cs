using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ООАП_лаба4.Form1;

namespace ООАП_лаба4
{
    public partial class avto : Form
    {
        public GuestUser user;
        private clother item1;
        private clother item2;
        private clother item3;

        public avto(GuestUser user)
        {

            InitializeComponent();
            this.user = user;
            Start();
        }

        public class clother
        {
            public string name { get; set; }
            public int price { get; set; }
            public clother(string name, int price)
            {
                this.name = name;
                this.price = price; 

            }
            public int Price
            {
                get { return price; }
            }
        }




        void Start()
        {
            item1 = new clother("jeans", 20);
            jeans_label.Text = "jeans";
            textBox_jeans.Text = "20";

            item2 = new clother("sweater", 40);
            sweater_label.Text = "sweater";
            textBox_sweater.Text = "40";

            item3 = new clother("shirt", 30);
            shirt_label.Text = "sweater";
            textBox_shirt.Text = "30";

            name_label.Text = user.UserName;
            email_label.Text = user.email;
            
            if (user.status == false ) 
            {
                button_add_jeans.Visible = false;
                button_add_sweater.Visible = false;
                button_add_shirt.Visible = false;
            }


        }

        private void button_add_jeans_Click(object sender, EventArgs e)
        {
            user.order(item1.price);
        }

        private void button_add_sweater_Click(object sender, EventArgs e)
        {
            user.order(item2.price);
        }

        private void button_add_shirt_Click(object sender, EventArgs e)
        {
            user.order(item3.price);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            final_price.Text = "" + user.OrderPrice;
        }
    }
}
