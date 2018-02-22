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
    public partial class Login_Change_Passwd : Form
    {
        public Login_Change_Passwd()
        {
            InitializeComponent();
        }
        SqlConnection con;
        private void Login_Change_Passwd_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=DESKTOP-MJLTEC9;Initial Catalog=Chatlan;User ID=sa;Password=sa2014");

            txt_passwd1.Enabled = false;//Khi form Login_Change_Passwd hiển thị thì trường txt_passwd1 sẽ bị mờ và không sử dụng được.
            btn_login.Enabled = false;//Khi form Login_Change_Passwd hiển thị thì btn_login sẽ bị mờ và không sử dụng được.
           
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //String str = "insert into nguoidung(Username, Passwd) values ('" + textBox1.Text + "', '" + textBox2.Text + "')";
                //insert into nguoidung(Username, Passwd) values('thuongvt', '12345')
                string str = "select count(*) from nguoidung where Username='" + txt_usernam2.Text + "'and Passwd='" + txt_passwd1.Text + "'";
                //Truy vấn trong bảng "nguoidung" có dòng Username, Passwd vừa nhập không ?
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();

                SqlDataAdapter sqldata = new SqlDataAdapter(str, con);
                DataTable dtbl = new DataTable();
                sqldata.Fill(dtbl);
                if (dtbl.Rows[0][0].ToString() == "1")//Nếu Username và Passwd đúng thì truy xuất tới form Infomation_Account
                {
                    Information_Account inform = new Information_Account(txt_usernam2.Text);
                    inform.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Vui lòng kiểm tra lại tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Đăng nhập thất bại","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_passwd1_TextChanged(object sender, EventArgs e)//Khi trường txt_passwd1 được sử dụng(nhập dữ liệu trong trường) thì đồng thời btn_login sẽ không còn mờ và sử dụng được.
        {
            btn_login.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)//Khi chọn vào button1_click thì form login sẽ hiện thị đồng thới ẩn form vừa sử dụng đi.
        {
            login lg1 = new login();
            lg1.Show();
            this.Close();
        }

        private void txt_usernam2_TextChanged(object sender, EventArgs e)//khi trường txt_username2 được sử dụng(nhập dữ liệu trong trường) thì đồng thời trường txt_passwd1 cũng sử dụng tiếp theo
        {
            txt_passwd1.Enabled = true;
        }

        private void txt_passwd1_KeyDown(object sender, KeyEventArgs e)//Khi tới txt_passwd1 được sử dụng xong, ta gõ Enter thì btn_login_click được thực thi. 
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login_Click(this, new EventArgs());
            }
        }


    }
}
