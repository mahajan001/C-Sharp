using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace studentinfo
{
    public partial class Form1 : Form
    {
        OleDbConnection con = new OleDbConnection();
        public Form1()
        {
            InitializeComponent();
            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\AdityaCM\\student.mdb";
            display();
        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = new OleDbCommand("insert into stud values(@rno , @nm , @gen)",con);
            cmd.Parameters.AddWithValue("@rno",Convert.ToInt32(txtroll.Text));
            cmd.Parameters.AddWithValue("@nm",txtname.Text);

            if (rdbmale.Checked == true)
            {
                cmd.Parameters.AddWithValue("@gen", rdbmale.Text);
            }
            else {
                cmd.Parameters.AddWithValue("@gen", rdbfemale.Text);
            }
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Inserted");
            display();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            con.Open();
            OleDbCommand cmd = new OleDbCommand("update stud set name=@nm , gender=@gen where rollno=@rno", con);
            cmd.Parameters.AddWithValue("@rno", Convert.ToInt32(txtroll.Text));
            cmd.Parameters.AddWithValue("@nm", txtname.Text);

            if (rdbmale.Checked == true)
            {
                cmd.Parameters.AddWithValue("@gen", rdbmale.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@gen", rdbfemale.Text);
            }
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Updated");
            display();

        }

        private void btndelete_Click(object sender, EventArgs e)
        {

            con.Open();
            OleDbCommand cmd = new OleDbCommand("delete from stud where rollno=@rno", con);
            cmd.Parameters.AddWithValue("@rno", Convert.ToInt32(txtroll.Text));

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Deleted");
            display();
        }

        void display()
        {

            con.Open();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from stud",con);
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            con.Close();
        
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtroll.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

            string a =  dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            if (a == "Male")
            {
                rdbmale.Checked = true;
            }
          else {
                rdbfemale.Checked = true;
            
            }
           display();
         
        }
    }
}
