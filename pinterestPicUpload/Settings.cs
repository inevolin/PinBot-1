using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinBot
{
    [Serializable]
    public class Settings
    {
        public int TimeoutMin, TimeoutMax, Max;

        public Settings(int TimeoutMin, int TimeoutMax, int Max)
        {
            this.TimeoutMin = TimeoutMin;
            this.TimeoutMax = TimeoutMax;
            this.Max = Max;
        }
    }
}
