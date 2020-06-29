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
                Program.conn.Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                SqlCommand command = Program.conn.CreateCommand();
                command.CommandText = "INSERT INTO Customer VALUES(@id, @usename, @fullname, @phone, @mail, @hasspass, @cart)";
                command.Parameters.AddWithValue("id", Int16.Parse(id.Text));
                command.Parameters.AddWithValue("@usename", usename.Text);
                command.Parameters.AddWithValue("@fullname", fullname.Text);
                command.Parameters.AddWithValue("@email", email.Text);
                command.Parameters.AddWithValue("@hasspass", hasspass.Text);
                command.Parameters.AddWithValue("cart", Int16.Parse(cart.Text));
                command.ExecuteNonQuery();
                Program.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                SqlCommand command = Program.conn.CreateCommand();
                command.CommandText = "UPDATE  Customer SET  username = @usename, fullname = @fullname, phone_number = @phone, email = @mail, user_pass = @hasspass, basket_ownerd = @cart WHERE id = @id,";
                command.Parameters.AddWithValue("id", Int16.Parse(id.Text));
                command.Parameters.AddWithValue("usename", usename);
                command.Parameters.AddWithValue("fullname", fullname);
                command.Parameters.AddWithValue("email", email);
                command.Parameters.AddWithValue("hasspass", hasspass);
                command.Parameters.AddWithValue("cart", Int16.Parse(cart.Text));
                command.ExecuteNonQuery();
                Program.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
