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

        public List<Polygon> GeneratePolygonList(List<Goal> goals, Department department)
        {
            goals = FilterGoals(goals, department);
            return _matrixService.GeneratePolygonList(goals);
        }

        public List<RectItem> GenerateRectList(List<Goal> goals, List<Department> departments, List<Person> people, Department department)
        {
            goals = FilterGoals(goals, department);
            return _matrixService.GenerateRectList(goals, departments, people, department);
        }

        private List<Goal> FilterGoals(List<Goal> goals, Department selectedDepartment)
        {
            var result = new List<Goal>();
            if(selectedDepartment != null)
            {
                var shortRelatedDeps = goals.Where(x =>
                x.RelatedDepartments.Contains(selectedDepartment.Id)
                && x.GoalType == GoalType.ShortTerm).ToList();
                shortRelatedDeps.ForEach(x => result.Add(x));

                var oneYear = new List<Goal>();
                foreach (var shortTerm in shortRelatedDeps)
                {
                    oneYear = goals.Where(x => x.RelatesGoals.Contains(shortTerm.Id)).ToList();
                }
                oneYear.ForEach(x => result.Add(x));

                var longterm = new List<Goal>();
                foreach (var oneyear in oneYear)
                {
                    longterm = goals.Where(x => x.RelatesGoals.Contains(oneyear.Id)).ToList();
                }
                longterm.ForEach(x => result.Add(x));



                var initiativesOne = new List<Goal>();
                foreach (var one in shortRelatedDeps)
                {
                    foreach(var relatedOne in one.RelatesGoals)
                    {
                        var goal = goals.SingleOrDefault(x => x.Id == relatedOne);
                        if(goal != null)
                        {
                            initiativesOne.Add(goal);
                        }
                    }
                }
                initiativesOne.ForEach(x => result.Add(x));

                var initiativesTwo = new List<Goal>();
                foreach (var initiative in initiativesOne)
                {
                    foreach (var relatedtwo in initiative.RelatesGoals)
                    {
                        var goal = goals.SingleOrDefault(x => x.Id == relatedtwo);
                        if (goal != null)
                        {
                            initiativesTwo.Add(goal);
                        }
                    }
                }
                initiativesTwo.ForEach(x => result.Add(x));

                var initiativesThree = new List<Goal>();
                foreach (var three in initiativesTwo)
                {
                    foreach (var relatedthree in three.RelatesGoals)
                    {
                        var goal = goals.SingleOrDefault(x => x.Id == relatedthree);
                        if (goal != null)
                        {
                            initiativesThree.Add(goal);
                        }
                    }
                }
                initiativesThree.ForEach(x => result.Add(x));

            } else
            {
                return goals;
            }

            //public const string LongTerm = "LongTerm";
            //public const string OneYear = "OneYear";
            //public const string InitiativesOne = "Initiatives One";
            //public const string InitiativesTwo = "Initiatives Two";
            //public const string InitiativesThree = "Initiatives Three";
            return result;
        }
    }
}
