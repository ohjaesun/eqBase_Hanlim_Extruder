using EQ.Domain.Entities;
using EQ.UI.Controls;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EQ.UI.UserViews
{
    public class ProductMap_ViewBase : UserControlBaseplain
    {
        protected SKGLControl _skControl;
        protected Panel _PanelMain;

        protected float _chipSize = 20.0f;
        protected SKMatrix _viewMatrix = SKMatrix.CreateIdentity();
        protected Dictionary<ProductUnitChipGrade, SKPaint> _colorMap = new();

        protected SKPaint _paintDefault;
        protected SKPaint _paintGrid;
        protected SKPaint _paintShot; // Shot 라인용 (굵은 선)

        public int ShotSizeX { get; set; } = 5; // X축 Shot 간격
        public int ShotSizeY { get; set; } = 5; // Y축 Shot 간격

        private Point _lastMousePos;
        private bool _isDragging = false;

        // [수정] ZoomLevel은 Matrix에서 직접 추출하므로 변수 제거 가능하나 편의상 유지
        protected float _zoomLevel = 1.0f;

        public bool ShowGrid { get; set; } = true;

        public ProductMap_ViewBase()
        {
            InitializeBaseUI();

            if (this.DesignMode) return;

            InitPaints();
            InitializeSkia();
        }

        private void InitializeBaseUI()
        {
            _PanelMain = new Panel();
            _PanelMain.Dock = DockStyle.Fill;
            _PanelMain.BackColor = SystemColors.Control;
            this.Controls.Add(_PanelMain);
        }

        private void InitializeSkia()
        {
            _skControl = new SKGLControl();
            _skControl.Dock = DockStyle.Fill;
            _skControl.PaintSurface += OnPaintSurface;

            _skControl.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Middle || (e.Button == MouseButtons.Left && ModifierKeys == Keys.Control))
                {
                    _isDragging = true;
                    _lastMousePos = e.Location;
                }
                else
                {
                    HandleMouseClick(e);
                }
            };
            _skControl.MouseMove += (s, e) =>
            {
                if (_isDragging)
                {
                    float dx = e.X - _lastMousePos.X;
                    float dy = e.Y - _lastMousePos.Y;

                    // 현재 스케일 유지한 채 이동
                    _viewMatrix = _viewMatrix.PostConcat(SKMatrix.CreateTranslation(dx, dy));
                    _lastMousePos = e.Location;
                    _skControl.Invalidate();
                }
            };
            _skControl.MouseUp += (s, e) => _isDragging = false;

            // 줌 인/아웃
            _skControl.MouseWheel += (s, e) =>
            {
                float scaleFactor = (e.Delta > 0) ? 1.1f : 0.9f;

                // 마우스 커서 위치 기준으로 줌
                _viewMatrix = _viewMatrix.PostConcat(SKMatrix.CreateScale(scaleFactor, scaleFactor, e.X, e.Y));
                _skControl.Invalidate();
            };

            // [요청] 더블 클릭 시 FitToScreen
            _skControl.DoubleClick += (s, e) => FitToScreen();

            _PanelMain.Controls.Add(_skControl);
        }

        private void InitPaints()
        {
            _paintDefault = new SKPaint { Style = SKPaintStyle.Fill, Color = SKColors.WhiteSmoke };

            // [수정] 초기 두께는 0 (Hairline)으로 설정하되, OnPaintSurface에서 동적 조정
            _paintGrid = new SKPaint { Style = SKPaintStyle.Stroke, Color = SKColors.LightGray, IsAntialias = false };
            _paintShot = new SKPaint { Style = SKPaintStyle.Stroke, Color = SKColors.Gray, IsAntialias = false };

            _colorMap[ProductUnitChipGrade.Good] = new SKPaint { Style = SKPaintStyle.Fill, Color = SKColors.LimeGreen };
            _colorMap[ProductUnitChipGrade.Fail] = new SKPaint { Style = SKPaintStyle.Fill, Color = SKColors.Red };
            _colorMap[ProductUnitChipGrade.Skip] = new SKPaint { Style = SKPaintStyle.Fill, Color = SKColors.LightGray };
            _colorMap[ProductUnitChipGrade.None] = new SKPaint { Style = SKPaintStyle.Fill, Color = SKColors.White };
        }

        // 상속받은 클래스에서 구현
        protected virtual int GetRows() { return 0; }
        protected virtual int GetCols() { return 0; }
        protected virtual ProductUnitChipGrade GetChipGrade(int x, int y) { return ProductUnitChipGrade.None; }
        protected virtual void OnChipClick(int x, int y, MouseButtons btn) { }

        private void OnPaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White);

            if (this.DesignMode || _paintDefault == null) return;

            int rows = GetRows();
            int cols = GetCols();

            if (rows == 0 || cols == 0) return;

            // 0. [중요] 선 두께 동적 보정 (Zoom Out시 선 사라짐 방지)
            // 화면상에서 항상 Grid=1px, Shot=2px로 보이도록 역보정
            float currentScale = _viewMatrix.ScaleX;
            if (currentScale > 0)
            {
                _paintGrid.StrokeWidth = 1.0f / currentScale; // 화면상 1px
                _paintShot.StrokeWidth = 2.0f / currentScale; // 화면상 2px (굵은 선)
            }

            canvas.SetMatrix(_viewMatrix);

            // 1. 칩(사각형) 먼저 그리기
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    var grade = GetChipGrade(x, y);
                    SKRect rect = new SKRect(x * _chipSize, y * _chipSize, (x + 1) * _chipSize, (y + 1) * _chipSize);

                    if (_colorMap.TryGetValue(grade, out var paint))
                        canvas.DrawRect(rect, paint);
                    else
                        canvas.DrawRect(rect, _paintDefault);
                }
            }

            // 2. [요청] 선(Grid/Shot)을 가장 마지막에 그리기
            if (ShowGrid)
            {
                float mapWidth = cols * _chipSize;
                float mapHeight = rows * _chipSize;

                // 세로선
                for (int x = 0; x <= cols; x++)
                {
                    float xPos = x * _chipSize;

                    // Shot 라인 조건: ShotSizeX 단위 or 외곽선
                    bool isShot = (ShotSizeX > 0 && x % ShotSizeX == 0) || x == 0 || x == cols;

                    canvas.DrawLine(xPos, 0, xPos, mapHeight, isShot ? _paintShot : _paintGrid);
                }

                // 가로선
                for (int y = 0; y <= rows; y++)
                {
                    float yPos = y * _chipSize;

                    // Shot 라인 조건: ShotSizeY 단위 or 외곽선
                    bool isShot = (ShotSizeY > 0 && y % ShotSizeY == 0) || y == 0 || y == rows;

                    canvas.DrawLine(0, yPos, mapWidth, yPos, isShot ? _paintShot : _paintGrid);
                }
            }
        }

        private void HandleMouseClick(MouseEventArgs e)
        {
            if (_viewMatrix.TryInvert(out SKMatrix inverted))
            {
                SKPoint world = inverted.MapPoint(new SKPoint(e.X, e.Y));
                int x = (int)(world.X / _chipSize);
                int y = (int)(world.Y / _chipSize);

                if (x >= 0 && x < GetCols() && y >= 0 && y < GetRows())
                {
                    OnChipClick(x, y, e.Button);
                }
            }
        }

        // [요청] 화면 맞춤 (Fit)
        public void FitToScreen()
        {
            if (_skControl == null || _skControl.Width == 0) return;
            int rows = GetRows();
            int cols = GetCols();
            if (rows == 0 || cols == 0) return;

            // 전체 맵의 World 크기
            float mapW = cols * _chipSize;
            float mapH = rows * _chipSize;

            // 화면(Control) 크기
            float screenW = _skControl.Width;
            float screenH = _skControl.Height;

            // [비율 계산] 가로/세로 중 더 꽉 차는 쪽 기준 (Padding 5% 적용)
            float scaleX = screenW / mapW;
            float scaleY = screenH / mapH;
            float scale = Math.Min(scaleX, scaleY) * 0.95f;

            // 중앙 정렬 좌표 계산
            float dx = (screenW - mapW * scale) / 2.0f;
            float dy = (screenH - mapH * scale) / 2.0f;

            // 행렬 초기화 및 적용
            _viewMatrix = SKMatrix.CreateIdentity();
            _viewMatrix = _viewMatrix.PostConcat(SKMatrix.CreateScale(scale, scale));
            _viewMatrix = _viewMatrix.PostConcat(SKMatrix.CreateTranslation(dx, dy));

            _zoomLevel = scale;

            RefreshView();
        }

        public void RefreshView()
        {
            _skControl?.Invalidate();
        }

        // UserControl 로드 시 최초 Fit (상속받는 View의 Load 이벤트에서 호출됨을 가정)
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 데이터가 이미 있다면 Fit 수행
            if (!DesignMode && GetRows() > 0)
            {
                FitToScreen();
            }
        }
    }
}