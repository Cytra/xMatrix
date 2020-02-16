using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xMatrix.Core.Interfaces;
using xMatrix.Core.Models;

namespace xMatrix.Core.Services
{
    public class MatrixService : IMatrixService
    {
        private List<RectItem> RectItems = new List<RectItem>();
        public List<Polygon> Polygons { get; set; } = new List<Polygon>();
        private double _squareWidth;
        private double _rectWidth;
        private double _rectheight;
        private readonly IMatrixGridService _matrixGridService;

        private string _bottomGoalType;
        private string _leftGoalType;
        private string _topGoalType;
        private string _rightGoalType;

        public MatrixService(
            IMatrixGridService matrixGridService,
            string bottomGoalType,
            string leftGoalType,
            string topGoalType,
            string rightGoalType
            )
        {
            _bottomGoalType = bottomGoalType;
            _leftGoalType = leftGoalType;
            _topGoalType = topGoalType;
            _rightGoalType = rightGoalType;
            _matrixGridService = matrixGridService;
            _squareWidth = matrixGridService.GetSquareWidth();
            _rectWidth = matrixGridService.GetRectWidth();
            _rectheight = matrixGridService.GetRectheight();
        }

        public List<Polygon> GeneratePolygonList(List<Goal> goals)
        {
            ClearLists();
            GenerateMid(goals);
            return Polygons;
        }

        public List<RectItem> GenerateRectList(List<Goal> goals)
        {
            ClearLists();
            GenerateTopLeftRects(goals);
            GenerateTopMidRects(goals);
            GenerateTopRightRects(goals);
            GenerateMidLeftRects(goals);
            GenerateMidRightRects(goals);
            GenerateBottomLeftRects(goals);
            GenerateBottomMidRects(goals);
            GenerateBottomRightRects(goals);
            GenerateMidRects(goals, GoalType.ShortTerm, GoalType.LongTerm, GoalType.OneYear, GoalType.Monthly);
            return RectItems;
        }


        private void ClearLists()
        {
            RectItems = new List<RectItem>();
            Polygons = new List<Polygon>();
        }

        private void GenerateTopLeftRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == _topGoalType);
            var oneYearGoals = goals.Where(x => x.GoalType == _leftGoalType);

