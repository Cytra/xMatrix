using System;
using System.Collections.Generic;
using System.Text;
using xMatrix.Core.Interfaces;

namespace xMatrix.Core.Services
{
    public class MatrixGridService : IMatrixGridService
    {
        private const double _squareWidth = 20;
        private const double _rectWidth = 200;
        private const double _rectHeight = 20;

        public double GetRectheight()
        {
            return _rectHeight;
        }

        public double GetRectWidth()
        {
            return _rectWidth;
        }

        public double GetSquareWidth()
        {
            return _squareWidth;
        }
    }
}
