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
    public partial class manDelivery : Form
    {
        public manDelivery()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from Delivery", Program.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Delivery");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Delivery";
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

                command.CommandText = "exec Create_delivery @name, @country_code, @region, @city, @phone";
                command.Parameters.AddWithValue("@name", name.Text);
                command.Parameters.AddWithValue("@country_code", countryCode.Text);
                command.Parameters.AddWithValue("@region", region.Text);
                command.Parameters.AddWithValue("@city", city.Text);
                command.Parameters.AddWithValue("@phone", phone.Text);
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

                command.CommandText = "UPDATE Delivery SET  country_code = @country_code, region = @region, city = @city, phone = @phone  WHERE ID = @ID";
                command.Parameters.AddWithValue("@name", name.Text);
                command.Parameters.AddWithValue("@country_code", countryCode.Text);
                command.Parameters.AddWithValue("@region", region.Text);
                command.Parameters.AddWithValue("@city", city.Text);
                command.Parameters.AddWithValue("@phone", phone.Text);
                command.Parameters.AddWithValue("@ID", id.Text);
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
                command.CommandText = "DELETE FROM Delivery WHERE ID = @ID;";
                command.Parameters.AddWithValue("@ID", id.Text);
                command.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Delete Delivery success!");
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
