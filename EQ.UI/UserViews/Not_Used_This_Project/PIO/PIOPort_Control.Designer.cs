using EQ.UI.Controls;

namespace EQ.UI.UserViews.Setup.Components
{
    partial class PIOPort_Control
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
            this.grpBox = new EQ.UI.Controls._GroupBox();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelStatus = new EQ.UI.Controls._Panel();
            this.btnLoad = new EQ.UI.Controls._Button();
            this.lblStateValue = new EQ.UI.Controls._Label();
            this.lblStateTitle = new EQ.UI.Controls._Label();
            this.grpSignals = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelSignals = new System.Windows.Forms.TableLayoutPanel();
            this.lblTxHeader = new EQ.UI.Controls._Label();
            this.lblRxHeader = new EQ.UI.Controls._Label();
            this.grpBox.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.grpSignals.SuspendLayout();
            this.tableLayoutPanelSignals.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBox
            // 
            this.grpBox.Controls.Add(this.tableLayoutPanelMain);
            this.grpBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBox.Font = new System.Drawing.Font("D2Coding", 12F);
            this.grpBox.Location = new System.Drawing.Point(0, 0);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(400, 300);
            this.grpBox.TabIndex = 0;
            this.grpBox.TabStop = false;
            this.grpBox.Text = "PIO Port ID";
            this.grpBox.ThemeStyle = EQ.UI.Controls.ThemeStyle.Highlight_DeepYellow;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanelMain.Controls.Add(this.panelStatus, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.grpSignals, 1, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(394, 275);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelStatus
            // 
            this.panelStatus.Controls.Add(this.btnLoad);
            this.panelStatus.Controls.Add(this.lblStateValue);
            this.panelStatus.Controls.Add(this.lblStateTitle);
            this.panelStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatus.Location = new System.Drawing.Point(3, 3);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(131, 269);
            this.panelStatus.TabIndex = 0;
            this.panelStatus.ThemeStyle = EQ.UI.Controls.ThemeStyle.Default;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(5, 210);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(120, 50);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "LOAD";
            this.btnLoad.ThemeStyle = EQ.UI.Controls.ThemeStyle.Primary_Indigo;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblStateValue
            // 
            this.lblStateValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStateValue.BackColor = System.Drawing.Color.White;
            this.lblStateValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStateValue.Location = new System.Drawing.Point(5, 45);
            this.lblStateValue.Name = "lblStateValue";
            this.lblStateValue.Size = new System.Drawing.Size(120, 30);
            this.lblStateValue.TabIndex = 1;
            this.lblStateValue.Text = "Idle";
            this.lblStateValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStateValue.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // lblStateTitle
            // 
            this.lblStateTitle.AutoSize = true;
            this.lblStateTitle.Font = new System.Drawing.Font("D2Coding", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStateTitle.Location = new System.Drawing.Point(5, 15);
            this.lblStateTitle.Name = "lblStateTitle";
            this.lblStateTitle.Size = new System.Drawing.Size(104, 18);
            this.lblStateTitle.TabIndex = 0;
            this.lblStateTitle.Text = "Current State";
            this.lblStateTitle.ThemeStyle = EQ.UI.Controls.ThemeStyle.Default;
            // 
            // grpSignals
            // 
            this.grpSignals.Controls.Add(this.tableLayoutPanelSignals);
            this.grpSignals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSignals.Location = new System.Drawing.Point(140, 3);
            this.grpSignals.Name = "grpSignals";
            this.grpSignals.Size = new System.Drawing.Size(251, 269);
            this.grpSignals.TabIndex = 1;
            this.grpSignals.TabStop = false;
            this.grpSignals.Text = "Signals";
            // 
            // tableLayoutPanelSignals
            // 
            this.tableLayoutPanelSignals.ColumnCount = 2;
            this.tableLayoutPanelSignals.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSignals.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSignals.Controls.Add(this.lblTxHeader, 0, 0);
            this.tableLayoutPanelSignals.Controls.Add(this.lblRxHeader, 1, 0);
            this.tableLayoutPanelSignals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelSignals.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanelSignals.Name = "tableLayoutPanelSignals";
            this.tableLayoutPanelSignals.RowCount = 2;
            this.tableLayoutPanelSignals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelSignals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSignals.Size = new System.Drawing.Size(245, 244);
            this.tableLayoutPanelSignals.TabIndex = 0;
            // 
            // lblTxHeader
            // 
            this.lblTxHeader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTxHeader.AutoSize = true;
            this.lblTxHeader.Font = new System.Drawing.Font("D2Coding", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTxHeader.Location = new System.Drawing.Point(26, 7);
            this.lblTxHeader.Name = "lblTxHeader";
            this.lblTxHeader.Size = new System.Drawing.Size(70, 15);
            this.lblTxHeader.TabIndex = 0;
            this.lblTxHeader.Text = "Tx (Output)";
            this.lblTxHeader.ThemeStyle = EQ.UI.Controls.ThemeStyle.Default;
            // 
            // lblRxHeader
            // 
            this.lblRxHeader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRxHeader.AutoSize = true;
            this.lblRxHeader.Font = new System.Drawing.Font("D2Coding", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblRxHeader.Location = new System.Drawing.Point(150, 7);
            this.lblRxHeader.Name = "lblRxHeader";
            this.lblRxHeader.Size = new System.Drawing.Size(63, 15);
            this.lblRxHeader.TabIndex = 1;
            this.lblRxHeader.Text = "Rx (Input)";
            this.lblRxHeader.ThemeStyle = EQ.UI.Controls.ThemeStyle.Default;
            // 
            // PIOPort_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpBox);
            this.Name = "PIOPort_Control";
            this.Size = new System.Drawing.Size(400, 300);
            this.grpBox.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.grpSignals.ResumeLayout(false);
            this.tableLayoutPanelSignals.ResumeLayout(false);
            this.tableLayoutPanelSignals.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private EQ.UI.Controls._GroupBox grpBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private EQ.UI.Controls._Panel panelStatus;
        private EQ.UI.Controls._Label lblStateTitle;
        private EQ.UI.Controls._Label lblStateValue;
        private EQ.UI.Controls._Button btnLoad;
        private System.Windows.Forms.GroupBox grpSignals;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSignals;
        private EQ.UI.Controls._Label lblTxHeader;
        private EQ.UI.Controls._Label lblRxHeader;
    }
}