using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaddenEditor.Core
{
    public class DraftReport
    {
        public int DraftSize { get; set; }
        public int HighestRated { get; set; }
        public int LowestRated { get; set; }
        public int Ovr80plus { get; set; }
        public int Ovr70to79 { get; set; }
        public int Ovr60to69 { get; set; }
        public int Ovr50to59 { get; set; }
        public int Ovr40to49 { get; set; }
        public int OvrSub40 { get; set; }
        public int XFactors { get; set; }
        public int Superstars { get; set; }
        public int Stars { get; set; }
        public int Normals { get; set; }
    }
}
