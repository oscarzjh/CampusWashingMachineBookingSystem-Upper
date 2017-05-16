namespace WashingMachineBooking
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textSysInfo = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBoxTimeSelect = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonBook = new System.Windows.Forms.Button();
            this.textBoxWaterTime = new System.Windows.Forms.TextBox();
            this.textBoxWashTime = new System.Windows.Forms.TextBox();
            this.buttonStartWash = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxState = new System.Windows.Forms.GroupBox();
            this.labelWashState = new System.Windows.Forms.Label();
            this.groupBoxWash = new System.Windows.Forms.GroupBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxTime = new System.Windows.Forms.GroupBox();
            this.labelSysTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.listViewShow = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxTimeSelect.SuspendLayout();
            this.groupBoxState.SuspendLayout();
            this.groupBoxWash.SuspendLayout();
            this.groupBoxTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM6";
            this.serialPort1.ReadBufferSize = 10;
            this.serialPort1.WriteBufferSize = 10;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(197, 311);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(136, 39);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "取消洗衣";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textSysInfo
            // 
            this.textSysInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textSysInfo.ForeColor = System.Drawing.Color.Teal;
            this.textSysInfo.Location = new System.Drawing.Point(30, 49);
            this.textSysInfo.MaxLength = 0;
            this.textSysInfo.Multiline = true;
            this.textSysInfo.Name = "textSysInfo";
            this.textSysInfo.ReadOnly = true;
            this.textSysInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textSysInfo.Size = new System.Drawing.Size(318, 249);
            this.textSysInfo.TabIndex = 1;
            this.textSysInfo.Text = "欢迎来到洗衣机预约系统！";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker1.Location = new System.Drawing.Point(129, 159);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(100, 24);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.Value = new System.DateTime(2017, 4, 30, 0, 0, 0, 0);
            // 
            // groupBoxTimeSelect
            // 
            this.groupBoxTimeSelect.Controls.Add(this.label6);
            this.groupBoxTimeSelect.Controls.Add(this.label5);
            this.groupBoxTimeSelect.Controls.Add(this.label4);
            this.groupBoxTimeSelect.Controls.Add(this.label3);
            this.groupBoxTimeSelect.Controls.Add(this.buttonBook);
            this.groupBoxTimeSelect.Controls.Add(this.textBoxWaterTime);
            this.groupBoxTimeSelect.Controls.Add(this.textBoxWashTime);
            this.groupBoxTimeSelect.Controls.Add(this.dateTimePicker1);
            this.groupBoxTimeSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxTimeSelect.Location = new System.Drawing.Point(467, 176);
            this.groupBoxTimeSelect.Name = "groupBoxTimeSelect";
            this.groupBoxTimeSelect.Size = new System.Drawing.Size(259, 379);
            this.groupBoxTimeSelect.TabIndex = 4;
            this.groupBoxTimeSelect.TabStop = false;
            this.groupBoxTimeSelect.Text = "时间选择";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Teal;
            this.label6.Location = new System.Drawing.Point(6, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(247, 109);
            this.label6.TabIndex = 9;
            this.label6.Text = "注意事项：\r\n1.只能预约起始和结束均在当天的洗衣时刻；\r\n2.洗衣、脱水为洗衣机两个模式，时长设置单位为min；\r\n3.时长输入整数，否则自动四舍六入五凑偶。";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 18);
            this.label5.TabIndex = 8;
            this.label5.Text = "脱水时长：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "洗衣时长：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "开始时刻：";
            // 
            // buttonBook
            // 
            this.buttonBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBook.Location = new System.Drawing.Point(72, 310);
            this.buttonBook.Name = "buttonBook";
            this.buttonBook.Size = new System.Drawing.Size(110, 40);
            this.buttonBook.TabIndex = 5;
            this.buttonBook.Text = "预约";
            this.buttonBook.UseVisualStyleBackColor = true;
            this.buttonBook.Click += new System.EventHandler(this.buttonBook_Click);
            // 
            // textBoxWaterTime
            // 
            this.textBoxWaterTime.Location = new System.Drawing.Point(129, 249);
            this.textBoxWaterTime.Name = "textBoxWaterTime";
            this.textBoxWaterTime.Size = new System.Drawing.Size(100, 24);
            this.textBoxWaterTime.TabIndex = 4;
            // 
            // textBoxWashTime
            // 
            this.textBoxWashTime.Location = new System.Drawing.Point(129, 206);
            this.textBoxWashTime.Name = "textBoxWashTime";
            this.textBoxWashTime.Size = new System.Drawing.Size(100, 24);
            this.textBoxWashTime.TabIndex = 3;
            // 
            // buttonStartWash
            // 
            this.buttonStartWash.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartWash.Location = new System.Drawing.Point(40, 311);
            this.buttonStartWash.Name = "buttonStartWash";
            this.buttonStartWash.Size = new System.Drawing.Size(124, 40);
            this.buttonStartWash.TabIndex = 5;
            this.buttonStartWash.Text = "开始洗衣！";
            this.buttonStartWash.UseVisualStyleBackColor = true;
            this.buttonStartWash.Click += new System.EventHandler(this.buttonStartWash_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(131, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "洗衣机当前状态 ：";
            // 
            // groupBoxState
            // 
            this.groupBoxState.Controls.Add(this.labelWashState);
            this.groupBoxState.Controls.Add(this.label1);
            this.groupBoxState.Location = new System.Drawing.Point(350, 28);
            this.groupBoxState.Name = "groupBoxState";
            this.groupBoxState.Size = new System.Drawing.Size(586, 69);
            this.groupBoxState.TabIndex = 8;
            this.groupBoxState.TabStop = false;
            // 
            // labelWashState
            // 
            this.labelWashState.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWashState.ForeColor = System.Drawing.Color.Red;
            this.labelWashState.Location = new System.Drawing.Point(323, 18);
            this.labelWashState.Name = "labelWashState";
            this.labelWashState.Size = new System.Drawing.Size(140, 42);
            this.labelWashState.TabIndex = 7;
            this.labelWashState.Text = "空闲";
            this.labelWashState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxWash
            // 
            this.groupBoxWash.Controls.Add(this.textSysInfo);
            this.groupBoxWash.Controls.Add(this.buttonCancel);
            this.groupBoxWash.Controls.Add(this.buttonStartWash);
            this.groupBoxWash.ForeColor = System.Drawing.Color.Black;
            this.groupBoxWash.Location = new System.Drawing.Point(52, 176);
            this.groupBoxWash.Name = "groupBoxWash";
            this.groupBoxWash.Size = new System.Drawing.Size(389, 379);
            this.groupBoxWash.TabIndex = 9;
            this.groupBoxWash.TabStop = false;
            // 
            // buttonReset
            // 
            this.buttonReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReset.Location = new System.Drawing.Point(82, 603);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(99, 49);
            this.buttonReset.TabIndex = 10;
            this.buttonReset.Text = "重置";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "系统时间：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxTime
            // 
            this.groupBoxTime.Controls.Add(this.labelSysTime);
            this.groupBoxTime.Controls.Add(this.label2);
            this.groupBoxTime.Location = new System.Drawing.Point(1035, 598);
            this.groupBoxTime.Name = "groupBoxTime";
            this.groupBoxTime.Size = new System.Drawing.Size(266, 54);
            this.groupBoxTime.TabIndex = 12;
            this.groupBoxTime.TabStop = false;
            // 
            // labelSysTime
            // 
            this.labelSysTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSysTime.ForeColor = System.Drawing.Color.Red;
            this.labelSysTime.Location = new System.Drawing.Point(131, 18);
            this.labelSysTime.Name = "labelSysTime";
            this.labelSysTime.Size = new System.Drawing.Size(115, 23);
            this.labelSysTime.TabIndex = 12;
            this.labelSysTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // listViewShow
            // 
            this.listViewShow.AllowColumnReorder = true;
            this.listViewShow.AutoArrange = false;
            this.listViewShow.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewShow.FullRowSelect = true;
            this.listViewShow.GridLines = true;
            this.listViewShow.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewShow.Location = new System.Drawing.Point(757, 176);
            this.listViewShow.MultiSelect = false;
            this.listViewShow.Name = "listViewShow";
            this.listViewShow.Size = new System.Drawing.Size(597, 379);
            this.listViewShow.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewShow.TabIndex = 13;
            this.listViewShow.UseCompatibleStateImageBehavior = false;
            this.listViewShow.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "开始时间";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 180;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "结束时间";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 180;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "洗衣时长";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 90;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "脱水时长";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 90;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1389, 735);
            this.Controls.Add(this.listViewShow);
            this.Controls.Add(this.groupBoxTime);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.groupBoxWash);
            this.Controls.Add(this.groupBoxState);
            this.Controls.Add(this.groupBoxTimeSelect);
            this.Name = "Form1";
            this.Text = "洗衣机预约系统";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxTimeSelect.ResumeLayout(false);
            this.groupBoxTimeSelect.PerformLayout();
            this.groupBoxState.ResumeLayout(false);
            this.groupBoxState.PerformLayout();
            this.groupBoxWash.ResumeLayout(false);
            this.groupBoxWash.PerformLayout();
            this.groupBoxTime.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textSysInfo;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox groupBoxTimeSelect;
        private System.Windows.Forms.TextBox textBoxWaterTime;
        private System.Windows.Forms.TextBox textBoxWashTime;
        private System.Windows.Forms.Button buttonBook;
        private System.Windows.Forms.Button buttonStartWash;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxState;
        private System.Windows.Forms.GroupBox groupBoxWash;
        private System.Windows.Forms.Label labelWashState;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxTime;
        private System.Windows.Forms.Label labelSysTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListView listViewShow;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}

