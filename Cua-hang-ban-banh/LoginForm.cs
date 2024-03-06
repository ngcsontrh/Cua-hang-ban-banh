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
    public partial class LoginForm : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;" +
            "Initial Catalog=CUAHANG;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True");

        public LoginForm()
        {
            InitializeComponent();
        }

        private void cb_ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_ShowPassword.Checked)
            {
                tb_Password.UseSystemPasswordChar = false;
            }
            else
            {
                tb_Password.UseSystemPasswordChar= true;
            }
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string username = tb_Username.Text.Trim();
            string password = tb_Password.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from TaiKhoan where Username = @username and Password = @password", conn);
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("password", password);
                    var isExistUser = cmd.ExecuteScalar();
                    if (isExistUser != null)
                    {
                        this.Hide();
                        EmployeesForm f = new EmployeesForm();
                        f.Show();
                    }
                    else
                    {
                        MessageBox.Show
                            ("Tên đăng nhập hoặc mật khẩu không chính xác!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
    }
}
