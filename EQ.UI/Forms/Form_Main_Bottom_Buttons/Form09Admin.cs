using EQ.UI.Controls; // EqBase 컨트롤 사용
using EQ.UI.UserViews; // EqBase 유저 뷰 사용
using EQ.UI.UserViews.Setup;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EQ.UI
{
    // 1. Form_Base -> FormBase 상속
    public partial class Form09Admin : FormBase
    {
        List<Form> formList = new List<Form>();

        public Form09Admin()
        {
            InitializeComponent();
            Disposed += FormAdmin_Disposed;

            // 2. FormBase에 FormBorderStyle이 없으므로, 직접 설정
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void FormAdmin_Disposed(object? sender, EventArgs e)
        {
            Disposed -= FormAdmin_Disposed;

            foreach (var form in formList)
            {
                form.Dispose();
            }
            formList.Clear();
        }

        /// <summary>
        /// 모든 버튼이 이 하나의 이벤트 핸들러를 공유합니다.
        /// </summary>
        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            // 3. Button -> _Button (EqBase 컨트롤)
            _Button btn = sender as _Button;
            if (btn == null) return;

            // 4. Regex를 사용해 이름에서 숫자 인덱스 추출 (기존 로직 동일)
            // (예: "_Button1" -> "1", "_Button18" -> "18")
            string BtnIdx = Regex.Replace(btn.Name, @"\D", "");
            if (!int.TryParse(BtnIdx, out int idx)) return;

            bool isRightClick = e.Button == MouseButtons.Right;
            Form fm = null;

            if (isRightClick)
            {
                fm = new Form();
                fm.Text = btn.Text;
                fm.TopMost = true;
                fm.Size = new System.Drawing.Size(1024, 768); // 새 폼 크기 지정
                formList.Add(fm);
            }
            else
            {
                // 5. panel1 -> _PanelMain (EqBase 컨트롤)
                // 컨트롤을 Dispose하고 Panel을 비웁니다.
                foreach (Control ctrl in _PanelMain.Controls)
                {
                    ctrl.Dispose();
                }
                _PanelMain.Controls.Clear();
            }

            // 6. UserControl 인스턴스를 담을 변수
            UserControl viewToLoad = null;

            // 7. 버튼 name의 숫자값을 사용해 인덱싱 (요청대로 주석 처리)
            switch (idx)
            {
                case 1: // 시퀀스 뷰
                     viewToLoad = new SequencesPanel_View(); // (이전에 생성한 컨트롤)
                    break;
                case 2: // IO
                     viewToLoad = new IO_View();
                    break;
                case 3: // 모터 상태 
                    viewToLoad = new Motor_View();
                    break;
                case 4: // 모터 포지션 (미구현)
                    viewToLoad = new MotorPosition_View();
                    break;
                case 5: // 모터 속도
                    viewToLoad = new MotionSpeed_View();                    
                    break;
                case 6: // 타임 체크 (라이브러리 사용하는걸로 교체)
                    //viewToLoad = new Statistics_View();
                    viewToLoad = new Statistics_ScottPlot_View();
                    break;
                case 7: // 모터 인터락 (미구현)
                    viewToLoad = new MotorInterlock_View();
                    break;
                case 8: // 모터 반복 테스트 (미구현)
                    break;
                case 9: // 비전 
                    viewToLoad = new GVision_View();
                    break;
                case 10: // 알람 이력 
                     viewToLoad = new Alarm_View(); 
                    break;
                case 11: // 레시피
                     viewToLoad = new Recipe_View();
                    break;
                case 12: // 옵션
                     viewToLoad = new UserOption_View();
                    break;
                case 13: // DB 복구
                    viewToLoad = new DB_Export_View();
                    break;
                case 14: //온도
                    viewToLoad = new Temperature_View();
                    break;
                case 15: // 
                  
                    break;
                case 16: // 
                  
                    break;
                // ... (다른 버튼들) ...
                case 17: 
                    break;
                case 18: // 로그
                     viewToLoad = new Log_View(); 
                    break;
                case 19: //SDO write
                    viewToLoad = new EtherCAT_SDO_View();
                    break;
                case 20: 
                    break;
                case 21: 
                    break;
                case 22: 
                    break;
                case 23: 
                    break;
                case 24: 
                    break;
                case 25: 
                    break;

                default:
                    break;
            }           
            
            //마우스 오른쪽이면 새창으로 띄움
            if (viewToLoad != null)
            {
                viewToLoad.Dock = DockStyle.Fill;
                if (isRightClick && fm != null)
                {
                    fm.Controls.Add(viewToLoad);
                    fm.Show();
                }
                else
                {
                    _PanelMain.Controls.Add(viewToLoad);
                }
            }
            
        }

        private void _CheckBoxTopMost_CheckedChanged(object sender, EventArgs e)
        {
            // 9. _CheckBox1 -> _CheckBoxTopMost
            if (sender is _CheckBox checkBox)
            {
                // 10. 부모 폼(FormAdmin)이 아닌,
                //     이 폼을 담고 있는 최상위 폼(FormMain)의 TopMost를 제어
                Form main = this.TopLevelControl as Form;
                if (main != null)
                {
                    main.TopMost = checkBox.Checked;
                }
            }
        }
    }
}