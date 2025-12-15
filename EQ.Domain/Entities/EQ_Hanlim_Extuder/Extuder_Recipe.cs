using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Domain.Entities.EQ_Hanlim_Extuder
{
    public class Extuder_Recipe
    {
        public string RecipeName { get; set; } = "Default";
        public double Zone1_Temp { get; set; } = 200.0;
        public double Zone2_Temp { get; set; } = 220.0;
        public double Extrude_Speed { get; set; } = 5.0;

       
    }
}
