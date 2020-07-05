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

namespace DBApp
{
    public partial class manCustomer : Form
    {
        public manCustomer()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

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
                SqlDataAdapter da = new SqlDataAdapter("select * from Customer", Program.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Customer");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Customer";
                dataGridView1.Columns[0].ReadOnly = true;
                Program.conn.Close();
            } catch (Exception ex)
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
                //command.CommandText = "INSERT INTO Customer( username, fullname, phone_number, email, user_pass, basket_owned) VALUES(@id, @usename, @fullname, @phone, @mail, @hasspass, @cart)";
                command.CommandText = "exec Create_customer @usename, @fullname, @phone, @mail, @hasspass;";
                command.Parameters.AddWithValue("@usename", usename.Text);
                command.Parameters.AddWithValue("@fullname", fullname.Text);
                command.Parameters.AddWithValue("@phone", phone.Text);
                command.Parameters.AddWithValue("@mail", email.Text);
                command.Parameters.AddWithValue("@hasspass", hasspass.Text);
                command.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Add customer success!");
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
                command.CommandText = "exec Update_customer @id, @phone, @mail, @hasspass;";
                command.Parameters.AddWithValue("id", Int32.Parse(id.Text));
                command.Parameters.AddWithValue("@phone", phone.Text);
                command.Parameters.AddWithValue("@mail", email.Text);
                command.Parameters.AddWithValue("@hasspass", hasspass.Text);
                command.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Update customer success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Program.conn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                SqlCommand command = Program.conn.CreateCommand();
                command.CommandText = "DELETE FROM Customer WHERE ID = @id;";
                command.Parameters.AddWithValue("id", Int32.Parse(id.Text));
                command.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Delete customer success!");
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Program.conn.Close();
            }
        }
    }
}
