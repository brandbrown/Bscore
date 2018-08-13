using System;
using System.Collections.Generic;
using System.Text;

namespace Bscore.Models
{
    public class Frame
    {
        public Frame(int number) {
            this.Number = number;
        }

        public int Number { get; set; }
        public List<Roll> Rolls { get; set; } = new List<Roll>();
        public int? Score { get; set; } = null;
    }
}