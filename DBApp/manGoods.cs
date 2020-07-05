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
    public partial class manGoods : Form
    {
        public manGoods()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from Goods", Program.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Goods");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Goods";
                dataGridView1.Columns[0].ReadOnly = true;
                Program.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Program.conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                SqlCommand command = Program.conn.CreateCommand();

                command.CommandText = "exec Create_goods @item, @sell_price, @supplied_by;";
                command.Parameters.AddWithValue("@item", item.Text);
                command.Parameters.AddWithValue("@sell_price", price.Text);
                command.Parameters.AddWithValue("@supplied_by", supplied.Text);
                command.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Add success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Program.conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                SqlCommand command = Program.conn.CreateCommand();

                command.CommandText = "UPDATE Goods SET  sell_price = @sell_price, supplied_by = @supplied_by WHERE ID = @ID";
                command.Parameters.AddWithValue("@ID", id.Text);
                command.Parameters.AddWithValue("@item", item.Text);
                command.Parameters.AddWithValue("@sell_price", price.Text);
                command.Parameters.AddWithValue("@supplied_by", supplied.Text);
                command.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Update success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Program.conn.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                SqlCommand command = Program.conn.CreateCommand();
                command.CommandText = "DELETE FROM Goods WHERE ID = @ID;";
                command.Parameters.AddWithValue("@ID", id.Text);
                command.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Delete goods success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Program.conn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 cus = new Form1();
            cus.ShowDialog();
            this.Close();
        }
    }
}
