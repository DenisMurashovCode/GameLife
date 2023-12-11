using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    class cell
    {
        private int x { get; set; }
        private int y { get; set; }

        public cell():this(0,0){ }
        public cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void draw(Graphics g, int scale,int x1,int y1)
        {
            g.FillRectangle(Brushes.Green, x1 * scale,  y1 * scale, scale, scale);
        }
    }
}
