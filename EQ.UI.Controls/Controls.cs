
using EQ.Common.Logs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.UI.Controls
{
    public enum ThemeStyle
    {
        Default,        // 시스템 기본
        Primary_Indigo,        // 주요 버튼, 강조 포인트 (Indigo/Blue 계열)
        Success_Green,        // 성공, 정상 상태 (Green)
        Warning_Yellow,        // 주의, 대기 (Yellow)
        Danger_Red,         // 오류, 긴급 (Red)
        Info_Sky,           // 정보, 일반 메시지 (Flat River)
        Highlight_DeepYellow,      // 포커스, 선택 강조 (Flat SunFlower)
        Neutral_Gray,        // 중립, 보조 패널/배경 (Gray)
        Display_LightYellow,   // 표시용 (연한 노란색)
        Black_White,
        DesignModeOnly
    }

    public static class ThemeHelper
    {
        public static (Color Back, Color Fore) GetColorPair(ThemeStyle style)
        {
            return style switch
            {
                // [유지] 어두운 배경 + 흰색 글씨 (대비 좋음)
                //    ThemeStyle.Primary_Indigo => (Color.FromArgb(52, 73, 94), Color.White),
                ThemeStyle.Primary_Indigo => (Color.FromArgb(48, 63, 159), Color.White),

                // [수정] 밝은 녹색 배경 + 흰색 글씨(X) -> 검은색 글씨(O)
                ThemeStyle.Success_Green => (Color.FromArgb(46, 204, 113), Color.Black),

                // [유지] 밝은 노란색 배경 + 검은색 글씨 (대비 좋음)
                ThemeStyle.Warning_Yellow => (Color.FromArgb(241, 196, 15), Color.Black),

                // [수정] 밝은 빨간색 배경 + 흰색 글씨(X) -> 검은색 글씨(O)
                ThemeStyle.Danger_Red => (Color.FromArgb(231, 76, 60), Color.Black),

                // [수정] 밝은 파란색 배경 + 흰색 글씨(X) -> 검은색 글씨(O)
                ThemeStyle.Info_Sky => (Color.FromArgb(52, 152, 219), Color.Black),

                // [수정] 밝은 보라색 배경 + 흰색 글씨(X) -> 검은색 글씨(O)
                ThemeStyle.Highlight_DeepYellow => (Color.FromArgb(155, 89, 182), Color.Black),

                // [수정] 중간 회색 배경 + 검은색 글씨(X) -> 흰색 글씨(O) (흰색 글씨가 대비가 더 높음)
                ThemeStyle.Neutral_Gray => (Color.FromArgb(149, 165, 166), Color.White),

                ThemeStyle.Display_LightYellow => (Color.FromArgb(255, 255, 225), Color.Black),
                
                ThemeStyle.Black_White => (Color.Black, Color.White),


                ThemeStyle.Default => (SystemColors.Control, SystemColors.ControlText),
                ThemeStyle.DesignModeOnly => (Color.LightYellow, Color.Black),
                _ => (SystemColors.Control, SystemColors.ControlText)
            };
            /* 시인성이 낮음
            return style switch
            {
                ThemeStyle.Primary_Indigo => (Color.FromArgb(52, 73, 94), Color.White),       // 진한 블루그레이
                ThemeStyle.Success_Green => (Color.FromArgb(46, 204, 113), Color.White),     // 에메랄드 그린
                ThemeStyle.Warning_Yellow => (Color.FromArgb(241, 196, 15), Color.Black),     // 선플라워 옐로
                ThemeStyle.Danger_Red => (Color.FromArgb(231, 76, 60), Color.White),       // 알리자린 레드
                ThemeStyle.Info_Sky => (Color.FromArgb(52, 152, 219), Color.White),        // 리버 블루
                ThemeStyle.Highlight_DeepYellow => (Color.FromArgb(155, 89, 182), Color.White),   // 퍼플 계열 강조
                ThemeStyle.Neutral_Gray => (Color.FromArgb(149, 165, 166), Color.Black),    // 중립 그레이
                ThemeStyle.Default => (SystemColors.Control, SystemColors.ControlText),
                ThemeStyle.DesignModeOnly => (Color.LightYellow, Color.Black),
                _ => (SystemColors.Control, SystemColors.ControlText)
            };
            */
        }
    }

    public class _Label : Label
    {
        private ThemeStyle themeStyle = ThemeStyle.Default;
        private ToolTip tooltip = new ToolTip();
        private string tooltipText;

        [Category("Theme")]
        [Description("테마 스타일")]
        [DefaultValue(ThemeStyle.Neutral_Gray)]
        public ThemeStyle ThemeStyle
        {
            get => themeStyle;
            set { themeStyle = value; ApplyTheme(); Invalidate(); }
        }
        [Category("Theme")]
        [Description("ToolTip")]
        public string TooltipText
        {
            get => tooltipText;
            set
            {
                tooltipText = value;
            }
        }

        public _Label()
        {
            themeStyle = ThemeStyle.Neutral_Gray;
            Font = new Font("D2Coding", 12F);
            // AutoSize = true;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            BackColor = back;
            ForeColor = fore;


            if (!DesignMode && ThemeStyle == ThemeStyle.DesignModeOnly)
            {
                base.SetVisibleCore(false);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            //tooltip.Show(tooltipText, this, Width /2, Height /2, 3000);
            tooltip.Show(tooltipText, this, 0, -15, 3000);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            tooltip.Hide(this);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            // Log.Instance.Controls($"Label Text Changed: Name:[{this.Name}] Text:[{this.Text}]");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Font?.Dispose();
                tooltip?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class _TextBox : TextBox
    {
        private ThemeStyle themeStyle = ThemeStyle.Neutral_Gray;

        [Category("Theme")]
        [Description("테마 스타일")]
        [DefaultValue(ThemeStyle.Neutral_Gray)]
        public ThemeStyle ThemeStyle
        {
            get => themeStyle;
            set { themeStyle = value; ApplyTheme(); Invalidate(); }
        }

        public _TextBox()
        {
            Font = new Font("D2Coding", 12F);
            AutoSize = true;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            BackColor = back;
            ForeColor = fore;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            // Log.Instance.Controls($"Label Text Changed: Name:[{this.Name}] Text:[{this.Text}]");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Font?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class _RadioButton : RadioButton
    {
        private ThemeStyle themeStyle = ThemeStyle.Info_Sky;

        [Category("Theme")]
        [Description("테마 스타일")]
        [DefaultValue(ThemeStyle.Info_Sky)]
        public ThemeStyle ThemeStyle
        {
            get => themeStyle;
            set { themeStyle = value; ApplyTheme(); Invalidate(); }
        }

        public _RadioButton()
        {
            Font = new Font("D2Coding", 12F);
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            BackColor = back;
            ForeColor = fore;
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            Log.Instance.Controls($"RadioButton Checked: Name:[{this.Name}] Text:[{this.Text}] Checked:[{this.Checked}]");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Font?.Dispose();
            }
            base.Dispose(disposing);
        }
    }


    public class _Button : System.Windows.Forms.Button
    {
        private ThemeStyle themeStyle = ThemeStyle.Primary_Indigo;
        private Font font = new Font("D2Coding", 12F);
        private ToolTip tooltip = new ToolTip();
        private string tooltipText;

        [Category("Theme")]
        [Description("테마 스타일")]
        [DefaultValue(ThemeStyle.Primary_Indigo)]
        public ThemeStyle ThemeStyle
        {
            get => themeStyle;
            set { themeStyle = value; ApplyTheme(); Invalidate(); }
        }
        [Category("Theme")]
        [Description("ToolTip")]
        public string TooltipText
        {
            get => tooltipText;
            set
            {
                tooltipText = value;
            }
        }

        public _Button()
        {
            themeStyle = ThemeStyle.Primary_Indigo; // Default theme
            Size = new Size(100, 55);
            Font = font;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            BackColor = back;
            ForeColor = fore;
        }

        private string GetTopLevelParentName()
        {
            Control? parent = this;
            while (parent != null && !(parent is Form))
            {
                parent = parent.Parent;
            }
            return parent?.Name ?? "UnknownForm";
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            string formName = GetTopLevelParentName();
            Log.Instance.Controls($"Button Click: Form:[{formName}] Name:[{this.Name}] Text:[{this.Text}]");

        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            //tooltip.Show(tooltipText, this, Width /2, Height /2, 3000);
            tooltip.Show(tooltipText, this, 0, -15, 3000);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            tooltip.Hide(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                font?.Dispose();
                tooltip?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class _CheckBox : CheckBox
    {
        private ThemeStyle themeStyle = ThemeStyle.Info_Sky;
        private Font font = new Font("D2Coding", 12F);
        private ToolTip tooltip = new ToolTip();

        [Category("Theme")]
        [Description("테마 스타일")]
        [DefaultValue(ThemeStyle.Info_Sky)]
        public ThemeStyle ThemeStyle
        {
            get => themeStyle;
            set { themeStyle = value; ApplyTheme(); Invalidate(); }
        }

        public _CheckBox()
        {
            Font = font;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            BackColor = back;
            ForeColor = fore;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
         //   tooltip.Show(this.Text, this, Width / 2, Height / 2, 1000);
            Log.Instance.Controls($"CheckBox Click: Name:[{this.Name}] Text:[{this.Text}] Checked:[{this.Checked}]");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                font?.Dispose();
                tooltip?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class _ComboBox : ComboBox
    {
        private ThemeStyle themeStyle = ThemeStyle.Highlight_DeepYellow;
        private Font font = new Font("D2Coding", 12F);

        private ToolTip tooltip = new ToolTip();
        private string tooltipText;

        private bool rightClickFlag = false;

        [Category("Theme")]
        [Description("테마 스타일")]
        [DefaultValue(ThemeStyle.Highlight_DeepYellow)]
        public ThemeStyle ThemeStyle
        {
            get => themeStyle;
            set { themeStyle = value; ApplyTheme(); Invalidate(); }
        }
        [Category("Theme")]
        [Description("ToolTip")]
        public string TooltipText
        {
            get => tooltipText;
            set
            {
                tooltipText = value;
            }
        }

        public bool Flags { get => rightClickFlag; }

        public _ComboBox()
        {
            Font = font;
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            BackColor = back;
            ForeColor = fore;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            //tooltip.Show(tooltipText, this, Width /2, Height /2, 3000);
            tooltip.Show(tooltipText, this, 0, -30, 3000);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            tooltip.Hide(this);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Right)
            {
                /*
                rightClickFlag = !rightClickFlag;

                if (rightClickFlag)
                    BackColor = Color.Red;
                else
                    ApplyTheme();
                */
            }
        }

        protected override void OnSelectionChangeCommitted(EventArgs e)
        {
            base.OnSelectionChangeCommitted(e);
            string selected = SelectedItem?.ToString() ?? "(없음)";
            Log.Instance.Controls($"ComboBox Selected: Name:[{this.Name}] Text:[{selected}]");
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            e.DrawBackground();

            using (var brush = new SolidBrush(fore))
            {
                e.Graphics.DrawString(Items[e.Index]?.ToString() ?? "", e.Font, brush, e.Bounds);
            }

            base.OnDrawItem(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                font?.Dispose();
                tooltip?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class _ListBox : ListBox
    {
        private ThemeStyle themeStyle = ThemeStyle.Neutral_Gray;
        private Font font = new Font("D2Coding", 12F);

        [Category("Theme")]
        [Description("테마 스타일")]
        [DefaultValue(ThemeStyle.Neutral_Gray)]
        public ThemeStyle ThemeStyle
        {
            get => themeStyle;
            set { themeStyle = value; ApplyTheme(); Invalidate(); }
        }

        public _ListBox()
        {
            Font = font;
            DrawMode = DrawMode.OwnerDrawFixed;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            BackColor = back;
            ForeColor = fore;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (DesignMode || e.Index < 0) return;

            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            e.DrawBackground();

            using (var brush = new SolidBrush(fore))
            {
                e.Graphics.DrawString(Items[e.Index]?.ToString() ?? "", e.Font, brush, e.Bounds);
            }

            base.OnDrawItem(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                font?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class _DataGridView : DataGridView
    {
        private ThemeStyle themeStyle = ThemeStyle.Display_LightYellow;
        private Font font = new Font("D2Coding", 12F);

        public (int col, int row) getCurrentCell()
        {
            int c = CurrentCell.ColumnIndex;
            int r = CurrentCell.RowIndex;
            return (c, r);
        }

        [Category("Theme")]
        [Description("테마 스타일")]
        [DefaultValue(ThemeStyle.Display_LightYellow)]
        public ThemeStyle ThemeStyle
        {
            get => themeStyle;
            set { themeStyle = value; ApplyTheme(); Invalidate(); }
        }

        public _DataGridView()
        {
            Font = font;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            RowHeadersVisible = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ApplyTheme();

            this.DoubleBuffered = true;

            this.CellPainting += _DataGridView_CellPainting;
            this.DataError += _DataGridView_DataError;
        }

        private void _DataGridView_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            Log.Instance.Error(e.Exception.Message);
            e.Cancel = true;
        }

        private void ApplyTheme()
        {
            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            BackgroundColor = back;
            DefaultCellStyle.BackColor = back;
            DefaultCellStyle.ForeColor = fore;
        }

        private void _DataGridView_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                e.CellStyle.Font = font;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                font?.Dispose();
                this.CellPainting -= _DataGridView_CellPainting;
                this.DataError -= _DataGridView_DataError;
            }
            base.Dispose(disposing);
        }
    }

    public class _Panel : Panel
    {
        private ThemeStyle themeStyle = ThemeStyle.Default;

        [Category("Theme")]
        [Description("테마 스타일")]
        [DefaultValue(ThemeStyle.Default)]
        public ThemeStyle ThemeStyle
        {
            get => themeStyle;
            set { themeStyle = value; ApplyTheme(); Invalidate(); }
        }

        public _Panel()
        {
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            BackColor = back;
            ForeColor = fore;
        }
    }

    public class _GroupBox : GroupBox
    {
        private ThemeStyle themeStyle = ThemeStyle.Highlight_DeepYellow;
        private Font font = new Font("D2Coding", 12F);

        [Category("Theme")]
        [Description("테마 스타일")]
        [DefaultValue(ThemeStyle.Highlight_DeepYellow)]
        public ThemeStyle ThemeStyle
        {
            get => themeStyle;
            set { themeStyle = value; ApplyTheme(); Invalidate(); }
        }

        public _GroupBox()
        {
            Font = font;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            BackColor = back;
            ForeColor = fore;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                font?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class _ListView : ListView
    {
        private ThemeStyle themeStyle = ThemeStyle.Default;
        private Font font = new Font("D2Coding", 12F);

        [Category("Theme")]
        [Description("테마 스타일")]
        [DefaultValue(ThemeStyle.Default)]
        public ThemeStyle ThemeStyle
        {
            get => themeStyle;
            set { themeStyle = value; ApplyTheme(); Invalidate(); }
        }

        public _ListView()
        {
            Font = font;
            OwnerDraw = true; // Enable owner drawing for custom theme
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            BackColor = back;
            ForeColor = fore;
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            base.OnDrawColumnHeader(e);
            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            using (SolidBrush backBrush = new SolidBrush(back))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }
            using (SolidBrush foreBrush = new SolidBrush(fore))
            {
                e.Graphics.DrawString(e.Header.Text, e.Font, foreBrush, e.Bounds);
            }
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            base.OnDrawItem(e);
            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), e.Bounds); // Highlight selected item
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(back), e.Bounds);
            }
            e.Graphics.DrawString(e.Item.Text, e.Item.Font, new SolidBrush(fore), e.Bounds);
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            base.OnDrawSubItem(e);
            var (back, fore) = ThemeHelper.GetColorPair(themeStyle);
            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), e.Bounds); // Highlight selected item
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(back), e.Bounds);
            }
            e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, new SolidBrush(fore), e.Bounds);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                font?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}