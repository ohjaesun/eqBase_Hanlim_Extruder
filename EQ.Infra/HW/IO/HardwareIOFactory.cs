using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using EQ.Domain.Interface;

namespace EQ.Infra.HW.IO
{

    public static class HardwareIOFactory
    {
        public static class IoFactory
        {
            public static IIoController CreateIoController(string ioType)
            {
                string assemblyName = "";
                string className = "";

                // 1. 설정값에 따라 로드할 DLL과 클래스 이름을 결정
                switch (ioType)
                {
                    case "Ajin":
                        assemblyName = "EQ.Infra.Ajin.dll"; 
                        className = "EQ.Infra.Ajin.AjinIoController";
                        break;

                    default:
                    case "Simulation":                    
                    case "WMX":
                        assemblyName = "Hardware.Infra.IO.WMX.dll"; 
                        className = "Hardware.Infra.IO.WMX.WMX_IO";
                        break;                                         
                }

                try
                {
                    // 2. 런타임에 필요한 DLL만 동적으로 로드
                    string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName);
                    Assembly assembly = Assembly.LoadFrom(dllPath);

                    Type type = assembly.GetType(className);
                    if (type == null)
                        throw new Exception($"클래스를 찾을 수 없음: {className}");

                    // 3. 인스턴스 생성
                    object instance = Activator.CreateInstance(type);

                    return (IIoController)instance;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"'{ioType}' IO 하드웨어({assemblyName}) 로드 실패.", ex);
                }
            }
        }
    }
}