            double xLoc = 0;
            foreach (var oneYearGoal in oneYearGoals)
            {
                double yLoc = 0;
                foreach (var shortTermGoal in shortTermGoals)
                {
                    var relates = oneYearGoal.Relates.Contains(shortTermGoal.Id);
                    RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = _rectheight, Width = _rectheight, Text = relates ? "O" : "" });
                    yLoc += _squareWidth;
                }
                xLoc += _squareWidth;
            }
        }

        private void GenerateTopMidRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == _topGoalType);
            var oneYearGoals = goals.Where(x => x.GoalType == _leftGoalType);

            double xLoc = (double)oneYearGoals.ToList().Count * _squareWidth;
            double yLoc = 0;
            foreach (var shortTermGoal in shortTermGoals)
            {
                RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = _rectheight, Width = _rectWidth, Text = shortTermGoal.Name });
                yLoc += _squareWidth;
            }
        }

        private void GenerateTopRightRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == _topGoalType);
            var oneYearGoals = goals.Where(x => x.GoalType == _leftGoalType);
            var monthlyGoals = goals.Where(x => x.GoalType == _rightGoalType);

            double xLoc = (double)oneYearGoals.ToList().Count * _squareWidth + _rectWidth;
            foreach (var monthlyGoal in monthlyGoals)
            {
                double yLoc = 0;
                foreach (var shortTermGoal in shortTermGoals)
                {
                    var relates = shortTermGoal.Relates.Contains(monthlyGoal.Id);
                    RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = _rectheight, Width = _rectheight, Text = relates ? "O" : "" });
                    yLoc += _squareWidth;
                }
                xLoc += _squareWidth;
            }
        }

        private void GenerateMidLeftRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == _topGoalType);
            var oneYearGoals = goals.Where(x => x.GoalType == _leftGoalType);

            double xLoc = 0;
            double yLoc = (double)shortTermGoals.ToList().Count * _squareWidth;

            foreach (var oneYearGoal in oneYearGoals)
            {
                RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = _rectWidth, Width = _rectheight, Text = oneYearGoal.Name, Rotate = 270 });
                xLoc += _squareWidth;
            }
        }

        private void GenerateMidRightRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == _topGoalType);
            var oneYearGoals = goals.Where(x => x.GoalType == _leftGoalType);
            var monthlyGoals = goals.Where(x => x.GoalType == _rightGoalType);

            double xLoc = (double)oneYearGoals.ToList().Count * _squareWidth + _rectWidth;
            double yLoc = (double)shortTermGoals.ToList().Count * _squareWidth;

            foreach (var monthlyGoal in monthlyGoals)
            {
                RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = _rectWidth, Width = _rectheight, Text = monthlyGoal.Name, Rotate = 270 });
                xLoc += _squareWidth;
            }
        }

        private void GenerateBottomLeftRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == _topGoalType);
            var oneYearGoals = goals.Where(x => x.GoalType == _leftGoalType);
            var longTermGoals = goals.Where(x => x.GoalType == _bottomGoalType);

            double xLoc = 0;
            foreach (var oneYearGoal in oneYearGoals)
            {
                double yLoc = shortTermGoals.ToList().Count * _squareWidth + _rectWidth;
                foreach (var longTermGoal in longTermGoals)
                {
                    var relates = longTermGoal.Relates.Contains(oneYearGoal.Id);
                    RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = _rectheight, Width = _rectheight, Text = relates ? "O" : "" });
                    yLoc += _squareWidth;
                }
                xLoc += _squareWidth;
            }
        }

        private void GenerateBottomMidRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == _topGoalType);
            var oneYearGoals = goals.Where(x => x.GoalType == _leftGoalType);
            var monthlyGoals = goals.Where(x => x.GoalType == _rightGoalType);
            var longTermGoals = goals.Where(x => x.GoalType == _bottomGoalType);

            double xLoc = (double)oneYearGoals.ToList().Count * _squareWidth;
            double yLoc = (double)shortTermGoals.ToList().Count * _squareWidth + _rectWidth;

            foreach (var longTermGoal in longTermGoals)
            {
                RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = _rectheight, Width = _rectWidth, Text = longTermGoal.Name });
                yLoc += _squareWidth;
            }
        }

        private void GenerateBottomRightRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == _topGoalType);
            var oneYearGoals = goals.Where(x => x.GoalType == _leftGoalType);
            var monthlyGoals = goals.Where(x => x.GoalType == _rightGoalType);
            var longTermGoals = goals.Where(x => x.GoalType == _bottomGoalType);

            double xLoc = (double)oneYearGoals.ToList().Count * _squareWidth + _rectWidth;
            double yLoc = (double)shortTermGoals.ToList().Count * _squareWidth + _rectWidth;

            RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = longTermGoals.ToList().Count * _rectheight, Width = monthlyGoals.ToList().Count * _rectheight });
        }

        private void GenerateMid(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == _topGoalType);
            var oneYearGoals = goals.Where(x => x.GoalType == _leftGoalType);
            var monthlyGoals = goals.Where(x => x.GoalType == _rightGoalType);
            var longTermGoals = goals.Where(x => x.GoalType == _bottomGoalType);

            double xLoc = (double)oneYearGoals.ToList().Count * _squareWidth;
            double yLoc = (double)shortTermGoals.ToList().Count * _squareWidth;

            Polygons.Add(new Polygon() { Points = $"{xLoc},{yLoc} {xLoc},{yLoc + _rectWidth}, {xLoc + _rectWidth / 2},{yLoc + _rectWidth / 2}" });
            Polygons.Add(new Polygon() { Points = $"{xLoc},{yLoc} {xLoc + _rectWidth},{yLoc}, {xLoc + _rectWidth / 2},{yLoc + _rectWidth / 2}" });
            Polygons.Add(new Polygon() { Points = $"{xLoc + _rectWidth},{yLoc} {xLoc + _rectWidth},{yLoc + _rectWidth}, {xLoc + _rectWidth / 2},{yLoc + _rectWidth / 2}" });
            Polygons.Add(new Polygon() { Points = $"{xLoc},{yLoc + _rectWidth} {xLoc + _rectWidth},{yLoc + _rectWidth}, {xLoc + _rectWidth / 2},{yLoc + _rectWidth / 2}" });
        }

        private void GenerateMidRects(List<Goal> goals, string topGoal, string bottomGoal, string leftGoal, string rightGoal)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == _topGoalType);
            var oneYearGoals = goals.Where(x => x.GoalType == _leftGoalType);
            var monthlyGoals = goals.Where(x => x.GoalType == _rightGoalType);
            var longTermGoals = goals.Where(x => x.GoalType == _bottomGoalType);

            double xLoc = (double)oneYearGoals.ToList().Count * _squareWidth;
            double yLoc = (double)shortTermGoals.ToList().Count * _squareWidth;
            RectItems.Add(new RectItem() { X = xLoc, Y = yLoc + _rectheight, Height = _rectheight, Width = _rectWidth, Text = topGoal, Stroke = "" });
            RectItems.Add(new RectItem() { X = xLoc, Y = yLoc + _rectWidth - _rectheight * 2, Height = _rectheight, Width = _rectWidth, Text = bottomGoal, Stroke = "" });
            RectItems.Add(new RectItem() { X = xLoc, Y = yLoc + _rectWidth / 2 - _rectheight / 2, Height = _rectheight, Width = _rectWidth / 2, Text = leftGoal, Stroke = "" });
            RectItems.Add(new RectItem() { X = xLoc + _rectWidth / 2, Y = yLoc + _rectWidth / 2 - _rectheight / 2, Height = _rectheight, Width = _rectWidth / 2, Text = rightGoal, Stroke = "" });
        }
    }
}
