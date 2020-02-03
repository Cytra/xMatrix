﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xMatrix.Core.Interfaces;
using xMatrix.Core.Models;

namespace xMatrix.ViewModels
{
    public class LevelOneMatrixViewModel
    {
        private readonly IGoalRepo _repo;
        private const double SquareWidth = 20;
        private const double RectWidth = 200;
        private const double rectheight = 20;
        public ObservableCollection<RectItem> RectItems { get; set; } = new ObservableCollection<RectItem>();
        public ObservableCollection<Polygon> Polygons { get; set; } = new ObservableCollection<Polygon>();
        public List<Goal> GoalList { get; set; } = new List<Goal>();

        public LevelOneMatrixViewModel(IGoalRepo repo)
        {
            _repo = repo;
            GoalList = _repo.GetAllGoals();
            GenerateTopLeftRects(GoalList);
            GenerateTopMidRects(GoalList);
            GenerateTopRightRects(GoalList);
            GenerateMidLeftRects(GoalList);
            GenerateMidRightRects(GoalList);
            GenerateBottomLeftRects(GoalList);
            GenerateBottomMidRects(GoalList);
            GenerateBottomRightRects(GoalList);
            GenerateMid(GoalList);
        }

        private void GenerateTopLeftRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == GoalType.ShortTerm);
            var oneYearGoals = goals.Where(x => x.GoalType == GoalType.OneYear);

