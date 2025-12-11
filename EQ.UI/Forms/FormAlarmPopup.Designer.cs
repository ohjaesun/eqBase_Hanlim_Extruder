namespace EQ.UI.Forms
{
    partial class FormAlarmPopup
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
            this._PanelTitle = new EQ.UI.Controls._Panel();
            this._LabelTitle = new EQ.UI.Controls._Label();
            this.labelMessage = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._ButtonSilence = new EQ.UI.Controls._Button();
            this._ButtonReset = new EQ.UI.Controls._Button();
            this._ButtonClose = new EQ.UI.Controls._Button();
            this._PanelTitle.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _PanelTitle
            // 
            this._PanelTitle.Controls.Add(this._LabelTitle);
            this._PanelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._PanelTitle.Location = new System.Drawing.Point(0, 0);
            this._PanelTitle.Name = "_PanelTitle";
            this._PanelTitle.Size = new System.Drawing.Size(550, 40); // 너비 조금 더 확보
            this._PanelTitle.TabIndex = 0;
            // 
            // _LabelTitle
            // 
            this._LabelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this._LabelTitle.Font = new System.Drawing.Font("D2Coding", 14F, System.Drawing.FontStyle.Bold);
            this._LabelTitle.Location = new System.Drawing.Point(0, 0);
            this._LabelTitle.Name = "_LabelTitle";
            this._LabelTitle.Size = new System.Drawing.Size(550, 40);
            this._LabelTitle.TabIndex = 0;
            this._LabelTitle.Text = "ALARM";
            this._LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._LabelTitle.ThemeStyle = EQ.UI.Controls.ThemeStyle.Danger_Red;
            // 
            // labelMessage
            // 
            this.labelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMessage.Font = new System.Drawing.Font("D2Coding", 12F);
            this.labelMessage.Location = new System.Drawing.Point(0, 40);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Padding = new System.Windows.Forms.Padding(10);
            this.labelMessage.Size = new System.Drawing.Size(550, 160);
            this.labelMessage.TabIndex = 1;
            this.labelMessage.Text = "Alarm Message Here...";
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.Controls.Add(this._ButtonSilence, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._ButtonReset, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._ButtonClose, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 200);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(550, 50);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // _ButtonSilence
            // 
            this._ButtonSilence.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ButtonSilence.Location = new System.Drawing.Point(3, 3);
            this._ButtonSilence.Name = "_ButtonSilence";
            this._ButtonSilence.Size = new System.Drawing.Size(177, 44);
            this._ButtonSilence.TabIndex = 0;
            this._ButtonSilence.Text = "부저 끄기";
            this._ButtonSilence.ThemeStyle = EQ.UI.Controls.ThemeStyle.Warning_Yellow;
            // 
            // _ButtonReset
            // 
            this._ButtonReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ButtonReset.Location = new System.Drawing.Point(186, 3);
            this._ButtonReset.Name = "_ButtonReset";
            this._ButtonReset.Size = new System.Drawing.Size(177, 44);
            this._ButtonReset.TabIndex = 1;
            this._ButtonReset.Text = "초기화 (Reset)";
            this._ButtonReset.ThemeStyle = EQ.UI.Controls.ThemeStyle.Primary_Indigo;
            // 
            // _ButtonClose
            // 
            this._ButtonClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ButtonClose.Location = new System.Drawing.Point(369, 3);
            this._ButtonClose.Name = "_ButtonClose";
            this._ButtonClose.Size = new System.Drawing.Size(178, 44);
            this._ButtonClose.TabIndex = 2;
            this._ButtonClose.Text = "닫기";
            this._ButtonClose.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // FormAlarmPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 250);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this._PanelTitle);
            this.Name = "FormAlarmPopup";
            this.Text = "Alarm";
            this._PanelTitle.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Controls._Panel _PanelTitle;
        private Controls._Label _LabelTitle;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls._Button _ButtonSilence;
        private Controls._Button _ButtonReset;
        private Controls._Button _ButtonClose;
    }
}