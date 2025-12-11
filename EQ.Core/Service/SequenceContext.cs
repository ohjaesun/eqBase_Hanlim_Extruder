using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Core.Sequence
{
    public static class SequenceContext
    {
        public static AsyncLocal<string> CurrentSequenceId = new AsyncLocal<string>();
    }
}
