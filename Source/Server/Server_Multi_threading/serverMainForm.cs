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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlClient;
using System.Collections;
using System.Text.RegularExpressions;

namespace Server_Multi_threading
{
    public partial class frm_server : Form
    {
        //Khởi tạo biến kết nối
        SqlConnection con;

        //Phần code này để xử lý cập nhật dữ liệu cho ListView hiển thị dữ liệu nhận được từ các form khác nhau. 
        //Vì ở các phiên bản code sau này bảo mật hơn nên phải có các phương thức này để tránh xảy ra lỗi.
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
        //Khai báo các biến nhận được từ form khởi tạo truyền qua
        string setIP;
        string limitCon;
        public frm_server(string serverIp, string limit)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //Để chạy được CSDL ở máy khác, thì thay dòng Data Source bằng tên của CSDL máy đó, User ID và cả Password tương ứng.
            con = new SqlConnection("Data Source=DESKTOP-MJLTEC9;Initial Catalog=Chatlan;User ID=sa;Password=sa2014");            
            setIP = serverIp;
            limitCon = limit;
        }

        private void frm_server_Load(object sender, EventArgs e)
        {
            //Gọi hàm xử lý chờ kết nối
            Connect();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            //Đổ dữ liệu câu hỏi vào dataGridView của chương trình để sử dụng ở các bước sau, khỏi phải xuống CSDL lấy lên từng lần một
            //sẽ làm ảnh hưởng tốc độ chạy của chương trình
            DataTable dtbl = new DataTable();
            String str = "select CauHoi from Dethi";
            SqlCommand cmd = new SqlCommand(str, con);

            SqlDataAdapter sqldata = new SqlDataAdapter(str, con);

            sqldata.Fill(dtbl);
            dataGridView1.DataSource = dtbl;

                      

            if (con.State == ConnectionState.Open)
            {
                con.Close();
                return;
            }
        }       

        //Khai báo các biến chung cho việc xử lý kết nối đến
        IPEndPoint ipep;
        Socket server;
        List<Socket> clientList;

