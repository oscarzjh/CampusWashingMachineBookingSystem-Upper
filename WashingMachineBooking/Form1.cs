using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.OleDb;

using System.Threading;



namespace WashingMachineBooking
{


    public partial class Form1 : Form
    {

//***********************************全局变量定义**********************************

        string strSys = System.Environment.CurrentDirectory;   //获取和设置当前目录（即该进程从中启动的目录 .exe所在目录）
        
        byte[] startData = new byte[2];
        byte[] yuyueflagData = new byte[1] { 0xff };
        byte[] cancelData = new byte[1] { 0xfe };
        byte[] ifStartOkData = new byte[2] { 0xfa, 0xfb };  //非预约洗衣，给下位机发送洗衣是否可以开始信号

        //bool ifZaiXiYuyue;  //有点问题！！//当前在洗衣服是否通过预约的。如果在预约等待状态，点了一下button_Book（因为yuyue的ZaiXiEndTime是在发yuyueData就已经设置好）,
                            //且当前表中item count>0，而会造成预约逻辑有误（如果这个等待状态有恰好要cancel掉）

        DateTime recordPreviousStart;//记录发送预约信号，其中的StartTime，用于以后判断上位机3min等待是否超时

        DateTime nonYuyueEndTime;

        DateTime ZaiXiEndTime;


        byte[] _128 = new byte[1] { 0x80 };

        byte[] temp = new byte[4];

        byte[] washdatain = new byte[2];  //washdatain[0] 为下位机发来洗衣时长，washdatain[1] 为下位机发来脱水时长
        
        TimeSpan ts1s = new TimeSpan(0, 0, 1);
        static TimeSpan ts1m = new TimeSpan(0, 1, 0);
        DateTime firstStart = DateTime.Now.Subtract(ts1m);  //预约记录表中第一条（即将最先开始的记录）的开始时间

//****************************窗体自动Resize*********************************

        private Size m_szInit;//初始窗体大小
        private Dictionary<Control, Rectangle> m_dicSize
            = new Dictionary<Control, Rectangle>();

        protected override void OnLoad(EventArgs e)
        {
            m_szInit = this.Size;//获取初始大小
            this.GetInitSize(this);
            base.OnLoad(e);
        }

        private void GetInitSize(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                m_dicSize.Add(c, new Rectangle(c.Location, c.Size));
                this.GetInitSize(c);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            //计算当前大小和初始大小的比例
            float fx = (float)this.Width / m_szInit.Width;
            float fy = (float)this.Height / m_szInit.Height;
            foreach (var v in m_dicSize)
            {
                v.Key.Left = (int)(v.Value.Left * fx);
                v.Key.Top = (int)(v.Value.Top * fy);
                v.Key.Width = (int)(v.Value.Width * fx);
                v.Key.Height = (int)(v.Value.Height * fy);
            }
            base.OnResize(e);
        }

//****************************线程帮助*********************************
        public static class ThreadHelperClass
        {
            delegate void SetTextCallback(Form f, Control ctrl, string text);

            delegate void AddTextCallback(Form f, Control ctrl, string text);
            /// <summary>
            /// Set text property of various controls
            /// </summary>
            /// <param name="form">The calling form</param>
            /// <param name="ctrl"></param>
            /// <param name="text"></param>
            public static void SetText(Form form, Control ctrl, string text)
            {
                // InvokeRequired required compares the thread ID of the 
                // calling thread to the thread ID of the creating thread. 
                // If these threads are different, it returns true. 
                if (ctrl.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(SetText);
                    form.Invoke(d, new object[] { form, ctrl, text });
                }
                else
                {
                    ctrl.Text = text;
                }
            }

            public static void AddText(Form form, Control ctrl, string text)
            {
                // InvokeRequired required compares the thread ID of the 
                // calling thread to the thread ID of the creating thread. 
                // If these threads are different, it returns true. 
                if (ctrl.InvokeRequired)
                {
                    AddTextCallback d = new AddTextCallback(AddText);
                    form.Invoke(d, new object[] { form, ctrl, text });
                }
                else
                {
                    ctrl.Text += text;
                }
            }

      
        }

//***********************Int32转为4个Byte*****************************

        /// <summary>  
        /// 把int32类型的数据转存到4个字节的byte数组中  
        /// </summary>  
        /// <param name="m">int32类型的数据</param>  
        /// <param name="arry">4个字节大小的byte数组</param>  
        /// <returns></returns> 
        
        // 调用方法：
        // byte [] buf = new byte[4];  
        // bool ok = ConvertIntToByteArray(0x12345678, ref buf);
         
