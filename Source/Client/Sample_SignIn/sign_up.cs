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
    public partial class sign_up : Form
    {
        //Xử lý kết nối xuống CSDL để thực hiện đăng ký tài khoản
        SqlConnection con;
        public sign_up()
        {
            InitializeComponent();
            con = new SqlConnection("Data Source=DESKTOP-MJLTEC9;Initial Catalog=Chatlan;User ID=sa;Password=sa2014");            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private Boolean kiemtraTen(string user)
        {

            if (user.Length > 10) // kiểm tra tên tài khoản nhập vào form đăng kí nếu vượt 10 ký tự thì sẽ không cho.
            {
                MessageBox.Show("Chiều dài tài khoản quá lớn! Vui lòng kiểm tra lại!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // char[] chuoi = user.ToCharArray();
            //Boolean check=false;
            foreach (char c in user)//Kiểm tra ký tự nhập vào không thuộc trong khoảng ký tự quy định trong mã ASCII dưới thì sẽ không nhập được. Ngược lại thì cho phép.
            {
                if (!((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c >= 48 && c <= 57) || (c == 95)))
                {
                    MessageBox.Show("Username không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        private Boolean kiemtraPasswd(string pass)
        {

            if (pass.Length > 10) // kiểm tra tên Mật khẩu nhập vào form đăng kí nếu vượt 10 ký tự thì sẽ không cho.
            {
                MessageBox.Show("Chiều dài Password quá lớn! Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //  char[] chuoi = user.ToCharArray();
            //Boolean check=false;
            foreach (char c in pass) //Kiểm tra ký tự nhập vào không thuộc trong khoảng ký tự quy định trong mã ASCII dưới đây thì sẽ không nhập được. Ngược lại thì cho phép.
            {
                if (!((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c >= 48 && c <= 57) || (c == 95)))
                {
                    MessageBox.Show("Password không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        private Boolean kiemtraEmail(string user)
        {

            if (user.Length > 50) // kiểm tra tên Email nhập vào form đăng kí nếu vượt 50 ký tự thì sẽ không cho.
            {
                MessageBox.Show("Chiều dài Email quá lớn! Vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (IsValidEmail(user))//Kiểm tra ký tự nhập nếu không có dạng "abc@gmai.com" hoặc "abc@abc.abc" thì sẽ không cho phép nhập.
            {
                return true;
            }
            MessageBox.Show("Email không hợp lệ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        private Boolean kiemtraEmailHopLe(String user)
        {
            foreach (char c in user)//Kiểm tra ký tự nhập vào không thuộc trong khoảng ký tự quy định trong mã ASCII dưới đây thì sẽ không nhập được. Ngược lại thì cho phép.
            {
                if (!((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c >= 48 && c <= 57) || (c == 95)||(c==46)||(c==64)))
                {
                    //MessageBox.Show("Email không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || txt_email4.Text.Equals("")) //Kiểm tra một trong các trường trong form đăng kí mà bỏ trống. thì yêu cầu nhập đầy đủ thông tin. 
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }
            else if(!kiemtraTen(textBox1.Text))// yêu cầu user nhập vào phải thuộc quy ước trong hàm "kiemtraTen".
            {
                return;
            }
            else if(!kiemtraPasswd(textBox2.Text.ToString()))// yêu cầu passwd nhập vào phải thuộc theo quy ước của hàm "kiemtraPasswd".
            {
                return;      
            }
            else if (!kiemtraEmail(txt_email4.Text.ToString()))//yêu cầu Email nhập vào phải thuộc theo quy ước của hàm "kiemtraEmail".
            {
                return;    
            }
            else
            {
                if (!textBox2.Text.ToString().Equals(textBox3.Text.ToString()))//nếu passwd nhập vào và passwd xác nhập không trùng sẽ báo lỗi. Và không cho đăng ký.
                {
                    MessageBox.Show("Mật khẩu nhập lại không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        String query = "select Username from nguoidung where Username = '" + textBox1.Text.ToString() + "'";//truy vấn Username từ bảng "nguoidung" ở Username trong dữ liệu SQL.
                        SqlCommand cmd1 = new SqlCommand(query, con);
                        SqlDataReader reader = cmd1.ExecuteReader();
                        String user = "";

                        while (reader.Read())
                        {
                            user = reader[0].ToString();//Đọc dữ liệu từng dòng trên dữ liệu SQL trong while()
                        }
                        //reader.Read();
                        //count = reader[0].ToString();
                        con.Close();
                        if (textBox1.Text.Equals(user))//kiểm tra nếu username vửa tạo mà đã có trong dữ liệu SQL thì sẽ báo lỗi. Yêu cầu nhập lại username.
                        {
                            MessageBox.Show("Tài khoản đã tồn tại! Vui lòng tạo tài khoản khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else if (!kiemtraTen(textBox1.Text))//Ngoài ra cũng đồng thời kiểm tra username đó phải thuộc quy ước trong hàm "kiemtraTen".
                        {
                            return;
                        }
                    }
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        String query = "select Email from nguoidung where Email = '" + txt_email4.Text.ToString() + "'";
                        //truy vấn Email từ bảng "nguoidung" ở Email trong dữ liệu SQL.
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
                        if (txt_email4.Text.Equals(count1))//kiểm tra nếu Email vửa tạo mà đã có trong dữ liệu SQL thì sẽ báo lỗi. Yêu cầu nhập lại Email.
                        {
                            MessageBox.Show("Email đã tồn tại! Vui lòng tạo tài khoản khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        con.Close();
                    }

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String str = "insert into nguoidung(Username,Passwd,Email) values ('" + textBox1.Text + "', '" + textBox2.Text + "','" + txt_email4.Text + "')";
                    //Thêm vào bảng "nguoidung" với Username,Passwd,Email với giá trị tương ứng với từng trường trong form đăng kí. Đã kiểm tra tất cả điều kiện, đúng mới thêm vào được trong dữ liệu SQL.
                    //insert into nguoidung(Username, Passwd) values('thuongvt', '12345')
                    string str2 = "select count(*) from nguoidung where Username='" + textBox1.Text + "'and Passwd='" + textBox2.Text + "'and Email='" + txt_email4.Text + "'";
                    //Truy vấn trong bảng "nguoidung" với Username,Passwd,Email có dòng thoải điều kiện trong bảng không.
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();

                    SqlDataAdapter sqldata = new SqlDataAdapter(str2, con);
                    DataTable dtbl = new DataTable();
                    sqldata.Fill(dtbl);
                    if (dtbl.Rows[0][0].ToString() == "1")//nếu thoải điều kiện thì cho đăng kí đồng thời mở lại form login.
                    {

                        MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        login frm2 = new login();
                        frm2.Show();
                        this.Hide();
                    }
                    else
                    {

                        MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                        return;
                    }

                }
                catch
                {
                    MessageBox.Show("Đăng ký thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        
            


        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)//Khi con trỏ ở dong textBox2 sử dụng Enter sẽ đồng thời thực thi button1_click
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs()) ;
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//Link liên kết để gọi 1 form mới. ở đây gọi form login đồng thời đóng form sign_up
        {
            sign_up sg = new sign_up();
            sg.Close();
            login frm2 = new login();
            frm2.Show();
            this.Hide();
            
        }

        private void sign_up_FormClosed(object sender, FormClosedEventArgs e)//Form sign_up đóng thì form login đồng thời được gọi lên.
        {
            login lg = new login();
            lg.Close();
        }

        private void sign_up_FormClosing(object sender, FormClosingEventArgs e)
        {
            login lg = new login();
            lg.Close();
        }

        private void btn_exit_Click(object sender, EventArgs e)//Khi click btn_exit_click thì sẽ trả về form login
        {
            login lg1 = new login();
            lg1.Show();
            this.Hide();
        }

        private void txt_email4_KeyDown(object sender, KeyEventArgs e)//Khi tới dòng txt_email4 ta gõ enter thì button1_click sẽ thực thi.
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }
       private bool IsValidEmail(string email)//Kiểm tra Email phải nhập với đinh dạng "abc@gmail.com" hoặc "abc@avc.avc"
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
