using EQ.Common.Logs;
using EQ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Tcp;

namespace EQ.Domain.Entities
{
    #region 설정 값들
    /// <summary>
    /// 사용자 옵션 정의
    /// </summary>
    public class UserOption1
    {

        [CategoryAttribute("ChipData")]
        [DescriptionAttribute("Magazine 갯수")]
        public int Chip_MagazineCount { get; set; } = 1;
        [CategoryAttribute("ChipData")]
        [DescriptionAttribute("Magazine의 Tray 갯수")]
        public int Chip_TrayCount { get; set; } = 10;

        [CategoryAttribute("ChipData")]
        [DescriptionAttribute("Tray X ")]
        public int Chip_Tray_X { get; set; } = 20;
        [CategoryAttribute("ChipData")]
        [DescriptionAttribute("Tray Y ")]
        public int Chip_Tray_Y { get; set; } = 50;

        public enum LanguageType
        {
            English,
            Korean,
        }

        [CategoryAttribute("언어")]
        [DescriptionAttribute("언어설정")]
        public LanguageType Language { get; set; } = LanguageType.English;

        [CategoryAttribute("Soaking")]
        [DescriptionAttribute("Soaking Start Temp")]
        public double SoakingZone1 { get; set; } = 50.0;
        [CategoryAttribute("Soaking")]
        [DescriptionAttribute("Soaking Start Temp")]
        public double SoakingZone2 { get; set; } = 50.0;
        [CategoryAttribute("Soaking")]
        [DescriptionAttribute("Soaking Time (Minute)")]
        public long SoakingTime { get; set; } = 5;
        [CategoryAttribute("Soaking")]
        [DescriptionAttribute("Soaking Off Margin (Persent)")]
        public long SoakingOffMargin { get; set; } = 5;

    }

    public class UserOption2  // 네트워크 관련
    {
        public struct NetworkInfo
        {
            [ReadOnly(true)]
            public string Name { get; set; } // 네트워크 이름
            public string IP { get; set; }
            public int Port { get; set; }
            public bool AutoReconnect { get; set; } = true;
            public EndType MsgEnd { get; set; } = EndType.None;

            public NetworkInfo(string name, string ip, int port , bool autoReconnect = true , EndType endType = EndType.ETX)
            {
                Name = name;
                IP = ip;
                Port = port;
                AutoReconnect = autoReconnect;
                MsgEnd = endType;
            }
        }

        [CategoryAttribute("Vision")]
        [DescriptionAttribute("Global Vision")]
        public NetworkInfo[] GVision { get; set; } =
        {
            new NetworkInfo(GVisionType.GVisionTOP.ToString(), "192.168.0.1", 8080),
            new NetworkInfo(GVisionType.GVisionBOTTOM.ToString(), "192.168.0.2", 8081),
            new NetworkInfo(GVisionType.GVisionSIDE.ToString(), "192.168.0.3", 8082),
        };     


    }
    public class UserOption3
    {
        [CategoryAttribute("ChipData")]
        [DescriptionAttribute("Magazine 갯수")]
        public int Chip_MagazineCount { get; set; } = 1;
        [CategoryAttribute("ChipData")]
        [DescriptionAttribute("Magazine의 Tray 갯수")]
        public int Chip_TrayCount { get; set; } = 10;

        [CategoryAttribute("WaferMap")]
        [DescriptionAttribute(" X Length")]
        public int WaferMap_X { get; set; } = 20;
        [CategoryAttribute("WaferMap")]
        [DescriptionAttribute(" Y Length")]
        public int WaferMap_Y { get; set; } = 50;       
    }


    public enum ProductType
    {
        Tray,               // 낱장 트레이
        Tray_Magazine,      // 트레이 매거진    
        Wafer,              // 낱장 웨이퍼
        Wafer_Magazine,     // 웨이퍼 매거진
    
    }
    public class UserOption4 // 데이터 설정 ( 동일 프로젝트에서는 바뀔일이 없겠지? )
    {
        [CategoryAttribute("Temperature")]
        [DescriptionAttribute("COM Port")]
public string Temperature_COMPort { get; set; } = "COM1";

        [CategoryAttribute("Product")]
        [DescriptionAttribute("장비 구동 타입 설정 (재시작 필요)")]
        ProductType ProductType { get; set; } = ProductType.Wafer_Magazine;

        [CategoryAttribute("Product")]
        [DescriptionAttribute("Tray or Wafer의 X Y 사이즈")]
         int ProductX { get; set; } = 10;
        [CategoryAttribute("Product")]
        [DescriptionAttribute("Tray or Wafer의 X Y 사이즈")]
         int ProductY { get; set; } = 10;

        [CategoryAttribute("Product")]
        [DescriptionAttribute("Magazine의 slot수")]
         int MagazineSlot { get; set; } = 10;
        [CategoryAttribute("Product")]
        [DescriptionAttribute("Magazine의 갯수")]
         int MagazineSet { get; set; } = 1;             


        [CategoryAttribute("Sequence")]
        [DescriptionAttribute("Sequence TimeOut")]
         public int MaxSequenceTime { get; set; } = 1000 * 60;       

    }

    public class UserOptionUI
    {
        [CategoryAttribute("Type")]
        [DescriptionAttribute("Type")]
        public Type uiType { get; set; } // 버튼인지 텍스트박스인지 등
        public string value { get; set; } // 값

        public string name { get; set; } // 이름

        public T GetValue<T>()
        {
            if (value == null)
                return default(T); 

            try
            {             
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
              Log.Instance.Error($"UserOptionUI GetValue Error : {ex.Message}");
                return default(T);
            }
        }
    }
    #endregion
}
