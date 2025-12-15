namespace EQ.UI
{
    partial class Form01AUTO
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
            temp_View1 = new EQ.UI.UserViews.EQ_HanLim_Extuder.Temp_View();
            chart_View1 = new EQ.UI.UserViews.EQ_HanLim_Extuder.Chart_View();
            _Panel1 = new EQ.UI.Controls._Panel();
            _Panel2 = new EQ.UI.Controls._Panel();
            _Panel1.SuspendLayout();
            _Panel2.SuspendLayout();
            SuspendLayout();
            // 
            // temp_View1
            // 
            temp_View1.Dock = DockStyle.Left;
            temp_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            temp_View1.Location = new Point(0, 0);
            temp_View1.Margin = new Padding(3, 4, 3, 4);
            temp_View1.Name = "temp_View1";
            temp_View1.Size = new Size(219, 190);
            temp_View1.TabIndex = 0;
            // 
            // chart_View1
            // 
            chart_View1.Dock = DockStyle.Fill;
            chart_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            chart_View1.Location = new Point(0, 0);
            chart_View1.Margin = new Padding(3, 4, 3, 4);
            chart_View1.Name = "chart_View1";
            chart_View1.Size = new Size(993, 642);
            chart_View1.TabIndex = 1;
            // 
            // _Panel1
            // 
            _Panel1.BackColor = SystemColors.Control;
            _Panel1.Controls.Add(temp_View1);
            _Panel1.Dock = DockStyle.Top;
            _Panel1.ForeColor = SystemColors.ControlText;
            _Panel1.Location = new Point(0, 0);
            _Panel1.Name = "_Panel1";
            _Panel1.Size = new Size(993, 190);
            _Panel1.TabIndex = 2;
            // 
            // _Panel2
            // 
            _Panel2.BackColor = SystemColors.Control;
            _Panel2.Controls.Add(chart_View1);
            _Panel2.Dock = DockStyle.Fill;
            _Panel2.ForeColor = SystemColors.ControlText;
            _Panel2.Location = new Point(0, 190);
            _Panel2.Name = "_Panel2";
            _Panel2.Size = new Size(993, 642);
            _Panel2.TabIndex = 2;
            // 
            // Form01AUTO
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 832);
            Controls.Add(_Panel2);
            Controls.Add(_Panel1);
            Name = "Form01AUTO";
            Text = "Form1";
            _Panel1.ResumeLayout(false);
            _Panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private UserViews.EQ_HanLim_Extuder.Temp_View temp_View1;
        private UserViews.EQ_HanLim_Extuder.Chart_View chart_View1;
        private Controls._Panel _Panel1;
        private Controls._Panel _Panel2;
    }
}