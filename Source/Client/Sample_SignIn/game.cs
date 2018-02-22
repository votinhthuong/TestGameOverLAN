using System;
using System.Collections;
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
    public partial class game : Form
    {
        SqlConnection con;
        public game()
        {
            InitializeComponent();
            con = new SqlConnection("Data Source=DESKTOP-MJLTEC9;Initial Catalog=Chatlan;User ID=sa;Password=sa2014");
            //con = new SqlConnection("Data Source=192.168.43.117;Initial Catalog=Chatlan;User ID=sa;Password=sa2014");
        }

        private void game_Load(object sender, EventArgs e)
        {
            
            this.cauHoiTableAdapter.Fill(this.chatlanDataSet.CauHoi);

            if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

            DataTable dtbl = new DataTable();
            String str = "select CauHoi from TracNghiem";
            SqlCommand cmd = new SqlCommand(str, con);
            
            SqlDataAdapter sqldata = new SqlDataAdapter(str, con);

            sqldata.Fill(dtbl);
            dataGridView1.DataSource = dtbl;
            
            ////////////////////////////////////////////////////////////////////

            DataTable dtbl1 = new DataTable();
            String str1 = "select DapAn1,DapAn2,DapAn3,DapAn4 from TracNghiem";
            SqlCommand cmd1 = new SqlCommand(str1, con);

            SqlDataAdapter sqldata1 = new SqlDataAdapter(str1, con);

            sqldata1.Fill(dtbl1);
            dataGridView2.DataSource = dtbl1;

            ///////////////////////////////////////////////////////////////////
            DataTable dtbl2 = new DataTable();
            String str2 = "select Answer from TracNghiem";
            SqlCommand cmd2 = new SqlCommand(str2, con);

            SqlDataAdapter sqldata2 = new SqlDataAdapter(str2, con);

            sqldata2.Fill(dtbl2);
            dataGridView3.DataSource = dtbl2;

            if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    return;
                }                        
        }
        int timeLeft;
        ArrayList choice;
        private void timer1_Tick(object sender, EventArgs e)
        {
            choice = new ArrayList();
            if(timeLeft==50)
            {                
                listView1.Items.Add(dataGridView1.Rows[0].Cells[0].Value.ToString());
                radioButton1.Text = dataGridView2.Rows[0].Cells[0].Value.ToString();
                radioButton2.Text = dataGridView2.Rows[0].Cells[1].Value.ToString();
                radioButton3.Text = dataGridView2.Rows[0].Cells[2].Value.ToString();
                radioButton4.Text = dataGridView2.Rows[0].Cells[3].Value.ToString();
                if(radioButton1.Checked)
                {
                    choice.Add(radioButton1.Text);
                }
                else if (radioButton2.Checked)
                {
                    choice.Add(radioButton2.Text);
                }
                else if (radioButton3.Checked)
                {
                    choice.Add(radioButton3.Text);
                }
                else
                {
                    choice.Add(radioButton4.Text);
                }
               
                
            }
            if (timeLeft == 40)
            {
                listView1.Clear();
                listView1.Items.Add(dataGridView1.Rows[1].Cells[0].Value.ToString());
                radioButton1.Text = dataGridView2.Rows[1].Cells[0].Value.ToString();
                radioButton2.Text = dataGridView2.Rows[1].Cells[1].Value.ToString();
                radioButton3.Text = dataGridView2.Rows[1].Cells[2].Value.ToString();
                radioButton4.Text = dataGridView2.Rows[1].Cells[3].Value.ToString();
                if (radioButton1.Checked)
                {
                    choice.Add(radioButton1.Text);
                }
                else if (radioButton2.Checked)
                {
                    choice.Add(radioButton2.Text);
                }
                else if (radioButton3.Checked)
                {
                    choice.Add(radioButton3.Text);
                }
                else
                {
                    choice.Add(radioButton4.Text);
                }
                //if(choice[0].ToString() == cau1)
            }
            if (timeLeft == 30)
            {
                listView1.Clear();
                listView1.Items.Add(dataGridView1.Rows[2].Cells[0].Value.ToString());
                radioButton1.Text = dataGridView2.Rows[2].Cells[0].Value.ToString();
                radioButton2.Text = dataGridView2.Rows[2].Cells[1].Value.ToString();
                radioButton3.Text = dataGridView2.Rows[2].Cells[2].Value.ToString();
                radioButton4.Text = dataGridView2.Rows[2].Cells[3].Value.ToString();
                if (radioButton1.Checked)
                {
                    choice.Add(radioButton1.Text);
                }
                else if (radioButton2.Checked)
                {
                    choice.Add(radioButton2.Text);
                }
                else if (radioButton3.Checked)
                {
                    choice.Add(radioButton3.Text);
                }
                else
                {
                    choice.Add(radioButton4.Text);
                }
            }
            if (timeLeft == 20)
            {
                listView1.Clear();
                listView1.Items.Add(dataGridView1.Rows[3].Cells[0].Value.ToString());
                radioButton1.Text = dataGridView2.Rows[3].Cells[0].Value.ToString();
                radioButton2.Text = dataGridView2.Rows[3].Cells[1].Value.ToString();
                radioButton3.Text = dataGridView2.Rows[3].Cells[2].Value.ToString();
                radioButton4.Text = dataGridView2.Rows[3].Cells[3].Value.ToString();
                if (radioButton1.Checked)
                {
                    choice.Add(radioButton1.Text);
                }
                else if (radioButton2.Checked)
                {
                    choice.Add(radioButton2.Text);
                }
                else if (radioButton3.Checked)
                {
                    choice.Add(radioButton3.Text);
                }
                else
                {
                    choice.Add(radioButton4.Text);
                }
            }
            if (timeLeft == 10)
            {
                listView1.Clear();
                listView1.Items.Add(dataGridView1.Rows[4].Cells[0].Value.ToString());
                radioButton1.Text = dataGridView2.Rows[4].Cells[0].Value.ToString();
                radioButton2.Text = dataGridView2.Rows[4].Cells[1].Value.ToString();
                radioButton3.Text = dataGridView2.Rows[4].Cells[2].Value.ToString();
                radioButton4.Text = dataGridView2.Rows[4].Cells[3].Value.ToString();
                if (radioButton1.Checked)
                {
                    choice.Add(radioButton1.Text);
                }
                else if (radioButton2.Checked)
                {
                    choice.Add(radioButton2.Text);
                }
                else if (radioButton3.Checked)
                {
                    choice.Add(radioButton3.Text);
                }
                else
                {
                    choice.Add(radioButton4.Text);
                }
            }

            if (timeLeft > 0)
            {
                // Display the new time left
                // by updating the Time Left label.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                // If the user ran out of time, stop the timer, show
                // a MessageBox, and fill in the answers.
                timer1.Stop();
                MessageBox.Show("Time's up!");                                            
            }
        }
        int diembd = 10;
        
        void DapAn()
        {
            timer1.Stop();
            string cau1 = dataGridView3.Rows[0].Cells[0].Value.ToString();
            string cau2 = dataGridView3.Rows[1].Cells[0].Value.ToString();
            string cau3 = dataGridView3.Rows[2].Cells[0].Value.ToString();
            string cau4 = dataGridView3.Rows[3].Cells[0].Value.ToString();
            string cau5 = dataGridView3.Rows[4].Cells[0].Value.ToString();
            if (choice[0].ToString() == cau1 )
            {
                diembd+=5;
            }
            //else if(choice[0].ToString() == cau1 && choice[1].ToString() != cau2 && choice[2].ToString() == cau3 && choice[3].ToString() == cau4 && choice[4].ToString() == cau5)
            else
            {
                diembd -= 5;
            }
            label1.Text = diembd.ToString();

            
        }
        private void startButton_Click(object sender, EventArgs e)
        {
           ////Câu hỏi cho vào khung hiển thị
           // string[,] DataValue = new string[dataGridView1.Rows.Count, dataGridView1.Columns.Count];
           // foreach (DataGridViewRow row in dataGridView1.Rows)
           // {
           //     foreach (DataGridViewColumn cols in dataGridView1.Columns)
           //     {
           //         DataValue[row.Index, cols.Index] = dataGridView1.Rows[row.Index].Cells[cols.Index].Value.ToString();
           //     }
           // }
           // int i = 0;
           // foreach (string s in DataValue)
           // {
           //     listView2.Items.Add(s);
           //     i++;
           // }
           // //Đáp án và câu trả lời đúng
           // string[,] DataValue1 = new string[dataGridView2.Rows.Count, dataGridView2.Columns.Count];
           // foreach (DataGridViewRow row in dataGridView2.Rows)
           // {
           //     foreach (DataGridViewColumn cols in dataGridView2.Columns)
           //     {
           //         DataValue1[row.Index, cols.Index] = dataGridView2.Rows[row.Index].Cells[cols.Index].Value.ToString();
           //     }
           // }
           // int j = 0;
           // foreach (string s in DataValue1)
           // {
           //     listView3.Items.Add(s);
           //     j++;
           // }
            timeLeft = 50;
            timeLabel.Text = "50 seconds";
            //timer1.Start();
        }

        private void Send_Click(object sender, EventArgs e)
        {
            //ArrayList choice1 = new ArrayList();
            //choice1.Add("hihi");
            //choice1.Add(dataGridView2.Rows[0].Cells[0].Value.ToString());
            //MessageBox.Show(choice1[0].ToString());
            //MessageBox.Show(choice[0].ToString());
            //foreach(var item in choice)
            //{
            //    MessageBox.Show(item.ToString());
            //}
            DapAn();
        }
    }
}
