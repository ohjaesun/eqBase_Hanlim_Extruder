using EQ.Core.Act; // [추가]
using EQ.Core.Service;
using EQ.Domain.Entities; // [추가]
using System.Windows.Forms;
using static EQ.Core.Sequence.SEQ; // [추가]
using System; // [추가]
using EQ.UI.Controls; // [추가]

namespace EQ.UI.UserViews
{
    // UserControlBase 상속
    public partial class SequencesPanel_View : UserControlBase
    {
        public SequencesPanel_View()
        {
            InitializeComponent();
        }

        private void All_Sequences_View_Load(object sender, System.EventArgs e)
        {
            if (DesignMode) return;

            _ButtonSave.Visible = false; // 저장 버튼 숨김
            _LabelTitle.Text = "All Sequence Status";

            // 시퀀스 타임아웃 정보 표시
            var act = ActManager.Instance.Act;
            int timeoutMin = act.Option.Option4.MaxSequenceTime / 60000;
            _LabelTimeout.Text = $"Timeout: {timeoutMin} min";

            var seqManager = SeqManager.Instance.Seq;

            // SeqName Enum에 정의된 모든 시퀀스를 순회
            foreach (SeqName seqName in Enum.GetValues(typeof(SeqName)))
            {
                // GetSequence를 통해 실제 인스턴스가 있는지 확인
                var seqInstance = seqManager.GetSequence(seqName);

                if (seqInstance != null)
                {
                    // 인스턴스가 있으면 Sequence_View를 생성
                    Sequence_View view = new Sequence_View();
                    view.InitializeSequence(seqName); // 새 컨트롤 초기화
                    _FlowLayoutPanel.Controls.Add(view);
                }
            }
        }
    }
}