        //Hàm xử lý kết nối đến. Cứ mỗi kết nối đến, sẽ được tạo ra 1 thread riêng để hoạt động
        void Connect()
        {
            try
            {
                clientList = new List<Socket>();
                ipep = new IPEndPoint(IPAddress.Parse(setIP), 9050);
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Bind(ipep);
                ListAddItem("Đang chờ client kết nối đến...");
                lbl_ipServer.Text = ipep.Address.ToString();
                Thread Listen = new Thread(Ketnoi);
                Listen.IsBackground = true;
                Listen.Start();
            }
            catch
            {
                MessageBox.Show("Vui lòng cho biết IP Server!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            
            
        }
        //Khi có kết nối đến, ta kiểm tra xem đã đủ giới hạn quy định số client kết nối chưa, nếu chưa thì cho vào, ngược lại thì chặn lại.
        void Ketnoi()
        {
            try
            {
                //while (true)
                Parallel.For(0, Convert.ToInt32(limitCon), new ParallelOptions { MaxDegreeOfParallelism = 2 }, (i) =>
                     {
                         server.Listen(100);
                         Socket client = server.Accept();
                         byte[] data = new byte[1024];
                         //Khi đã được chấp nhận kết nối, sẽ add client đó vào mảng danh sách các client để kiểm soát. 
                         recv = client.Receive(data);
                         stringData = Encoding.UTF8.GetString(data);                         
                         ListAddItem("Đã kết nối thành công với: " + stringData);
                         clientList.Add(client);   
                         //Bắt đầu lắng nghe dữ liệu đến.               
                         Thread rev = new Thread(DataReceive);
                         rev.IsBackground = true;
                         rev.Start(client);
                     });
            }
            catch
            {
                ipep = new IPEndPoint(IPAddress.Parse(textBox1.Text), 9050);
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
        }

        //Hàm xử lý cho việc lắng nghe các dữ liệu gửi lên từ các client.
        private int recv;
        private string stringData;
        List<int> chungket = new List<int>();
        List<string> ctrLoi = new List<string>();
        string winner;
        int max = Int32.MinValue;
        void DataReceive(object obj)
        {
            
            Socket client = obj as Socket;
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024];
                    recv = client.Receive(data);
                    stringData = Encoding.UTF8.GetString(data);                    
                    string[] split = stringData.Split(new Char[] { ':', ' ' });
                    int nt = Convert.ToInt32(split[2]);
                    chungket.Add(nt);
                    ctrLoi.Add(stringData);
                    ListAddItem(stringData);
                    //Khi nhận được câu trả lời - tức là điểm số của các client, thì sẽ tách phần message nhận được ra làm 2 phần bao gồm tên client và điểm đi kèm.
                    //Sau đó kiểm tra số lượng message nhận được từ các client có bằng với số client limit ban đầu không. Nếu bằng thì gọi hàm xử lý điểm.
                    if(chungket.Count==Convert.ToInt32(limitCon))
                    {
                        tinhdiem();
                    }
                }
            }
            catch
            {
                clientList.Remove(client);
                client.Close();
            }
        }
        //Kiểm tra xem trong mảng chứa các điểm số của các client gửi lên, điểm của ai là lớn nhất.
        void tinhdiem()
        {
            for (var i = 0; i < chungket.Count; i++)
            {
                if (chungket[i] > max)
                {
                    max = chungket[i];
                }                
            }
            //Nếu xác định được điểm lớn nhất rồi, thì kiểm tra điểm đó là của client nào trong danh sách nhận được ta có ở phía trên.             
            foreach(string item in ctrLoi)
            {
                if(item.Contains(max.ToString()))
                {
                    ListAddItem("The winner is: "+item);
                    winner = item;
                    break;
                }
                else
                {

                }
            }
            //Gửi kết quả người thắng chung cuộc tới cho các client biết kết quả.
            data = new byte[1024];
            data = Encoding.UTF8.GetBytes("The winner is: "+winner);
            foreach (Socket item in clientList)
            {                
                item.Send(data);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Connect();
        }
        
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                Connect();
            }
        }
        byte[] data;
        byte[] data1;
        
        //Khi nhấn GAME ON, bộ đếm sẽ bắt đầu đếm lùi thời gian.
        int timeLeft;
        ArrayList choice;
        private void button2_Click(object sender, EventArgs e)
        {
            timeLeft = 100;
            timeLabel.Text = "100 seconds";
            timer1.Start();
        }
        int socau;        
        //Cứ mỗi giây chẵn, vì mỗi câu có 10 giây, sẽ gửi câu hỏi xuống cho các client chơi.
        private void timer1_Tick(object sender, EventArgs e)
        {
            socau = dataGridView1.RowCount/2;
            if (timeLeft == 100)
            {
                data = new byte[1024];
                data1 = new byte[1024];
                data = Encoding.UTF8.GetBytes(dataGridView1.Rows[0].Cells[0].Value.ToString()+ dataGridView1.Rows[1].Cells[0].Value.ToString()+ "| . 1/"+socau);
                foreach (Socket item in clientList)
                {
                    item.Send(data);
                }
            }
            if (timeLeft == 90)
            {                
                data = new byte[1024];
                data = Encoding.UTF8.GetBytes(dataGridView1.Rows[2].Cells[0].Value.ToString()+ dataGridView1.Rows[3].Cells[0].Value.ToString() + "| . 2/" + socau);
                foreach (Socket item in clientList)
                {
                    item.Send(data);
                }
            }
            if (timeLeft == 80)
            {
                data = new byte[1024];
                data = Encoding.UTF8.GetBytes(dataGridView1.Rows[4].Cells[0].Value.ToString() + dataGridView1.Rows[5].Cells[0].Value.ToString() + "| . 3/" + socau);
                foreach (Socket item in clientList)
                {
                    item.Send(data);
                }
            }
            if (timeLeft == 70)
            {
                data = new byte[1024];
                data = Encoding.UTF8.GetBytes(dataGridView1.Rows[6].Cells[0].Value.ToString() + dataGridView1.Rows[7].Cells[0].Value.ToString() + "| . 4/" + socau);
                foreach (Socket item in clientList)
                {
                    item.Send(data);
                }
            }
            if (timeLeft == 60)
            {
                data = new byte[1024];
                data = Encoding.UTF8.GetBytes(dataGridView1.Rows[8].Cells[0].Value.ToString() + dataGridView1.Rows[9].Cells[0].Value.ToString() + "| . 5/" + socau);
                foreach (Socket item in clientList)
                {
                    item.Send(data);
                }
            }
            if (timeLeft == 50)
            {
                data = new byte[1024];
                data = Encoding.UTF8.GetBytes(dataGridView1.Rows[10].Cells[0].Value.ToString() + dataGridView1.Rows[11].Cells[0].Value.ToString() + "| . 6/" + socau);
                foreach (Socket item in clientList)
                {
                    item.Send(data);
                }
            }
            if (timeLeft == 40)
            {
                data = new byte[1024];
                data = Encoding.UTF8.GetBytes(dataGridView1.Rows[12].Cells[0].Value.ToString() + dataGridView1.Rows[13].Cells[0].Value.ToString() + "| . 7/" + socau);
                foreach (Socket item in clientList)
                {
                    item.Send(data);
                }
            }
            if (timeLeft == 30)
            {
                data = new byte[1024];
                data = Encoding.UTF8.GetBytes(dataGridView1.Rows[14].Cells[0].Value.ToString() + dataGridView1.Rows[15].Cells[0].Value.ToString() + "| . 8/" + socau);
                foreach (Socket item in clientList)
                {
                    item.Send(data);
                }
            }
            if (timeLeft == 20)
            {
                data = new byte[1024];
                data = Encoding.UTF8.GetBytes(dataGridView1.Rows[16].Cells[0].Value.ToString() + dataGridView1.Rows[17].Cells[0].Value.ToString() + "| . 9/" + socau);
                foreach (Socket item in clientList)
                {
                    item.Send(data);
                }
            }
            if (timeLeft == 10)
            {
                data = new byte[1024];
                data = Encoding.UTF8.GetBytes(dataGridView1.Rows[18].Cells[0].Value.ToString() + dataGridView1.Rows[19].Cells[0].Value.ToString() + "| . 10/" + socau);
                foreach (Socket item in clientList)
                {
                    item.Send(data);
                }
            }
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                timer1.Stop();
            }
        }

        private void lst_show_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
