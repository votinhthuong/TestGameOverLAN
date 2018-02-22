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
using System.Text.RegularExpressions;

namespace Sample_SignIn
{
    public partial class Information_Account : Form
    {
        public Information_Account()
        {
            InitializeComponent();
        }
        private string p = "";
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-MJLTEC9;Initial Catalog=Chatlan;User ID=sa;Password=sa2014");
        //SqlConnection con = new SqlConnection("Data Source=192.168.43.117;Initial Catalog=Chatlan;User ID=sa;Password=sa2014");
        //SqlConnection con = new SqlConnection("Data Source=MR_DUY\\SQLEXPRESS;Initial Catalog=Chatlan;User ID=sa;Password=sa2014");

        public Information_Account(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
            InitializeComponent();
        }
        private void Information_Account_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;//khi form Information_Account được gọi thì nhóm gropBox1 sẽ bị mờ và không sử dụng được.
            txt_username3.Enabled = false;//khi form Information_Account được gọi thì trường txt_username sẽ bị mờ và không sử dụng được.
            txt_username3.Text = p;//khi form Information_Account được gọi thì trường txt_username sẽ mặc đinh là Username đăng nhập vào ở form Login_Change_Passwd.
            txt_email2.Enabled = false;//khi form Information_Account được gọi thì trường txt_email2 sẽ bị mờ và không sử dụng được.
            btn_update.Enabled = false;//khi form Information_Account được gọi thì btn_update sẽ bị mờ và không sử dụng được
            con.Open();
            string query = "select Email from nguoidung where Username = '" + txt_username3.Text.ToString() + "'";
            //Truy vấn Email tại bảng "nguoidung" với Username tương ứng và hiện thị trên trường txt_email2.
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            String email = reader[0].ToString();
            txt_email2.Text = email;
            con.Close();

        }

        private void btn_changepasswd_Click(object sender, EventArgs e)//Khi btn_changepasswd được sử dụng thì nhóm groupBox1 được phép sử dụng, còn btn_update sẽ bị mờ và không sử dụng được.
        {
            groupBox1.Enabled = true;
            btn_update.Enabled = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)//Khi nhóm groupBox1 được sử dụng thì đồng thời btn_changepasswd1 bị mờ và không sử dụng được.
        {
            btn_changepasswd1.Enabled = false;
        }

        private void txt_repsswd_TextChanged(object sender, EventArgs e)//Khi trường txt_repasswd sử dụng thì btn_changepasswd1 được phép sử dụng.
        {
            btn_changepasswd1.Enabled = true;
        }

