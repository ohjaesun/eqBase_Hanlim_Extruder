namespace EQ.UI.UserViews
{
    partial class SequencesPanel_View
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
            _FlowLayoutPanel = new FlowLayoutPanel();
            _LabelTimeout = new EQ.UI.Controls._Label();
            _PanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.Size = new Size(600, 59);
            // 
            // _ButtonSave
            // 
            _ButtonSave.Location = new Point(600, 0);
            _ButtonSave.Visible = false;
            // 
            // _PanelMain
            // 
            _PanelMain.Controls.Add(_FlowLayoutPanel);
            _PanelMain.Controls.Add(_LabelTimeout);
            _PanelMain.Size = new Size(700, 400);
            // 
            // _FlowLayoutPanel
            // 
            _FlowLayoutPanel.AutoScroll = true;
            _FlowLayoutPanel.Dock = DockStyle.Fill;
            _FlowLayoutPanel.Location = new Point(0, 25);
            _FlowLayoutPanel.Name = "_FlowLayoutPanel";
            _FlowLayoutPanel.Size = new Size(700, 375);
            _FlowLayoutPanel.TabIndex = 0;
            // 
            // _LabelTimeout
            // 
            _LabelTimeout.BackColor = Color.FromArgb(241, 196, 15);
            _LabelTimeout.Dock = DockStyle.Top;
            _LabelTimeout.Font = new Font("D2Coding", 12F);
            _LabelTimeout.ForeColor = Color.Black;
            _LabelTimeout.Location = new Point(0, 0);
            _LabelTimeout.Name = "_LabelTimeout";
            _LabelTimeout.Size = new Size(700, 25);
            _LabelTimeout.TabIndex = 1;
            _LabelTimeout.Text = "Timeout: 0 min";
            _LabelTimeout.TextAlign = ContentAlignment.MiddleCenter;
            _LabelTimeout.ThemeStyle = UI.Controls.ThemeStyle.Warning_Yellow;
            _LabelTimeout.TooltipText = null;
            // 
            // SequencesPanel_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "SequencesPanel_View";
            Size = new Size(700, 459);
            Load += All_Sequences_View_Load;
            _PanelMain.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.FlowLayoutPanel _FlowLayoutPanel;
        private Controls._Label _LabelTimeout;
    }
}