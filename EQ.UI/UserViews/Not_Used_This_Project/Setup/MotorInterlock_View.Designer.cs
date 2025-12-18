namespace EQ.UI.UserViews
{
    partial class MotorInterlock_View
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._ListTargetAxis = new EQ.UI.Controls._ListBox();
            this._LabelTargetTitle = new EQ.UI.Controls._Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this._GridRules = new EQ.UI.Controls._DataGridView();
            this.panelIO = new EQ.UI.Controls._Panel();
            this._GroupIO = new EQ.UI.Controls._GroupBox();
            this._BtnAddIO = new EQ.UI.Controls._Button();
            this._ComboIODir = new EQ.UI.Controls._ComboBox();
            this._ComboIOSignal = new EQ.UI.Controls._ComboBox();
            this._ComboIOIndex = new EQ.UI.Controls._ComboBox();
            this._ComboIOType = new EQ.UI.Controls._ComboBox();
            this.labelIO = new EQ.UI.Controls._Label();
            this.panelPos = new EQ.UI.Controls._Panel();
            this._GroupPos = new EQ.UI.Controls._GroupBox();
            this._BtnAddPos = new EQ.UI.Controls._Button();
            this._ComboPosDir = new EQ.UI.Controls._ComboBox();
            this._TextRange = new EQ.UI.Controls._TextBox();
            this._TextValue = new EQ.UI.Controls._TextBox();
            this._ComboCondition = new EQ.UI.Controls._ComboBox();
            this._ComboSourceAxis = new EQ.UI.Controls._ComboBox();
            this.labelPos = new EQ.UI.Controls._Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this._BtnDelete = new EQ.UI.Controls._Button();
            this._PanelMain.SuspendLayout();
            this._Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._GridRules)).BeginInit();
            this.panelIO.SuspendLayout();
            this._GroupIO.SuspendLayout();
            this.panelPos.SuspendLayout();
            this._GroupPos.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // _LabelTitle (UserControlBase)
            // 
            this._LabelTitle.Size = new System.Drawing.Size(800, 59);
            this._LabelTitle.Text = "Motor Motion Interlock";
            // 
            // _ButtonSave (UserControlBase)
            // 
            this._ButtonSave.Location = new System.Drawing.Point(800, 0);
            this._ButtonSave.Visible = true;
            // 
            // _PanelMain (UserControlBase)
            // 
            this._PanelMain.Controls.Add(this.splitContainer1);
            this._PanelMain.Size = new System.Drawing.Size(900, 641);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._ListTargetAxis);
            this.splitContainer1.Panel1.Controls.Add(this._LabelTargetTitle);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelRight);
            this.splitContainer1.Size = new System.Drawing.Size(900, 641);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 0;
            // 
            // _ListTargetAxis
            // 
            this._ListTargetAxis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._ListTargetAxis.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ListTargetAxis.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._ListTargetAxis.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ListTargetAxis.ForeColor = System.Drawing.Color.Black;
            this._ListTargetAxis.FormattingEnabled = true;
            this._ListTargetAxis.ItemHeight = 24;
            this._ListTargetAxis.Location = new System.Drawing.Point(0, 30);
            this._ListTargetAxis.Name = "_ListTargetAxis";
            this._ListTargetAxis.Size = new System.Drawing.Size(200, 611);
            this._ListTargetAxis.TabIndex = 1;
            this._ListTargetAxis.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            this._ListTargetAxis.SelectedIndexChanged += new System.EventHandler(this._ListTargetAxis_SelectedIndexChanged);
            // 
            // _LabelTargetTitle
            // 
            this._LabelTargetTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this._LabelTargetTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._LabelTargetTitle.Font = new System.Drawing.Font("D2Coding", 12F);
            this._LabelTargetTitle.ForeColor = System.Drawing.Color.Black;
            this._LabelTargetTitle.Location = new System.Drawing.Point(0, 0);
            this._LabelTargetTitle.Name = "_LabelTargetTitle";
            this._LabelTargetTitle.Size = new System.Drawing.Size(200, 30);
            this._LabelTargetTitle.TabIndex = 0;
            this._LabelTargetTitle.Text = "Target Axis";
            this._LabelTargetTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._LabelTargetTitle.ThemeStyle = EQ.UI.Controls.ThemeStyle.Success_Green;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this._GridRules);
            this.panelRight.Controls.Add(this.panelBottom);
            this.panelRight.Controls.Add(this.panelIO);
            this.panelRight.Controls.Add(this.panelPos);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(696, 641);
            this.panelRight.TabIndex = 0;
            // 
            // _GridRules
            // 
            this._GridRules.AllowUserToAddRows = false;
            this._GridRules.AllowUserToDeleteRows = false;
            this._GridRules.AllowUserToResizeRows = false;
            this._GridRules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._GridRules.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._GridRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("D2Coding", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._GridRules.DefaultCellStyle = dataGridViewCellStyle1;
            this._GridRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this._GridRules.Font = new System.Drawing.Font("D2Coding", 12F);
            this._GridRules.Location = new System.Drawing.Point(0, 210);
            this._GridRules.Name = "_GridRules";
            this._GridRules.RowHeadersVisible = false;
            this._GridRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._GridRules.Size = new System.Drawing.Size(696, 391);
            this._GridRules.TabIndex = 2;
            this._GridRules.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // panelIO
            // 
            this.panelIO.Controls.Add(this._GroupIO);
            this.panelIO.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelIO.Location = new System.Drawing.Point(0, 105);
            this.panelIO.Name = "panelIO";
            this.panelIO.Padding = new System.Windows.Forms.Padding(5);
            this.panelIO.Size = new System.Drawing.Size(696, 105);
            this.panelIO.TabIndex = 1;
            // 
            // _GroupIO
            // 
            this._GroupIO.Controls.Add(this._BtnAddIO);
            this._GroupIO.Controls.Add(this._ComboIODir);
            this._GroupIO.Controls.Add(this._ComboIOSignal);
            this._GroupIO.Controls.Add(this._ComboIOIndex);
            this._GroupIO.Controls.Add(this._ComboIOType);
            this._GroupIO.Controls.Add(this.labelIO);
            this._GroupIO.Dock = System.Windows.Forms.DockStyle.Fill;
            this._GroupIO.Font = new System.Drawing.Font("D2Coding", 12F);
            this._GroupIO.Location = new System.Drawing.Point(5, 5);
            this._GroupIO.Name = "_GroupIO";
            this._GroupIO.Size = new System.Drawing.Size(686, 95);
            this._GroupIO.TabIndex = 0;
            this._GroupIO.TabStop = false;
            this._GroupIO.Text = "I/O Condition";
            this._GroupIO.ThemeStyle = EQ.UI.Controls.ThemeStyle.Highlight_DeepYellow;
            // 
            // _BtnAddIO
            // 
            this._BtnAddIO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this._BtnAddIO.Font = new System.Drawing.Font("D2Coding", 12F);
            this._BtnAddIO.ForeColor = System.Drawing.Color.Black;
            this._BtnAddIO.Location = new System.Drawing.Point(580, 40);
            this._BtnAddIO.Name = "_BtnAddIO";
            this._BtnAddIO.Size = new System.Drawing.Size(90, 40);
            this._BtnAddIO.TabIndex = 5;
            this._BtnAddIO.Text = "Add IO";
            this._BtnAddIO.ThemeStyle = EQ.UI.Controls.ThemeStyle.Info_Sky;
            this._BtnAddIO.Click += new System.EventHandler(this._BtnAddIO_Click);
            // 
            // _ComboIODir
            // 
            this._ComboIODir.BackColor = System.Drawing.SystemColors.Control;
            this._ComboIODir.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._ComboIODir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ComboIODir.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ComboIODir.ForeColor = System.Drawing.SystemColors.ControlText;
            this._ComboIODir.FormattingEnabled = true;
            this._ComboIODir.Location = new System.Drawing.Point(430, 50);
            this._ComboIODir.Name = "_ComboIODir";
            this._ComboIODir.Size = new System.Drawing.Size(120, 27);
            this._ComboIODir.TabIndex = 4;
            this._ComboIODir.ThemeStyle = EQ.UI.Controls.ThemeStyle.Highlight_DeepYellow;
            this._ComboIODir.TooltipText = "Stop Direction";
            // 
            // _ComboIOSignal
            // 
            this._ComboIOSignal.BackColor = System.Drawing.SystemColors.Control;
            this._ComboIOSignal.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._ComboIOSignal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ComboIOSignal.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ComboIOSignal.ForeColor = System.Drawing.SystemColors.ControlText;
            this._ComboIOSignal.FormattingEnabled = true;
            this._ComboIOSignal.Location = new System.Drawing.Point(330, 50);
            this._ComboIOSignal.Name = "_ComboIOSignal";
            this._ComboIOSignal.Size = new System.Drawing.Size(90, 27);
            this._ComboIOSignal.TabIndex = 3;
            this._ComboIOSignal.ThemeStyle = EQ.UI.Controls.ThemeStyle.Highlight_DeepYellow;
            this._ComboIOSignal.TooltipText = "Signal State";
            // 
            // _ComboIOIndex
            // 
            this._ComboIOIndex.BackColor = System.Drawing.SystemColors.Control;
            this._ComboIOIndex.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._ComboIOIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ComboIOIndex.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ComboIOIndex.ForeColor = System.Drawing.SystemColors.ControlText;
            this._ComboIOIndex.FormattingEnabled = true;
            this._ComboIOIndex.Location = new System.Drawing.Point(110, 50);
            this._ComboIOIndex.Name = "_ComboIOIndex";
            this._ComboIOIndex.Size = new System.Drawing.Size(210, 27);
            this._ComboIOIndex.TabIndex = 2;
            this._ComboIOIndex.ThemeStyle = EQ.UI.Controls.ThemeStyle.Highlight_DeepYellow;
            // 
            // _ComboIOType
            // 
            this._ComboIOType.BackColor = System.Drawing.SystemColors.Control;
            this._ComboIOType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._ComboIOType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ComboIOType.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ComboIOType.ForeColor = System.Drawing.SystemColors.ControlText;
            this._ComboIOType.FormattingEnabled = true;
            this._ComboIOType.Location = new System.Drawing.Point(10, 50);
            this._ComboIOType.Name = "_ComboIOType";
            this._ComboIOType.Size = new System.Drawing.Size(90, 27);
            this._ComboIOType.TabIndex = 1;
            this._ComboIOType.ThemeStyle = EQ.UI.Controls.ThemeStyle.Highlight_DeepYellow;
            // 
            // labelIO
            // 
            this.labelIO.AutoSize = true;
            this.labelIO.Location = new System.Drawing.Point(10, 25);
            this.labelIO.Name = "labelIO";
            this.labelIO.Size = new System.Drawing.Size(424, 18);
            this.labelIO.TabIndex = 0;
            this.labelIO.Text = "Type       IO Name             Signal    StopDir";
            // 
            // panelPos
            // 
            this.panelPos.Controls.Add(this._GroupPos);
            this.panelPos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPos.Location = new System.Drawing.Point(0, 0);
            this.panelPos.Name = "panelPos";
            this.panelPos.Padding = new System.Windows.Forms.Padding(5);
            this.panelPos.Size = new System.Drawing.Size(696, 105);
            this.panelPos.TabIndex = 0;
            // 
            // _GroupPos
            // 
            this._GroupPos.Controls.Add(this._BtnAddPos);
            this._GroupPos.Controls.Add(this._ComboPosDir);
            this._GroupPos.Controls.Add(this._TextRange);
            this._GroupPos.Controls.Add(this._TextValue);
            this._GroupPos.Controls.Add(this._ComboCondition);
            this._GroupPos.Controls.Add(this._ComboSourceAxis);
            this._GroupPos.Controls.Add(this.labelPos);
            this._GroupPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this._GroupPos.Font = new System.Drawing.Font("D2Coding", 12F);
            this._GroupPos.Location = new System.Drawing.Point(5, 5);
            this._GroupPos.Name = "_GroupPos";
            this._GroupPos.Size = new System.Drawing.Size(686, 95);
            this._GroupPos.TabIndex = 0;
            this._GroupPos.TabStop = false;
            this._GroupPos.Text = "Position Condition";
            this._GroupPos.ThemeStyle = EQ.UI.Controls.ThemeStyle.Highlight_DeepYellow;
            // 
            // _BtnAddPos
            // 
            this._BtnAddPos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this._BtnAddPos.Font = new System.Drawing.Font("D2Coding", 12F);
            this._BtnAddPos.ForeColor = System.Drawing.Color.Black;
            this._BtnAddPos.Location = new System.Drawing.Point(580, 40);
            this._BtnAddPos.Name = "_BtnAddPos";
            this._BtnAddPos.Size = new System.Drawing.Size(90, 40);
            this._BtnAddPos.TabIndex = 6;
            this._BtnAddPos.Text = "Add Pos";
            this._BtnAddPos.ThemeStyle = EQ.UI.Controls.ThemeStyle.Info_Sky;
            this._BtnAddPos.Click += new System.EventHandler(this._BtnAddPos_Click);
            // 
            // _ComboPosDir
            // 
            this._ComboPosDir.BackColor = System.Drawing.SystemColors.Control;
            this._ComboPosDir.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._ComboPosDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ComboPosDir.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ComboPosDir.ForeColor = System.Drawing.SystemColors.ControlText;
            this._ComboPosDir.FormattingEnabled = true;
            this._ComboPosDir.Location = new System.Drawing.Point(430, 50);
            this._ComboPosDir.Name = "_ComboPosDir";
            this._ComboPosDir.Size = new System.Drawing.Size(120, 27);
            this._ComboPosDir.TabIndex = 5;
            this._ComboPosDir.ThemeStyle = EQ.UI.Controls.ThemeStyle.Highlight_DeepYellow;
            this._ComboPosDir.TooltipText = "Stop Direction";
            // 
            // _TextRange
            // 
            this._TextRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._TextRange.Font = new System.Drawing.Font("D2Coding", 12F);
            this._TextRange.ForeColor = System.Drawing.Color.White;
            this._TextRange.Location = new System.Drawing.Point(360, 50);
            this._TextRange.Name = "_TextRange";
            this._TextRange.Size = new System.Drawing.Size(60, 26);
            this._TextRange.TabIndex = 4;
            this._TextRange.Text = "10";
            this._TextRange.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _TextValue
            // 
            this._TextValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._TextValue.Font = new System.Drawing.Font("D2Coding", 12F);
            this._TextValue.ForeColor = System.Drawing.Color.White;
            this._TextValue.Location = new System.Drawing.Point(280, 50);
            this._TextValue.Name = "_TextValue";
            this._TextValue.Size = new System.Drawing.Size(70, 26);
            this._TextValue.TabIndex = 3;
            this._TextValue.Text = "0";
            this._TextValue.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _ComboCondition
            // 
            this._ComboCondition.BackColor = System.Drawing.SystemColors.Control;
            this._ComboCondition.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._ComboCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ComboCondition.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ComboCondition.ForeColor = System.Drawing.SystemColors.ControlText;
            this._ComboCondition.FormattingEnabled = true;
            this._ComboCondition.Location = new System.Drawing.Point(170, 50);
            this._ComboCondition.Name = "_ComboCondition";
            this._ComboCondition.Size = new System.Drawing.Size(100, 27);
            this._ComboCondition.TabIndex = 2;
            this._ComboCondition.ThemeStyle = EQ.UI.Controls.ThemeStyle.Highlight_DeepYellow;
            // 
            // _ComboSourceAxis
            // 
            this._ComboSourceAxis.BackColor = System.Drawing.SystemColors.Control;
            this._ComboSourceAxis.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._ComboSourceAxis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ComboSourceAxis.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ComboSourceAxis.ForeColor = System.Drawing.SystemColors.ControlText;
            this._ComboSourceAxis.FormattingEnabled = true;
            this._ComboSourceAxis.Location = new System.Drawing.Point(10, 50);
            this._ComboSourceAxis.Name = "_ComboSourceAxis";
            this._ComboSourceAxis.Size = new System.Drawing.Size(150, 27);
            this._ComboSourceAxis.TabIndex = 1;
            this._ComboSourceAxis.ThemeStyle = EQ.UI.Controls.ThemeStyle.Highlight_DeepYellow;
            this._ComboSourceAxis.TooltipText = "Source Axis";
            // 
            // labelPos
            // 
            this.labelPos.AutoSize = true;
            this.labelPos.Location = new System.Drawing.Point(10, 25);
            this.labelPos.Name = "labelPos";
            this.labelPos.Size = new System.Drawing.Size(544, 18);
            this.labelPos.TabIndex = 0;
            this.labelPos.Text = "Source Axis      Condition  Value   Range   StopDir";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this._BtnDelete);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 601);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(696, 40);
            this.panelBottom.TabIndex = 3;
            // 
            // _BtnDelete
            // 
            this._BtnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this._BtnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this._BtnAddPos.Font = new System.Drawing.Font("D2Coding", 12F);
            this._BtnDelete.ForeColor = System.Drawing.Color.White;
            this._BtnDelete.Location = new System.Drawing.Point(576, 0);
            this._BtnDelete.Name = "_BtnDelete";
            this._BtnDelete.Size = new System.Drawing.Size(120, 40);
            this._BtnDelete.TabIndex = 0;
            this._BtnDelete.Text = "Delete Selected";
            this._BtnDelete.ThemeStyle = EQ.UI.Controls.ThemeStyle.Danger_Red;
            this._BtnDelete.Click += new System.EventHandler(this._BtnDelete_Click);
            // 
            // MotorInterlock_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "MotorInterlock_View";
            this.Size = new System.Drawing.Size(900, 700);
            this.Load += new System.EventHandler(this.MotorInterlock_View_Load);
            this._PanelMain.ResumeLayout(false);
            this._Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._GridRules)).EndInit();
            this.panelIO.ResumeLayout(false);
            this._GroupIO.ResumeLayout(false);
            this._GroupIO.PerformLayout();
            this.panelPos.ResumeLayout(false);
            this._GroupPos.ResumeLayout(false);
            this._GroupPos.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private EQ.UI.Controls._ListBox _ListTargetAxis;
        private EQ.UI.Controls._Label _LabelTargetTitle;
        private System.Windows.Forms.Panel panelRight;
        private EQ.UI.Controls._Panel panelPos;
        private EQ.UI.Controls._GroupBox _GroupPos;
        private EQ.UI.Controls._Label labelPos;
        private EQ.UI.Controls._ComboBox _ComboSourceAxis;
        private EQ.UI.Controls._ComboBox _ComboCondition;
        private EQ.UI.Controls._TextBox _TextValue;
        private EQ.UI.Controls._TextBox _TextRange;
        private EQ.UI.Controls._ComboBox _ComboPosDir;
        private EQ.UI.Controls._Button _BtnAddPos;
        private EQ.UI.Controls._Panel panelIO;
        private EQ.UI.Controls._GroupBox _GroupIO;
        private EQ.UI.Controls._Label labelIO;
        private EQ.UI.Controls._ComboBox _ComboIOType;
        private EQ.UI.Controls._ComboBox _ComboIOIndex;
        private EQ.UI.Controls._ComboBox _ComboIOSignal;
        private EQ.UI.Controls._ComboBox _ComboIODir;
        private EQ.UI.Controls._Button _BtnAddIO;
        private EQ.UI.Controls._DataGridView _GridRules;
        private System.Windows.Forms.Panel panelBottom;
        private EQ.UI.Controls._Button _BtnDelete;
    }
}