        private void btn_changepasswd1_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "select Passwd from nguoidung where Username = '" + txt_username3.Text.ToString() + "'";
            //Truy vần với Passwd trong bảng "nguoidung" tại Username trong dữ liệu SQL
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            String pass = reader[0].ToString();
            con.Close();
            if (txt_currentpswd.Text.Equals(pass))//kiểm tra so sánh Passwd được nhập chính xác chưa.
            {
                if (txt_newpswd.Text.ToString().Equals(txt_repsswd.Text.ToString()))//nếu chính xác thì yêu cầu nhập passwd mới ở trường txt_newpswd và đồng thời phải nhập lại passwd xác nhập ở trường repaswd.
                {
                    con.Open();
                    query = "update nguoidung set Passwd = '" + txt_newpswd.Text.ToString() + "' " + "where Username = '" + txt_username3.Text.ToString() + "'";
                    //Passwd nhập chính xác ở 2 trường txt_newpswd và tx_repsswd thì update bảng "nguoidung" với Passwd mới vừa nhập tại Username đã đăng nhập.
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Cập nhật mật khẩu mới thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    login lg = new login();
                    lg.Show();
                    this.Hide();
                }
                else {
                    MessageBox.Show("Mật khẩu mới không khớp !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                MessageBox.Show("Mật khẩu hiện tại không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
           try
            {
                if (con.State == ConnectionState.Closed)
                {
                    if (kiemtraEmail(txt_email1.Text.ToString()))
                    {

                        con.Open();
                        String query = "select Email from nguoidung where Email = '" + txt_email1.Text.ToString() + "'";
                        //Truy vần Email tại bảng "nguoidung" với Email tương ứng trong dữ liệu SQL
                        SqlCommand cmd2 = new SqlCommand(query, con);
                        SqlDataReader reader = cmd2.ExecuteReader();
                        
                        String count1 = "";

                        while (reader.Read())
                        {
                            count1 = reader[0].ToString();//Đọc dữ liệu từng dòng trên dữ liệu SQL trong while()
                        }
                        //reader.Read();
                        //count = reader[0].ToString();
                        con.Close();
                        if (txt_email1.Text.Equals(count1))//Nếu email nhập trong trường txt_email1 mà trùng với email đã truy vấn trong dữ liệu thì thông báo lỗi và yếu cầu nhập Email khác.
                        {
                            MessageBox.Show("Email đã tồn tại! Vui lòng nhập Email khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_email1.Clear();
                            return;
                        }
                        con.Close();

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                            string str = "update nguoidung set Email = '" + txt_email1.Text.ToString() + "' " +
                        "where Username = '" + txt_username3.Text.ToString() + "'";
                            //Truy vần vào bảng "nguoidung" cập nhật lại Email mới ở Username tương ứng trong dữ liệu SQL.
                            SqlCommand cmd = new SqlCommand(str, con);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Cập nhật email thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                con.Close();
            }

            catch (Exception e1)
            {
                MessageBox.Show("Cập nhật email thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)//Khi button1_click sử dụng thì sẽ gọi form login đồng thời đóng form trước đó.
        {
            login l = new login();
            l.Show();
            this.Close();
        }

        private void txt_email1_TextChanged(object sender, EventArgs e)//Khi trường txt_email1 được sử dụng thì btn_update được phép sử dụng.
        {
            btn_update.Enabled = true;
        }

        private void txt_currentpswd_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_changepasswd1_KeyDown(object sender, KeyEventArgs e)//khi con trỏ thực thi ở btn_changpasswd1 sau khi xong, ta sử dụng Enter sẽ thực hiện btn_changpasswd1
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_changepasswd1_Click(this, new EventArgs());
            }
        }

        private void btn_update_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txt_email1_KeyDown(object sender, KeyEventArgs e)//khi con trỏ ở trường txt_email1 được sử dụng xong, ta sử dụng Enter thì btn_update_Click được thực thị.
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_update_Click(this, new EventArgs());
            }
        }

        private void txt_repsswd_KeyDown(object sender, KeyEventArgs e)//khi con trỏ ở trường txt_resspaswd được sử dụng xong, ta sử dụng Enter thì btn_changepasswd1_Click được thực thị.
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_changepasswd1_Click(this, new EventArgs());
            }
        }

        private void txt_email1_TextChanged_1(object sender, EventArgs e)//Khi trường txt_email1 được sử dụng đồng thới btn_update được phép sử dụng.
        {
            btn_update.Enabled = true;
        }

        private Boolean kiemtraEmail(string email1)
        {

            if (email1.Length > 50 )//Kiểm tra Email nhập vào có vượt quá 50 ký tự không. Nếu vược quá thông báo lỗi. Yêu cầu nhập lại Email mới.
            {
                MessageBox.Show("Chiều dài Email quá lớn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (IsValidEmail(email1))//Kiểm tra Email vừa nhập có thuộc đinh dạng "abc@gmail.com" hoặc "abc@acv.sdf" hay không.
            {
                return true;
            }
            MessageBox.Show("Email không hợp lệ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
            
        }
        private Boolean kiemtraEmailHopLe(String email1)//Kiểm tra Email vừa nhập có thuộc quy ước theo mã ASCII dưới đây không.
        {
            foreach (char c in email1)
            {
                if (!((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c >= 48 && c <= 57) || (c == 95) || (c == 46) || (c == 64)))
                {
                   //MessageBox.Show("Email không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;

        }
        private bool IsValidEmail(string email)//hàm Kiểm tra Email vừa nhập có thuộc đinh dạng "abc@gmail.com" hoặc "abc@acv.sdf" hay không
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
