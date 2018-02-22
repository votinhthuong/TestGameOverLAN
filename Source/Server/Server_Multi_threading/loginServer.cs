using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_Multi_threading
{
    public partial class loginServer : Form
    {
        public loginServer()
        {
            InitializeComponent();
        }

        
        //Đóng cửa sổ chương trình
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Khi người dùng nhấn START, ta truyền 2 giá trị vừa nhập ở 2 textbox qua cho form chính sẽ được gọi lên tiếp theo.
        //Đồng thời, form hiện tại bị ẩn đi. Do đó, khi tắt form chính, thì chương trình vẫn còn chạy ngầm. Phải tắt hẳn bằng Stop Debug.
        private void btn_login_Click(object sender, EventArgs e)
        {
            frm_server frmServer = new frm_server(txt_serverIP.Text,txt_limitConnection.Text);
            frmServer.Show();
            this.Hide();
            
        }

        private void loginServer_Load(object sender, EventArgs e)
        {

        }

        //Thay vì nhấn START, ta có thể nhấn Enter ngay tại textbox thứ 2 để xử lý.
        private void txt_limitConnection_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                btn_login_Click(this, new EventArgs());
            }
        }
    }
}
