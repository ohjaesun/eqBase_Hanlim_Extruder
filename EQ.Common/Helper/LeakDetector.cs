using EQ.Common.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EQ.Common.Helper
{
    public static class LeakDetector
    {
        private static readonly List<(WeakReference Ref, string Name, DateTime RegisterTime)> _trackingList = new();

        // 등록: 폼/컨트롤이 닫힐(Dispose) 때 호출
        public static void Register(object target, string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = target.GetType().Name;
            }

            lock (_trackingList)
            {
                _trackingList.Add((new WeakReference(target), name, DateTime.Now));
            }
            
            Task.Run(async () =>
            {              
                await Task.Delay(1000 * 60 * 10); // 10분 후 검사 - GC동작에 따라 가성 불량 발생 가능함.
                CheckLeaks(forceGc: false); // 테스트외 true 하지 말것
            });
        }

        public static void CheckLeaks(bool forceGc)
        {
            if (forceGc)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

            lock (_trackingList)
            {
                for (int i = _trackingList.Count - 1; i >= 0; i--)
                {
                    var item = _trackingList[i];

                    if (!item.Ref.IsAlive)
                    {                       
                        _trackingList.RemoveAt(i);
                    }
                    else
                    {                       
                        if (!forceGc && (DateTime.Now - item.RegisterTime).TotalMinutes < 10)
                            continue;
                       
                        string logPrefix = forceGc ? "[Leak-Confirmed]" : "[Leak-Suspected]";
                        Log.Instance.Warning($"{item.Name} 객체가 {DateTime.Now - item.RegisterTime} 경과 후에도 메모리에 남아있음 ");
                    }
                }
            }
        }
    }
}