       public static bool ConvertIntToByteArray(Int32 m, ref byte[] arry)
        {
            if (arry == null) return false;
            if (arry.Length < 4) return false;  //arry.Length必须为4

            arry[0] = (byte)(m & 0xFF);         //低8位
            arry[1] = (byte)((m & 0xFF00) >> 8);
            arry[2] = (byte)((m & 0xFF0000) >> 16);
            arry[3] = (byte)((m >> 24) & 0xFF);  //高8位

            return true;
        }

//*********************创建开始时间和洗衣脱水时长用于传送串口**************************
       public void createYuyueData()
       {
           if (listViewShow.Items.Count > 0)
           { 
           
               firstStart = Convert.ToDateTime(listViewShow.Items[0].SubItems[1].Text);

               byte[] buf1 = new byte[4];
               bool ok1 = ConvertIntToByteArray(Convert.ToInt32(listViewShow.Items[0].SubItems[3].Text), ref buf1);
               if (ok1)
               {
                   startData[0] = buf1[0];  //洗衣时长
               }
               byte[] buf2 = new byte[4];
               bool ok2 = ConvertIntToByteArray(Convert.ToInt32(listViewShow.Items[0].SubItems[4].Text), ref buf2);
               if (ok2)
               {
                   startData[1] = (byte)(buf2[0] + _128[0]);  //脱水时长
                   //编译器对待 + 时，有 int 相加、有 decimal 相加、有字符串相加……就是没有 byte 相加
                   //所以它会用最接近的 int 相加，自然返回的结果也是 int，而 int 类型是不能直接赋值给更小的 byte 类型的。
               }
           }
       }

/*
       public void checkTimeAtStart()         //循环检测系统时间是否到StartTime
       {
            while (true)
            {
                if (listViewShow.Items.Count > 0)
                {
                    if (DateTime.Now == firstStart)
                    {
                        serialPort1.Write(yuyueflagData, 0, 1);
                        ReadFromDB();   // 自动remove第一行
                        createYuyueData();
                    }
                }
            }
                          
       }

        */

       public void isStartOk()   //此时由于洗衣机只能是空闲状态，产生冲突的情景能且只能是预约表中第一个的StartTime（nextYuyueStart）和结束time冲突
       {
           
           int sum = washdatain[0] + washdatain[1];
           nonYuyueEndTime = DateTime.Now.AddMinutes(sum); //可能的nonYuyueEndTime，因为可能上位机不允许洗衣。若是预约等待？
           //if (labelWashState.Text == "空闲")
           //{
               if (listViewShow.Items.Count == 0)
               {
                   serialPort1.Write(ifStartOkData, 1, 1);  //发送0xfb代表可以开始
                   ZaiXiEndTime = nonYuyueEndTime;

                   /*
                                   ListViewItem li = new ListViewItem();

                                   li.SubItems[0].Text ="X";  //X代表非预约洗衣
                                   li.SubItems.Add(DateTime.Now.ToString());
                                   li.SubItems.Add(rdr["EndTime"].ToString());
                                   li.SubItems.Add(rdr["WashTime"].ToString());
                                   li.SubItems.Add(rdr["WaterTime"].ToString());
                                   listViewShow.Items.Add(li);     */

               }
               else
               {
                   DateTime nextYuyueStart = firstStart;
                   //DateTime nextYuyueStart = Convert.ToDateTime(listViewShow.Items[0].SubItems[1].Text);
                   DateTime stok = nonYuyueEndTime.AddMinutes(3);  //预留3分钟
                   if (stok > nextYuyueStart)
                       serialPort1.Write(ifStartOkData, 0, 1);  //发送0xfa代表不可以开始
                   else
                   {
                       serialPort1.Write(ifStartOkData, 1, 1);  //发送0xfb代表可以开始
                       ZaiXiEndTime = nonYuyueEndTime;
                   }
               }
           //}
       }


//**********************************窗口初始化**********************************
        public Form1()
        {
            InitializeComponent();
        }

//****************************从数据库读取并显示*********************************

