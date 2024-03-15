using NccForm;
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

namespace Cua_hang_ban_banh
{
    public partial class CakeIngredients : Form
    {
        public CakeIngredients()
        {
            InitializeComponent();
        }
        string chuoiketnoi = "Data Source=.\\SQLEXPRESS;" +
            "Initial Catalog=CUAHANG;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True";
        SqlConnection Conn = null;
        SqlCommand cmd = new SqlCommand();
        private void Form1_Load(object sender, EventArgs e)
        {
            Conn = new SqlConnection(chuoiketnoi);
            Conn.Open();
            string sql = "Select * from NguyenLieu";
            SqlDataAdapter daNguyenLieu = new SqlDataAdapter(sql,Conn);
            DataTable dtNLieu = new DataTable();
            daNguyenLieu.Fill(dtNLieu);
            dtNL.DataSource = dtNLieu;
            Conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Conn.Open();
                cmd.Connection = Conn;
                cmd.CommandText = "Insert into NguyenLieu values('" + Convert.ToInt32(ID.Text) + "','" + tbNL.Text + "', '" + Convert.ToInt32(Gt.Text) + "' , N'" + Convert.ToInt32(Sltk.Text) + "','" + dtpNHH.Value.ToString("yyyy-MM-dd") + "', '" + dtpHSD.Value.ToString("yyyy-MM-dd") + "')";
                cmd.ExecuteNonQuery();
                Form1_Load(sender, e);
                
            }
            catch (Exception)
            {
                MessageBox.Show("Trùng ID!! Yêu cầu nhập lại!!");
            }
            Conn.Close();
         }

    private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Conn.Open();
                string sql_delete = "delete from NguyenLieu where id = " + Convert.ToInt32(ID.Text);
                SqlCommand cmd = new SqlCommand(sql_delete, Conn);
                cmd.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi nhập thông tin");
            }
            Form1_Load(sender, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            EmployeesForm f = new EmployeesForm();
            f.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Cake f = new Cake();
            f.Show();
            this.Hide();
        }

        private void CakeIngredients_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conn.Open();
            cmd.Connection = Conn;
            cmd.CommandText = "Update NguyenLieu set TenNL = '"+tbNL.Text+"', GiaThanh = '"+ Gt.Text +"', SLTonKho = '" +Sltk.Text+ "',NgayNhap = '" +dtpNHH.Text + "',HanSuDung = '" + dtpHSD.Text+"' where ID = '" + Convert.ToInt32(ID.Text) + "'";
            cmd.ExecuteNonQuery();
            Form1_Load(sender, e);
            Conn.Close();
        }

        private void dtNL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dtNL.CurrentRow.Index;
            ID.Text = dtNL.Rows[i].Cells[0].Value.ToString();
            tbNL.Text = dtNL.Rows[i].Cells[1].Value.ToString();
            Gt.Text = dtNL.Rows[i].Cells[2].Value.ToString();
            Sltk.Text = dtNL.Rows[i].Cells[3].Value.ToString();
            dtpNHH.Text = dtNL.Rows[i].Cells[4].Value.ToString();
            dtpHSD.Text = dtNL.Rows[i].Cells[5].Value.ToString();

        }

        private void dtNL_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
