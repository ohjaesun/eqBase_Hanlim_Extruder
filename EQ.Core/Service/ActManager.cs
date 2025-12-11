using EQ.Core.Act;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Core.Service
{
    public class ActManager
    {
        
        private static readonly Lazy<ActManager> lazyInstance =
            new Lazy<ActManager>(() => new ActManager());

       
        public static ActManager Instance => lazyInstance.Value;

      
        public ACT Act { get; private set; }

      
        private ActManager()
        {
            Act = new ACT();           
        }

    }
}
