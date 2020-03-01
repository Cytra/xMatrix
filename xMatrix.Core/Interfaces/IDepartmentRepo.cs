using System;
using System.Collections.Generic;
using System.Text;
using xMatrix.Core.Models;

namespace xMatrix.Core.Interfaces
{
    public interface IDepartmentRepo
    {
        List<Department> GetAllDepartments();
        void SaveDepartments(List<Department> department);
    }
}
