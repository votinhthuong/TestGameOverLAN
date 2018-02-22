using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample_SignIn
{
    public partial class Forgot_Passwd : Form
    {
        public Forgot_Passwd()
        {
            InitializeComponent();
        }
        SqlConnection con;
        private void button1_Click(object sender, EventArgs e)
        {
           
            if (txt_email.Text.ToString().Equals(""))
            {
                MessageBox.Show("Email trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            con = new SqlConnection("Data Source=DESKTOP-MJLTEC9;Initial Catalog=Chatlan;User ID=sa;Password=sa2014");
            //con = new SqlConnection("Data Source=192.168.43.117;Initial Catalog=Chatlan;User ID=sa;Password=sa2014");
            //con = new SqlConnection("Data Source=MR_DUY\\SQLEXPRESS;Initial Catalog=Chatlan;User ID=sa;Password=sa2014");
            con.Open();
            string str = "select Email from nguoidung where Username='" + txt_username1.Text.ToString() + "'";
            //Truy vấn với Email trong bảng "nguoidung" tại Username tương ứng trong dữ liệu SQL.
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader reader = cmd.ExecuteReader();

            String email = "";
            while (reader.Read())
            {
                email = reader[0].ToString();//Đọc dữ liệu từng dòng trên dữ liệu SQL trong while()
            }

            //reader.Read();
            //email = reader[0].ToString();

            con.Close();
            if (txt_email.Text.ToString().Equals(email))
            {
                //gửi mail
                email_send(txt_email.Text.ToString());
                //MessageBox.Show("Đã gửi Email thành công ! Kiểm tra lại Email !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                
                
            }
            else
            {
                MessageBox.Show("Không tồn tại email tương ứng với tài khoản !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        NetworkCredential login;
        SmtpClient client;
        MailMessage msg;
        Random rd = new Random();
        //Khởi tạo một biến số bất kỳ bằng hàm Random, dãy giá trị trong khoảng từ 1000 tới 9000. 
        //Khi gửi mail, thì gửi biến số này tới cho email của người dùng. 
        //ĐỒNG THỜI, ghi xuống CSDL biến này vào cột password của account tương ứng email đã gửi. 
        int rand;
        public void email_send(String email)
        {

            rand = rd.Next(1000, 9000);
            login = new NetworkCredential("sae.mitsuko@gmail.com", "meomeo$$$");
            client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = login;
            msg = new MailMessage { From = new MailAddress("sae.mitsuko@gmail.com", "Little Talk", Encoding.UTF8) };
            msg.To.Add(new MailAddress(txt_email.Text));
            msg.Subject = "REQUEST FOR RECOVER YOUR PASSWORD!";
            //Email khôi phục mật khẩu có sử dụng các thuộc tính thẻ HTML nên phần body sẽ dài nhất.
            msg.Body = "<table border='0' cellspacing='0' cellpadding='0' style='max-width:600px'><tbody><tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tbody><tr><td align='left'></td><td align='right'><img width='32' height='32' style='display:block;width:32px;height:32px' src='https://ci3.googleusercontent.com/proxy/M66ZNacPlHAzr1syxHojC07BuO63gs0WeUx82IAyCm74zrziOOWb2InfAWL4PI5pkUNG4LG2jaZGZZ-l8d1ogxMxKRf7zQXAhtGygw=s0-d-e1-ft#https://www.gstatic.com/accountalerts/email/shield.png' class='CToWUd'></td></tr></tbody></table></td></tr><tr height='16'></tr><tr><td><table bgcolor='#D94235' width='100%' border='0' cellspacing='0' cellpadding='0' style='min-width:332px;max-width:600px;border:1px solid #f0f0f0;border-bottom:0;border-top-left-radius:3px;border-top-right-radius:3px'><tbody><tr><td height='72px' colspan='3'></td></tr><tr><td width='32px'></td><td style='font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:24px;color:#ffffff;line-height:1.25;min-width:300px'>Your password changed</td><td width='32px'></td></tr><tr><td height='18px' colspan='3'></td></tr></tbody></table></td></tr><tr><td><table bgcolor='#FAFAFA' width='100%' border='0' cellspacing='0' cellpadding='0' style='min-width:332px;max-width:600px;border:1px solid #f0f0f0;border-bottom:1px solid #c0c0c0;border-top:0;border-bottom-left-radius:3px;border-bottom-right-radius:3px'><tbody><tr height='16px'><td width='32px' rowspan='3'></td><td></td><td width='32px' rowspan='3'></td></tr><tr><td><table style='min-width:300px' border='0' cellspacing='0' cellpadding='0'><tbody><tr><td style='font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5;padding-bottom:4px'>Hi " + txt_username1.Text + ",</td></tr><tr><td style='font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5;padding:4px 0'>The password for your <span class='il'>Little Talk</span> Account was recently changed.<br><br><b>Thank you for using my service!</b><br><h2>This is your new password: " + rand + "</h2></td></tr><tr><td style='font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5;padding-top:28px'>The <span class='il'>Little Talk</span> Accounts team: Vo Tinh Thuong - Pham Dang Thien Duy</td></tr><tr height='16px'></tr><tr><td><table style='font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:12px;color:#b9b9b9;line-height:1.5'><tbody><tr><td>This email can't receive replies. For more information, visit the <a href='https://votinhthuong.net' style='text-decoration:none;color:#4285f4' target='_blank'><span class='il'>Little Talk</span> Accounts Help Center</a>.</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr height='32px'></tr></tbody></table></td></tr><tr height='16'></tr><tr><td style='max-width:600px;font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:10px;color:#bcbcbc;line-height:1.5'></td></tr><tr><td><table style='font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:10px;color:#666666;line-height:18px;padding-bottom:10px'><tbody><tr><td>You received this mandatory email service announcement to update you about important changes to your <span class='il'>account.</span></td></tr><tr height='6px'></tr><tr><td><div style='direction:ltr;text-align:left'>© 2017 <span class='il'>Vo Tinh Thuong, Pham Dang Thien Duy</span>, <a href='https://votinhthuong.net'>140, Le Trong Tan Street, Tan Phu District, Ho Chi Minh City</a></div><div style='display:none!important;max-height:0px;max-width:0px'>et:1</div></td></tr></tbody></table></td></tr></tbody></table>";
            //Phải bật thuộc tính này lên thì mới gửi mail có thuộc tính HTML được.
            msg.IsBodyHtml = true;

            msg.Priority = MailPriority.High;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.SendCompleted += new SendCompletedEventHandler(SendCallback);
            client.SendAsync(msg, "Đang gửi mail...");     
        }
       
        private void SendCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show(string.Format("Email bị hủy: {0}", e.UserState));
            if (e.Error != null)
                MessageBox.Show(string.Format("Có lỗi khi gửi! Xem lại đường truyền: {0}", e.UserState));
            else
            {
                //Nếu gửi mail thành công thì mới ghi password mới xuống CSDL
                con.Open();
                MessageBox.Show("Email đã gửi thành công!");
                string str = "update nguoidung set Passwd='" + rand.ToString() + "' where Username='" + txt_username1.Text.ToString() + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                con.Close();
                login lg = new login();
                lg.Show();
                this.Hide();
            }

        }
        private void txt_username1_TextChanged(object sender, EventArgs e)//Khi trường txt_username1 được sử dụng đồng thời trường txt_email được phép sử dụng.
        {
            txt_email.Enabled = true;

        }

        private void Forgot_Passwd_Load(object sender, EventArgs e)//Khi form Forgot_Passwd được gọi thì trường txt_email,button1 sẽ bị mờ và không sử dụng được.
        {
            txt_email.Enabled = false;
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)//Khi button2_Click thì sẽ gọi form login đồng thời đóng form vừa được sử dụng.
        {
            login lg1 = new login();
            lg1.Show();
            this.Close();
        }

        private void txt_email_TextChanged(object sender, EventArgs e)//Khi trường txt_email được sử dụng thì đồng thới button1 được phép sử dụng.
        {
            button1.Enabled = true;
        }

        private void txt_email_KeyDown(object sender, KeyEventArgs e)//Khi con trỏ ở trường txt_email sử dụng xong, ta gõ Enter thì button1_ Click được thực thi.
        {
            if (e.KeyCode == Keys.Enter)
            {
               button1_Click(this, new EventArgs());
            }
        }

    }
}
