namespace Kiwoom_AutoTrade_FF
{
    partial class Kwansim
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.tbJongCode = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAllDelete = new System.Windows.Forms.Button();
            this.grdKwansim = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdKwansim)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "종목코드";
            // 
            // tbJongCode
            // 
            this.tbJongCode.Location = new System.Drawing.Point(71, 9);
            this.tbJongCode.Name = "tbJongCode";
            this.tbJongCode.Size = new System.Drawing.Size(100, 21);
            this.tbJongCode.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(178, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(65, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "종목추가";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(248, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(65, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "종목삭제";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAllDelete
            // 
            this.btnAllDelete.Location = new System.Drawing.Point(320, 8);
            this.btnAllDelete.Name = "btnAllDelete";
            this.btnAllDelete.Size = new System.Drawing.Size(65, 23);
            this.btnAllDelete.TabIndex = 2;
            this.btnAllDelete.Text = "전체삭제";
            this.btnAllDelete.UseVisualStyleBackColor = true;
            this.btnAllDelete.Click += new System.EventHandler(this.btnAllDelete_Click);
            // 
            // grdKwansim
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdKwansim.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdKwansim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdKwansim.Location = new System.Drawing.Point(15, 38);
            this.grdKwansim.Name = "grdKwansim";
            this.grdKwansim.RowHeadersVisible = false;
            this.grdKwansim.RowTemplate.Height = 23;
            this.grdKwansim.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.grdKwansim.Size = new System.Drawing.Size(371, 110);
            this.grdKwansim.TabIndex = 3;
            // 
            // Kwansim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 160);
            this.Controls.Add(this.grdKwansim);
            this.Controls.Add(this.btnAllDelete);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tbJongCode);
            this.Controls.Add(this.label1);
            this.Name = "Kwansim";
            this.Text = "관심 종목 설정";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Kwansim_FormClosing);
            this.Load += new System.EventHandler(this.Kwansim_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdKwansim)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbJongCode;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAllDelete;
        private System.Windows.Forms.DataGridView grdKwansim;
    }
}