using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ООАП_лаба4.avto;
using static ООАП_лаба4.Form1;

namespace ООАП_лаба4
{
    public partial class Form1 : Form
    {
        DB db;

        DataTable table;
        public Form1()
        {

            InitializeComponent();
        }



        public class GuestUser  // Объявление класса User
        {
            public string UserName { get; set; }
            public string email { get; set; }
            public bool status { get; set; }
            public int OrderPrice { get; set; }

            public GuestUser(DataTable table)
            {
                if (table != null && table.Rows.Count > 0)
                {
                    this.UserName = table.Rows[0].Field<string>("name");
                    this.email = table.Rows[0].Field<string>("email");
                }
                status = false;

                OrderPrice = 0;
            }
            public virtual void order(int itemPrice)
            {
                // Реализация метода по умолчанию
            }

        }


        public class User : GuestUser
        {
            public User(DataTable table) : base(table)
            {
                if (table != null && table.Rows.Count > 0)
                {
                    this.UserName = table.Rows[0].Field<string>("name");
                    this.email = table.Rows[0].Field<string>("email");
                }
                status = true;
                OrderPrice = 0;
            }
            public override void order(int itemPrice)
            {
                OrderPrice += itemPrice;
            }

        }


        private void login_button_Click(object sender, EventArgs e)
        {
            db = new DB();
            
            table = new DataTable();

            
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            string loginUser = login_text.Text;
            string passUser = pass_text.Text;
            MySqlCommand command = new MySqlCommand("SELECT * FROM `us` WHERE `login` = @uL AND `pass` = @uP;\r\n", db.GetConnection());
            command.Parameters.AddWithValue("@uL", loginUser);
            command.Parameters.AddWithValue("@uP", passUser);
            /*command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;*/
            adapter.SelectCommand = command;
           
            adapter.Fill(table);
            GuestUser user;
            
            if (table.Rows.Count == 1)
            {
                user = new User(table);
                this.Hide();
                avto avto = new avto(user);
                avto.Show();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //user = new GuestUser();
            }
            /*this.Hide();
            avto avto = new avto(user);
            avto.Show();*/



        }
        DataTable GuestT()
        {
            DataTable GuestTable = new DataTable();
            GuestTable.Columns.Add("ID", typeof(int));
            GuestTable.Columns.Add("Name", typeof(string));
            GuestTable.Columns.Add("email", typeof(string));

            // Создаем массив значений для строки
            object[] rowValues = new object[3];
            rowValues[0] = 1;  // ID
            rowValues[1] = "Unknown user";  // Name
            rowValues[2] = "Unknown user";  // email

            // Добавляем строку в таблицу
            GuestTable.Rows.Add(rowValues);

            return GuestTable;
        }

        private void null_label_Click(object sender, EventArgs e)
        {

            DataTable GuestTable = GuestT(); // Заполняем таблицу значениями

            GuestUser user; // Используем тип GuestUser

            if (GuestTable.Rows.Count == 1) // Исправлено на GuestTable.Rows.Count
            {
                user = new GuestUser(GuestTable); // Здесь передается DataTable
                this.Hide();
                avto avto = new avto(user);
                avto.Show();
            }
        }

    }
}












