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
        string chuoiketnoi = "Data Source=.\\SQLEXPRESS;Initial Catalog=CUAHANG;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
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
            Conn.Open();

            string sql_Insert = "Insert into NguyenLieu values('" + Convert.ToInt32(ID.Text) + "','" + tbNL.Text + "', '" + Convert.ToInt32(Gt.Text) + "' , N'" + Convert.ToInt32(Sltk.Text) + "','" + dtpNNH.Value.ToString("yyyy-MM-dd") + "', '" + dtpHSD.Value.ToString("yyyy-MM-dd") + "')";
            SqlCommand cmd = new SqlCommand(sql_Insert, Conn);
            cmd.ExecuteNonQuery();
            Form1_Load(sender, e);
            Conn.Close();
            if (ID.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã ID", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ID.Focus();
                return;
            }
            if (tbNL.Text.Trim().Length == 0)
            {
                MessageBox.Show("Nhập tên nguyên liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbNL.Focus();
                return;
            }
            if (Gt.Text.Trim().Length == 0)
            {
                MessageBox.Show("Nhập giá thành", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Gt.Focus();
                return;
            }
            if (Sltk.Text == "(  )    -")
            {
                MessageBox.Show("Nhập số lượng hàng trong kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Sltk.Focus();
                return;
            }
            
    }

    private void button3_Click(object sender, EventArgs e)
        {
           
            Conn.Open();

            string sql_delete = "delete from NguyenLieu where ID = " +  Convert.ToInt32(ID.Text);
            SqlCommand cmd = new SqlCommand(sql_delete, Conn);
            cmd.ExecuteNonQuery();
            Form1_Load(sender, e);
            Conn.Close();
            if (ID.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dtNL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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
    }
}
