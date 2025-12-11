namespace EQ.UI.UserViews
{
    partial class Sequence_View
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();

            // [New] 직접 정의한 Title Label
            this._LabelTitle = new EQ.UI.Controls._Label();

            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._LabelStep = new EQ.UI.Controls._Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._ButtonStop = new EQ.UI.Controls._Button();
            this._ButtonStep = new EQ.UI.Controls._Button();
            this._ButtonRun = new EQ.UI.Controls._Button();
            this._LabelStatus = new EQ.UI.Controls._Label();
            this._LabelSet = new EQ.UI.Controls._Label();
            this._LabelWait = new EQ.UI.Controls._Label();
            this._DataGridViewSteps = new EQ.UI.Controls._DataGridView();
            this._timer = new System.Windows.Forms.Timer(this.components);

            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._DataGridViewSteps)).BeginInit();
            this.SuspendLayout();

            // 
            // _LabelTitle
            // 
            this._LabelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._LabelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._LabelTitle.Font = new System.Drawing.Font("D2Coding", 12F);
            this._LabelTitle.ForeColor = System.Drawing.Color.White;
            this._LabelTitle.Location = new System.Drawing.Point(0, 0);
            this._LabelTitle.Name = "_LabelTitle";
            this._LabelTitle.Size = new System.Drawing.Size(301, 40); // 높이 조정 (59 -> 40)
            this._LabelTitle.TabIndex = 0;
            this._LabelTitle.Text = "Sequence Name";
            this._LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._LabelTitle.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._DataGridViewSteps, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 40); // Title 아래로 배치
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(301, 332);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._LabelStep);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Controls.Add(this._LabelStatus);
            this.panel1.Controls.Add(this._LabelSet);
            this.panel1.Controls.Add(this._LabelWait);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 104);
            this.panel1.TabIndex = 0;
            // 
            // _LabelStep
            // 
            this._LabelStep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._LabelStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this._LabelStep.Font = new System.Drawing.Font("D2Coding", 12F);
            this._LabelStep.ForeColor = System.Drawing.Color.Black;
            this._LabelStep.Location = new System.Drawing.Point(98, 0);
            this._LabelStep.Name = "_LabelStep";
            this._LabelStep.Size = new System.Drawing.Size(197, 31);
            this._LabelStep.TabIndex = 0;
            this._LabelStep.Text = "Step";
            this._LabelStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._LabelStep.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this._ButtonStop, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this._ButtonStep, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this._ButtonRun, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(98, 31);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(197, 33);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // _ButtonStop
            // 
            this._ButtonStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this._ButtonStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ButtonStop.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ButtonStop.ForeColor = System.Drawing.Color.White;
            this._ButtonStop.Location = new System.Drawing.Point(133, 3);
            this._ButtonStop.Name = "_ButtonStop";
            this._ButtonStop.Size = new System.Drawing.Size(61, 27);
            this._ButtonStop.TabIndex = 0;
            this._ButtonStop.Text = "STOP";
            this._ButtonStop.ThemeStyle = EQ.UI.Controls.ThemeStyle.Danger_Red;
            this._ButtonStop.UseVisualStyleBackColor = false;
            this._ButtonStop.Click += new System.EventHandler(this._ButtonStop_Click);
            // 
            // _ButtonStep
            // 
            this._ButtonStep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this._ButtonStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ButtonStep.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ButtonStep.ForeColor = System.Drawing.Color.White;
            this._ButtonStep.Location = new System.Drawing.Point(68, 3);
            this._ButtonStep.Name = "_ButtonStep";
            this._ButtonStep.Size = new System.Drawing.Size(59, 27);
            this._ButtonStep.TabIndex = 0;
            this._ButtonStep.Text = "STEP";
            this._ButtonStep.ThemeStyle = EQ.UI.Controls.ThemeStyle.Info_Sky;
            this._ButtonStep.UseVisualStyleBackColor = false;
            this._ButtonStep.Click += new System.EventHandler(this._ButtonStep_Click);
            // 
            // _ButtonRun
            // 
            this._ButtonRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this._ButtonRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ButtonRun.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ButtonRun.ForeColor = System.Drawing.Color.White;
            this._ButtonRun.Location = new System.Drawing.Point(3, 3);
            this._ButtonRun.Name = "_ButtonRun";
            this._ButtonRun.Size = new System.Drawing.Size(59, 27);
            this._ButtonRun.TabIndex = 0;
            this._ButtonRun.Text = "RUN";
            this._ButtonRun.ThemeStyle = EQ.UI.Controls.ThemeStyle.Success_Green;
            this._ButtonRun.UseVisualStyleBackColor = false;
            this._ButtonRun.Click += new System.EventHandler(this._ButtonRun_Click);
            // 
            // _LabelStatus
            // 
            this._LabelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._LabelStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this._LabelStatus.Font = new System.Drawing.Font("D2Coding", 12F, System.Drawing.FontStyle.Bold);
            this._LabelStatus.ForeColor = System.Drawing.Color.Black;
            this._LabelStatus.Location = new System.Drawing.Point(0, 0);
            this._LabelStatus.Name = "_LabelStatus";
            this._LabelStatus.Size = new System.Drawing.Size(98, 64);
            this._LabelStatus.TabIndex = 0;
            this._LabelStatus.Text = "STOP";
            this._LabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._LabelStatus.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _LabelSet
            // 
            this._LabelSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this._LabelSet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._LabelSet.Font = new System.Drawing.Font("D2Coding", 9F);
            this._LabelSet.ForeColor = System.Drawing.Color.Lime;
            this._LabelSet.Location = new System.Drawing.Point(0, 64);
            this._LabelSet.Name = "_LabelSet";
            this._LabelSet.Size = new System.Drawing.Size(295, 20);
            this._LabelSet.TabIndex = 2;
            this._LabelSet.Text = "Set: None";
            this._LabelSet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._LabelSet.ThemeStyle = EQ.UI.Controls.ThemeStyle.DesignModeOnly;
            // 
            // _LabelWait
            // 
            this._LabelWait.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this._LabelWait.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._LabelWait.Font = new System.Drawing.Font("D2Coding", 9F);
            this._LabelWait.ForeColor = System.Drawing.Color.Yellow;
            this._LabelWait.Location = new System.Drawing.Point(0, 84);
            this._LabelWait.Name = "_LabelWait";
            this._LabelWait.Size = new System.Drawing.Size(295, 20);
            this._LabelWait.TabIndex = 3;
            this._LabelWait.Text = "Wait: None";
            this._LabelWait.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._LabelWait.ThemeStyle = EQ.UI.Controls.ThemeStyle.DesignModeOnly;
            // 
            // _DataGridViewSteps
            // 
            this._DataGridViewSteps.AllowUserToAddRows = false;
            this._DataGridViewSteps.AllowUserToDeleteRows = false;
            this._DataGridViewSteps.AllowUserToResizeRows = false;
            this._DataGridViewSteps.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._DataGridViewSteps.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._DataGridViewSteps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("D2Coding", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._DataGridViewSteps.DefaultCellStyle = dataGridViewCellStyle1;
            this._DataGridViewSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this._DataGridViewSteps.Font = new System.Drawing.Font("D2Coding", 12F);
            this._DataGridViewSteps.Location = new System.Drawing.Point(3, 113);
            this._DataGridViewSteps.MultiSelect = false;
            this._DataGridViewSteps.Name = "_DataGridViewSteps";
            this._DataGridViewSteps.ReadOnly = true;
            this._DataGridViewSteps.RowHeadersVisible = false;
            this._DataGridViewSteps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._DataGridViewSteps.Size = new System.Drawing.Size(295, 216);
            this._DataGridViewSteps.TabIndex = 1;
            this._DataGridViewSteps.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            this._DataGridViewSteps.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._DataGridViewSteps_CellDoubleClick);
            // 
            // _timer
            // 
            this._timer.Interval = 200;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // Sequence_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this._LabelTitle);
            this.Name = "Sequence_View";
            this.Size = new System.Drawing.Size(301, 372);
            this.Load += new System.EventHandler(this.Sequence_View_Load);
            this.Disposed += new System.EventHandler(this.Sequence_View_Disposed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._DataGridViewSteps)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private EQ.UI.Controls._Label _LabelTitle; // [New]
        private EQ.UI.Controls._Label _LabelStatus;
        private EQ.UI.Controls._Label _LabelStep;
        private EQ.UI.Controls._Button _ButtonRun;
        private EQ.UI.Controls._Button _ButtonStep;
        private EQ.UI.Controls._Button _ButtonStop;
        private EQ.UI.Controls._DataGridView _DataGridViewSteps;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private EQ.UI.Controls._Label _LabelWait;
        private EQ.UI.Controls._Label _LabelSet;
    }
}