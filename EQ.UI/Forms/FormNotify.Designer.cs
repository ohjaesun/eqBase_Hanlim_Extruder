// EQ.UI/FormNotify.Designer.cs
namespace EQ.UI
{
    partial class FormNotify
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
            this._ButtonOK = new EQ.UI.Controls._Button();
            this._PanelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // _PanelTitle
            // 
            this._PanelTitle.Controls.Add(this._LabelTitle);
            this._PanelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._PanelTitle.Location = new System.Drawing.Point(0, 0);
            this._PanelTitle.Name = "_PanelTitle";
            this._PanelTitle.Size = new System.Drawing.Size(450, 40);
            this._PanelTitle.TabIndex = 0;
            // 
            // _LabelTitle
            // 
            this._LabelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this._LabelTitle.Font = new System.Drawing.Font("D2Coding", 14F, System.Drawing.FontStyle.Bold);
            this._LabelTitle.Location = new System.Drawing.Point(0, 0);
            this._LabelTitle.Name = "_LabelTitle";
            this._LabelTitle.Size = new System.Drawing.Size(450, 40);
            this._LabelTitle.TabIndex = 0;
            this._LabelTitle.Text = "Title";
            this._LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._LabelTitle.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // labelMessage
            // 
            this.labelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMessage.Location = new System.Drawing.Point(0, 40);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Padding = new System.Windows.Forms.Padding(10);
            this.labelMessage.Size = new System.Drawing.Size(450, 115);
            this.labelMessage.TabIndex = 1;
            this.labelMessage.Text = "Message content goes here.";
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _ButtonOK
            // 
            this._ButtonOK.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._ButtonOK.Location = new System.Drawing.Point(0, 155);
            this._ButtonOK.Name = "_ButtonOK";
            this._ButtonOK.Size = new System.Drawing.Size(450, 45);
            this._ButtonOK.TabIndex = 2;
            this._ButtonOK.Text = "확 인 (Close)";
            this._ButtonOK.ThemeStyle = EQ.UI.Controls.ThemeStyle.Primary_Indigo;
            // 
            // FormNotify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 200);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this._ButtonOK);
            this.Controls.Add(this._PanelTitle);
            this.Name = "FormNotify";
            this.Text = "FormNotify";
            this._PanelTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        // (수정) 컨트롤 변수들을 클래스 멤버로 선언
        private Controls._Panel _PanelTitle;
        private Controls._Label _LabelTitle;
        private System.Windows.Forms.Label labelMessage;
        private Controls._Button _ButtonOK;
    }
}