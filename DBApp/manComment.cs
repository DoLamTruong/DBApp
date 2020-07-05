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
    public partial class manComment : Form
    {
        public manComment()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                SqlCommand command = Program.conn.CreateCommand();

                //command.CommandText = "INSERT INTO Customer( username, fullname, phone_number, email, user_pass, basket_owned) VALUES(@id, @usename, @fullname, @phone, @mail, @hasspass, @cart)";
                command.CommandText = "UPDATE Comment SET  rating = @rating, content = @content, related_to = @related WHERE ID = @id";
                command.Parameters.AddWithValue("id", Int32.Parse(id.Text));
                command.Parameters.AddWithValue("@rating", rating.Text);
                command.Parameters.AddWithValue("@content", content.Text);
                command.Parameters.AddWithValue("@related", related.Text);
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
                string sql = "select * from Comment";
                string rate = leastRating.Text;
                if (leastRating.Text != "")
                    sql = "select * from Comment WHERE rating >= " + rate;
                SqlDataAdapter da = new SqlDataAdapter(sql, Program.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Comment");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Comment";
                dataGridView1.Columns[0].ReadOnly = true;
                Program.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Program.conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                SqlCommand command = Program.conn.CreateCommand();

                //command.CommandText = "INSERT INTO Customer( username, fullname, phone_number, email, user_pass, basket_owned) VALUES(@id, @usename, @fullname, @phone, @mail, @hasspass, @cart)";
                command.CommandText = "exec Create_comment @from, @rating, @content, @related;";
                command.Parameters.AddWithValue("from", Int32.Parse(from.Text));
                command.Parameters.AddWithValue("@rating", rating.Text);
                command.Parameters.AddWithValue("@content", content.Text);
                command.Parameters.AddWithValue("@related", related.Text);
                command.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Add comment success!");
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
                command.CommandText = "DELETE FROM Comment WHERE ID = @id;";
                command.Parameters.AddWithValue("id", Int32.Parse(id.Text));
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
