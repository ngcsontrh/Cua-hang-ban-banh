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
    public partial class EmployeesForm : Form
    {
        private SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;" +
            "Initial Catalog=CUAHANG;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True");
        public EmployeesForm()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select Id, " +
                    "HoTen as 'Họ tên', " +
                    "SDT as 'Số điện thoại', " +
                    "Gioitinh as 'Giới tính', " +
                    "DiaChi as 'Địa chỉ', " +
                    "NgaySinh as 'Ngày sinh' " +
                    "from NhanVien", conn);
            DataTable bangNhanVien = new DataTable();
            adapter.Fill(bangNhanVien);
            EmployeeData.DataSource = bangNhanVien;
        }

        private void DelText()
        {
            tb_id.Text = string.Empty;
            tb_fullname.Text = string.Empty;
            tb_address.Text = string.Empty;
            tb_phoneNumber.Text = string.Empty;
            cb_gender.SelectedItem = null;
            dtp_birthday.Text = string.Empty;
        }

        private void GetData()
        {
            if (EmployeeData != null && EmployeeData.Rows.Count > 0)
            {
                int i = EmployeeData.CurrentRow.Index;
                tb_id.Text = EmployeeData.Rows[i].Cells[0].Value.ToString();
                tb_fullname.Text = EmployeeData.Rows[i].Cells[1].Value.ToString();
                tb_phoneNumber.Text = EmployeeData.Rows[i].Cells[2].Value.ToString();
                cb_gender.Text = EmployeeData.Rows[i].Cells[3].Value.ToString();
                tb_address.Text = EmployeeData.Rows[i].Cells[4].Value.ToString();
                dtp_birthday.Text = EmployeeData.Rows[i].Cells[5].Value.ToString();
            }
            else
            {
                return;
            }
        }

        private void Employees_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void EmployeesForm_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                this.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            string ten = tb_fullname.Text.Trim();
            string sdt = tb_phoneNumber.Text.Trim();
            string gioiTinh = cb_gender.SelectedItem.ToString();
            string diaChi = tb_address.Text.Trim();
            string ngaySinh = dtp_birthday.Value.ToShortDateString();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into NhanVien(HoTen, SDT, Gioitinh, DiaChi, NgaySinh) " +
                    "values(@hoTen, @sdt, @gioiTinh, @diaChi, @ngaySinh)", conn);
                cmd.Parameters.AddWithValue("hoTen", ten);
                cmd.Parameters.AddWithValue("sdt", sdt);
                cmd.Parameters.AddWithValue("gioiTinh", gioiTinh);
                cmd.Parameters.AddWithValue("diaChi", diaChi);
                cmd.Parameters.AddWithValue("ngaySinh", ngaySinh);
                cmd.ExecuteNonQuery();
                this.LoadData();
                this.DelText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            DelText();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc muốn xoá?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(rs == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from NhanVien where Id=@id", conn);
                    cmd.Parameters.AddWithValue("id", Convert.ToInt32(tb_id.Text));
                    cmd.ExecuteNonQuery();
                    LoadData();
                    DelText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            string ten = tb_fullname.Text.Trim();
            string sdt = tb_phoneNumber.Text.Trim();
            string gioiTinh = cb_gender.SelectedItem.ToString();
            string diaChi = tb_address.Text.Trim();
            string ngaySinh = dtp_birthday.Value.ToShortDateString();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update NhanVien set Hoten = @hoTen, SDT = @sdt, Gioitinh = @gioiTinh, DiaChi = @diaChi, NgaySinh = @ngaySinh where Id = @id", conn);
                cmd.Parameters.AddWithValue("id", Convert.ToInt32(tb_id.Text));
                cmd.Parameters.AddWithValue("hoTen", ten);
                cmd.Parameters.AddWithValue("sdt", sdt);
                cmd.Parameters.AddWithValue("gioiTinh", gioiTinh);
                cmd.Parameters.AddWithValue("diaChi", diaChi);
                cmd.Parameters.AddWithValue("ngaySinh", ngaySinh);
                cmd.ExecuteNonQuery();
                this.LoadData();
                this.DelText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void EmployeeData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            GetData();
        }


        private void btn_search_id_Click(object sender, EventArgs e)
        {
            string id = searchBoxId.Text.Trim();
            try
            {
                conn.Open();
                if (id.Length == 0)
                {
                    LoadData();
                    return;
                }
                SqlCommand cmd = new SqlCommand("select * from NhanVien where Id = @id", conn);
                cmd.Parameters.AddWithValue("id", id);
                SqlDataReader dataReader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                EmployeeData.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void searchBoxId_KeyPress(object sender, KeyPressEventArgs e)
        {
            btn_search_id_Click((object)sender, e);
        }

        private void btn_search_name_Click(object sender, EventArgs e)
        {
            string name = searchBoxName.Text.Trim();
            try
            {
                conn.Open();
                if (name.Length == 0)
                {
                    LoadData();
                    return;
                }
                SqlCommand cmd = new SqlCommand("select * from NhanVien where HoTen like @hoTen", conn);
                cmd.Parameters.AddWithValue("hoTen", name);
                SqlDataReader dataReader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                EmployeeData.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void searchBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            btn_search_name_Click((object)sender, e);   
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Cake f = new Cake();
            f.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            CakeIngredients f  = new CakeIngredients();
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
