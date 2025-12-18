using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EQ.UI.UserViews
{
    public partial class IO_View : UserControlBase
    {
        private readonly ACT act = ActManager.Instance.Act;
        private System.Windows.Forms.Timer _uiUpdateTimer;
     
        private DataTable _dataTableIn;
        private DataTable _dataTableOut;

       
        public IO_View()
        {
            InitializeComponent();
            
        }

        private void IO_View_Disposed(object? sender, EventArgs e)
        {
            _uiUpdateTimer.Stop();
            _uiUpdateTimer.Tick -= UiUpdateTimer_Tick;
            _uiUpdateTimer.Dispose();
        }

        private void IO_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return; // 코드 디자인 모드에서는 탭 보이도록 함

            Disposed += IO_View_Disposed;

            _dataTableIn = new DataTable();
            _dataTableIn.Columns.Add("No", typeof(string));
            _dataTableIn.Columns.Add("Label", typeof(string));
            _dataTableIn.Columns.Add("Module", typeof(string));
            _dataTableIn.Columns.Add("Name", typeof(string));
            _dataTableIn.Columns.Add("Status", typeof(bool));
            _dataTableIn.Columns.Add("Ref", typeof(int));

            _dataTableOut = new DataTable();
            _dataTableOut.Columns.Add("No", typeof(string));
            _dataTableOut.Columns.Add("Label", typeof(string));
            _dataTableOut.Columns.Add("Module", typeof(string));
            _dataTableOut.Columns.Add("Name", typeof(string));
            _dataTableOut.Columns.Add("Status", typeof(bool));
            _dataTableOut.Columns.Add("Ref", typeof(int));


            int num = 0;
            int startLabel = 0;

            int inLength = Enum.GetValues(typeof(IO_IN)).Length;


            foreach (IO_IN d in Enum.GetValues(typeof(IO_IN)))
            {
                string _no = num.ToString();
                string _label = $"I-{startLabel.ToString("X04")}";
                string _name = d.ToString();
                string _model = $"{(int)(num / 16)}-{(int)(num % 16)}";
                bool _status = false;
                int _ref = -1;
                num++;
                startLabel++;


                if (Enum.TryParse<IO_OUT>(_name, true, out IO_OUT enumValue))
                    _ref = Convert.ToInt32(enumValue) + inLength;

                _dataTableIn.Rows.Add(_no, _label, _model, _name, _status, _ref);
            }
            _dataGridView1.DataSource = _dataTableIn;



            startLabel = 0x1100;
            foreach (IO_OUT d in Enum.GetValues(typeof(IO_OUT)))
            {
                string _no = num.ToString();
                string _label = $"O-{startLabel.ToString("X04")}";
                string _name = d.ToString();
                string _model = $"{(int)(num / 16)}-{(int)(num % 16)}";
                bool _status = false;
                int _ref = -1;

                num++;
                startLabel++;

                if (Enum.TryParse<IO_IN>(_name, true, out IO_IN enumValue))
                    _ref = Convert.ToInt32(enumValue);

                _dataTableOut.Rows.Add(_no, _label, _model, _name, _status, _ref);
            }
            _dataGridView2.DataSource = _dataTableOut;


            _dataGridView1.Columns[0].Width = 25;
            _dataGridView2.Columns[0].Width = 25;
            _dataGridView1.Columns[1].Width = 45;
            _dataGridView2.Columns[1].Width = 45;
            _dataGridView1.Columns[2].Width = 35;
            _dataGridView2.Columns[2].Width = 35;
            _dataGridView1.Columns[4].Width = 35;
            _dataGridView2.Columns[4].Width = 35;
            _dataGridView1.Columns[5].Width = 40;
            _dataGridView2.Columns[5].Width = 40;

            _dataGridView1.ReadOnly = true;
            _dataGridView2.ReadOnly = true;

            // 선택시 특정 컬럼만 색상 변경
            _dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            _dataGridView1.DefaultCellStyle.SelectionBackColor = _dataGridView1.DefaultCellStyle.BackColor;
            _dataGridView1.DefaultCellStyle.SelectionForeColor = _dataGridView1.DefaultCellStyle.ForeColor;
            _dataGridView2.DefaultCellStyle.SelectionBackColor = _dataGridView2.DefaultCellStyle.BackColor;
            _dataGridView2.DefaultCellStyle.SelectionForeColor = _dataGridView2.DefaultCellStyle.ForeColor;

            var statusColumn1 = _dataGridView1.Columns["Name"];
            statusColumn1.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight; 
            statusColumn1.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            var statusColumn2 = _dataGridView2.Columns["Name"];
            statusColumn2.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            statusColumn2.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;


            _uiUpdateTimer = new System.Windows.Forms.Timer();
            _uiUpdateTimer.Interval = 100;
            _uiUpdateTimer.Tick += UiUpdateTimer_Tick;
            _uiUpdateTimer.Start();
        }

        private void UiUpdateTimer_Tick(object? sender, EventArgs e)
        {
            var (inData, outData) = ActManager.Instance.Act.IO.GetIoStatus();

            int bitIndex = 0;

            foreach (byte b in inData)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (bitIndex >= _dataTableIn.Rows.Count) break; // 배열 범위 초과 방지

                    bool isOn = (b & (1 << i)) != 0;

                    DataRow row = _dataTableIn.Rows[bitIndex];

                    if ((bool)row["Status"] != isOn)
                    {
                        row["Status"] = isOn;
                    }

                    bitIndex++;
                }
                if (bitIndex >= _dataTableIn.Rows.Count) break;
            }

            bitIndex = 0;
            foreach (byte b in outData)
            {

                for (int i = 0; i < 8; i++)
                {
                    if (bitIndex >= _dataTableOut.Rows.Count) break;

                    bool isOn = (b & (1 << i)) != 0;
                    string newState = isOn ? "On" : "Off";

                    DataRow row = _dataTableOut.Rows[bitIndex];

                    if ((bool)row["Status"] != isOn)
                    {
                        row["Status"] = isOn;
                    }

                    bitIndex++;
                }
                if (bitIndex >= _dataTableOut.Rows.Count) break;
            }
        }

        private void _DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var p = sender as DataGridView;
            int rowidx = p.CurrentCell.RowIndex;
            int colIdx = p.CurrentCell.ColumnIndex;

            if (rowidx != -1 && colIdx == 4)
            {
                if (p.Name.Contains(nameof(_dataGridView1)))
                {
                    bool onoff = (bool)this._dataGridView1[4, rowidx].Value;
                    
                    act.IO.WriteInput((IO_IN)rowidx, !onoff);                  
                }
                else if (p.Name.Contains(nameof(_dataGridView2)))
                {
                    this._dataGridView1.ClearSelection();
                    bool onoff = (bool)this._dataGridView2[4, rowidx].Value;
                    act.IO.WriteOutput((IO_OUT)rowidx, !onoff);
                }
            }


            // 선택한 IO에 대한 상대 IO로 포커스 이동
            if (rowidx != -1)
            {
                //OUT IO에서 IN IO로 참조 이동
                if (p.Name.Contains(nameof(_dataGridView2)))
                {
                    this._dataGridView1.ClearSelection();
                    var refNo = (int)this._dataGridView2[5, rowidx].Value;

                    if (refNo != -1)
                    {
                        this._dataGridView1.Rows[refNo].Selected = true;

                        // 선택한 행이 현재 보이는 영역에 없으면 스크롤
                        int firstVisible = _dataGridView1.FirstDisplayedScrollingRowIndex;
                        int visibleCount = _dataGridView1.DisplayedRowCount(false);
                        int lastVisible = firstVisible + visibleCount - 1;

                        if (refNo < firstVisible || refNo > lastVisible)
                        {
                            _dataGridView1.FirstDisplayedScrollingRowIndex = refNo;
                        }
                    }
                    else
                    {
                        this._dataGridView1.ClearSelection();
                    }
                }

                //IN IO에서 OUT IO로 참조 이동
                if (p.Name.Contains(nameof(_dataGridView1)))
                {
                    this._dataGridView2.ClearSelection();
                    var refNo = (int)this._dataGridView1[5, rowidx].Value;

                    if (refNo != -1)
                    {
                        var selRow = refNo - _dataGridView1.RowCount;

                        this._dataGridView2.Rows[selRow].Selected = true;

                        // 선택한 행이 현재 보이는 영역에 없으면 스크롤
                        int firstVisible = _dataGridView2.FirstDisplayedScrollingRowIndex;
                        int visibleCount = _dataGridView2.DisplayedRowCount(false);
                        int lastVisible = firstVisible + visibleCount - 1;

                        if (selRow < firstVisible || selRow > lastVisible)
                        {
                            _dataGridView2.FirstDisplayedScrollingRowIndex = selRow;
                        }
                    }
                    else
                    {
                        this._dataGridView2.ClearSelection();
                    }
                }
            }
        }
    }
}
