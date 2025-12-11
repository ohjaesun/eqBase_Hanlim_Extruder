namespace EQ.UI.UserViews
{
    partial class Log_View
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
            _SplitContainer = new SplitContainer();
            _FlowLayoutPanel = new FlowLayoutPanel();
            _CheckBoxAll = new EQ.UI.Controls._CheckBox();
            _CheckBoxAutoScroll = new EQ.UI.Controls._CheckBox();
            _CheckBoxPause = new EQ.UI.Controls._CheckBox();
            _ButtonClear = new EQ.UI.Controls._Button();
            _RichTextBoxLog = new RichTextBox();
            _PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_SplitContainer).BeginInit();
            _SplitContainer.Panel1.SuspendLayout();
            _SplitContainer.Panel2.SuspendLayout();
            _SplitContainer.SuspendLayout();
            _FlowLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.Size = new Size(619, 59);
            _LabelTitle.Text = "Log_View";
            // 
            // _ButtonSave
            // 
            _ButtonSave.Location = new Point(619, 0);
            _ButtonSave.Visible = false;
            // 
            // _PanelMain
            // 
            _PanelMain.Controls.Add(_SplitContainer);
            _PanelMain.Size = new Size(719, 425);
            // 
            // _SplitContainer
            // 
            _SplitContainer.Dock = DockStyle.Fill;
            _SplitContainer.FixedPanel = FixedPanel.Panel1;
            _SplitContainer.Location = new Point(0, 0);
            _SplitContainer.Name = "_SplitContainer";
            _SplitContainer.Orientation = Orientation.Horizontal;
            // 
            // _SplitContainer.Panel1
            // 
            _SplitContainer.Panel1.Controls.Add(_FlowLayoutPanel);
            // 
            // _SplitContainer.Panel2
            // 
            _SplitContainer.Panel2.Controls.Add(_RichTextBoxLog);
            _SplitContainer.Size = new Size(719, 425);
            _SplitContainer.SplitterDistance = 70;
            _SplitContainer.TabIndex = 0;
            // 
            // _FlowLayoutPanel
            // 
            _FlowLayoutPanel.AutoScroll = true;
            _FlowLayoutPanel.Controls.Add(_CheckBoxAll);
            _FlowLayoutPanel.Controls.Add(_CheckBoxAutoScroll);
            _FlowLayoutPanel.Controls.Add(_CheckBoxPause);
            _FlowLayoutPanel.Controls.Add(_ButtonClear);
            _FlowLayoutPanel.Dock = DockStyle.Fill;
            _FlowLayoutPanel.Location = new Point(0, 0);
            _FlowLayoutPanel.Name = "_FlowLayoutPanel";
            _FlowLayoutPanel.Size = new Size(719, 70);
            _FlowLayoutPanel.TabIndex = 0;
            // 
            // _CheckBoxAll
            // 
            _CheckBoxAll.AutoSize = true;
            _CheckBoxAll.BackColor = Color.FromArgb(46, 204, 113);
            _CheckBoxAll.Checked = true;
            _CheckBoxAll.CheckState = CheckState.Checked;
            _CheckBoxAll.Font = new Font("D2Coding", 12F);
            _CheckBoxAll.ForeColor = Color.Black;
            _CheckBoxAll.Location = new Point(3, 3);
            _CheckBoxAll.Name = "_CheckBoxAll";
            _CheckBoxAll.Size = new Size(51, 22);
            _CheckBoxAll.TabIndex = 0;
            _CheckBoxAll.Text = "ALL";
            _CheckBoxAll.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _CheckBoxAll.UseVisualStyleBackColor = false;
            _CheckBoxAll.CheckedChanged += _CheckBoxAll_CheckedChanged;
            // 
            // _CheckBoxAutoScroll
            // 
            _CheckBoxAutoScroll.AutoSize = true;
            _CheckBoxAutoScroll.BackColor = Color.FromArgb(52, 152, 219);
            _CheckBoxAutoScroll.Checked = true;
            _CheckBoxAutoScroll.CheckState = CheckState.Checked;
            _CheckBoxAutoScroll.Font = new Font("D2Coding", 12F);
            _CheckBoxAutoScroll.ForeColor = Color.Black;
            _CheckBoxAutoScroll.Location = new Point(60, 3);
            _CheckBoxAutoScroll.Name = "_CheckBoxAutoScroll";
            _CheckBoxAutoScroll.Size = new Size(107, 22);
            _CheckBoxAutoScroll.TabIndex = 1;
            _CheckBoxAutoScroll.Text = "AutoScroll";
            _CheckBoxAutoScroll.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _CheckBoxAutoScroll.UseVisualStyleBackColor = false;
            // 
            // _CheckBoxPause
            // 
            _CheckBoxPause.AutoSize = true;
            _CheckBoxPause.BackColor = Color.FromArgb(241, 196, 15);
            _CheckBoxPause.Font = new Font("D2Coding", 12F);
            _CheckBoxPause.ForeColor = Color.Black;
            _CheckBoxPause.Location = new Point(173, 3);
            _CheckBoxPause.Name = "_CheckBoxPause";
            _CheckBoxPause.Size = new Size(67, 22);
            _CheckBoxPause.TabIndex = 2;
            _CheckBoxPause.Text = "Pause";
            _CheckBoxPause.ThemeStyle = UI.Controls.ThemeStyle.Warning_Yellow;
            _CheckBoxPause.UseVisualStyleBackColor = false;
            // 
            // _ButtonClear
            // 
            _ButtonClear.BackColor = Color.FromArgb(231, 76, 60);
            _ButtonClear.Font = new Font("D2Coding", 12F);
            _ButtonClear.ForeColor = Color.Black;
            _ButtonClear.Location = new Point(246, 3);
            _ButtonClear.Name = "_ButtonClear";
            _ButtonClear.Size = new Size(70, 25);
            _ButtonClear.TabIndex = 3;
            _ButtonClear.Text = "Clear";
            _ButtonClear.ThemeStyle = UI.Controls.ThemeStyle.Danger_Red;
            _ButtonClear.TooltipText = null;
            _ButtonClear.UseVisualStyleBackColor = false;
            _ButtonClear.Click += _ButtonClear_Click;
            // 
            // _RichTextBoxLog
            // 
            _RichTextBoxLog.BackColor = Color.White;
            _RichTextBoxLog.Dock = DockStyle.Fill;
            _RichTextBoxLog.Font = new Font("D2Coding", 9F);
            _RichTextBoxLog.ForeColor = Color.White;
            _RichTextBoxLog.Location = new Point(0, 0);
            _RichTextBoxLog.Name = "_RichTextBoxLog";
            _RichTextBoxLog.ReadOnly = true;
            _RichTextBoxLog.Size = new Size(719, 351);
            _RichTextBoxLog.TabIndex = 0;
            _RichTextBoxLog.Text = "";
            _RichTextBoxLog.WordWrap = false;
            // 
            // Log_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "Log_View";
            Size = new Size(719, 484);
            Load += Log_View_Load;
            _PanelMain.ResumeLayout(false);
            _SplitContainer.Panel1.ResumeLayout(false);
            _SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_SplitContainer).EndInit();
            _SplitContainer.ResumeLayout(false);
            _FlowLayoutPanel.ResumeLayout(false);
            _FlowLayoutPanel.PerformLayout();
            ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.FlowLayoutPanel _FlowLayoutPanel;
        private Controls._CheckBox _CheckBoxAll;
        private Controls._CheckBox _CheckBoxAutoScroll;
        private Controls._CheckBox _CheckBoxPause;
        private Controls._Button _ButtonClear;
        private System.Windows.Forms.RichTextBox _RichTextBoxLog;
        private SplitContainer _SplitContainer;
    }
}