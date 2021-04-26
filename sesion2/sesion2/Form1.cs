using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sesion2
{
    public partial class Form1 : Form
    {

        int numericUp1 = 1;
        int numericDown = 2;
        string conStr = @"data source=vc-stud-mssql1;user id=user79_db;password=user79;MultipleActiveResultSets=True;App=EntityFramework";
        SqlConnection connection;
        public int n = 0;
        public int id = 0;
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("По убыванию");
            comboBox1.Items.Add("По Возрастанию");
            comboBox1.Items.Add("Без сортировки");
            


        }
        public void load_data()
        {
            flowLayoutPanel1.Controls.Clear();
            connection = new SqlConnection(conStr);
            connection.Open();
            List<string> filters_list = new List<string>();
            string filters = "";
            string sort = "Id";
            
            if (comboBox1.Text == "Без сортировки")
            {
                sort = "Id";
            }
            if (comboBox1.Text == "По убыванию")
            {
                sort = "Cost desc";
            }
            if (comboBox1.Text == "По Возрастанию")
            {
                sort = "Cost";
            }
            
            if (textBox4.Text != "")
            {
                filters_list.Add("Title like N'" + textBox4.Text + "%'");
            }
           
            if (filters_list.Count > 0)
            {
                filters = "WHERE " + String.Join(" and ", filters_list.ToArray());
            }

            string q_ = "SELECT COUNT(*) FROM Product  " + filters;

            SqlCommand _cmd = new SqlCommand(q_);
            _cmd.Connection = connection;
            SqlDataReader _rd = _cmd.ExecuteReader();

            _rd.Read();
            int c = Int32.Parse(_rd.GetValue(0).ToString());
            
            if (n <= 0 || c < n)
            {
                numericUp.Text = " из 1";
                
                //numericUpDown = 1;
                label1.Text = "Показано записей: " + n + " из " + c;
            }
            else
            {
                numericUp.Text = " из " + (c / n);
                //numericUp1 = c / n;
                label1.Text = "Показано записей: " + n* numericUp1 + " из " + c;
                numericDown = c;
            }


           
           string q = "SELECT ID,Title,Cost,Description,MainImagePath,IsActive,ManufacturerID FROM Product " + filters + " ORDER BY " + sort + " OFFSET " + ((numericUp1 - 1) * n) + " ROWS FETCH NEXT " + (n <= 0 ? c : n) + " ROWS ONLY";
            SqlCommand cmd = new SqlCommand(q);
            cmd.Connection = connection;
            SqlDataReader rd = cmd.ExecuteReader();
            
            while (rd.Read())
            {

                id = Convert.ToInt32(rd.GetValue(0));

                flowLayoutPanel1.Controls.Add(new UserControl1("G:\\Help god\\Сессия 1\\Товары автосервиса\\" + rd.GetValue(4).ToString().Substring(29), rd.GetValue(1).ToString(), rd.GetValue(2).ToString(), rd.GetValue(5).ToString()));


            }


            numericUpDown.Text = Convert.ToString(numericUp1);
            rd.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            n = 9;
            load_data();

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            load_data();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            load_data();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data();
        }

       

        private void flowLayoutPanel1_DoubleClick_1(object sender, EventArgs e)
        {
            

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
           
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            if (numericUp1 > 1) {
                numericUp1--;
            }
           
            load_data();
        }

        private void btn_go_to_Click(object sender, EventArgs e)
        {
            if (numericUp1< 11) {
                numericUp1++;
            }
            
            load_data();
        }
    }
}
