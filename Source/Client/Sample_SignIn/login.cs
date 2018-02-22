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

namespace Sample_SignIn
{
    public partial class login : Form
    {
        SqlConnection con;

        public login()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//Link liên kết mở form sign_up khi click vào, đồng thời form login sẽ ẩn đi.
        {
            
            sign_up dk = new sign_up();
            dk.Show();
            login lg1 = new login();
            lg1.Close();
            this.Hide();
            //this.Visible = false;
            //this.Close();

        }

        private void login_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=DESKTOP-MJLTEC9;Initial Catalog=Chatlan;User ID=sa;Password=sa2014");
            //con = new SqlConnection("Data Source=192.168.43.117;Initial Catalog=Chatlan;User ID=sa;Password=sa2014");
            //con = new SqlConnection("Data Source=MR_DUY\\SQLEXPRESS;Initial Catalog=Chatlan;User ID=sa;Password=sa2014");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                main_form mf1 = new main_form(textBox1.Text);
                //String str = "insert into nguoidung(Username, Passwd) values ('" + textBox1.Text + "', '" + textBox2.Text + "')";
                //insert into nguoidung(Username, Passwd) values('thuongvt', '12345')
                string str = "select count(*) from nguoidung where Username='" + textBox1.Text + "'and Passwd='" + textBox2.Text + "'";
                //Truy vấn trong bảng người dùng có Username, Passwd đã nhập trong form không?
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();

                SqlDataAdapter sqldata = new SqlDataAdapter(str, con);
                DataTable dtbl = new DataTable();
                sqldata.Fill(dtbl);
                if (dtbl.Rows[0][0].ToString() == "1")//kiểm tra Username, Passwd vừa nhập có đúng trong dữ liệu SQL không. Đúng thì cho đăng nhập, khi đăng nhập thành công form main_form được thực thi.
                {
                    main_form mf = new main_form(textBox1.Text);
                    mf.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Vui lòng kiểm tra lại tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Đăng nhập thất bại !");
            }

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)//khi tới trường textBox2 được sử dụng xong, gõ Enter thì button_click thực hiện. 
        {
            if(e.KeyCode==Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//Link liên kết mở form Forgot_Passwd khi click vào, đồng thời form login sẽ ẩn đi.
        {
            Forgot_Passwd fg = new Forgot_Passwd();
            fg.Show();
            this.Hide();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//Link liên kết mở form Login_Change_Passwd khi click vào, đồng thời form login sẽ ẩn đi.
        {
            Login_Change_Passwd lcp = new Login_Change_Passwd();
            login lg1 = new login();
            lcp.Show();
            lg1.Close();
            this.Hide();
        }
    }
}
