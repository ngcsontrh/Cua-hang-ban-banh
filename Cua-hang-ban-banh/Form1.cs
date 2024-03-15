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
using System.Xml.Linq;
using Cua_hang_ban_banh;

namespace NccForm
{

    public partial class Form1 : Form
    {
        private SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;" +
            "Initial Catalog=CUAHANG;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True");

        private void LoadData()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From NhaCungCap", conn);
            DataTable bangNcc = new DataTable();
            adapter.Fill(bangNcc);
            dgv.DataSource = bangNcc;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            conn.Open();
            LoadData();

            checkBox1_CheckedChanged(sender, e);
            conn.Close();
        }

        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into NhaCungCap values(N'" + tb_ncc.Text + "', '" + Convert.ToInt32(tb_sdt.Text) + "','" + dtime_ncc.Text + "', N'" + tb_loaibanh.Text + "', N'" + tb_diachi.Text + "')";
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi! Vui lòng kiểm tra thông tin nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadData();
            conn.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "update NhaCungCap set TenNCC = N'" + tb_ncc.Text + "', SDT = '" + Convert.ToInt32(tb_sdt.Text) + "', NgayCungCap = '" + dtime_ncc.Text + "', LoaiBanh = N'" + tb_loaibanh.Text + "', DiaChi = N'" + tb_diachi.Text + "' Where Id = '" + tb_id.Text + "'";
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi! Vui lòng nhập đúng thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadData();
            conn.Close();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa thông tin", "", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete from NhaCungCap where Id = " + tb_id.Text + "";
                cmd.ExecuteNonQuery();
                LoadData();
                conn.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn làm mới", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                conn.Open();
                tb_id.Text = "";
                tb_ncc.Text = "";
                tb_sdt.Text = "";
                dtime_ncc.Text = "1/1/2024";
                tb_loaibanh.Text = "";
                tb_diachi.Text = "";
                LoadData();
                conn.Close();
            }

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chktimkiem.Checked == true)
            {
                chktheoid.Visible = true;
                chktheoid.Checked = true;
                tb1.Visible = true;

            }
            else
            {
                chktheoid.Visible = false;
                tb1.Visible = false;

            }
        }
        private void tb1_TextChanged(object sender, EventArgs e)
        {
            string id = tb1.Text.Trim();
            try
            {
                conn.Open();
                if (id.Length == 0)
                {
                    LoadData();
                    return;
                }
                SqlCommand cmd = new SqlCommand("select * from NhaCungCap where Id = @id", conn);
                cmd.Parameters.AddWithValue("id", id);
                SqlDataReader dataReader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                dgv.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi nhập tìm kiếm", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int i;
            i = dgv.CurrentRow.Index;
            tb_id.Text = dgv.Rows[i].Cells[0].Value.ToString();
            tb_ncc.Text = dgv.Rows[i].Cells[1].Value.ToString();
            tb_sdt.Text = dgv.Rows[i].Cells[2].Value.ToString();
            dtime_ncc.Text = dgv.Rows[i].Cells[3].Value.ToString();
            tb_loaibanh.Text = dgv.Rows[i].Cells[4].Value.ToString();
            tb_diachi.Text = dgv.Rows[i].Cells[5].Value.ToString();
        }
        private void tb1_KeyPress(object sender, KeyPressEventArgs e)
        {
            tb1_TextChanged((object)sender, e);

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            EmployeesForm f = new EmployeesForm();
            f.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Cake f = new Cake();
            f.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            CakeIngredients f = new CakeIngredients();
            f.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            EmployeesForm f = new EmployeesForm();
            f.Show();
            this.Hide();
        }

        private void label8_Click_1(object sender, EventArgs e)
        {
            Cake f = new Cake();
            f.Show();
            this.Hide();
        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            CakeIngredients f = new CakeIngredients();
            f.Show();
            this.Hide();
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
