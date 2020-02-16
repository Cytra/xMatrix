using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using xMatrix.Core.Interfaces;
using xMatrix.Core.Models;

namespace xMatrix.Core.Services
{
    public class LevelMatrixService : ILevelMatrixService
    {
        private List<RectItem> RectItems = new List<RectItem>();
        public List<Polygon> Polygons { get; set; } = new List<Polygon>();
        private double _squareWidth;
        private double _rectWidth;
        private double _rectheight;
        private readonly IMatrixService _matrixService;
        private readonly IMatrixGridService _matrixGridService;

        public LevelMatrixService(
            IMatrixService matrixService,
            IMatrixGridService matrixGridService
            )
        {
            _matrixService = matrixService;
            _matrixGridService = matrixGridService;
            _squareWidth = _matrixGridService.GetSquareWidth();
            _rectWidth = _matrixGridService.GetRectWidth();
            _rectheight = _matrixGridService.GetRectheight();
        }

        public List<Polygon> GeneratePolygonList(List<Goal> goals)
        {
            return _matrixService.GeneratePolygonList(goals);
        }

        public List<RectItem> GenerateRectList(List<Goal> goals, List<Person> people)
        {
            return _matrixService.GenerateRectList(goals, people);
        }
    }
}
