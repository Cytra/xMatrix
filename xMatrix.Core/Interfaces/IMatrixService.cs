using System;
using System.Collections.Generic;
using System.Text;
using xMatrix.Core.Models;

namespace xMatrix.Core.Interfaces
{
    public interface IMatrixService
    {
        List<Polygon> GeneratePolygonList(List<Goal> goals);
        List<RectItem> GenerateRectList(List<Goal> goals, List<Department> departments, List<Person> people, Department department);
    }
}
