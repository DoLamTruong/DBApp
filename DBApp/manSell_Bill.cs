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

namespace DBApp
{
    public partial class manSell_Bill : Form
    {
        public manSell_Bill()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 cus = new Form1();
            cus.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from Sell_Bill", Program.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Sell_Bill");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Sell_Bill";
                dataGridView1.Columns[0].ReadOnly = true;
                Program.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Program.conn.Close();
            }
        }
    }
}
