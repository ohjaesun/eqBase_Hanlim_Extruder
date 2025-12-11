using EQ.Domain.Enums;

using EQ.Domain.Interface;
using System.Collections.Generic;

namespace EQ.Infra.HW.IO
{
    public class PIOHandoverController : IPIOHandover
    {
        private readonly IIoController _ioController;

        // PIO 입력 신호를 실제 I/O 모듈의 입력 접점 번호에 매핑합니다.
        // 이 값들은 나중에 설정 파일에서 로드해야 합니다.
        private Dictionary<PIOSignal, int> _inputMap;
        /*
        = new Dictionary<PIOSignal, int>
        {
            { PIOSignal.VALID, 0 },
            { PIOSignal.CS_0, 1 },
            { PIOSignal.CS_1, 2 },
            { PIOSignal.TR_REQ, 3 },
            { PIOSignal.BUSY, 4 },
            { PIOSignal.COMPT, 5 },
            { PIOSignal.CONT, 6 },
            { PIOSignal.AM_AVBL, 7 },
        };
        */
        // PIO 출력 신호를 실제 I/O 모듈의 출력 접점 번호에 매핑합니다.
        private Dictionary<PIOSignal, int> _outputMap;
          

        /// <summary>
        /// 생성자에서 I/O 컨트롤러 구현체를 주입받습니다.
        /// </summary>
        /// <param name="ioController">실제 I/O 하드웨어를 제어하는 컨트롤러</param>
        public PIOHandoverController(IIoController ioController)
        {
            _ioController = ioController;
        }

        /// <summary>
        /// PIO 출력 신호의 값을 설정합니다.
        /// </summary>
        public void SetSignal(PIOSignal signal, bool value)
        {
            if (_outputMap.TryGetValue(signal, out int address))
            {
                _ioController.WriteOutput(address, value ? (byte)1 : (byte)0);
            }
            // else: 출력으로 정의되지 않은 신호에 대한 예외 처리 또는 로깅을 추가할 수 있습니다.
        }

        /// <summary>
        /// PIO 입력 신호의 현재 값을 가져옵니다.
        /// </summary>
        public bool GetSignal(PIOSignal signal)
        {
            if (_inputMap.TryGetValue(signal, out int address))
            {
                return _ioController.ReadInput(address);
            }
            
            // 출력 신호의 상태도 읽어야 할 경우
            if (_outputMap.TryGetValue(signal, out address))
            {
                return _ioController.ReadOutput(address);
            }

            // 정의되지 않은 신호에 대한 예외 처리 또는 로깅
            return false;
        }

        public void SetIOStartIndex(IO_IN _input, IO_OUT _output)
        {
            int startInput = (int)_input;
            _inputMap = new Dictionary<PIOSignal, int>()
            {
                { PIOSignal.VALID, startInput++ },
                { PIOSignal.CS_0, startInput++ },
                { PIOSignal.CS_1, startInput++ },
                { PIOSignal.TR_REQ, startInput ++ },
                { PIOSignal.BUSY, startInput ++ },
                { PIOSignal.COMPT, startInput ++ },
                { PIOSignal.CONT, startInput ++ },
                { PIOSignal.AM_AVBL, startInput ++ },
            };

            int startOutput = (int)_output;
            _outputMap = new Dictionary<PIOSignal, int>()
            {
                { PIOSignal.L_REQ, startOutput++ },
                { PIOSignal.U_REQ, startOutput++ },
                { PIOSignal.READY, startOutput ++ },
                { PIOSignal.HO_AVBL, startOutput ++ },
                { PIOSignal.ES, startOutput ++ },
                { PIOSignal.RES_OUT_5, startOutput ++ },
                { PIOSignal.RES_OUT_6, startOutput ++ },
                { PIOSignal.RES_OUT_7, startOutput ++ },
            };
        }
    }
}