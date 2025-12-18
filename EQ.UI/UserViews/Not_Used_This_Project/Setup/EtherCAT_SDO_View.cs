using System;
using System.Drawing;
using System.Windows.Forms;
using EQ.Core;
using EQ.Core.Act;
using static EQ.Core.Globals;
using EQ.UI.Controls;
using EQ.UI.UserViews;
using EQ.Core.Service; // Needed for UserControlBaseplain

namespace EQ.UI.UserViews.Setup
{
    public partial class EtherCAT_SDO_View : UserControlBaseplain
    {
        public EtherCAT_SDO_View()
        {
            InitializeComponent();
        }

        private int ParseInt(string text)
        {
            text = text.Trim();
            if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                return Convert.ToInt32(text.Substring(2), 16);
            }
            return int.Parse(text);
        }

        private string BytesToHex(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return "Empty";
            return BitConverter.ToString(bytes).Replace("-", " ");
        }

        #region SDO

        private void btnSdoRead_Click(object sender, EventArgs e)
        {
            try
            {
                int slave = ParseInt(txtSdoSlave.Text);
                int index = ParseInt(txtSdoIndex.Text);
                int subIndex = ParseInt(txtSdoSubIndex.Text);

                var result = ActManager.Instance.Act.Motion.SDO_Read(slave, index, subIndex);
                txtSdoResult.Text = L("Read: {0}", BytesToHex(result));
            }
            catch (Exception ex)
            {
                txtSdoResult.Text = L("Error: {0}", ex.Message);
            }
        }

        private void btnSdoWrite_Click(object sender, EventArgs e)
        {
            try
            {
                int slave = ParseInt(txtSdoSlave.Text);
                int index = ParseInt(txtSdoIndex.Text);
                int subIndex = ParseInt(txtSdoSubIndex.Text);
                int data = ParseInt(txtSdoData.Text);

                bool success = ActManager.Instance.Act.Motion.SDO_Write(slave, index, subIndex, data);
                txtSdoResult.Text = success ? L("Write Success") : L("Write Failed");
            }
            catch (Exception ex)
            {
                txtSdoResult.Text = L("Error: {0}", ex.Message);
            }
        }

        #endregion

        #region PDO

        private void btnPdoRead_Click(object sender, EventArgs e)
        {
            try
            {
                int master = ParseInt(txtPdoMaster.Text);
                int slave = ParseInt(txtPdoSlave.Text);
                int index = ParseInt(txtPdoIndex.Text);
                int subIndex = ParseInt(txtPdoSubIndex.Text);

                var result = ActManager.Instance.Act.Motion.PDO_Read(master, slave, index, subIndex);
                txtPdoResult.Text = L("Read: {0}", BytesToHex(result));
            }
            catch (Exception ex)
            {
                txtPdoResult.Text = L("Error: {0}", ex.Message);
            }
        }

        private void btnPdoWrite_Click(object sender, EventArgs e)
        {
            try
            {
                int master = ParseInt(txtPdoMaster.Text);
                int slave = ParseInt(txtPdoSlave.Text);
                int index = ParseInt(txtPdoIndex.Text);
                int subIndex = ParseInt(txtPdoSubIndex.Text);
                int data = ParseInt(txtPdoData.Text);

                var result = ActManager.Instance.Act.Motion.PDO_Write(master, slave, index, subIndex, data);
                txtPdoResult.Text = L("Write Ret: {0}", BytesToHex(result));
            }
            catch (Exception ex)
            {
                txtPdoResult.Text = L("Error: {0}", ex.Message);
            }
        }

        #endregion
    }
}
