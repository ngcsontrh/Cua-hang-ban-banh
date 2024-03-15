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
    public partial class Cake : Form
    {
        public Cake()
        {
            InitializeComponent();
        }
        string chuoiketnoi = "Data Source=.\\SQLEXPRESS;" +
            "Initial Catalog=CUAHANG;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True";
        SqlConnection conn = null;
        SqlCommand cmd = new SqlCommand();

        private void Cake_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            string sql = "select * from Banh ";
            SqlDataAdapter daBanh = new SqlDataAdapter(sql, conn);
            DataTable dtBanh = new DataTable();
            daBanh.Fill(dtBanh);
            dgvBanh.DataSource = dtBanh;
            conn.Close();
            
        }

        private void Addbt_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "insert into Banh(Id,TenBanh,LoaiBanh,GiaThanh,NgaySanXuat) values (" + Convert.ToInt32(Idtext.Text) + ",N'" + Caketext.Text + "',N'" + Caketypetext.Text + "'," + Convert.ToInt32(Pricetext.Text) + ",'" + CakeDate.Value.ToString("yyyy-MM-dd") + "')";
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi! Vui lòng kiểm tra thông tin nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
            Cake_Load(sender, e);
        }

        private void Deletebt_Click(object sender, EventArgs e)
        {
            if (dgvBanh.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Idtext.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "delete Banh WHERE Id =N'" + Convert.ToInt32(Idtext.Text) + "'";
                cmd.ExecuteNonQuery();
                conn.Close();
                Cake_Load(sender, e);
            }

        }
        private void dgvBanh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvBanh != null && dgvBanh.Rows.Count > 0)
            {
                int i = dgvBanh.CurrentRow.Index;
                Idtext.Text = dgvBanh.Rows[i].Cells[0].Value.ToString();
                Caketext.Text = dgvBanh.Rows[i].Cells[1].Value.ToString();
                Pricetext.Text = dgvBanh.Rows[i].Cells[2].Value.ToString();
                Caketypetext.Text = dgvBanh.Rows[i].Cells[3].Value.ToString();
                CakeDate.Text = dgvBanh.Rows[i].Cells[4].Value.ToString();
            }
            else return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Idtext.Text = string.Empty;
            Caketext.Text = string.Empty;
            Caketypetext.Text = string.Empty;
            Pricetext.Text = string.Empty;
            CakeDate.Text = string.Empty;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Updatabt_Click(object sender, EventArgs e)
        {
            if (dgvBanh.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Idtext.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn thay đổi không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE Banh  SET TenBanh =N'" + Caketext.Text + "'"+",GiaThanh =" + Convert.ToInt32(Pricetext.Text) + ",LoaiBanh =N'"+ Caketypetext.Text+"'"+",NgaySanXuat='"+CakeDate.Value.ToString("yyyy-MM-dd")+ "' WHERE Id=" + Convert.ToInt32(Idtext.Text) + "";
                cmd.ExecuteNonQuery();
                conn.Close();
                Cake_Load(sender, e);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
            EmployeesForm f = new EmployeesForm();
            f.Show();
            this.Hide();
        }

        private void Cake_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            CakeIngredients f = new CakeIngredients();
            f.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
    }
}
