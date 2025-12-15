namespace EQ.UI
{
    partial class Form03SETUP
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            extruderSystemGroup1_View1 = new EQ.UI.UserViews.Extruder.ExtruderSystemGroup1_View();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Alignment = TabAlignment.Left;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.ItemSize = new Size(200, 30);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1920, 851);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(extruderSystemGroup1_View1);
            tabPage1.Location = new Point(34, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1882, 843);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Group 1   ";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(34, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1882, 843);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "   Group 2   ";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(34, 4);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1882, 843);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "   PID   ";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // extruderSystemGroup1_View1
            // 
            extruderSystemGroup1_View1.BackColor = Color.FromArgb(240, 240, 240);
            extruderSystemGroup1_View1.Dock = DockStyle.Fill;
            extruderSystemGroup1_View1.Font = new Font("D2Coding", 10F);
            extruderSystemGroup1_View1.Location = new Point(3, 3);
            extruderSystemGroup1_View1.Margin = new Padding(3, 5, 3, 5);
            extruderSystemGroup1_View1.Name = "extruderSystemGroup1_View1";
            extruderSystemGroup1_View1.Size = new Size(1876, 837);
            extruderSystemGroup1_View1.TabIndex = 0;
            // 
            // Form03SETUP
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1920, 851);
            Controls.Add(tabControl1);
            Name = "Form03SETUP";
            Text = "Form3";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private UserViews.Extruder.ExtruderSystemGroup1_View extruderSystemGroup1_View1;
        private TabPage tabPage3;
    }
}