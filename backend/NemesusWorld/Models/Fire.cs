using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class Fire
    {
        public float xPos { get; set; }
        public float yPos { get; set; }
        public float zPos { get; set; }
        public int maxChilderen { get; set; }
        public bool gasPowerd { get; set; }

        public Fire() { }

        public Fire(float xPos, float yPos, float zPos, int maxChilderen, bool gasPowerd)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.zPos = zPos;
            this.maxChilderen = maxChilderen;
            this.gasPowerd = gasPowerd;
        }
    }
}
