
namespace EQ.UI.UserViews.Setup
{
    partial class EtherCAT_SDO_View
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
            _LabelTitle = new EQ.UI.Controls._Label();
            tableLayoutPanelMain = new TableLayoutPanel();
            groupBoxSDO = new GroupBox();
            tblSdo = new TableLayoutPanel();
            lblSdoSlave = new EQ.UI.Controls._Label();
            txtSdoSlave = new EQ.UI.Controls._TextBox();
            lblSdoIndex = new EQ.UI.Controls._Label();
            txtSdoIndex = new EQ.UI.Controls._TextBox();
            lblSdoSubIndex = new EQ.UI.Controls._Label();
            txtSdoSubIndex = new EQ.UI.Controls._TextBox();
            lblSdoData = new EQ.UI.Controls._Label();
            txtSdoData = new EQ.UI.Controls._TextBox();
            panelSdoButtons = new Panel();
            btnSdoWrite = new EQ.UI.Controls._Button();
            btnSdoRead = new EQ.UI.Controls._Button();
            lblSdoResult = new EQ.UI.Controls._Label();
            txtSdoResult = new EQ.UI.Controls._TextBox();
            groupBoxPDO = new GroupBox();
            tblPdo = new TableLayoutPanel();
            lblPdoMaster = new EQ.UI.Controls._Label();
            txtPdoMaster = new EQ.UI.Controls._TextBox();
            lblPdoSlave = new EQ.UI.Controls._Label();
            txtPdoSlave = new EQ.UI.Controls._TextBox();
            lblPdoIndex = new EQ.UI.Controls._Label();
            txtPdoIndex = new EQ.UI.Controls._TextBox();
            lblPdoSubIndex = new EQ.UI.Controls._Label();
            txtPdoSubIndex = new EQ.UI.Controls._TextBox();
            lblPdoData = new EQ.UI.Controls._Label();
            txtPdoData = new EQ.UI.Controls._TextBox();
            panelPdoButtons = new Panel();
            btnPdoWrite = new EQ.UI.Controls._Button();
            btnPdoRead = new EQ.UI.Controls._Button();
            lblPdoResult = new EQ.UI.Controls._Label();
            txtPdoResult = new EQ.UI.Controls._TextBox();
            tableLayoutPanelMain.SuspendLayout();
            groupBoxSDO.SuspendLayout();
            tblSdo.SuspendLayout();
            panelSdoButtons.SuspendLayout();
            groupBoxPDO.SuspendLayout();
            tblPdo.SuspendLayout();
            panelPdoButtons.SuspendLayout();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.BackColor = Color.FromArgb(46, 204, 113);
            _LabelTitle.Dock = DockStyle.Top;
            _LabelTitle.Font = new Font("D2Coding", 12F);
            _LabelTitle.ForeColor = Color.Black;
            _LabelTitle.Location = new Point(0, 0);
            _LabelTitle.Name = "_LabelTitle";
            _LabelTitle.Size = new Size(960, 46);
            _LabelTitle.TabIndex = 0;
            _LabelTitle.Text = "EtherCAT SDO/PDO Control";
            _LabelTitle.TextAlign = ContentAlignment.MiddleCenter;
            _LabelTitle.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _LabelTitle.TooltipText = null;
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 2;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelMain.Controls.Add(groupBoxSDO, 0, 0);
            tableLayoutPanelMain.Controls.Add(groupBoxPDO, 1, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 46);
            tableLayoutPanelMain.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 1;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(960, 418);
            tableLayoutPanelMain.TabIndex = 1;
            // 
            // groupBoxSDO
            // 
            groupBoxSDO.Controls.Add(tblSdo);
            groupBoxSDO.Dock = DockStyle.Fill;
            groupBoxSDO.Font = new Font("D2Coding", 12F);
            groupBoxSDO.Location = new Point(3, 4);
            groupBoxSDO.Margin = new Padding(3, 4, 3, 4);
            groupBoxSDO.Name = "groupBoxSDO";
            groupBoxSDO.Padding = new Padding(3, 4, 3, 4);
            groupBoxSDO.Size = new Size(474, 410);
            groupBoxSDO.TabIndex = 0;
            groupBoxSDO.TabStop = false;
            groupBoxSDO.Text = "SDO (Service Data Object)";
            // 
            // tblSdo
            // 
            tblSdo.ColumnCount = 2;
            tblSdo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tblSdo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblSdo.Controls.Add(lblSdoSlave, 0, 0);
            tblSdo.Controls.Add(txtSdoSlave, 1, 0);
            tblSdo.Controls.Add(lblSdoIndex, 0, 1);
            tblSdo.Controls.Add(txtSdoIndex, 1, 1);
            tblSdo.Controls.Add(lblSdoSubIndex, 0, 2);
            tblSdo.Controls.Add(txtSdoSubIndex, 1, 2);
            tblSdo.Controls.Add(lblSdoData, 0, 3);
            tblSdo.Controls.Add(txtSdoData, 1, 3);
            tblSdo.Controls.Add(panelSdoButtons, 0, 4);
            tblSdo.Controls.Add(lblSdoResult, 0, 5);
            tblSdo.Controls.Add(txtSdoResult, 1, 5);
            tblSdo.Dock = DockStyle.Fill;
            tblSdo.Location = new Point(3, 23);
            tblSdo.Margin = new Padding(3, 4, 3, 4);
            tblSdo.Name = "tblSdo";
            tblSdo.RowCount = 7;
            tblSdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tblSdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tblSdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tblSdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tblSdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tblSdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tblSdo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblSdo.Size = new Size(468, 383);
            tblSdo.TabIndex = 0;
            // 
            // lblSdoSlave
            // 
            lblSdoSlave.BackColor = Color.FromArgb(149, 165, 166);
            lblSdoSlave.Dock = DockStyle.Fill;
            lblSdoSlave.Font = new Font("D2Coding", 12F);
            lblSdoSlave.ForeColor = Color.White;
            lblSdoSlave.Location = new Point(3, 0);
            lblSdoSlave.Name = "lblSdoSlave";
            lblSdoSlave.Size = new Size(154, 52);
            lblSdoSlave.TabIndex = 0;
            lblSdoSlave.Text = "Slave ID:";
            lblSdoSlave.TextAlign = ContentAlignment.MiddleRight;
            lblSdoSlave.TooltipText = null;
            // 
            // txtSdoSlave
            // 
            txtSdoSlave.BackColor = Color.FromArgb(149, 165, 166);
            txtSdoSlave.Dock = DockStyle.Fill;
            txtSdoSlave.Font = new Font("D2Coding", 12F);
            txtSdoSlave.ForeColor = Color.White;
            txtSdoSlave.Location = new Point(163, 8);
            txtSdoSlave.Margin = new Padding(3, 8, 34, 8);
            txtSdoSlave.Name = "txtSdoSlave";
            txtSdoSlave.Size = new Size(271, 26);
            txtSdoSlave.TabIndex = 1;
            txtSdoSlave.Text = "0";
            // 
            // lblSdoIndex
            // 
            lblSdoIndex.BackColor = Color.FromArgb(149, 165, 166);
            lblSdoIndex.Dock = DockStyle.Fill;
            lblSdoIndex.Font = new Font("D2Coding", 12F);
            lblSdoIndex.ForeColor = Color.White;
            lblSdoIndex.Location = new Point(3, 52);
            lblSdoIndex.Name = "lblSdoIndex";
            lblSdoIndex.Size = new Size(154, 52);
            lblSdoIndex.TabIndex = 2;
            lblSdoIndex.Text = "Index (Hex):";
            lblSdoIndex.TextAlign = ContentAlignment.MiddleRight;
            lblSdoIndex.TooltipText = null;
            // 
            // txtSdoIndex
            // 
            txtSdoIndex.BackColor = Color.FromArgb(149, 165, 166);
            txtSdoIndex.Dock = DockStyle.Fill;
            txtSdoIndex.Font = new Font("D2Coding", 12F);
            txtSdoIndex.ForeColor = Color.White;
            txtSdoIndex.Location = new Point(163, 60);
            txtSdoIndex.Margin = new Padding(3, 8, 34, 8);
            txtSdoIndex.Name = "txtSdoIndex";
            txtSdoIndex.Size = new Size(271, 26);
            txtSdoIndex.TabIndex = 3;
            txtSdoIndex.Text = "0x6000";
            // 
            // lblSdoSubIndex
            // 
            lblSdoSubIndex.BackColor = Color.FromArgb(149, 165, 166);
            lblSdoSubIndex.Dock = DockStyle.Fill;
            lblSdoSubIndex.Font = new Font("D2Coding", 12F);
            lblSdoSubIndex.ForeColor = Color.White;
            lblSdoSubIndex.Location = new Point(3, 104);
            lblSdoSubIndex.Name = "lblSdoSubIndex";
            lblSdoSubIndex.Size = new Size(154, 52);
            lblSdoSubIndex.TabIndex = 4;
            lblSdoSubIndex.Text = "SubIndex (Hex):";
            lblSdoSubIndex.TextAlign = ContentAlignment.MiddleRight;
            lblSdoSubIndex.TooltipText = null;
            // 
            // txtSdoSubIndex
            // 
            txtSdoSubIndex.BackColor = Color.FromArgb(149, 165, 166);
            txtSdoSubIndex.Dock = DockStyle.Fill;
            txtSdoSubIndex.Font = new Font("D2Coding", 12F);
            txtSdoSubIndex.ForeColor = Color.White;
            txtSdoSubIndex.Location = new Point(163, 112);
            txtSdoSubIndex.Margin = new Padding(3, 8, 34, 8);
            txtSdoSubIndex.Name = "txtSdoSubIndex";
            txtSdoSubIndex.Size = new Size(271, 26);
            txtSdoSubIndex.TabIndex = 5;
            txtSdoSubIndex.Text = "0x00";
            // 
            // lblSdoData
            // 
            lblSdoData.BackColor = Color.FromArgb(149, 165, 166);
            lblSdoData.Dock = DockStyle.Fill;
            lblSdoData.Font = new Font("D2Coding", 12F);
            lblSdoData.ForeColor = Color.White;
            lblSdoData.Location = new Point(3, 156);
            lblSdoData.Name = "lblSdoData";
            lblSdoData.Size = new Size(154, 52);
            lblSdoData.TabIndex = 6;
            lblSdoData.Text = "Write Data:";
            lblSdoData.TextAlign = ContentAlignment.MiddleRight;
            lblSdoData.TooltipText = null;
            // 
            // txtSdoData
            // 
            txtSdoData.BackColor = Color.FromArgb(149, 165, 166);
            txtSdoData.Dock = DockStyle.Fill;
            txtSdoData.Font = new Font("D2Coding", 12F);
            txtSdoData.ForeColor = Color.White;
            txtSdoData.Location = new Point(163, 164);
            txtSdoData.Margin = new Padding(3, 8, 34, 8);
            txtSdoData.Name = "txtSdoData";
            txtSdoData.Size = new Size(271, 26);
            txtSdoData.TabIndex = 7;
            txtSdoData.Text = "0";
            // 
            // panelSdoButtons
            // 
            tblSdo.SetColumnSpan(panelSdoButtons, 2);
            panelSdoButtons.Controls.Add(btnSdoWrite);
            panelSdoButtons.Controls.Add(btnSdoRead);
            panelSdoButtons.Dock = DockStyle.Fill;
            panelSdoButtons.Location = new Point(3, 212);
            panelSdoButtons.Margin = new Padding(3, 4, 3, 4);
            panelSdoButtons.Name = "panelSdoButtons";
            panelSdoButtons.Padding = new Padding(23, 0, 23, 0);
            panelSdoButtons.Size = new Size(462, 52);
            panelSdoButtons.TabIndex = 8;
            // 
            // btnSdoWrite
            // 
            btnSdoWrite.BackColor = Color.FromArgb(231, 76, 60);
            btnSdoWrite.Dock = DockStyle.Right;
            btnSdoWrite.Font = new Font("D2Coding", 12F);
            btnSdoWrite.ForeColor = Color.Black;
            btnSdoWrite.Location = new Point(302, 0);
            btnSdoWrite.Margin = new Padding(3, 4, 3, 4);
            btnSdoWrite.Name = "btnSdoWrite";
            btnSdoWrite.Size = new Size(137, 52);
            btnSdoWrite.TabIndex = 0;
            btnSdoWrite.Text = "Write";
            btnSdoWrite.ThemeStyle = UI.Controls.ThemeStyle.Danger_Red;
            btnSdoWrite.TooltipText = null;
            btnSdoWrite.UseVisualStyleBackColor = false;
            btnSdoWrite.Click += btnSdoWrite_Click;
            // 
            // btnSdoRead
            // 
            btnSdoRead.BackColor = Color.FromArgb(52, 152, 219);
            btnSdoRead.Dock = DockStyle.Left;
            btnSdoRead.Font = new Font("D2Coding", 12F);
            btnSdoRead.ForeColor = Color.Black;
            btnSdoRead.Location = new Point(23, 0);
            btnSdoRead.Margin = new Padding(3, 4, 3, 4);
            btnSdoRead.Name = "btnSdoRead";
            btnSdoRead.Size = new Size(137, 52);
            btnSdoRead.TabIndex = 1;
            btnSdoRead.Text = "Read";
            btnSdoRead.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            btnSdoRead.TooltipText = null;
            btnSdoRead.UseVisualStyleBackColor = false;
            btnSdoRead.Click += btnSdoRead_Click;
            // 
            // lblSdoResult
            // 
            lblSdoResult.BackColor = Color.FromArgb(149, 165, 166);
            lblSdoResult.Dock = DockStyle.Fill;
            lblSdoResult.Font = new Font("D2Coding", 12F);
            lblSdoResult.ForeColor = Color.White;
            lblSdoResult.Location = new Point(3, 268);
            lblSdoResult.Name = "lblSdoResult";
            lblSdoResult.Size = new Size(154, 52);
            lblSdoResult.TabIndex = 9;
            lblSdoResult.Text = "Result:";
            lblSdoResult.TextAlign = ContentAlignment.MiddleRight;
            lblSdoResult.TooltipText = null;
            // 
            // txtSdoResult
            // 
            txtSdoResult.BackColor = Color.FromArgb(149, 165, 166);
            txtSdoResult.Dock = DockStyle.Fill;
            txtSdoResult.Font = new Font("D2Coding", 12F);
            txtSdoResult.ForeColor = Color.White;
            txtSdoResult.Location = new Point(163, 276);
            txtSdoResult.Margin = new Padding(3, 8, 34, 8);
            txtSdoResult.Name = "txtSdoResult";
            txtSdoResult.ReadOnly = true;
            txtSdoResult.Size = new Size(271, 26);
            txtSdoResult.TabIndex = 10;
            // 
            // groupBoxPDO
            // 
            groupBoxPDO.Controls.Add(tblPdo);
            groupBoxPDO.Dock = DockStyle.Fill;
            groupBoxPDO.Font = new Font("D2Coding", 12F);
            groupBoxPDO.Location = new Point(483, 4);
            groupBoxPDO.Margin = new Padding(3, 4, 3, 4);
            groupBoxPDO.Name = "groupBoxPDO";
            groupBoxPDO.Padding = new Padding(3, 4, 3, 4);
            groupBoxPDO.Size = new Size(474, 410);
            groupBoxPDO.TabIndex = 1;
            groupBoxPDO.TabStop = false;
            groupBoxPDO.Text = "PDO (Process Data Object)";
            // 
            // tblPdo
            // 
            tblPdo.ColumnCount = 2;
            tblPdo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tblPdo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblPdo.Controls.Add(lblPdoMaster, 0, 0);
            tblPdo.Controls.Add(txtPdoMaster, 1, 0);
            tblPdo.Controls.Add(lblPdoSlave, 0, 1);
            tblPdo.Controls.Add(txtPdoSlave, 1, 1);
            tblPdo.Controls.Add(lblPdoIndex, 0, 2);
            tblPdo.Controls.Add(txtPdoIndex, 1, 2);
            tblPdo.Controls.Add(lblPdoSubIndex, 0, 3);
            tblPdo.Controls.Add(txtPdoSubIndex, 1, 3);
            tblPdo.Controls.Add(lblPdoData, 0, 4);
            tblPdo.Controls.Add(txtPdoData, 1, 4);
            tblPdo.Controls.Add(panelPdoButtons, 0, 5);
            tblPdo.Controls.Add(lblPdoResult, 0, 6);
            tblPdo.Controls.Add(txtPdoResult, 1, 6);
            tblPdo.Dock = DockStyle.Fill;
            tblPdo.Location = new Point(3, 23);
            tblPdo.Margin = new Padding(3, 4, 3, 4);
            tblPdo.Name = "tblPdo";
            tblPdo.RowCount = 8;
            tblPdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tblPdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tblPdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tblPdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tblPdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tblPdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tblPdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tblPdo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblPdo.Size = new Size(468, 383);
            tblPdo.TabIndex = 0;
            // 
            // lblPdoMaster
            // 
            lblPdoMaster.BackColor = Color.FromArgb(149, 165, 166);
            lblPdoMaster.Dock = DockStyle.Fill;
            lblPdoMaster.Font = new Font("D2Coding", 12F);
            lblPdoMaster.ForeColor = Color.White;
            lblPdoMaster.Location = new Point(3, 0);
            lblPdoMaster.Name = "lblPdoMaster";
            lblPdoMaster.Size = new Size(154, 52);
            lblPdoMaster.TabIndex = 0;
            lblPdoMaster.Text = "Master ID:";
            lblPdoMaster.TextAlign = ContentAlignment.MiddleRight;
            lblPdoMaster.TooltipText = null;
            // 
            // txtPdoMaster
            // 
            txtPdoMaster.BackColor = Color.FromArgb(149, 165, 166);
            txtPdoMaster.Dock = DockStyle.Fill;
            txtPdoMaster.Font = new Font("D2Coding", 12F);
            txtPdoMaster.ForeColor = Color.White;
            txtPdoMaster.Location = new Point(163, 8);
            txtPdoMaster.Margin = new Padding(3, 8, 34, 8);
            txtPdoMaster.Name = "txtPdoMaster";
            txtPdoMaster.Size = new Size(271, 26);
            txtPdoMaster.TabIndex = 1;
            txtPdoMaster.Text = "0";
            // 
            // lblPdoSlave
            // 
            lblPdoSlave.BackColor = Color.FromArgb(149, 165, 166);
            lblPdoSlave.Dock = DockStyle.Fill;
            lblPdoSlave.Font = new Font("D2Coding", 12F);
            lblPdoSlave.ForeColor = Color.White;
            lblPdoSlave.Location = new Point(3, 52);
            lblPdoSlave.Name = "lblPdoSlave";
            lblPdoSlave.Size = new Size(154, 52);
            lblPdoSlave.TabIndex = 2;
            lblPdoSlave.Text = "Slave ID:";
            lblPdoSlave.TextAlign = ContentAlignment.MiddleRight;
            lblPdoSlave.TooltipText = null;
            // 
            // txtPdoSlave
            // 
            txtPdoSlave.BackColor = Color.FromArgb(149, 165, 166);
            txtPdoSlave.Dock = DockStyle.Fill;
            txtPdoSlave.Font = new Font("D2Coding", 12F);
            txtPdoSlave.ForeColor = Color.White;
            txtPdoSlave.Location = new Point(163, 60);
            txtPdoSlave.Margin = new Padding(3, 8, 34, 8);
            txtPdoSlave.Name = "txtPdoSlave";
            txtPdoSlave.Size = new Size(271, 26);
            txtPdoSlave.TabIndex = 3;
            txtPdoSlave.Text = "0";
            // 
            // lblPdoIndex
            // 
            lblPdoIndex.BackColor = Color.FromArgb(149, 165, 166);
            lblPdoIndex.Dock = DockStyle.Fill;
            lblPdoIndex.Font = new Font("D2Coding", 12F);
            lblPdoIndex.ForeColor = Color.White;
            lblPdoIndex.Location = new Point(3, 104);
            lblPdoIndex.Name = "lblPdoIndex";
            lblPdoIndex.Size = new Size(154, 52);
            lblPdoIndex.TabIndex = 4;
            lblPdoIndex.Text = "Index (Hex):";
            lblPdoIndex.TextAlign = ContentAlignment.MiddleRight;
            lblPdoIndex.TooltipText = null;
            // 
            // txtPdoIndex
            // 
            txtPdoIndex.BackColor = Color.FromArgb(149, 165, 166);
            txtPdoIndex.Dock = DockStyle.Fill;
            txtPdoIndex.Font = new Font("D2Coding", 12F);
            txtPdoIndex.ForeColor = Color.White;
            txtPdoIndex.Location = new Point(163, 112);
            txtPdoIndex.Margin = new Padding(3, 8, 34, 8);
            txtPdoIndex.Name = "txtPdoIndex";
            txtPdoIndex.Size = new Size(271, 26);
            txtPdoIndex.TabIndex = 5;
            txtPdoIndex.Text = "0x6000";
            // 
            // lblPdoSubIndex
            // 
            lblPdoSubIndex.BackColor = Color.FromArgb(149, 165, 166);
            lblPdoSubIndex.Dock = DockStyle.Fill;
            lblPdoSubIndex.Font = new Font("D2Coding", 12F);
            lblPdoSubIndex.ForeColor = Color.White;
            lblPdoSubIndex.Location = new Point(3, 156);
            lblPdoSubIndex.Name = "lblPdoSubIndex";
            lblPdoSubIndex.Size = new Size(154, 52);
            lblPdoSubIndex.TabIndex = 6;
            lblPdoSubIndex.Text = "SubIndex (Hex):";
            lblPdoSubIndex.TextAlign = ContentAlignment.MiddleRight;
            lblPdoSubIndex.TooltipText = null;
            // 
            // txtPdoSubIndex
            // 
            txtPdoSubIndex.BackColor = Color.FromArgb(149, 165, 166);
            txtPdoSubIndex.Dock = DockStyle.Fill;
            txtPdoSubIndex.Font = new Font("D2Coding", 12F);
            txtPdoSubIndex.ForeColor = Color.White;
            txtPdoSubIndex.Location = new Point(163, 164);
            txtPdoSubIndex.Margin = new Padding(3, 8, 34, 8);
            txtPdoSubIndex.Name = "txtPdoSubIndex";
            txtPdoSubIndex.Size = new Size(271, 26);
            txtPdoSubIndex.TabIndex = 7;
            txtPdoSubIndex.Text = "0x00";
            // 
            // lblPdoData
            // 
            lblPdoData.BackColor = Color.FromArgb(149, 165, 166);
            lblPdoData.Dock = DockStyle.Fill;
            lblPdoData.Font = new Font("D2Coding", 12F);
            lblPdoData.ForeColor = Color.White;
            lblPdoData.Location = new Point(3, 208);
            lblPdoData.Name = "lblPdoData";
            lblPdoData.Size = new Size(154, 52);
            lblPdoData.TabIndex = 8;
            lblPdoData.Text = "Write Data:";
            lblPdoData.TextAlign = ContentAlignment.MiddleRight;
            lblPdoData.TooltipText = null;
            // 
            // txtPdoData
            // 
            txtPdoData.BackColor = Color.FromArgb(149, 165, 166);
            txtPdoData.Dock = DockStyle.Fill;
            txtPdoData.Font = new Font("D2Coding", 12F);
            txtPdoData.ForeColor = Color.White;
            txtPdoData.Location = new Point(163, 216);
            txtPdoData.Margin = new Padding(3, 8, 34, 8);
            txtPdoData.Name = "txtPdoData";
            txtPdoData.Size = new Size(271, 26);
            txtPdoData.TabIndex = 9;
            txtPdoData.Text = "0";
            // 
            // panelPdoButtons
            // 
            tblPdo.SetColumnSpan(panelPdoButtons, 2);
            panelPdoButtons.Controls.Add(btnPdoWrite);
            panelPdoButtons.Controls.Add(btnPdoRead);
            panelPdoButtons.Dock = DockStyle.Fill;
            panelPdoButtons.Location = new Point(3, 264);
            panelPdoButtons.Margin = new Padding(3, 4, 3, 4);
            panelPdoButtons.Name = "panelPdoButtons";
            panelPdoButtons.Padding = new Padding(23, 0, 23, 0);
            panelPdoButtons.Size = new Size(462, 52);
            panelPdoButtons.TabIndex = 10;
            // 
            // btnPdoWrite
            // 
            btnPdoWrite.BackColor = Color.FromArgb(231, 76, 60);
            btnPdoWrite.Dock = DockStyle.Right;
            btnPdoWrite.Font = new Font("D2Coding", 12F);
            btnPdoWrite.ForeColor = Color.Black;
            btnPdoWrite.Location = new Point(302, 0);
            btnPdoWrite.Margin = new Padding(3, 4, 3, 4);
            btnPdoWrite.Name = "btnPdoWrite";
            btnPdoWrite.Size = new Size(137, 52);
            btnPdoWrite.TabIndex = 0;
            btnPdoWrite.Text = "Write";
            btnPdoWrite.ThemeStyle = UI.Controls.ThemeStyle.Danger_Red;
            btnPdoWrite.TooltipText = null;
            btnPdoWrite.UseVisualStyleBackColor = false;
            btnPdoWrite.Click += btnPdoWrite_Click;
            // 
            // btnPdoRead
            // 
            btnPdoRead.BackColor = Color.FromArgb(52, 152, 219);
            btnPdoRead.Dock = DockStyle.Left;
            btnPdoRead.Font = new Font("D2Coding", 12F);
            btnPdoRead.ForeColor = Color.Black;
            btnPdoRead.Location = new Point(23, 0);
            btnPdoRead.Margin = new Padding(3, 4, 3, 4);
            btnPdoRead.Name = "btnPdoRead";
            btnPdoRead.Size = new Size(137, 52);
            btnPdoRead.TabIndex = 1;
            btnPdoRead.Text = "Read";
            btnPdoRead.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            btnPdoRead.TooltipText = null;
            btnPdoRead.UseVisualStyleBackColor = false;
            btnPdoRead.Click += btnPdoRead_Click;
            // 
            // lblPdoResult
            // 
            lblPdoResult.BackColor = Color.FromArgb(149, 165, 166);
            lblPdoResult.Dock = DockStyle.Fill;
            lblPdoResult.Font = new Font("D2Coding", 12F);
            lblPdoResult.ForeColor = Color.White;
            lblPdoResult.Location = new Point(3, 320);
            lblPdoResult.Name = "lblPdoResult";
            lblPdoResult.Size = new Size(154, 52);
            lblPdoResult.TabIndex = 11;
            lblPdoResult.Text = "Result:";
            lblPdoResult.TextAlign = ContentAlignment.MiddleRight;
            lblPdoResult.TooltipText = null;
            // 
            // txtPdoResult
            // 
            txtPdoResult.BackColor = Color.FromArgb(149, 165, 166);
            txtPdoResult.Dock = DockStyle.Fill;
            txtPdoResult.Font = new Font("D2Coding", 12F);
            txtPdoResult.ForeColor = Color.White;
            txtPdoResult.Location = new Point(163, 328);
            txtPdoResult.Margin = new Padding(3, 8, 34, 8);
            txtPdoResult.Name = "txtPdoResult";
            txtPdoResult.ReadOnly = true;
            txtPdoResult.Size = new Size(271, 26);
            txtPdoResult.TabIndex = 12;
            // 
            // EtherCAT_SDO_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelMain);
            Controls.Add(_LabelTitle);
            Margin = new Padding(3, 6, 3, 6);
            Name = "EtherCAT_SDO_View";
            Size = new Size(960, 464);
            tableLayoutPanelMain.ResumeLayout(false);
            groupBoxSDO.ResumeLayout(false);
            tblSdo.ResumeLayout(false);
            tblSdo.PerformLayout();
            panelSdoButtons.ResumeLayout(false);
            groupBoxPDO.ResumeLayout(false);
            tblPdo.ResumeLayout(false);
            tblPdo.PerformLayout();
            panelPdoButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private EQ.UI.Controls._Label _LabelTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        
        // SDO Group
        private System.Windows.Forms.GroupBox groupBoxSDO;
        private System.Windows.Forms.TableLayoutPanel tblSdo;
        private EQ.UI.Controls._Label lblSdoSlave;
        private EQ.UI.Controls._TextBox txtSdoSlave;
        private EQ.UI.Controls._Label lblSdoIndex;
        private EQ.UI.Controls._TextBox txtSdoIndex;
        private EQ.UI.Controls._Label lblSdoSubIndex;
        private EQ.UI.Controls._TextBox txtSdoSubIndex;
        private EQ.UI.Controls._Label lblSdoData;
        private EQ.UI.Controls._TextBox txtSdoData;
        private System.Windows.Forms.Panel panelSdoButtons;
        private EQ.UI.Controls._Button btnSdoRead;
        private EQ.UI.Controls._Button btnSdoWrite;
        private EQ.UI.Controls._Label lblSdoResult;
        private EQ.UI.Controls._TextBox txtSdoResult;

        // PDO Group
        private System.Windows.Forms.GroupBox groupBoxPDO;
        private System.Windows.Forms.TableLayoutPanel tblPdo;
        private EQ.UI.Controls._Label lblPdoMaster;
        private EQ.UI.Controls._TextBox txtPdoMaster;
        private EQ.UI.Controls._Label lblPdoSlave;
        private EQ.UI.Controls._TextBox txtPdoSlave;
        private EQ.UI.Controls._Label lblPdoIndex;
        private EQ.UI.Controls._TextBox txtPdoIndex;
        private EQ.UI.Controls._Label lblPdoSubIndex;
        private EQ.UI.Controls._TextBox txtPdoSubIndex;
        private EQ.UI.Controls._Label lblPdoData;
        private EQ.UI.Controls._TextBox txtPdoData;
        private System.Windows.Forms.Panel panelPdoButtons;
        private EQ.UI.Controls._Button btnPdoRead;
        private EQ.UI.Controls._Button btnPdoWrite;
        private EQ.UI.Controls._Label lblPdoResult;
        private EQ.UI.Controls._TextBox txtPdoResult;
    }
}
