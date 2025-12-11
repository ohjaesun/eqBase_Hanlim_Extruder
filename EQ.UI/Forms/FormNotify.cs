// EQ.UI/FormNotify.cs
using EQ.Domain.Enums;
using EQ.UI.Controls;
using System;
using System.Collections.Generic;
using System.Drawing; // Point 사용을 위해 필요
using System.Windows.Forms;

namespace EQ.UI
{
    public partial class FormNotify : FormBase // FormBase 상속
    {
        // 이 팝업과 "동일한" 팝업 그룹
        private List<FormNotify> _siblingForms;
        // 그룹 종료가 진행 중인지 확인 (무한 루프 방지)
        private bool _isClosingAsGroup = false;

        // [추가] 마우스 이동 좌표 저장을 위한 변수
        private Point _mousePoint;

        /// <summary>
        /// Windows Form 디자이너를 위한 생성자
        /// </summary>
        public FormNotify()
        {
            InitializeComponent();
        }

        /// <summary>
        /// FormSplash의 핸들러에서 호출할 생성자
        /// </summary>
        public FormNotify(string title, string message, NotifyType type)
        {
            InitializeComponent();

            this.TopMost = true;
            this.ShowInTaskbar = false;
            // 시작 위치는 FormSplash의 핸들러가 수동(Manual)으로 설정합니다.

            this._LabelTitle.Text = title;
            this.labelMessage.Text = message;
            SetTheme(type);

            // 닫기 버튼은 '그룹 종료'를 호출
            this._ButtonOK.Click += (s, e) => this.CloseGroup();

           
            this._LabelTitle.MouseDown += _LabelTitle_MouseDown;
            this._LabelTitle.MouseMove += _LabelTitle_MouseMove;
        }

        
        private void _LabelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            _mousePoint = new Point(e.X, e.Y);
        }

        
        private void _LabelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {                
                this.Location = new Point(this.Left - (_mousePoint.X - e.X), this.Top - (_mousePoint.Y - e.Y));
            }
        }

        /// <summary>
        /// Spawner(FormSplash)가 이 팝업 그룹을 설정
        /// </summary>
        public void SetSiblingGroup(List<FormNotify> siblings)
        {
            _siblingForms = siblings;
        }

        /// <summary>
        /// 자신을 포함한 모든 그룹 팝업을 닫음
        /// </summary>
        private void CloseGroup()
        {
            // 이미 그룹 종료가 진행 중이면 반환
            if (_isClosingAsGroup) return;

            // 그룹이 없으면 자신만 닫음
            if (_siblingForms == null)
            {
                this.Close();
                return;
            }

            // 모든 팝업을 닫음
            foreach (var form in _siblingForms)
            {
                form._isClosingAsGroup = true; // 플래그 설정
                form.Close(); // 닫기
            }
            _siblingForms.Clear();
        }

        /// <summary>
        /// 사용자가 폼의 닫기(X) 버튼을 눌렀을 때
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 사용자가 닫았고, 아직 그룹 종료가 시작되지 않았다면
            if (e.CloseReason == CloseReason.UserClosing && !_isClosingAsGroup)
            {
                // 1. 이 폼의 개별 닫기를 '취소'
                e.Cancel = true;
                // 2. 대신 '그룹 종료'를 트리거
                CloseGroup();
            }

            base.OnFormClosing(e);
        }

        private void SetTheme(NotifyType type)
        {
            ThemeStyle style = type switch
            {
                NotifyType.Info => ThemeStyle.Info_Sky,
                NotifyType.Warning => ThemeStyle.Warning_Yellow,
                NotifyType.Error => ThemeStyle.Danger_Red,
                _ => ThemeStyle.Neutral_Gray,
            };

            this._PanelTitle.ThemeStyle = style;
            this._LabelTitle.ThemeStyle = style;
            this._ButtonOK.ThemeStyle = style;
        }
    }
}