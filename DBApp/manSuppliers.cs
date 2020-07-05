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
    public partial class manSuppliers : Form
    {
        public manSuppliers()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from Supplier", Program.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Supplier");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Supplier";
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
                
                command.CommandText = "exec Create_supplier @name, @phone, @email;";
                command.Parameters.AddWithValue("@name", name.Text);
                command.Parameters.AddWithValue("@phone", phone.Text);
                command.Parameters.AddWithValue("@email", email.Text);
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

                command.CommandText = "UPDATE Supplier SET  phone_number = @phone, email = @email WHERE supplier_name = @name";
                command.Parameters.AddWithValue("@name", name.Text);
                command.Parameters.AddWithValue("@phone", phone.Text);
                command.Parameters.AddWithValue("@email", email.Text);
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
                command.CommandText = "DELETE FROM Supplier WHERE supplier_name = @name;";
                command.Parameters.AddWithValue("@name", name.Text);
                command.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Delete comment success!");
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
