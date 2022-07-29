using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringBuilder1
{
    public struct Point //điểm
    {
        public float x { get; set; }

        public float y { get; set; }

        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public string GetString()
        {
            return x.ToString() + y.ToString();
        }
    }
}
