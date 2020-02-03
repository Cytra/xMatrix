using System;
using System.Collections.Generic;
using System.Text;

namespace xMatrix.Core.Models
{
    public class RectItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Text { get; set; }
        public int Rotate { get; set; } = 0;
    }
}
