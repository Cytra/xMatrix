using System;
using System.Collections.Generic;
using System.Text;
using xMatrix.Core.Models;

namespace xMatrix.Core.Interfaces
{
    public interface IidService
    {
        int GetFreeId(List<Goal> goals);
        //int GetFreeId(List<Person> people);
        int GetFreeId(List<Department> people);
    }
}
