using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    class CellAngry
    {
        private int x { get; set; }
        private int y { get; set; }

        public CellAngry() : this(0, 0) { }
        public CellAngry(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void draw(Graphics g, int scale, int x1, int y1)
        {
            g.FillRectangle(Brushes.Red, x1 * scale, y1 * scale, scale, scale);
        }
    }
}
