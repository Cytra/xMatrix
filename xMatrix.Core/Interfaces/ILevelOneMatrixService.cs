using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using xMatrix.Core.Models;

namespace xMatrix.Core.Interfaces
{
    public interface ILevelMatrixService
    {
        List<Polygon> GeneratePolygonList(List<Goal> goals, Department department);
        List<RectItem> GenerateRectList(List<Goal> goals, List<Department> departments, Department department);
    }
}
