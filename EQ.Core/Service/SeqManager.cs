using EQ.Core.Act;
using EQ.Core.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Core.Service
{
    public class SeqManager
    {
        // 1. Thread-safe, lazy-loading을 위한 Lazy<T>
        private static readonly Lazy<SeqManager> lazyInstance =
            new Lazy<SeqManager>(() => new SeqManager());

        // 2. 외부에서 접근할 수 있는 public static 속성
        public static SeqManager Instance => lazyInstance.Value;

        // 3. 외부에서 사용할 실제 'SEQ' 매니저 인스턴스
        public SEQ Seq { get; private set; }

        /// <summary>
        /// 4. 외부 생성을 막는 private 생성자 (가장 중요)
        /// </summary>
        private SeqManager()
        {
            
            ACT actInstance = ActManager.Instance.Act;            
            Seq = new SEQ(actInstance);           
            Seq.InitSequence();
        }
    }
}
