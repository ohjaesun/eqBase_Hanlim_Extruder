using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static EQ.Core.Globals;

namespace EQ.UI.UserViews
{
    public partial class AuditTrail_View : UserControlBaseplain
    {
        public AuditTrail_View()
        {
            InitializeComponent();
        }

        private void AuditTrail_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            InitializeGrid();

            // 기본 날짜 범위: 최근 7일
            _dateFrom.Value = DateTime.Now.AddDays(-7);
            _dateTo.Value = DateTime.Now;

            LoadAuditTrail();
        }

        private void InitializeGrid()
        {
            _gridAuditTrail.ColumnCount = 6;
            _gridAuditTrail.Columns[0].Name = "DateTime";
            _gridAuditTrail.Columns[0].Width = 180;
            _gridAuditTrail.Columns[1].Name = "Event Type";
            _gridAuditTrail.Columns[1].Width = 150;
            _gridAuditTrail.Columns[2].Name = "User ID";
            _gridAuditTrail.Columns[2].Width = 120;
            _gridAuditTrail.Columns[3].Name = "User Name";
            _gridAuditTrail.Columns[3].Width = 150;
            _gridAuditTrail.Columns[4].Name = "Description";
            _gridAuditTrail.Columns[4].Width = 600;
            _gridAuditTrail.Columns[5].Name = "Detail";
            _gridAuditTrail.Columns[5].Width = 400;

            // 읽기 전용
            _gridAuditTrail.ReadOnly = true;
        }

        private void LoadAuditTrail()
        {
            _gridAuditTrail.Rows.Clear();

            try
            {
                var auditTrail = ActManager.Instance.Act.AuditTrail;
                var entries = auditTrail.GetEntriesByDateRange(
                    _dateFrom.Value.Date,
                    _dateTo.Value.Date.AddDays(1).AddSeconds(-1)
                );

                // 필터 적용
                var filteredEntries = ApplyEventTypeFilter(entries);

                foreach (var entry in filteredEntries)
                {
                    _gridAuditTrail.Rows.Add(
                        entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                        entry.EventType.ToString(),
                        entry.UserId,
                        entry.UserName,
                        entry.Description,
                        string.IsNullOrEmpty(entry.DetailJson) ? "-" : "JSON"
                    );
                }

                // 상태 표시 (선택 사항)
                // _LabelStatus.Text = $"Total: {filteredEntries.Count} records";
            }
            catch (Exception ex)
            {
                ActManager.Instance.Act.PopupNoti(
                    "Error",
                    $"Failed to load audit trail: {ex.Message}",
                    NotifyType.Error);
            }
        }

        private List<AuditTrailEntry> ApplyEventTypeFilter(List<AuditTrailEntry> entries)
        {
            // All 체크박스가 선택되어 있으면 모두 표시
            if (_chkAll.Checked)
                return entries;

            var filtered = new List<AuditTrailEntry>();

            foreach (var entry in entries)
            {
                bool include = false;

                // Login 필터 (LoginSuccess, LoginFailed, Logout)
                if (_chkLogin.Checked &&
                    (entry.EventType == AuditEventType.LoginSuccess ||
                     entry.EventType == AuditEventType.LoginFailed ||
                     entry.EventType == AuditEventType.Logout))
                {
                    include = true;
                }

                // User 필터 (UserCreated, UserDeleted, UserLocked, UserUnlocked, PasswordChanged, PasswordReset)
                if (_chkUser.Checked &&
                    (entry.EventType == AuditEventType.UserCreated ||
                     entry.EventType == AuditEventType.UserDeleted ||
                     entry.EventType == AuditEventType.UserLocked ||
                     entry.EventType == AuditEventType.UserUnlocked ||
                     entry.EventType == AuditEventType.PasswordChanged ||
                     entry.EventType == AuditEventType.PasswordReset))
                {
                    include = true;
                }

                // Recipe 필터 (RecipeCreated, RecipeModified, RecipeDeleted, RecipeLoaded)
                if (_chkRecipe.Checked &&
                    (entry.EventType == AuditEventType.RecipeCreated ||
                     entry.EventType == AuditEventType.RecipeModified ||
                     entry.EventType == AuditEventType.RecipeDeleted ||
                     entry.EventType == AuditEventType.RecipeLoaded))
                {
                    include = true;
                }

                // Parameter 필터 (ParameterChanged, SequenceStarted, SequenceStopped)
                if (_chkParameter.Checked &&
                    (entry.EventType == AuditEventType.ParameterChanged ||
                     entry.EventType == AuditEventType.SequenceStarted ||
                     entry.EventType == AuditEventType.SequenceStopped))
                {
                    include = true;
                }

                // System 필터 (SystemStartup, SystemShutdown, EmergencyStop, AlarmOccurred, AlarmCleared, ConfigurationChanged, DataExported, DataImported)
                if (_chkSystem.Checked &&
                    (entry.EventType == AuditEventType.SystemStartup ||
                     entry.EventType == AuditEventType.SystemShutdown ||
                     entry.EventType == AuditEventType.EmergencyStop ||
                     entry.EventType == AuditEventType.AlarmOccurred ||
                     entry.EventType == AuditEventType.AlarmCleared ||
                     entry.EventType == AuditEventType.ConfigurationChanged ||
                     entry.EventType == AuditEventType.DataExported ||
                     entry.EventType == AuditEventType.DataImported))
                {
                    include = true;
                }

                if (include)
                    filtered.Add(entry);
            }

            return filtered;
        }

