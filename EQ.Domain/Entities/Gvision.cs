using EQ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Domain.Entities
{

        public class StartPack
        {
            public string CMD { get; set; } = VisionCommandType.StartPack.ToString();
            public int encoderPulse { get; set; }
        }


        public class JobChange
        {
            public string CMD { get; set; } = VisionCommandType.JobChange.ToString();
            public string JobID { get; set; } = string.Empty;
        }

        public class SOT
        {
            public string CMD { get; set; } = VisionCommandType.SOT.ToString();
            public int iCamNO { get; set; }
            public int iFindNum { get; set; }

            public int iNozlID1 { get; set; }
            public string ReelID1 { get; set; } = string.Empty;
            public int iX1 { get; set; }
            public int iY1 { get; set; }
            public int iLogX1 { get; set; }
            public int iLogY1 { get; set; }
            public int iRetryCnt1 { get; set; }
            public int iChipDataType1 { get; set; }
            public string LotID1 { get; set; } = string.Empty;

            public int iNozlID2 { get; set; }
            public string ReelID2 { get; set; } = string.Empty;
            public int iX2 { get; set; }
            public int iY2 { get; set; }
            public int iLogX2 { get; set; }
            public int iLogY2 { get; set; }
            public int iRetryCnt2 { get; set; }
            public int iChipDataType2 { get; set; }
            public string LotID2 { get; set; } = string.Empty;

            public int iNozlID3 { get; set; }
            public string ReelID3 { get; set; } = string.Empty;
            public int iX3 { get; set; }
            public int iY3 { get; set; }
            public int iLogX3 { get; set; }
            public int iLogY3 { get; set; }
            public int iRetryCnt3 { get; set; }
            public int iChipDataType3 { get; set; }
            public string LotID3 { get; set; } = string.Empty;

            public int iNozlID4 { get; set; }
            public string ReelID4 { get; set; } = string.Empty;
            public int iX4 { get; set; }
            public int iY4 { get; set; }
            public int iLogX4 { get; set; }
            public int iLogY4 { get; set; }
            public int iRetryCnt4 { get; set; }
            public int iChipDataType4 { get; set; }
            public string LotID4 { get; set; } = string.Empty;
        }
        #region EOT
        public class InspectItem
        {
            public string ModuleName { get; set; } = string.Empty;
            public string SpecName { get; set; } = string.Empty;
            public int iScrabCode { get; set; }
            public int iItemResult { get; set; }
            public float fMinSpec { get; set; }
            public float fMaxSpec { get; set; }
            public float fWrostValue { get; set; }
            public int iPriority { get; set; } = -1;
        }

        public class MatchResult
        {
            public int iNozlID { get; set; }
            public int iInspectResult { get; set; }
            public float fCurrX { get; set; }
            public float fCurrY { get; set; }
            public float fCurrT { get; set; }
            public int iRetryCount { get; set; }
            public int iInspectItemCnt { get; set; }
            public List<InspectItem> InspITEM { get; set; } = new List<InspectItem>();
        }

        public class EOT
        {
            public string CMD { get; set; } = VisionCommandType.EOT.ToString();
            public int iCamNO { get; set; }
            public int iErrorCode { get; set; }
            public int iInspNO { get; set; }
            public int iPockNo { get; set; }
            public int iPinDir { get; set; }
            public string Barcode1 { get; set; } = string.Empty;
            public string Barcode2 { get; set; } = string.Empty;
            public string Barcode3 { get; set; } = string.Empty;
            public List<MatchResult> sRslt { get; set; } = new List<MatchResult>();
        }
        #endregion EOT

        public class LotEnd
        {
            public string CMD { get; set; } = VisionCommandType.LotEnd.ToString();
            public int iLotType { get; set; }
            public string LotID { get; set; } = string.Empty;
            public string SplitLotID { get; set; } = string.Empty;
            public string ETCReason { get; set; } = string.Empty;
            public int iRejectCount { get; set; }
        }

        public class GrabDone
        {
            public string CMD { get; set; } = VisionCommandType.GrabDone.ToString();
            public int iCamNO { get; set; }
            public int iErrCode { get; set; }
            public int iNozlNo { get; set; }
            public int iChipNo { get; set; }
        }
    }

