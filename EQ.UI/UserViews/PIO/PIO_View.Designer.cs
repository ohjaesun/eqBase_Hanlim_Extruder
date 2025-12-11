using EQ.UI.Controls;

namespace EQ.UI.UserViews.Setup
{
    partial class PIO_View
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Dispose는 PIO_View.cs에서 처리

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            flowLayoutPanelMain = new FlowLayoutPanel();
            tmrUpdate = new System.Windows.Forms.Timer(components);
            _PanelMain.SuspendLayout();
            _Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.Size = new Size(1170, 88);
            _LabelTitle.Text = "PIO View";
            // 
            // _PanelMain
            // 
            _PanelMain.Controls.Add(flowLayoutPanelMain);
            _PanelMain.Location = new Point(0, 88);
            _PanelMain.Margin = new Padding(3, 4, 3, 4);
            _PanelMain.Size = new Size(1170, 1064);
            // 
            // _Panel1
            // 
            _Panel1.Margin = new Padding(3, 4, 3, 4);
            _Panel1.Size = new Size(1170, 88);
            // 
            // flowLayoutPanelMain
            // 
            flowLayoutPanelMain.AutoScroll = true;
            flowLayoutPanelMain.BackColor = Color.WhiteSmoke;
            flowLayoutPanelMain.Dock = DockStyle.Fill;
            flowLayoutPanelMain.Location = new Point(0, 0);
            flowLayoutPanelMain.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanelMain.Name = "flowLayoutPanelMain";
            flowLayoutPanelMain.Padding = new Padding(11, 15, 11, 15);
            flowLayoutPanelMain.Size = new Size(1170, 1064);
            flowLayoutPanelMain.TabIndex = 1;
            // 
            // tmrUpdate
            // 
            tmrUpdate.Interval = 300;
            tmrUpdate.Tick += tmrUpdate_Tick;
            // 
            // PIO_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Margin = new Padding(3, 6, 3, 6);
            Name = "PIO_View";
            Size = new Size(1170, 1152);
            _PanelMain.ResumeLayout(false);
            _Panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMain;
        private System.Windows.Forms.Timer tmrUpdate;
    }
}