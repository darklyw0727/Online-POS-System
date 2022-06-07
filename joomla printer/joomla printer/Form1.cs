using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.IO;

namespace joomla_printer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            checkBox1.Checked = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 10000;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ReFresh();
            filllist();
            latestDetail();
            latestDetailPrinter();
        }

        private void labeld1_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
                return;
            latestDetailPrinter();
            printDialog1.Document = printDocumentSerch;
            printDocumentLatest.Print();
        }

        private void buttonrF_Click(object sender, EventArgs e)
        {
            ReFresh();
            filllist();
            latestDetail();
            latestDetailPrinter();
        }

        private void buttonserch_Click(object sender, EventArgs e)
        {
            if (textBoxsr.Text == "")
            {
                MessageBox.Show("請輸入單號");
                return;
            }
            Serch();
            detail(10);
            detailPrinter(10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (labelf1.Text == "x")
            {
                MessageBox.Show("尚無資料匯入，請稍後再試");
                return;
            }
            detail(1);
            detailPrinter(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (labelf2.Text == "x")
            {
                MessageBox.Show("尚無資料匯入，請稍後再試");
                return;
            }
            detail(2);
            detailPrinter(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (labelf3.Text == "x")
            {
                MessageBox.Show("尚無資料匯入，請稍後再試");
                return;
            }
            detail(3);
            detailPrinter(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (labelf4.Text == "x")
            {
                MessageBox.Show("尚無資料匯入，請稍後再試");
                return;
            }
            detail(4);
            detailPrinter(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (labelf5.Text == "x")
            {
                MessageBox.Show("尚無資料匯入，請稍後再試");
                return;
            }
            detail(5);
            detailPrinter(5);
        }

        private void buttonPrintLatest_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("尚無資料匯入，請稍後再試");
                return;
            }
            printDialog1.Document = printDocumentSerch;
            printDocumentLatest.Print();
        }

        private void printDocumentLatest_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(textBoxlatestprint.Text, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 0, 0);
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("沒有查詢中的訂單，請查詢後再試");
                return;
            }
            printDialog1.Document = printDocumentSerch;
            printDocumentSerch.Print();
        }

        private void printDocumentSerch_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(textBoxSerchPrint.Text, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 0, 0);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = listBox1.SelectedItem.ToString();
            string[] No = item.Split(new char[2] { ' ', '\t' });
            textBoxsr.Text = No[0];
            Serch();
            detail(10);
            detailPrinter(10);
        }

        private void filllist()
        {
            listBox1.Items.Clear();
            string MYSQLConString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nfutopic_joomla";
            MySqlConnection databaseCon = new MySqlConnection(MYSQLConString);
            string query = "SELECT * FROM `jp5oj_virtuemart_orders` ORDER BY `jp5oj_virtuemart_orders`.`virtuemart_order_id` DESC";

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseCon);
            commandDatabase.CommandTimeout = 60;

            databaseCon.Open();
            MySqlDataReader myReader = commandDatabase.ExecuteReader();

            while (myReader.Read())
            {
                string number = myReader.GetString(3);
                DateTime time0 = Convert.ToDateTime(myReader.GetString(36));
                time0 = time0.ToLocalTime();
                string time = time0.ToString("MM/dd\tHH:mm");

                string item = number + "     \t" + time;
                listBox1.Items.Add(item);
            }
        }

        private void ReFresh()
        {
            string MYSQLConString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nfutopic_joomla";
            MySqlConnection databaseCon = new MySqlConnection(MYSQLConString);

            for (int i = 1; i < 6; i++)
            {
                //以下是大綱
                string si = i.ToString();
                string rF = "SELECT * FROM `jp5oj_virtuemart_orders` ORDER BY `virtuemart_order_id` DESC LIMIT " + si;

                MySqlCommand commandDatabase = new MySqlCommand(rF, databaseCon);
                commandDatabase.CommandTimeout = 60;
                
                databaseCon.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                while (myReader.Read())
                {
                    string a = myReader.GetString(3);
                    int c0 = myReader.GetInt32(7);
                    string c = Convert.ToString(c0);
                    //string d = myReader.GetString(36);
                    string e = myReader.GetString(29);
                    string f = myReader.GetString(30);
                    DateTime d0 = Convert.ToDateTime(myReader.GetString(36));
                    d0 = d0.ToLocalTime();

                    this.Controls.Find("labela" + i, false)[0].Text = a;
                    this.Controls.Find("labelc" + i, false)[0].Text = c;
                    this.Controls.Find("labeld" + i, false)[0].Text = Convert.ToString(d0);
                    if (e == "1")
                        this.Controls.Find("labele" + i, false)[0].Text = "現金";
                    else
                        this.Controls.Find("labele" + i, false)[0].Text = "轉帳";
                    if (f == "1")
                    {
                        this.Controls.Find("labelf" + i, false)[0].Text = "來店";
                        this.Controls.Find("labelf" + i, false)[0].BackColor = Color.DarkGray;
                        this.Controls.Find("labelf" + i, false)[0].ForeColor = Color.Black;
                    }
                    else
                    {
                        this.Controls.Find("labelf" + i, false)[0].Text = "外送";
                        this.Controls.Find("labelf" + i, false)[0].BackColor = Color.Black;
                        this.Controls.Find("labelf" + i, false)[0].ForeColor = Color.White;
                    }
                }
                databaseCon.Close();

                //以下是備註
                rF = "SELECT * FROM `jp5oj_virtuemart_order_userinfos` ORDER BY `virtuemart_order_id` DESC LIMIT " + si;

                commandDatabase = new MySqlCommand(rF, databaseCon);
                commandDatabase.CommandTimeout = 60;

                databaseCon.Open();
                myReader = commandDatabase.ExecuteReader();
                while (myReader.Read())
                {
                    string g = myReader.GetString(22);

                    if (g == "")
                        this.Controls.Find("labelg" + i, false)[0].Text = "";
                    else
                        this.Controls.Find("labelg" + i, false)[0].Text = g;
                }
                databaseCon.Close();
            }
        }

        private void Serch()
        {
            string serchNo = textBoxsr.Text;
            string MYSQLConString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nfutopic_joomla";

            MySqlConnection databaseCon = new MySqlConnection(MYSQLConString);
            string serchCode = "SELECT * FROM `jp5oj_virtuemart_orders` WHERE `order_number`=\'" + serchNo + "\'";
            MySqlCommand commandDatabase = new MySqlCommand(serchCode, databaseCon);
            commandDatabase.CommandTimeout = 60;

            //以下是serch大綱
            databaseCon.Open();
            MySqlDataReader myReader = commandDatabase.ExecuteReader();
            while (myReader.Read())
            {
                string s0 = myReader.GetString(0);
                labels10.Text = s0;
                string s1 = myReader.GetString(3);
                labela10.Text = s1;
                int s30 = myReader.GetInt32(7);
                string s3 = Convert.ToString(s30);
                labelc10.Text = s3;
                DateTime d10 = Convert.ToDateTime(myReader.GetString(36));
                labeld10.Text = Convert.ToString(d10.ToLocalTime());
                string s5 = myReader.GetString(29);
                if (s5 == "1")
                    labele10.Text = "現金";
                else
                    labele10.Text = "轉帳";
                string s6 = myReader.GetString(30);
                if (s6 == "1")
                {
                    labelf10.Text = "來店";
                    labelf10.BackColor = Color.White;
                    labelf10.ForeColor = Color.Black;
                }
                else
                {
                    labelf10.Text = "外送";
                    labelf10.BackColor = Color.Black;
                    labelf10.ForeColor = Color.White;
                }
            }
            databaseCon.Close();

            //以下是serch備註
            string sx = labels10.Text;
            serchCode = "SELECT * FROM `jp5oj_virtuemart_order_userinfos` WHERE `virtuemart_order_id`=" + sx;

            commandDatabase = new MySqlCommand(serchCode, databaseCon);
            commandDatabase.CommandTimeout = 60;

            databaseCon.Open();
            myReader = commandDatabase.ExecuteReader();
            while (myReader.Read())
            {
                string s7 = myReader.GetString(22);

                if (s7 == "")
                    labelg10.Text = "";
                else
                    labelg10.Text = s7;
            }
            databaseCon.Close();
            textBoxsr.Text = "";
        }

        private void detail(int i)
        {
            string msg = "";

            labela10.Text = this.Controls.Find("labela" + i, false)[0].Text;
            labelc10.Text = this.Controls.Find("labelc" + i, false)[0].Text;
            labeld10.Text = this.Controls.Find("labeld" + i, false)[0].Text;
            labele10.Text = this.Controls.Find("labele" + i, false)[0].Text;
            labelf10.Text = this.Controls.Find("labelf" + i, false)[0].Text;
            if (labelf10.Text == "外送")
                labelf10.BackColor = Color.Black;
            else
                labelf10.BackColor = Color.White;
            labelf10.ForeColor = this.Controls.Find("labelf" + i, false)[0].ForeColor;
            labelg10.Text = this.Controls.Find("labelg" + i, false)[0].Text;

            string detailNo = labela10.Text;
            string MYSQLConString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nfutopic_joomla";
            MySqlConnection databaseCon = new MySqlConnection(MYSQLConString);

            //以下是detail大綱
            string detailCode = "SELECT * FROM `jp5oj_virtuemart_orders` WHERE `order_number`=\'" + detailNo + "\'";
            MySqlCommand commandDatabase = new MySqlCommand(detailCode, databaseCon);
            commandDatabase.CommandTimeout = 60;

            databaseCon.Open();
            MySqlDataReader myReader = commandDatabase.ExecuteReader();
            while (myReader.Read())
            {
                labels10.Text = myReader.GetString(0);
                int ss10i = myReader.GetInt32(8);
                labelss10.Text = Convert.ToString(ss10i);
                int sss10i = myReader.GetInt32(19);
                labelsss10.Text = Convert.ToString(sss10i);

                string ship = "";
                if (labelf10.Text == "來店")
                    ship = "來店";
                else
                    ship = "【外送】";
                string number = labela10.Text;
                string line1 = ship + "\t" + "單號 : " + number;

                string line2 = "時間 : " + labeld10.Text;

                string line3 = "品項\t\t數量\t單價\t總額";

                msg += line1 + "\r\n\r\n" + line2 + "\r\n\r\n" + line3 + "\r\n";
            }
            databaseCon.Close();

            //以下是detial菜單
            string sx = labels10.Text;
            detailCode = "SELECT * FROM `jp5oj_virtuemart_order_items` WHERE `virtuemart_order_id`=" + sx;

            commandDatabase = new MySqlCommand(detailCode, databaseCon);
            commandDatabase.CommandTimeout = 60;

            databaseCon.Open();
            myReader = commandDatabase.ExecuteReader();

            while (myReader.Read())
            {
                int line40i = myReader.GetInt32(12);
                string line40 = Convert.ToString(line40i);
                int line41i = myReader.GetInt32(14);
                string line41 = Convert.ToString(line41i);
                string line4 = "";
                if (myReader.GetString(5).Length > 4)
                    line4 = myReader.GetString(5) + "\t" + myReader.GetString(6) + "\t" + line40 + "\t" + line41;
                else
                    line4 = myReader.GetString(5) + "\t\t" + myReader.GetString(6) + "\t" + line40 + "\t" + line41;

                string line4s = myReader.GetString(17);
                if (line4s == "")
                    msg += line4 + "\r\n";
                else
                {
                    string[] sArray = line4s.Split(new char[5] { '{', '"', ':', ',', '}' });
                    if (sArray.Length == 8)
                    {
                        string line44 = "";
                        if (sArray[5] == "22")
                            line44 = "**正常冰";
                        else if (sArray[5] == "23")
                            line44 = "**少冰";
                        else if (sArray[5] == "24")
                            line44 = "**微冰";
                        else if (sArray[5] == "25")
                            line44 = "**去冰";
                        else if (sArray[5] == "26")
                            line44 = "**熱";

                        msg += line4 + "\r\n" + line44 + "\r\n";
                    }
                    else if (sArray.Length == 14)
                    {
                        string line44 = "";
                        if (sArray[5] == "12")
                            line44 = "**正常冰";
                        else if (sArray[5] == "2")
                            line44 = "**正常冰";
                        else if (sArray[5] == "13")
                            line44 = "**少冰";
                        else if (sArray[5] == "3")
                            line44 = "**少冰";
                        else if (sArray[5] == "14")
                            line44 = "**微冰";
                        else if (sArray[5] == "4")
                            line44 = "**微冰";
                        else if (sArray[5] == "15")
                            line44 = "**去冰";
                        else if (sArray[5] == "5")
                            line44 = "**去冰";
                        else if (sArray[5] == "16")
                            line44 = "**熱";
                        else if (sArray[5] == "11")
                            line44 = "**熱";

                        string line45 = "";
                        if (sArray[11] == "6")
                            line45 = "全糖";
                        if (sArray[11] == "27")
                            line45 = "全糖";
                        else if (sArray[11] == "7")
                            line45 = "少糖(8分)";
                        else if (sArray[11] == "28")
                            line45 = "少糖(8分)";
                        else if (sArray[11] == "8")
                            line45 = "半糖(5分)";
                        else if (sArray[11] == "29")
                            line45 = "半糖(5分)";
                        else if (sArray[11] == "9")
                            line45 = "微糖(3分)";
                        else if (sArray[11] == "30")
                            line45 = "微糖(3分)";
                        else if (sArray[11] == "10")
                            line45 = "無糖";
                        else if (sArray[11] == "31")
                            line45 = "無糖";

                        msg += line4 + "\r\n" + line44 + " " + line45 + "\r\n";
                    }
                }
            }
            databaseCon.Close();

            //以下是detial結尾
            sx = labels10.Text;
            detailCode = "SELECT * FROM `jp5oj_virtuemart_order_userinfos` WHERE `virtuemart_order_id`=" + sx;

            commandDatabase = new MySqlCommand(detailCode, databaseCon);
            commandDatabase.CommandTimeout = 60;

            databaseCon.Open();
            myReader = commandDatabase.ExecuteReader();
            while (myReader.Read())
            {
                string line5 = "------------------------------------------------------------------";

                string line6 = "";
                if (labelsss10.Text == "0")
                    line6 = labele10.Text + "付款" + "\t\t\t" + "總金額 : " + labelc10.Text + " NT";
                else
                    line6 = labele10.Text + "付款" + "\t\t\t" + "原金額 : " + labelss10.Text + "\r\n\t\t\t" + "折扣 : " + labelsss10.Text + "\r\n\t\t\t" + "總金額 : " + labelc10.Text + " NT";

                string line7 = "";//備註
                if (myReader.GetString(22) == "")
                    line7 = "";
                else
                    line7 = "備註 :" + "\r\n" + myReader.GetString(22) + "\r\n\r\n";

                string line80 = "";//姓名稱呼
                if (myReader.IsDBNull(6))
                    line80 = "";
                else if (myReader.GetString(6) == "Mr")
                    line80 = " 先生";
                else
                    line80 = " 小姐";
                string line8 = "姓名 : " + myReader.GetString(8) + line80;

                string line9 = "電話 : " + myReader.GetString(10);//電話

                string line100 = "";//地址
                if (myReader.IsDBNull(5))
                    line100 = "";
                else
                    line100 = " (" + myReader.GetString(5) + ")";
                string line10 = "地址 : " + myReader.GetString(13) + "\r\n          " + line100;

                msg += line5 + "\r\n" + line6 + "\r\n\r\n" + line7 + line8 + "\r\n" + line9 + "\r\n" + line10;
                textBox2.Text = msg;
            }
            databaseCon.Close();
        }

        private void latestDetail()
        {
            string msg = "";

            string detailNo = labela1.Text;
            string MYSQLConString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nfutopic_joomla";
            MySqlConnection databaseCon = new MySqlConnection(MYSQLConString);

            //以下是latestdetail大綱
            string detailCode = "SELECT * FROM `jp5oj_virtuemart_orders` WHERE `order_number`=\'" + detailNo + "\'";
            MySqlCommand commandDatabase = new MySqlCommand(detailCode, databaseCon);
            commandDatabase.CommandTimeout = 60;

            databaseCon.Open();
            MySqlDataReader myReader = commandDatabase.ExecuteReader();
            while (myReader.Read())
            {
                labelLatestNo.Text = myReader.GetString(0);
                int ss10i = myReader.GetInt32(8);
                labelLatest0.Text = Convert.ToString(ss10i);
                int sss10i = myReader.GetInt32(19);
                labelLatest1.Text = Convert.ToString(sss10i);

                string ship = "";
                if (labelf1.Text == "來店")
                    ship = "來店";
                else
                    ship = "【外送】";
                string number = labela1.Text;
                string line1 = ship + "\t" + "單號 : " + number;

                string line2 = "時間 : " + labeld1.Text;

                string line3 = "品項\t\t數量\t單價\t總額";

                msg += line1 + "\r\n\r\n" + line2 + "\r\n\r\n" + line3 + "\r\n";
            }
            databaseCon.Close();

            //以下是latestdetial菜單
            string sx = labelLatestNo.Text;
            detailCode = "SELECT * FROM `jp5oj_virtuemart_order_items` WHERE `virtuemart_order_id`=" + sx;

            commandDatabase = new MySqlCommand(detailCode, databaseCon);
            commandDatabase.CommandTimeout = 60;

            databaseCon.Open();
            myReader = commandDatabase.ExecuteReader();
            while (myReader.Read())
            {
                int line40i = myReader.GetInt32(12);
                string line40 = Convert.ToString(line40i);
                int line41i = myReader.GetInt32(14);
                string line41 = Convert.ToString(line41i);
                string line4 = "";
                if (myReader.GetString(5).Length > 4)
                    line4 = myReader.GetString(5) + "\t" + myReader.GetString(6) + "\t" + line40 + "\t" + line41;
                else
                    line4 = myReader.GetString(5) + "\t\t" + myReader.GetString(6) + "\t" + line40 + "\t" + line41;

                string line4s = myReader.GetString(17);
                if (line4s == "")
                    msg += line4 + "\r\n";
                else
                {
                    string[] sArray = line4s.Split(new char[5] { '{', '"', ':', ',', '}' });
                    if (sArray.Length == 8)
                    {
                        string line44 = "";
                        if (sArray[5] == "22")
                            line44 = "**正常冰";
                        else if (sArray[5] == "23")
                            line44 = "**少冰";
                        else if (sArray[5] == "24")
                            line44 = "**微冰";
                        else if (sArray[5] == "25")
                            line44 = "**去冰";
                        else if (sArray[5] == "26")
                            line44 = "**熱";

                        msg += line4 + "\r\n" + line44 + "\r\n";
                    }
                    else if (sArray.Length == 14)
                    {
                        string line44 = "";
                        if (sArray[5] == "12")
                            line44 = "**正常冰";
                        else if (sArray[5] == "2")
                            line44 = "**正常冰";
                        else if (sArray[5] == "13")
                            line44 = "**少冰";
                        else if (sArray[5] == "3")
                            line44 = "**少冰";
                        else if (sArray[5] == "14")
                            line44 = "**微冰";
                        else if (sArray[5] == "4")
                            line44 = "**微冰";
                        else if (sArray[5] == "15")
                            line44 = "**去冰";
                        else if (sArray[5] == "5")
                            line44 = "**去冰";
                        else if (sArray[5] == "16")
                            line44 = "**熱";
                        else if (sArray[5] == "11")
                            line44 = "**熱";

                        string line45 = "";
                        if (sArray[11] == "6")
                            line45 = "全糖";
                        if (sArray[11] == "27")
                            line45 = "全糖";
                        else if (sArray[11] == "7")
                            line45 = "少糖(8分)";
                        else if (sArray[11] == "28")
                            line45 = "少糖(8分)";
                        else if (sArray[11] == "8")
                            line45 = "半糖(5分)";
                        else if (sArray[11] == "29")
                            line45 = "半糖(5分)";
                        else if (sArray[11] == "9")
                            line45 = "微糖(3分)";
                        else if (sArray[11] == "30")
                            line45 = "微糖(3分)";
                        else if (sArray[11] == "10")
                            line45 = "無糖";
                        else if (sArray[11] == "31")
                            line45 = "無糖";

                        msg += line4 + "\r\n" + line44 + " " + line45 + "\r\n";
                    }
                }
            }
            databaseCon.Close();

            //以下是latestdetial結尾
            sx = labelLatestNo.Text;
            detailCode = "SELECT * FROM `jp5oj_virtuemart_order_userinfos` WHERE `virtuemart_order_id`=" + sx;

            commandDatabase = new MySqlCommand(detailCode, databaseCon);
            commandDatabase.CommandTimeout = 60;

            databaseCon.Open();
            myReader = commandDatabase.ExecuteReader();
            while (myReader.Read())
            {
                string line5 = "------------------------------------------------------------------";

                string line6 = "";
                if (labelLatest1.Text == "0")
                    line6 = labele1.Text + "付款" + "\t\t\t" + "總金額 : " + labelc1.Text + " NT";
                else
                    line6 = labele1.Text + "付款" + "\t\t\t" + "原金額 : " + labelLatest0.Text + "\r\n\t\t\t" + "折扣 : " + labelLatest1.Text + "\r\n\t\t\t" + "總金額 : " + labelc1.Text + " NT";

                string line7 = "";//備註
                if (myReader.GetString(22) == "")
                    line7 = "";
                else
                    line7 = "備註 :" + "\r\n" + myReader.GetString(22) + "\r\n\r\n";

                string line80 = "";//姓名稱呼
                if (myReader.IsDBNull(6))
                    line80 = "";
                else if (myReader.GetString(6) == "Mr")
                    line80 = " 先生";
                else
                    line80 = " 小姐";
                string line8 = "姓名 : " + myReader.GetString(8) + line80;

                string line9 = "電話 : " + myReader.GetString(10);//電話

                string line100 = "";//地址
                if (myReader.IsDBNull(5))
                    line100 = "";
                else
                    line100 = " (" + myReader.GetString(5) + ")";
                string line10 = "地址 : " + myReader.GetString(13) + "\r\n          " + line100;

                msg += line5 + "\r\n" + line6 + "\r\n\r\n" + line7 + line8 + "\r\n" + line9 + "\r\n" + line10;
                textBox1.Text = msg;
            }
            databaseCon.Close();
        }

        private void detailPrinter(int i)
        {
            string msg = "";

            labela10.Text = this.Controls.Find("labela" + i, false)[0].Text;
            labelc10.Text = this.Controls.Find("labelc" + i, false)[0].Text;
            labeld10.Text = this.Controls.Find("labeld" + i, false)[0].Text;
            labele10.Text = this.Controls.Find("labele" + i, false)[0].Text;
            labelf10.Text = this.Controls.Find("labelf" + i, false)[0].Text;
            if (labelf10.Text == "外送")
                labelf10.BackColor = Color.Black;
            else
                labelf10.BackColor = Color.White;
            labelf10.ForeColor = this.Controls.Find("labelf" + i, false)[0].ForeColor;
            labelg10.Text = this.Controls.Find("labelg" + i, false)[0].Text;

            string detailNo = labela10.Text;
            string MYSQLConString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nfutopic_joomla";
            MySqlConnection databaseCon = new MySqlConnection(MYSQLConString);

            //以下是printdetail大綱
            string detailCode = "SELECT * FROM `jp5oj_virtuemart_orders` WHERE `order_number`=\'" + detailNo + "\'";
            MySqlCommand commandDatabase = new MySqlCommand(detailCode, databaseCon);
            commandDatabase.CommandTimeout = 60;

            databaseCon.Open();
            MySqlDataReader myReader = commandDatabase.ExecuteReader();

            while (myReader.Read())
            {
                labels10.Text = myReader.GetString(0);
                int ss10i = myReader.GetInt32(8);
                labelss10.Text = Convert.ToString(ss10i);
                int sss10i = myReader.GetInt32(19);
                labelsss10.Text = Convert.ToString(sss10i);

                string ship = "";
                if (labelf10.Text == "來店")
                    ship = "來店";
                else
                    ship = "【外送】";
                string number = labela10.Text;
                string line1 = ship + "\t" + "單號 : " + number;

                string line2 = "時間 : " + labeld10.Text;

                string line3 = "品項\t\t數量\t單價\t總額";

                msg += line1 + "\r\n\r\n" + line2 + "\r\n\r\n" + line3 + "\r\n";
            }
            databaseCon.Close();

            //以下是printdetial菜單
            string sx = labels10.Text;
            detailCode = "SELECT * FROM `jp5oj_virtuemart_order_items` WHERE `virtuemart_order_id`=" + sx;

            commandDatabase = new MySqlCommand(detailCode, databaseCon);
            commandDatabase.CommandTimeout = 60;

            databaseCon.Open();
            myReader = commandDatabase.ExecuteReader();
            while (myReader.Read())
            {
                int line40i = myReader.GetInt32(12);
                string line40 = Convert.ToString(line40i);
                int line41i = myReader.GetInt32(14);
                string line41 = Convert.ToString(line41i);
                string line4 = "";
                if (myReader.GetString(5).Length > 3)
                    line4 = myReader.GetString(5) + "\t" + myReader.GetString(6) + "\t" + line40 + "\t" + line41;
                else
                    line4 = myReader.GetString(5) + "\t\t" + myReader.GetString(6) + "\t" + line40 + "\t" + line41;

                string line4s = myReader.GetString(17);
                if (line4s == "")
                    msg += line4 + "\r\n";
                else
                {
                    string[] sArray = line4s.Split(new char[5] { '{', '"', ':', ',', '}' });
                    if (sArray.Length == 8)
                    {
                        string line44 = "";
                        if (sArray[5] == "22")
                            line44 = "**正常冰";
                        else if (sArray[5] == "23")
                            line44 = "**少冰";
                        else if (sArray[5] == "24")
                            line44 = "**微冰";
                        else if (sArray[5] == "25")
                            line44 = "**去冰";
                        else if (sArray[5] == "26")
                            line44 = "**熱";

                        msg += line4 + "\r\n" + line44 + "\r\n";
                    }
                    else if (sArray.Length == 14)
                    {
                        string line44 = "";
                        if (sArray[5] == "12")
                            line44 = "**正常冰";
                        else if (sArray[5] == "2")
                            line44 = "**正常冰";
                        else if (sArray[5] == "13")
                            line44 = "**少冰";
                        else if (sArray[5] == "3")
                            line44 = "**少冰";
                        else if (sArray[5] == "14")
                            line44 = "**微冰";
                        else if (sArray[5] == "4")
                            line44 = "**微冰";
                        else if (sArray[5] == "15")
                            line44 = "**去冰";
                        else if (sArray[5] == "5")
                            line44 = "**去冰";
                        else if (sArray[5] == "16")
                            line44 = "**熱";
                        else if (sArray[5] == "11")
                            line44 = "**熱";

                        string line45 = "";
                        if (sArray[11] == "6")
                            line45 = "全糖";
                        if (sArray[11] == "27")
                            line45 = "全糖";
                        else if (sArray[11] == "7")
                            line45 = "少糖(8分)";
                        else if (sArray[11] == "28")
                            line45 = "少糖(8分)";
                        else if (sArray[11] == "8")
                            line45 = "半糖(5分)";
                        else if (sArray[11] == "29")
                            line45 = "半糖(5分)";
                        else if (sArray[11] == "9")
                            line45 = "微糖(3分)";
                        else if (sArray[11] == "30")
                            line45 = "微糖(3分)";
                        else if (sArray[11] == "10")
                            line45 = "無糖";
                        else if (sArray[11] == "31")
                            line45 = "無糖";

                        msg += line4 + "\r\n" + line44 + " " + line45 + "\r\n";
                    }
                }
            }
            databaseCon.Close();

            //以下是printdetial結尾
            sx = labels10.Text;
            detailCode = "SELECT * FROM `jp5oj_virtuemart_order_userinfos` WHERE `virtuemart_order_id`=" + sx;

            commandDatabase = new MySqlCommand(detailCode, databaseCon);
            commandDatabase.CommandTimeout = 60;

            databaseCon.Open();
            myReader = commandDatabase.ExecuteReader();
            while (myReader.Read())
            {
                string line5 = "------------------------------------------------------------------";

                string line6 = "";
                if (labelsss10.Text == "0")
                    line6 = labele10.Text + "付款" + "\t\t" + "總金額 : " + labelc10.Text + " NT";
                else
                    line6 = labele10.Text + "付款" + "\t\t" + "原金額 : " + labelss10.Text + "\r\n\t\t\t" + "折扣 : " + labelsss10.Text + "\r\n\t\t\t" + "總金額 : " + labelc10.Text + " NT";

                string line7 = "";//備註
                if (myReader.GetString(22) == "")
                    line7 = "";
                else
                    line7 = "備註 :" + "\r\n" + myReader.GetString(22) + "\r\n\r\n";

                string line80 = "";//姓名稱呼
                if (myReader.IsDBNull(6))
                    line80 = "";
                else if (myReader.GetString(6) == "Mr")
                    line80 = " 先生";
                else
                    line80 = " 小姐";
                string line8 = "姓名 : " + myReader.GetString(8) + line80;

                string line9 = "電話 : " + myReader.GetString(10);//電話

                string line100 = "";//地址
                if (myReader.IsDBNull(5))
                    line100 = "";
                else
                    line100 = " (" + myReader.GetString(5) + ")";
                string line10 = "地址 : " + myReader.GetString(13) + "\r\n          " + line100;

                msg += line5 + "\r\n" + line6 + "\r\n\r\n" + line7 + line8 + "\r\n" + line9 + "\r\n" + line10;
                textBoxSerchPrint.Text = msg;
            }
            databaseCon.Close();
        }

        private void latestDetailPrinter()
        {
            string msg = "";

            string detailNo = labela1.Text;
            string MYSQLConString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nfutopic_joomla";
            MySqlConnection databaseCon = new MySqlConnection(MYSQLConString);

            //以下是printlatestdetail大綱
            string detailCode = "SELECT * FROM `jp5oj_virtuemart_orders` WHERE `order_number`=\'" + detailNo + "\'";
            MySqlCommand commandDatabase = new MySqlCommand(detailCode, databaseCon);
            commandDatabase.CommandTimeout = 60;

            databaseCon.Open();
            MySqlDataReader myReader = commandDatabase.ExecuteReader();
            while (myReader.Read())
            {
                labelLatestNo.Text = myReader.GetString(0);
                int ss10i = myReader.GetInt32(8);
                labelLatest0.Text = Convert.ToString(ss10i);
                int sss10i = myReader.GetInt32(19);
                labelLatest1.Text = Convert.ToString(sss10i);

                string pay = myReader.GetString(29);
                if (pay == "1")
                    labele1.Text = "現金";
                else
                    labele1.Text = "轉帳";

                string ship = "";
                if (myReader.GetString(30) == "1")
                    ship = "來店";
                else
                    ship = "【外送】";

                string number = labela1.Text;
                string line1 = ship + "\t" + "單號 : " + number;

                string line2 = "時間 : " + labeld1.Text;

                string line3 = "品項\t\t數量\t單價\t總額";

                msg += line1 + "\r\n\r\n" + line2 + "\r\n\r\n" + line3 + "\r\n";
            }
            databaseCon.Close();

            //以下是printlatestdetial菜單
            string sx = labelLatestNo.Text;
            detailCode = "SELECT * FROM `jp5oj_virtuemart_order_items` WHERE `virtuemart_order_id`=" + sx;

            commandDatabase = new MySqlCommand(detailCode, databaseCon);
            commandDatabase.CommandTimeout = 60;

            databaseCon.Open();
            myReader = commandDatabase.ExecuteReader();
            while (myReader.Read())
            {
                int line40i = myReader.GetInt32(12);
                string line40 = Convert.ToString(line40i);
                int line41i = myReader.GetInt32(14);
                string line41 = Convert.ToString(line41i);
                string line4 = "";
                if (myReader.GetString(5).Length > 3)
                    line4 = myReader.GetString(5) + "\t" + myReader.GetString(6) + "\t" + line40 + "\t" + line41;
                else
                    line4 = myReader.GetString(5) + "\t\t" + myReader.GetString(6) + "\t" + line40 + "\t" + line41;

                string line4s = myReader.GetString(17);
                if (line4s == "")
                    msg += line4 + "\r\n";
                else
                {
                    string[] sArray = line4s.Split(new char[5] { '{', '"', ':', ',', '}' });
                    if (sArray.Length == 8)
                    {
                        string line44 = "";
                        if (sArray[5] == "22")
                            line44 = "**正常冰";
                        else if (sArray[5] == "23")
                            line44 = "**少冰";
                        else if (sArray[5] == "24")
                            line44 = "**微冰";
                        else if (sArray[5] == "25")
                            line44 = "**去冰";
                        else if (sArray[5] == "26")
                            line44 = "**熱";

                        msg += line4 + "\r\n" + line44 + "\r\n";
                    }
                    else if (sArray.Length == 14)
                    {
                        string line44 = "";
                        if (sArray[5] == "12")
                            line44 = "**正常冰";
                        else if (sArray[5] == "2")
                            line44 = "**正常冰";
                        else if (sArray[5] == "13")
                            line44 = "**少冰";
                        else if (sArray[5] == "3")
                            line44 = "**少冰";
                        else if (sArray[5] == "14")
                            line44 = "**微冰";
                        else if (sArray[5] == "4")
                            line44 = "**微冰";
                        else if (sArray[5] == "15")
                            line44 = "**去冰";
                        else if (sArray[5] == "5")
                            line44 = "**去冰";
                        else if (sArray[5] == "16")
                            line44 = "**熱";
                        else if (sArray[5] == "11")
                            line44 = "**熱";

                        string line45 = "";
                        if (sArray[11] == "6")
                            line45 = "全糖";
                        if (sArray[11] == "27")
                            line45 = "全糖";
                        else if (sArray[11] == "7")
                            line45 = "少糖(8分)";
                        else if (sArray[11] == "28")
                            line45 = "少糖(8分)";
                        else if (sArray[11] == "8")
                            line45 = "半糖(5分)";
                        else if (sArray[11] == "29")
                            line45 = "半糖(5分)";
                        else if (sArray[11] == "9")
                            line45 = "微糖(3分)";
                        else if (sArray[11] == "30")
                            line45 = "微糖(3分)";
                        else if (sArray[11] == "10")
                            line45 = "無糖";
                        else if (sArray[11] == "31")
                            line45 = "無糖";

                        msg += line4 + "\r\n" + line44 + " " + line45 + "\r\n";
                    }
                }
            }
            databaseCon.Close();

            //以下是printlatestdetial結尾
            sx = labelLatestNo.Text;
            detailCode = "SELECT * FROM `jp5oj_virtuemart_order_userinfos` WHERE `virtuemart_order_id`=" + sx;

            commandDatabase = new MySqlCommand(detailCode, databaseCon);
            commandDatabase.CommandTimeout = 60;

            databaseCon.Open();
            myReader = commandDatabase.ExecuteReader();
            while (myReader.Read())
            {
                string line5 = "------------------------------------------------------------------";

                string line6 = "";
                if (labelLatest1.Text == "0")
                    line6 = labele1.Text + "付款" + "\t\t" + "總金額 : " + labelc1.Text + " NT";
                else
                    line6 = labele1.Text + "付款" + "\t\t" + "原金額 : " + labelLatest0.Text + "\r\n\t\t\t" + "折扣 : " + labelLatest1.Text + "\r\n\t\t\t" + "總金額 : " + labelc1.Text + " NT";

                string line7 = "";//備註
                if (myReader.GetString(22) == "")
                    line7 = "";
                else
                    line7 = "備註 :" + "\r\n" + myReader.GetString(22) + "\r\n\r\n";

                string line80 = "";//姓名稱呼
                if (myReader.IsDBNull(6))
                    line80 = "";
                else if (myReader.GetString(6) == "Mr")
                    line80 = " 先生";
                else
                    line80 = " 小姐";
                string line8 = "姓名 : " + myReader.GetString(8) + line80;

                string line9 = "電話 : " + myReader.GetString(10);//電話

                string line100 = "";//地址
                if (myReader.IsDBNull(5))
                    line100 = "";
                else
                    line100 = " (" + myReader.GetString(5) + ")";
                string line10 = "地址 : " + myReader.GetString(13) + "\r\n          " + line100;

                msg += line5 + "\r\n" + line6 + "\r\n\r\n" + line7 + line8 + "\r\n" + line9 + "\r\n" + line10;
                textBoxlatestprint.Text = msg;
            }
            databaseCon.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("請問您確認要關閉本程式嗎?", "關閉確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Cancel)
                e.Cancel = true;
        }
    }
}
