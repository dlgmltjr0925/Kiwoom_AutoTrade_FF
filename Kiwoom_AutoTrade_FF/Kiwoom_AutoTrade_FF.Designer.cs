namespace Kiwoom_AutoTrade_FF
{
    partial class Kiwoom_AutoTrade_FF
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kiwoom_AutoTrade_FF));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.axKFOpenAPI1 = new AxKFOpenAPILib.AxKFOpenAPI();
            this.lbTest = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.계좌정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.로그인ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.로그아웃ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.편집EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종목설정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.자동매매설정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDB = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbAccNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdAccount = new System.Windows.Forms.DataGridView();
            this.btnTest = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.grdSettleListDetail = new System.Windows.Forms.DataGridView();
            this.grdSettleListSummary = new System.Windows.Forms.DataGridView();
            this.btnSearchList = new System.Windows.Forms.Button();
            this.btnAllOne = new System.Windows.Forms.Button();
            this.cbCode = new System.Windows.Forms.ComboBox();
            this.txtDay = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbChange = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdHoga = new System.Windows.Forms.DataGridView();
            this.dB서버설정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axKFOpenAPI1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAccount)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSettleListDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSettleListSummary)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdHoga)).BeginInit();
            this.SuspendLayout();
            // 
            // axKFOpenAPI1
            // 
            this.axKFOpenAPI1.Enabled = true;
            this.axKFOpenAPI1.Location = new System.Drawing.Point(960, 600);
            this.axKFOpenAPI1.Name = "axKFOpenAPI1";
            this.axKFOpenAPI1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axKFOpenAPI1.OcxState")));
            this.axKFOpenAPI1.Size = new System.Drawing.Size(81, 23);
            this.axKFOpenAPI1.TabIndex = 0;
            this.axKFOpenAPI1.OnReceiveTrData += new AxKFOpenAPILib._DKFOpenAPIEvents_OnReceiveTrDataEventHandler(this.axKFOpenAPI1_OnReceiveTrData);
            this.axKFOpenAPI1.OnReceiveMsg += new AxKFOpenAPILib._DKFOpenAPIEvents_OnReceiveMsgEventHandler(this.axKFOpenAPI1_OnReceiveMsg);
            this.axKFOpenAPI1.OnReceiveRealData += new AxKFOpenAPILib._DKFOpenAPIEvents_OnReceiveRealDataEventHandler(this.axKFOpenAPI1_OnReceiveRealData);
            this.axKFOpenAPI1.OnReceiveChejanData += new AxKFOpenAPILib._DKFOpenAPIEvents_OnReceiveChejanDataEventHandler(this.axKFOpenAPI1_OnReceiveChejanData);
            this.axKFOpenAPI1.OnEventConnect += new AxKFOpenAPILib._DKFOpenAPIEvents_OnEventConnectEventHandler(this.axKFOpenAPI1_OnEventConnet);
            // 
            // lbTest
            // 
            this.lbTest.FormattingEnabled = true;
            this.lbTest.ItemHeight = 12;
            this.lbTest.Location = new System.Drawing.Point(13, 582);
            this.lbTest.Name = "lbTest";
            this.lbTest.Size = new System.Drawing.Size(924, 40);
            this.lbTest.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.계좌정보ToolStripMenuItem,
            this.편집EToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(944, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 계좌정보ToolStripMenuItem
            // 
            this.계좌정보ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.로그인ToolStripMenuItem,
            this.로그아웃ToolStripMenuItem,
            this.종료ToolStripMenuItem});
            this.계좌정보ToolStripMenuItem.Name = "계좌정보ToolStripMenuItem";
            this.계좌정보ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.계좌정보ToolStripMenuItem.Text = "기능";
            // 
            // 로그인ToolStripMenuItem
            // 
            this.로그인ToolStripMenuItem.Name = "로그인ToolStripMenuItem";
            this.로그인ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.로그인ToolStripMenuItem.Text = "로그인";
            this.로그인ToolStripMenuItem.Click += new System.EventHandler(this.로그인ToolStripMenuItem_Click);
            // 
            // 로그아웃ToolStripMenuItem
            // 
            this.로그아웃ToolStripMenuItem.Name = "로그아웃ToolStripMenuItem";
            this.로그아웃ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.로그아웃ToolStripMenuItem.Text = "로그아웃";
            this.로그아웃ToolStripMenuItem.Click += new System.EventHandler(this.로그아웃ToolStripMenuItem_Click);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.종료ToolStripMenuItem.Text = "종료";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.종료ToolStripMenuItem_Click);
            // 
            // 편집EToolStripMenuItem
            // 
            this.편집EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.종목설정ToolStripMenuItem,
            this.자동매매설정ToolStripMenuItem,
            this.dB서버설정ToolStripMenuItem});
            this.편집EToolStripMenuItem.Name = "편집EToolStripMenuItem";
            this.편집EToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.편집EToolStripMenuItem.Text = "설정";
            // 
            // 종목설정ToolStripMenuItem
            // 
            this.종목설정ToolStripMenuItem.Name = "종목설정ToolStripMenuItem";
            this.종목설정ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.종목설정ToolStripMenuItem.Text = "관심 종목 설정";
            this.종목설정ToolStripMenuItem.Click += new System.EventHandler(this.종목설정ToolStripMenuItem_Click);
            // 
            // 자동매매설정ToolStripMenuItem
            // 
            this.자동매매설정ToolStripMenuItem.Name = "자동매매설정ToolStripMenuItem";
            this.자동매매설정ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.자동매매설정ToolStripMenuItem.Text = "거래 옵션 설정";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDB);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.cbAccNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(920, 49);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "계좌정보";
            // 
            // btnDB
            // 
            this.btnDB.Location = new System.Drawing.Point(839, 17);
            this.btnDB.Name = "btnDB";
            this.btnDB.Size = new System.Drawing.Size(75, 23);
            this.btnDB.TabIndex = 3;
            this.btnDB.Text = "DataBase";
            this.btnDB.UseVisualStyleBackColor = true;
            this.btnDB.Click += new System.EventHandler(this.btnDB_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(213, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(54, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "조 회";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbAccNo
            // 
            this.cbAccNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAccNo.FormattingEnabled = true;
            this.cbAccNo.Location = new System.Drawing.Point(74, 17);
            this.cbAccNo.Name = "cbAccNo";
            this.cbAccNo.Size = new System.Drawing.Size(133, 20);
            this.cbAccNo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "계좌번호";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdAccount);
            this.groupBox2.Location = new System.Drawing.Point(13, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(920, 149);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "실시간 계좌 현황";
            // 
            // grdAccount
            // 
            this.grdAccount.AllowUserToResizeColumns = false;
            this.grdAccount.AllowUserToResizeRows = false;
            this.grdAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdAccount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdAccount.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grdAccount.Location = new System.Drawing.Point(15, 23);
            this.grdAccount.Name = "grdAccount";
            this.grdAccount.RowHeadersVisible = false;
            this.grdAccount.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.grdAccount.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.grdAccount.RowTemplate.Height = 23;
            this.grdAccount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.grdAccount.Size = new System.Drawing.Size(888, 111);
            this.grdAccount.TabIndex = 0;
            this.grdAccount.TabStop = false;
            this.grdAccount.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdAccount_CellContentClick);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(857, 553);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 5;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.grdSettleListDetail);
            this.groupBox4.Controls.Add(this.grdSettleListSummary);
            this.groupBox4.Controls.Add(this.btnSearchList);
            this.groupBox4.Controls.Add(this.btnAllOne);
            this.groupBox4.Controls.Add(this.cbCode);
            this.groupBox4.Controls.Add(this.txtDay);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.cbChange);
            this.groupBox4.Location = new System.Drawing.Point(362, 247);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(571, 299);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "청산 내역";
            // 
            // grdSettleListDetail
            // 
            this.grdSettleListDetail.AllowUserToResizeColumns = false;
            this.grdSettleListDetail.AllowUserToResizeRows = false;
            this.grdSettleListDetail.BackgroundColor = System.Drawing.Color.White;
            this.grdSettleListDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdSettleListDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdSettleListDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdSettleListDetail.DefaultCellStyle = dataGridViewCellStyle4;
            this.grdSettleListDetail.Location = new System.Drawing.Point(18, 72);
            this.grdSettleListDetail.Name = "grdSettleListDetail";
            this.grdSettleListDetail.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.grdSettleListDetail.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.grdSettleListDetail.RowTemplate.Height = 21;
            this.grdSettleListDetail.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.grdSettleListDetail.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.grdSettleListDetail.Size = new System.Drawing.Size(537, 213);
            this.grdSettleListDetail.TabIndex = 8;
            this.grdSettleListDetail.TabStop = false;
            this.grdSettleListDetail.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdSettleListDetail_CellContentClick);
            // 
            // grdSettleListSummary
            // 
            this.grdSettleListSummary.AllowUserToResizeColumns = false;
            this.grdSettleListSummary.AllowUserToResizeRows = false;
            this.grdSettleListSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdSettleListSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSettleListSummary.ColumnHeadersVisible = false;
            this.grdSettleListSummary.Location = new System.Drawing.Point(18, 44);
            this.grdSettleListSummary.Name = "grdSettleListSummary";
            this.grdSettleListSummary.RowHeadersVisible = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.grdSettleListSummary.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.grdSettleListSummary.RowTemplate.Height = 21;
            this.grdSettleListSummary.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.grdSettleListSummary.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.grdSettleListSummary.Size = new System.Drawing.Size(537, 22);
            this.grdSettleListSummary.TabIndex = 8;
            this.grdSettleListSummary.TabStop = false;
            // 
            // btnSearchList
            // 
            this.btnSearchList.Location = new System.Drawing.Point(501, 15);
            this.btnSearchList.Name = "btnSearchList";
            this.btnSearchList.Size = new System.Drawing.Size(55, 21);
            this.btnSearchList.TabIndex = 3;
            this.btnSearchList.Text = "조 회";
            this.btnSearchList.UseVisualStyleBackColor = true;
            this.btnSearchList.Click += new System.EventHandler(this.btnSerchList_Click);
            // 
            // btnAllOne
            // 
            this.btnAllOne.Location = new System.Drawing.Point(338, 16);
            this.btnAllOne.Name = "btnAllOne";
            this.btnAllOne.Size = new System.Drawing.Size(55, 21);
            this.btnAllOne.TabIndex = 3;
            this.btnAllOne.UseVisualStyleBackColor = true;
            this.btnAllOne.Click += new System.EventHandler(this.btnAllOne_Click);
            // 
            // cbCode
            // 
            this.cbCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCode.FormattingEnabled = true;
            this.cbCode.Location = new System.Drawing.Point(398, 16);
            this.cbCode.MaxDropDownItems = 4;
            this.cbCode.Name = "cbCode";
            this.cbCode.Size = new System.Drawing.Size(97, 20);
            this.cbCode.TabIndex = 1;
            // 
            // txtDay
            // 
            this.txtDay.Location = new System.Drawing.Point(241, 16);
            this.txtDay.Name = "txtDay";
            this.txtDay.Size = new System.Drawing.Size(90, 21);
            this.txtDay.TabIndex = 2;
            this.txtDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDay.TextChanged += new System.EventHandler(this.txtDay_TextChanged);
            this.txtDay.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.txtDay_TextMouseWheel);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "거래일자";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "환산통화";
            // 
            // cbChange
            // 
            this.cbChange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChange.FormattingEnabled = true;
            this.cbChange.Location = new System.Drawing.Point(75, 16);
            this.cbChange.Name = "cbChange";
            this.cbChange.Size = new System.Drawing.Size(96, 20);
            this.cbChange.TabIndex = 1;
            this.cbChange.SelectedIndexChanged += new System.EventHandler(this.cbChange_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdHoga);
            this.groupBox3.Location = new System.Drawing.Point(14, 247);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(332, 300);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "종목 정보";
            // 
            // grdHoga
            // 
            this.grdHoga.AllowUserToResizeColumns = false;
            this.grdHoga.AllowUserToResizeRows = false;
            this.grdHoga.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdHoga.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdHoga.ColumnHeadersVisible = false;
            this.grdHoga.Location = new System.Drawing.Point(16, 20);
            this.grdHoga.Name = "grdHoga";
            this.grdHoga.RowHeadersVisible = false;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.grdHoga.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.grdHoga.RowTemplate.Height = 21;
            this.grdHoga.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.grdHoga.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.grdHoga.Size = new System.Drawing.Size(301, 265);
            this.grdHoga.TabIndex = 8;
            this.grdHoga.TabStop = false;
            // 
            // dB서버설정ToolStripMenuItem
            // 
            this.dB서버설정ToolStripMenuItem.Name = "dB서버설정ToolStripMenuItem";
            this.dB서버설정ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.dB서버설정ToolStripMenuItem.Text = "DB서버 설정";
            this.dB서버설정ToolStripMenuItem.Click += new System.EventHandler(this.dB서버설정ToolStripMenuItem_Click);
            // 
            // Kiwoom_AutoTrade_FF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 633);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbTest);
            this.Controls.Add(this.axKFOpenAPI1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Kiwoom_AutoTrade_FF";
            this.Text = "키움증권 해외선물 자동트레이더";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Kiwoom_AutoTrade_FF_FormClosing);
            this.Load += new System.EventHandler(this.Kiwoom_AutoTrade_FF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axKFOpenAPI1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAccount)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSettleListDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSettleListSummary)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdHoga)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public AxKFOpenAPILib.AxKFOpenAPI axKFOpenAPI1;
        private System.Windows.Forms.ListBox lbTest;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 계좌정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 로그인ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 로그아웃ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 편집EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종목설정ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cbAccNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView grdAccount;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ToolStripMenuItem 자동매매설정ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtDay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbChange;
        private System.Windows.Forms.Button btnAllOne;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.DataGridView grdHoga;
        private System.Windows.Forms.Button btnSearchList;
        private System.Windows.Forms.ComboBox cbCode;
        public System.Windows.Forms.DataGridView grdSettleListSummary;
        public System.Windows.Forms.DataGridView grdSettleListDetail;
        private System.Windows.Forms.Button btnDB;
        private System.Windows.Forms.ToolStripMenuItem dB서버설정ToolStripMenuItem;
    }
}

