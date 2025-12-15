namespace EQ.UI.UserViews.Extruder
{
    partial class ExtruderRecipe_View
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            _dataGridView1 = new EQ.UI.Controls._DataGridView();
            _ButtonRefresh = new EQ.UI.Controls._Button();
            _comboRecipe = new EQ.UI.Controls._ComboBox();
            _labelRecipe = new EQ.UI.Controls._Label();
            _Panel2 = new EQ.UI.Controls._Panel();
            _ButtonDown = new EQ.UI.Controls._Button();
            _ButtonUp = new EQ.UI.Controls._Button();
            _ButtonScrollUp = new EQ.UI.Controls._Button();
            _ButtonScrollDown = new EQ.UI.Controls._Button();
            _PanelScroll = new EQ.UI.Controls._Panel();
            _PanelMain.SuspendLayout();
            _Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dataGridView1).BeginInit();
            _Panel2.SuspendLayout();
            _PanelScroll.SuspendLayout();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.Size = new Size(700, 59);
            _LabelTitle.Text = "Extruder Recipe";
            // 
            // _ButtonSave
            // 
            _ButtonSave.Location = new Point(700, 0);
            // 
            // _PanelMain
            // 
            _PanelMain.Controls.Add(_dataGridView1);
            _PanelMain.Controls.Add(_Panel2);
            _PanelMain.Size = new Size(800, 391);
            // 
            // _Panel1
            // 
            _Panel1.Controls.Add(_ButtonRefresh);
            _Panel1.Size = new Size(800, 59);
            _Panel1.Controls.SetChildIndex(_ButtonSave, 0);
            _Panel1.Controls.SetChildIndex(_ButtonRefresh, 0);
            _Panel1.Controls.SetChildIndex(_LabelTitle, 0);
            // 
            // _dataGridView1
            // 
            _dataGridView1.AllowUserToAddRows = false;
            _dataGridView1.AllowUserToDeleteRows = false;
            _dataGridView1.AllowUserToResizeRows = false;
            _dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dataGridView1.BackgroundColor = Color.FromArgb(255, 255, 225);
            _dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 255, 225);
            dataGridViewCellStyle1.Font = new Font("D2Coding", 12F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            _dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            _dataGridView1.Dock = DockStyle.Fill;
            _dataGridView1.Font = new Font("D2Coding", 12F);
            _dataGridView1.Location = new Point(0, 40);
            _dataGridView1.Name = "_dataGridView1";
            _dataGridView1.RowHeadersVisible = false;
            _dataGridView1.RowHeadersWidth = 51;
            _dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridView1.Size = new Size(800, 351);
            _dataGridView1.TabIndex = 0;
            // 
            // _ButtonRefresh
            // 
            _ButtonRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _ButtonRefresh.BackColor = SystemColors.Control;
            _ButtonRefresh.Font = new Font("D2Coding", 12F);
            _ButtonRefresh.ForeColor = SystemColors.ControlText;
            _ButtonRefresh.Location = new Point(1148, 3);
            _ButtonRefresh.Name = "_ButtonRefresh";
            _ButtonRefresh.Size = new Size(85, 30);
            _ButtonRefresh.TabIndex = 1;
            _ButtonRefresh.Text = "Refresh";
            _ButtonRefresh.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _ButtonRefresh.TooltipText = null;
            _ButtonRefresh.UseVisualStyleBackColor = false;
            _ButtonRefresh.Click += _ButtonRefresh_Click;
            // 
            // _comboRecipe
            // 
            _comboRecipe.BackColor = Color.FromArgb(155, 89, 182);
            _comboRecipe.DrawMode = DrawMode.OwnerDrawFixed;
            _comboRecipe.DropDownStyle = ComboBoxStyle.DropDownList;
            _comboRecipe.Font = new Font("D2Coding", 12F);
            _comboRecipe.ForeColor = Color.Black;
            _comboRecipe.FormattingEnabled = true;
            _comboRecipe.Location = new Point(75, 5);
            _comboRecipe.Name = "_comboRecipe";
            _comboRecipe.Size = new Size(200, 27);
            _comboRecipe.TabIndex = 3;
            _comboRecipe.TooltipText = null;
            // 
            // _labelRecipe
            // 
            _labelRecipe.AutoSize = true;
            _labelRecipe.BackColor = SystemColors.Control;
            _labelRecipe.Font = new Font("D2Coding", 12F);
            _labelRecipe.ForeColor = SystemColors.ControlText;
            _labelRecipe.Location = new Point(10, 8);
            _labelRecipe.Name = "_labelRecipe";
            _labelRecipe.Size = new Size(64, 18);
            _labelRecipe.TabIndex = 2;
            _labelRecipe.Text = "Recipe:";
            _labelRecipe.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _labelRecipe.TooltipText = null;
            // 
            // _Panel2
            // 
            _Panel2.BackColor = SystemColors.Control;
            _Panel2.Controls.Add(_ButtonDown);
            _Panel2.Controls.Add(_ButtonUp);
            _Panel2.Controls.Add(_comboRecipe);
            _Panel2.Controls.Add(_labelRecipe);
            _Panel2.Controls.Add(_PanelScroll);
            _Panel2.Dock = DockStyle.Top;
            _Panel2.ForeColor = SystemColors.ControlText;
            _Panel2.Location = new Point(0, 0);
            _Panel2.Name = "_Panel2";
            _Panel2.Size = new Size(800, 40);
            _Panel2.TabIndex = 1;
            // 
            // _PanelScroll
            // 
            _PanelScroll.BackColor = SystemColors.Control;
            _PanelScroll.Controls.Add(_ButtonScrollDown);
            _PanelScroll.Controls.Add(_ButtonScrollUp);
            _PanelScroll.Dock = DockStyle.Right;
            _PanelScroll.ForeColor = SystemColors.ControlText;
            _PanelScroll.Name = "_PanelScroll";
            _PanelScroll.Size = new Size(110, 40);
            _PanelScroll.TabIndex = 6;
            // 
            // _ButtonScrollUp
            // 
            _ButtonScrollUp.BackColor = Color.FromArgb(52, 73, 94);
            _ButtonScrollUp.Font = new Font("D2Coding", 12F);
            _ButtonScrollUp.ForeColor = Color.White;
            _ButtonScrollUp.Location = new Point(5, 5);
            _ButtonScrollUp.Name = "_ButtonScrollUp";
            _ButtonScrollUp.Size = new Size(50, 30);
            _ButtonScrollUp.TabIndex = 0;
            _ButtonScrollUp.Text = "▲";
            _ButtonScrollUp.TooltipText = "Scroll Up";
            _ButtonScrollUp.UseVisualStyleBackColor = false;
            _ButtonScrollUp.Click += _ButtonScrollUp_Click;
            // 
            // _ButtonScrollDown
            // 
            _ButtonScrollDown.BackColor = Color.FromArgb(52, 73, 94);
            _ButtonScrollDown.Font = new Font("D2Coding", 12F);
            _ButtonScrollDown.ForeColor = Color.White;
            _ButtonScrollDown.Location = new Point(55, 5);
            _ButtonScrollDown.Name = "_ButtonScrollDown";
            _ButtonScrollDown.Size = new Size(50, 30);
            _ButtonScrollDown.TabIndex = 1;
            _ButtonScrollDown.Text = "▼";
            _ButtonScrollDown.TooltipText = "Scroll Down";
            _ButtonScrollDown.UseVisualStyleBackColor = false;
            _ButtonScrollDown.Click += _ButtonScrollDown_Click;
            // 
            // _ButtonDown
            // 
            _ButtonDown.BackColor = Color.FromArgb(48, 63, 159);
            _ButtonDown.Font = new Font("D2Coding", 12F);
            _ButtonDown.ForeColor = Color.White;
            _ButtonDown.Location = new Point(340, 5);
            _ButtonDown.Name = "_ButtonDown";
            _ButtonDown.Size = new Size(50, 30);
            _ButtonDown.TabIndex = 5;
            _ButtonDown.Text = "▼";
            _ButtonDown.TooltipText = null;
            _ButtonDown.UseVisualStyleBackColor = false;
            _ButtonDown.Click += _ButtonDown_Click;
            // 
            // _ButtonUp
            // 
            _ButtonUp.BackColor = Color.FromArgb(48, 63, 159);
            _ButtonUp.Font = new Font("D2Coding", 12F);
            _ButtonUp.ForeColor = Color.White;
            _ButtonUp.Location = new Point(285, 5);
            _ButtonUp.Name = "_ButtonUp";
            _ButtonUp.Size = new Size(50, 30);
            _ButtonUp.TabIndex = 4;
            _ButtonUp.Text = "▲";
            _ButtonUp.TooltipText = null;
            _ButtonUp.UseVisualStyleBackColor = false;
            _ButtonUp.Click += _ButtonUp_Click;
            // 
            // ExtruderRecipe_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "ExtruderRecipe_View";
            Size = new Size(800, 450);
            Load += ExtruderRecipe_View_Load;
            _PanelMain.ResumeLayout(false);
            _Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dataGridView1).EndInit();
            _PanelScroll.ResumeLayout(false);
            _Panel2.ResumeLayout(false);
            _Panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private EQ.UI.Controls._DataGridView _dataGridView1;
        private EQ.UI.Controls._Button _ButtonRefresh;
        private EQ.UI.Controls._ComboBox _comboRecipe;
        private EQ.UI.Controls._Label _labelRecipe;
        private Controls._Panel _Panel2;
        private EQ.UI.Controls._Button _ButtonUp;
        private EQ.UI.Controls._Button _ButtonDown;
        private EQ.UI.Controls._Panel _PanelScroll;
        private EQ.UI.Controls._Button _ButtonScrollUp;
        private EQ.UI.Controls._Button _ButtonScrollDown;
    }
}
