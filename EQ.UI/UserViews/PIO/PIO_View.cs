using EQ.Core.Service;
using EQ.Domain.Enums;
using EQ.UI.UserViews.Setup.Components;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EQ.Core;

namespace EQ.UI.UserViews.Setup
{
    public partial class PIO_View : UserControlBaseWithTitle
    {
        private List<PIOPort_Control> _portControls = new List<PIOPort_Control>();

        public PIO_View()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode) return;

            // [핵심] 부모 클래스의 제목 라벨 설정
            if (_LabelTitle != null)
            {
                _LabelTitle.Text = Globals.L("PIO (E84) Status");
            }

            InitPorts();
            tmrUpdate.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                tmrUpdate?.Stop();
                tmrUpdate?.Dispose();

                foreach (var ctrl in _portControls)
                {
                    ctrl.Dispose();
                }
                _portControls.Clear();

                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void InitPorts()
        {
            flowLayoutPanelMain.Controls.Clear();
            _portControls.Clear();

            foreach (PIOId id in Enum.GetValues(typeof(PIOId)))
            {
                var portCtrl = new PIOPort_Control();
                portCtrl.Initialize(id);

                portCtrl.Margin = new Padding(10);
                portCtrl.BackColor = System.Drawing.Color.White;
                portCtrl.Width = 400;
                portCtrl.Height = 400;

                flowLayoutPanelMain.Controls.Add(portCtrl);
                _portControls.Add(portCtrl);
            }
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (DesignMode || !this.Visible) return;

            foreach (var ctrl in _portControls)
            {
                ctrl.UpdateUI();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitPorts();
        }
    }
}