        private void _chkAll_CheckedChanged(object sender, EventArgs e)
        {
            // All 체크박스가 선택되면 다른 체크박스는 비활성화
            bool isAllChecked = _chkAll.Checked;

            _chkLogin.Enabled = !isAllChecked;
            _chkUser.Enabled = !isAllChecked;
            _chkRecipe.Enabled = !isAllChecked;
            _chkParameter.Enabled = !isAllChecked;
            _chkSystem.Enabled = !isAllChecked;

            if (isAllChecked)
            {
                _chkLogin.Checked = false;
                _chkUser.Checked = false;
                _chkRecipe.Checked = false;
                _chkParameter.Checked = false;
                _chkSystem.Checked = false;
            }
        }

        private void _btnApplyFilter_Click(object sender, EventArgs e)
        {
            LoadAuditTrail();
        }

        private void _btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "CSV Files (*.csv)|*.csv";
                    saveDialog.FileName = $"AuditTrail_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                    saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        var auditTrail = ActManager.Instance.Act.AuditTrail;
                        bool success = auditTrail.ExportToCsv(
                            saveDialog.FileName,
                            _dateFrom.Value.Date,
                            _dateTo.Value.Date.AddDays(1).AddSeconds(-1)
                        );

                        if (success)
                        {
                            ActManager.Instance.Act.PopupNoti(
                                "Export Success",
                                $"Exported to: {saveDialog.FileName}",
                                NotifyType.Info);
                        }
                        else
                        {
                            ActManager.Instance.Act.PopupNoti(
                                "Export Failed",
                                "Failed to export CSV file",
                                NotifyType.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActManager.Instance.Act.PopupNoti(
                    "Error",
                    $"Export error: {ex.Message}",
                    NotifyType.Error);
            }
        }

        private void _gridAuditTrail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var row = _gridAuditTrail.Rows[e.RowIndex];

                string dateTime = row.Cells[0].Value?.ToString() ?? "";
                string eventType = row.Cells[1].Value?.ToString() ?? "";
                string userId = row.Cells[2].Value?.ToString() ?? "";
                string userName = row.Cells[3].Value?.ToString() ?? "";
                string description = row.Cells[4].Value?.ToString() ?? "";
                string detail = row.Cells[5].Value?.ToString() ?? "";

                // 상세 정보 표시
                string message = $"DateTime: {dateTime}\n" +
                               $"Event Type: {eventType}\n" +
                               $"User ID: {userId}\n" +
                               $"User Name: {userName}\n" +
                               $"Description: {description}\n" +
                               $"Detail: {detail}";

                MessageBox.Show(message, "Audit Trail Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ActManager.Instance.Act.PopupNoti(
                    "Error",
                    $"Failed to show detail: {ex.Message}",
                    NotifyType.Error);
            }
        }

        private void _chkLogin_CheckStateChanged(object sender, EventArgs e)
        {
            var obj = sender as _CheckBox;

            if (obj.Checked)
                obj.ThemeStyle = ThemeStyle.Display_LightYellow;
            else
                obj.ThemeStyle = ThemeStyle.Info_Sky;
        }

        private void Export_PDF(object sender, EventArgs e)
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                    saveDialog.FileName = $"AuditTrail_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                    saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        var auditTrail = ActManager.Instance.Act.AuditTrail;
                        bool success = auditTrail.ExportToPdf(
                            saveDialog.FileName,
                            _dateFrom.Value.Date,
                            _dateTo.Value.Date.AddDays(1).AddSeconds(-1)
                        );

                        if (success)
                        {
                            ActManager.Instance.Act.PopupNoti(
                                "Export Success",
                                $"PDF exported to: {saveDialog.FileName}",
                                NotifyType.Info);
                        }
                        else
                        {
                            ActManager.Instance.Act.PopupNoti(
                                "Export Failed",
                                "Failed to export PDF file",
                                NotifyType.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActManager.Instance.Act.PopupNoti(
                    "Error",
                    $"Export error: {ex.Message}",
                    NotifyType.Error);
            }
        }
    }
}