            double xLoc = 0;
            foreach (var oneYearGoal in oneYearGoals)
            {
                double yLoc = 0;
                foreach (var shortTermGoal in shortTermGoals)
                {
                    var relates = oneYearGoal.Relates.Contains(shortTermGoal.Id);
                    RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = rectheight, Width = rectheight, Text = relates ? "O" : "" });
                    yLoc += SquareWidth;
                }
                xLoc += SquareWidth;
            }
        }

        private void GenerateTopMidRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == GoalType.ShortTerm);
            var oneYearGoals = goals.Where(x => x.GoalType == GoalType.OneYear);

            double xLoc = (double)oneYearGoals.ToList().Count * SquareWidth;
            double yLoc = 0;
            foreach (var shortTermGoal in shortTermGoals)
            {
                RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = rectheight, Width = RectWidth, Text = shortTermGoal.Id.ToString() + shortTermGoal.Name });
                yLoc += SquareWidth;
            }
        }

        private void GenerateTopRightRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == GoalType.ShortTerm);
            var oneYearGoals = goals.Where(x => x.GoalType == GoalType.OneYear);
            var monthlyGoals = goals.Where(x => x.GoalType == GoalType.Monthly);

            double xLoc = (double)oneYearGoals.ToList().Count * SquareWidth + RectWidth;
            foreach (var monthlyGoal in monthlyGoals)
            {
                double yLoc = 0;
                foreach (var shortTermGoal in shortTermGoals)
                {
                    var relates = shortTermGoal.Relates.Contains(monthlyGoal.Id);
                    RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = rectheight, Width = rectheight, Text = relates ? "O" : "" });
                    yLoc += SquareWidth;
                }
                xLoc += SquareWidth;
            }
        }

        private void GenerateMidLeftRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == GoalType.ShortTerm);
            var oneYearGoals = goals.Where(x => x.GoalType == GoalType.OneYear);

            double xLoc = 0;
            double yLoc = (double)shortTermGoals.ToList().Count * SquareWidth;

            foreach (var oneYearGoal in oneYearGoals)
            {
                RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = RectWidth, Width = rectheight, Text = oneYearGoal.Id.ToString() + oneYearGoal.Name, Rotate = 270 });
                xLoc += SquareWidth;
            }
        }

        private void GenerateMidRightRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == GoalType.ShortTerm);
            var oneYearGoals = goals.Where(x => x.GoalType == GoalType.OneYear);
            var monthlyGoals = goals.Where(x => x.GoalType == GoalType.Monthly);

            double xLoc = (double)oneYearGoals.ToList().Count * SquareWidth + RectWidth;
            double yLoc = (double)shortTermGoals.ToList().Count * SquareWidth;

            foreach (var monthlyGoal in monthlyGoals)
            {
                RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = RectWidth, Width = rectheight, Text = monthlyGoal.Id.ToString() + monthlyGoal.Name, Rotate = 270 });
                xLoc += SquareWidth;
            }
        }

        private void GenerateBottomLeftRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == GoalType.ShortTerm);
            var oneYearGoals = goals.Where(x => x.GoalType == GoalType.OneYear);
            var longTermGoals = goals.Where(x => x.GoalType == GoalType.LongTerm);

            double xLoc = 0;
            foreach (var oneYearGoal in oneYearGoals)
            {
                double yLoc = shortTermGoals.ToList().Count * SquareWidth + RectWidth;
                foreach (var longTermGoal in longTermGoals)
                {
                    var relates = longTermGoal.Relates.Contains(oneYearGoal.Id);
                    RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = rectheight, Width = rectheight, Text = relates ? "O" : "" });
                    yLoc += SquareWidth;
                }
                xLoc += SquareWidth;
            }
        }

        private void GenerateBottomMidRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == GoalType.ShortTerm);
            var oneYearGoals = goals.Where(x => x.GoalType == GoalType.OneYear);
            var monthlyGoals = goals.Where(x => x.GoalType == GoalType.Monthly);
            var longTermGoals = goals.Where(x => x.GoalType == GoalType.LongTerm);

            double xLoc = (double)oneYearGoals.ToList().Count * SquareWidth;
            double yLoc = (double)shortTermGoals.ToList().Count * SquareWidth + RectWidth;

            foreach (var longTermGoal in longTermGoals)
            {
                RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = rectheight, Width = RectWidth, Text = longTermGoal.Id.ToString() + longTermGoal.Name });
                yLoc += SquareWidth;
            }
        }

        private void GenerateBottomRightRects(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == GoalType.ShortTerm);
            var oneYearGoals = goals.Where(x => x.GoalType == GoalType.OneYear);
            var monthlyGoals = goals.Where(x => x.GoalType == GoalType.Monthly);
            var longTermGoals = goals.Where(x => x.GoalType == GoalType.LongTerm);

            double xLoc = (double)oneYearGoals.ToList().Count * SquareWidth + RectWidth;
            double yLoc = (double)shortTermGoals.ToList().Count * SquareWidth + RectWidth;

            RectItems.Add(new RectItem() { X = xLoc, Y = yLoc, Height = longTermGoals.ToList().Count * rectheight, Width = monthlyGoals.ToList().Count * rectheight });
        }

        private void GenerateMid(List<Goal> goals)
        {
            var shortTermGoals = goals.Where(x => x.GoalType == GoalType.ShortTerm);
            var oneYearGoals = goals.Where(x => x.GoalType == GoalType.OneYear);
            var monthlyGoals = goals.Where(x => x.GoalType == GoalType.Monthly);
            var longTermGoals = goals.Where(x => x.GoalType == GoalType.LongTerm);

            double xLoc = (double)oneYearGoals.ToList().Count * SquareWidth;
            double yLoc = (double)shortTermGoals.ToList().Count * SquareWidth;

            Polygons.Add(new Polygon() { Points = $"{xLoc},{yLoc} {xLoc},{yLoc + RectWidth}, {xLoc + RectWidth / 2},{yLoc + RectWidth / 2}" });
            Polygons.Add(new Polygon() { Points = $"{xLoc},{yLoc} {xLoc + RectWidth},{yLoc}, {xLoc + RectWidth / 2},{yLoc + RectWidth / 2}" });
            Polygons.Add(new Polygon() { Points = $"{xLoc + RectWidth},{yLoc} {xLoc + RectWidth},{yLoc + RectWidth}, {xLoc + RectWidth / 2},{yLoc + RectWidth / 2}" });
            Polygons.Add(new Polygon() { Points = $"{xLoc},{yLoc + RectWidth} {xLoc + RectWidth},{yLoc + RectWidth}, {xLoc + RectWidth / 2},{yLoc + RectWidth / 2}" });
        }
    }
}