        public void ReadFromDB()
        {
            int showboxCount = 0;  //用于listView中序号编写 初始值为0
            OleDbConnection strConnection = new OleDbConnection("Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + strSys+"\\mydb1.accdb");//"E:\\AccessDB Documents\\mydb1.accdb"+";Persist Security Info=False");
            //建立数据库引擎连接，注意数据表（后缀为.db）应放在DEBUG文件下
            //strConnection.Open();
            //OleDbDataAdapter myda = new OleDbDataAdapter("select StartTime,EndTime,WashTime,WaterTime from biao1 where StartTime>=Date() order by StartTime asc",strConnection);
            string sql = "select * from biao1 where StartTime>Now() order by StartTime asc";
            OleDbCommand cmd = new OleDbCommand(sql, strConnection);
            strConnection.Open();  //打开连接
            listViewShow.Items.Clear();
            OleDbDataReader rdr = cmd.ExecuteReader();  //执行命令，返回结果
            while (rdr.Read()) //循环读取结果集
            //每次读取一行
            {
                showboxCount++;
                ListViewItem li = new ListViewItem();
                li.SubItems.Clear();
                li.SubItems[0].Text = showboxCount.ToString();
                li.SubItems.Add(rdr["StartTime"].ToString());
                li.SubItems.Add(rdr["EndTime"].ToString());
                li.SubItems.Add(rdr["WashTime"].ToString());
                li.SubItems.Add(rdr["WaterTime"].ToString());
                listViewShow.Items.Add(li);
            }
            rdr.Close(); //关闭数据读取器
            strConnection.Close(); //关闭连接
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //*********************系统时间显示 计时器1打开***********************
            timer1.Start();


            //*************************数据库连接************************

            ReadFromDB();
                 

            //*************************串口打开**************************
            //serialPort1.Open();

            //**********************检测StartTime是否到达*****************
            //checkTimeAtStart();

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            serialPort1.Write(cancelData, 0, 1);   //发送取消信号
  
            //textSysInfo.AppendText("\r\n洗衣已取消！");
        }


        private void buttonStartWash_Click(object sender, EventArgs e)
        {  

            //get=dataGridView1.SelectAll();

            //byte[] startData = new byte[3];
            //startData[0] = 0xff;


            //startData[1] = 0x0d;
            //startData[2] = 0x0a;
            serialPort1.Write(startData, 0, 2);  //给下位机发送洗衣时长数据
            

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            textSysInfo.Clear();
            textSysInfo.Text = "欢迎来到洗衣机预约系统！";
            dateTimePicker1.Value = new DateTime(2017, 5, 1, 0, 0, 0);
            textBoxWashTime.Text = "";
            textBoxWaterTime.Text = "";
        }

        private void buttonBook_Click(object sender, EventArgs e)
        {
            DateTime getPickerTime;
            string getWashTime, getWaterTime, getDay, getTime, getStartTime;

            getWashTime = textBoxWashTime.Text;
            getWaterTime = textBoxWaterTime.Text;

            if (getWashTime == "" || getWaterTime == "")
            {
                MessageBox.Show("数据输入不完整，请填完整！");
                textBoxWashTime.Text = "";
                textBoxWaterTime.Text = "";
                return;
            }

            int WashTime, WaterTime;
            float x = float.Parse(getWashTime);
            WashTime = Convert.ToInt32(Math.Round(x));      //Math在System namespace下
            float y = float.Parse(getWaterTime);
            WaterTime = Convert.ToInt32(Math.Round(y));     //若输入小数，将parse为0，将round 四舍六入五凑偶 至最近整数

            if (WashTime > 120 || WashTime < 0 || WaterTime > 120 || WaterTime < 0)  //洗衣时间最长设置120min,脱水时间最长120min
            {
                MessageBox.Show("时间设置不规范！洗衣时间最长120min,脱水时间最长120min!");
                textBoxWashTime.Text = "";
                textBoxWaterTime.Text = "";
                return;
            }
            else if (WashTime + WaterTime == 0)
            {
                MessageBox.Show("洗衣时间和脱水时间不能都为零！");
                textBoxWashTime.Text = "";
                textBoxWaterTime.Text = "";
                return;
            }

            getPickerTime = dateTimePicker1.Value;

            getDay = DateTime.Now.ToString("yyyy-MM-dd");
            getTime = getPickerTime.ToString("HH:mm:ss");  //hh小写不可以！否则12小时制
            getStartTime = getDay + " " + getTime;

            DateTime StartTime = Convert.ToDateTime(getStartTime);  //StartTime是设置当日开始时间
            DateTime EndTime = new DateTime(2017, 5, 1, 0, 0, 0);

            if (StartTime <= DateTime.Now)
            {
                MessageBox.Show("不能选择过去的时刻！");
                dateTimePicker1.Value = new DateTime(2017, 5, 1, 0, 0, 0);
                return;
            }

            EndTime = StartTime.AddMinutes(WashTime + WaterTime);

            DateTime dt;
            dt = DateTime.Now.Date.AddDays(1);// 格式化明日时间  00：00：00

            if (EndTime > dt)
            {
                MessageBox.Show("不能选择结束时间在第二天！");
                dateTimePicker1.Value = new DateTime(2017, 5, 1, 0, 0, 0);
                textBoxWashTime.Text = "";
                textBoxWaterTime.Text = "";
                return;
            }   //若还往下执行，必然所有数据都设置好

            if (listViewShow.Items.Count == 0)
            {
                if (labelWashState.Text != "空闲")  //当前在洗既有可能是预约的洗衣，也有可能是非预约的洗衣。也有可能是预约等待状态!
                {
                    if (ZaiXiEndTime > DateTime.Now) //小于仅在预约等待状态cancel时
                    {
                        if (ZaiXiEndTime.AddMinutes(3) > StartTime)  //仅当此才不能预约
                        {
                            MessageBox.Show("您预约的时间太早或与结束时间差不足3min！当前洗衣结束时间为" + ZaiXiEndTime.ToLongTimeString()); 
                            dateTimePicker1.Value = new DateTime(2017, 5, 1, 0, 0, 0);
                            textBoxWashTime.Text = "";
                            textBoxWaterTime.Text = "";
                            return;
                        }
                    }
                }
            }





            else    //如果当前 listViewShow.Items.Count > 0
            {
                int i = 0;
                while (StartTime > Convert.ToDateTime(listViewShow.Items[i].SubItems[1].Text))
                {
                    i++;
                    if (i == listViewShow.Items.Count)
                    {
                        break;  //此时插入StartTime值为最大
                    }
                }  //找到第一个不小于StartTime的Item的Index i


                //预留5分钟？？？？


                if (i == 0)          //如果应该插入在预约记录第一行      
                {
                    if (labelWashState.Text != "空闲")    //如果是预约等待状态，ZaiXiEndTime只能是前一条预约记录的；如果在洗，不一定，但无影响。
                    {
                        if (ZaiXiEndTime > DateTime.Now) 
                        {
                            if (ZaiXiEndTime.AddMinutes(3) > StartTime)  //仅当此才不能预约
                            {
                                MessageBox.Show("您预约的时间太早或与结束时间差不足3min！当前洗衣结束时间为" + ZaiXiEndTime.ToLongTimeString());  //时间为？？？
                                dateTimePicker1.Value = new DateTime(2017, 5, 1, 0, 0, 0);
                                textBoxWashTime.Text = "";
                                textBoxWaterTime.Text = "";
                                return;
                            }
                        }
                    }
                    if (EndTime.AddMinutes(3) > Convert.ToDateTime(listViewShow.Items[i].SubItems[1].Text))  //如果空闲且...
                    {
                        MessageBox.Show("结束时间和他人预约开始时间冲突或不足3min！请缩短洗衣时间！");
                        textBoxWashTime.Text = "";
                        textBoxWaterTime.Text = "";
                        return;
                    }
                       
                   




/*                    if (EndTime > Convert.ToDateTime(listViewShow.Items[i].SubItems[1].Text))
                    {
                        MessageBox.Show("结束时间和他人预约开始时间冲突！请缩短洗衣时间！");
                        textBoxWashTime.Text = "";
                        textBoxWaterTime.Text = "";
                        return;
                    }  */
                }

                else if (i == listViewShow.Items.Count)
                {
                    if (StartTime < Convert.ToDateTime(listViewShow.Items[i - 1].SubItems[2].Text).AddMinutes(3))
                    {
                        MessageBox.Show("开始时间和他人预约结束时间冲突或不足3min！请重新设置！");
                        dateTimePicker1.Value = new DateTime(2017, 5, 1, 0, 0, 0);
                        return;
                    }
                }

                else
                {
                    if (EndTime.AddMinutes(3) > Convert.ToDateTime(listViewShow.Items[i].SubItems[1].Text))
                    {
                        MessageBox.Show("结束时间和他人预约开始时间冲突或不足3min！请缩短洗衣时间！");
                        textBoxWashTime.Text = "";
                        textBoxWaterTime.Text = "";
                        return;
                    }
                    if (StartTime < Convert.ToDateTime(listViewShow.Items[i - 1].SubItems[2].Text).AddMinutes(3))
                    {
                        MessageBox.Show("开始时间和他人预约结束时间冲突或不足3min！请重新设置！");
                        dateTimePicker1.Value = new DateTime(2017, 5, 1, 0, 0, 0);
                        return;
                    }
                }

            }

            string stTime = StartTime.ToString();
            string edTime = EndTime.ToString();
            OleDbConnection strConnection = new OleDbConnection("Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + strSys + "\\mydb1.accdb");
            string sql = "insert into biao1 values(#" + stTime + "#,#" + edTime + "#," + WashTime.ToString() + "," + WaterTime.ToString() + " )";
            OleDbCommand cmd = new OleDbCommand(sql, strConnection);  //创建命令
            try
            {
                strConnection.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("预约成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            strConnection.Close();

            ReadFromDB();

            createYuyueData();
            //checkTimeAtStart();

        }
         
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;     //实例化对象捕获系统当前时间
            string t = dt.ToLongTimeString();  //将此实例的值转化为等效时间字符串值
            this.labelSysTime.Text = t;                      //将系统当前时间显示在label1控件上
            
            if (listViewShow.Items.Count > 0)
            {
                if (dt.Subtract(ts1s) < firstStart && firstStart <= dt)
                //if (DateTime.Now == firstStart)
                {
                    serialPort1.Write(yuyueflagData, 0, 1);   //发送预约信号时，洗衣状态不可能为占用，也不可能为预约等待状态。若此时取消，需要保证此时预约在最前或者非预约可以洗！！！
                    labelWashState.Text = "预约等待";
                    textSysInfo.AppendText("\r\n洗衣机已锁定，进入预约等待状态！\r\n请3min内开始洗衣，否则取消！");

                    recordPreviousStart = firstStart.AddMinutes(3); //记录发送预约信号，其中的StartTime，用于以后判断上位机3min等待是否超时
                    ZaiXiEndTime = Convert.ToDateTime(listViewShow.Items[0].SubItems[2].Text);  //当预约表中没有item,而当前又有衣服在洗,预约不一定成功

                    ReadFromDB();   // 自动remove第一行
                    createYuyueData();   //创建新的YuyueData， 刚刚去除的一条记录中重要的是Endtime,可能产生冲突
                    //需要测试连续两个yuyue是否都可以自动发预约开始信号？？？？
                }               
            }

            if (dt.Subtract(ts1s) < recordPreviousStart && recordPreviousStart <= dt)
                if (labelWashState.Text == "预约等待")    //3min超时自动取消
                    serialPort1.Write(cancelData, 0, 1);   //发送取消信号   //？？？
            //时间到了开始时间否？发串口 并remove第一行   ??为啥没进？？

        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int datain;
            datain = serialPort1.ReadByte();
    //可用swtich case语句替换
            if (datain == 0xff)//预约的洗衣机门未关
            {
                ThreadHelperClass.AddText(this, textSysInfo, "\r\n洗衣机未关门！！关门后自动继续...");
            }
            else if (datain == 0xf9)//预约的洗衣机门已关
            {
                ThreadHelperClass.AddText(this, textSysInfo, "\r\n洗衣机已关门,开始洗衣！");
            }
            else if (datain == 0xfe)//预约的洗衣机开始洗衣
            {
                
                ThreadHelperClass.SetText(this, labelWashState, "占用");
                ThreadHelperClass.AddText(this, textSysInfo, "\r\n洗衣开始!");
                //ifZaiXiYuyue = true;
            }
            else if (datain == 0xfd)//预约的洗衣机结束洗衣
            {
                ThreadHelperClass.SetText(this, labelWashState, "空闲");
                ThreadHelperClass.AddText(this, textSysInfo, "\r\n洗衣结束!");
            }
            else if (datain == 0xfa)//预约取消
            {
                ThreadHelperClass.SetText(this, labelWashState, "空闲");
                ThreadHelperClass.AddText(this, textSysInfo, "\r\n预约已取消！");

                ZaiXiEndTime = DateTime.Now.Subtract(ts1m); //如果预约取消，将zaiXiEndTime置为当前时间以前的一个时刻  ？？？
            }
            else if (datain == 0xfc)//非预约的洗衣机正在洗衣
            {
                ThreadHelperClass.SetText(this, labelWashState, "占用");
                //ifZaiXiYuyue = false;
            }
            else if (datain == 0xfb)//非预约的洗衣机没有洗衣
            {
                ThreadHelperClass.SetText(this, labelWashState, "空闲");
            }

            else if (datain < _128[0])
            {
                if (ConvertIntToByteArray(datain, ref temp))
                    washdatain[0] = temp[0]; //下位机发来洗衣时长
                    //ThreadHelperClass.AddText(this, textSysInfo, washdatain[0].ToString());
                
            }
            else if (datain > _128[0] && datain < 256)   
            {
                if (ConvertIntToByteArray(datain, ref temp))
                {
                    washdatain[1] = (byte)(temp[0] - _128[0]);  //下位机发来脱水时长,按下位机的代码，只可能在洗衣机空闲时发来！
                                                                //下位机在预约等待 或 洗衣状态 都不能按键
                    isStartOk();  //可以调用吗？？？
                }              
            }
        }

    }
}
