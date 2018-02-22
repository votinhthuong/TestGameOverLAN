using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;

namespace Sample_SignIn
{
    
    public partial class main_form : Form
    {                
        private IPEndPoint ipep;
        private Socket client;

        
       
        public main_form(string tennd)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //Gán tên người dùng là tên đăng nhập ở form Login để nhận diện đang đăng nhập bằng tài khoản nào
            label2.Text = tennd;
        }
       

        
        private void main_form_Load(object sender, EventArgs e)
        {
            timeleft = 50;            
        }
        //Hàm xử lý việc gửi kết nối đến Server
        void Connect()
        {
            try
            {
                ipep = new IPEndPoint(IPAddress.Parse(textBox1.Text), 9050);
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    client.Connect(ipep);
                    ListAddItem("Đã kết nối thành công!");
                    data = new byte[1024];
                    data = Encoding.UTF8.GetBytes(label2.Text);
                    client.Send(data);
                }
                catch
                {
                    ListAddItem("Không tìm thấy sever!");
                    return;
                }
                Thread listen = new Thread(Recieve);
                listen.IsBackground = true;
                listen.Start();
            }
            catch
            {
                MessageBox.Show("Vui lòng cho biết IP của server!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            
        }
   
        //Khi gửi data lên server, sẽ gửi kèm cả biến tên người dùng để server dễ dàng biết đó là client nào.
        private byte[] data;
        private string stringData;
        void Send()
        {
            try
            {
                if (txt_input.Text == "exit")
                    this.Close();
                else
                {
                    if (txt_input.Text != string.Empty)
                    {
                        data = new byte[1024];
                        data = Encoding.UTF8.GetBytes(label2.Text+": "+ txt_input.Text);
                        client.Send(data);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Chưa kết nối với server!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        List<string> dapan = new List<string>();
        int timeleft;
        int i = 0;
        //Bắt đầu nhận dữ liệu câu hỏi cho trò chơi.
        void Recieve()
        {
            i++;         
            try
            {               
                while (true)
                {                    
                    timer1.Start();
                    byte[] data = new byte[1024 * 5000];
                    client.Receive(data);
                    stringData = Encoding.UTF8.GetString(data);

                    //Tách data nhận được ra các phần khác nhau nhờ vào Regex ta thiết lập quy định trong CSDL.
                    var substrings = Regex.Split(stringData, @"\|?\s+(?=\.)");
                    //Chỉ show lên Listview phần đầu của mảng chuỗi nhận được, tức là câu hỏi
                    ListAddItem(substrings[0]);                
                    //Phần thứ 2, 3, 4 và 5 của mảng tách ra được lần lượt chính là các đáp án cho client lựa chọn.
                    radioButton1.Text = substrings[1].Replace(".", string.Empty);
                    radioButton2.Text = substrings[2].Replace(".", string.Empty);
                    radioButton3.Text = substrings[3].Replace(".", string.Empty);
                    radioButton4.Text = substrings[4].Replace(".", string.Empty);
                    //Phần này chính là đáp án đúng của câu hỏi, sẽ lưu trữ riêng ra và chỉ dùng khi client bấm vào nút KẾT QUẢ
                    dapan.Add(substrings[5].Replace("\0", string.Empty).Replace(".", string.Empty));
                    timeLabel.Text =substrings[6].Replace(".", string.Empty);                    
                }                
            }
            catch
            {

            }
        }

        delegate void UpdateStringinTheGrid(string s);

        private void ListAddItem(string s)
        {
            if (lst_show.InvokeRequired)
            {
                var updateDel = new UpdateStringinTheGrid(ListAddItem);
                this.BeginInvoke(updateDel, s);
            }
            else
                lst_show.Items.Add(s);

        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            Connect();
           
        }

        private void txt_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Send();
                txt_input.Clear();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                Connect();
            }
        }
        //Xử lý việc bấm nút TRẢ LỜI của client. Nếu chọn radio button nào thì ghi nhận lại vào một Listview riêng để sau khi chơi xong,
        //ta sẽ so sánh các lựa chọn với đáp án đúng.
        List<string> chosen = new List<string>();
        private void btn_finish_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                chosen.Add(radioButton1.Text);
                listView1.Items.Add(radioButton1.Text);
            }
            if (radioButton2.Checked)
            {
                chosen.Add(radioButton2.Text);
                listView1.Items.Add(radioButton2.Text);
            }
            if (radioButton3.Checked)
            {
                chosen.Add(radioButton3.Text);
                listView1.Items.Add(radioButton3.Text);
            }
            if (radioButton4.Checked)
            {
                chosen.Add(radioButton4.Text);
                listView1.Items.Add(radioButton4.Text);
            }
        }
        //Xử lý tính điểm. So sánh đáp án đúng từ server với các lựa chọn của người dùng.
        //Khởi đầu mỗi client có 10 điểm. Nếu trả lời đúng thì được cộng 1 điểm, sai thì giữ nguyên điểm hiện tại.
        int point = 10;
        private void btn_result_Click(object sender, EventArgs e)
        {
            for(int i=0;i<chosen.Count;i++)
            {
                for(int j=0; j<dapan.Count;j++)
                {
                    if (chosen[i]==dapan[j])
                    {
                        point++;
                    }
                    else
                    {
                        //Nếu sai thì không làm gì cả!
                    }
                }
            }
            foreach (var item in dapan)
            {
                listView2.Items.Add(item);
            }
            Send1();
            lst_show.Clear();

        }
        //Gửi lên server điểm của client.
        void Send1()
        {
            try
            {
                data = new byte[1024];
                data = Encoding.UTF8.GetBytes(label2.Text+": "+point.ToString());
                client.Send(data);
            }
            catch
            {
                MessageBox.Show("Chưa kết nối với server!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        //Xử lý hiển thị thời gian đếm lùi.
        private void timer1_Tick(object sender, EventArgs e)
        {            
            if (timeleft > 0)
            {               
                timeleft = timeleft - 1;
                timeLabel.Text = timeleft + " seconds";
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Time's up!");
            }
        }
    }
}
