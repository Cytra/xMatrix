using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xMatrix.Core.Interfaces;
using xMatrix.Core.Models;

namespace xMatrix.Core.Services
{
    public class IdService : IidService
    {

        public IdService()
        {

        }

        public int GetFreeId(List<Goal> goals)
        {
            var result = 0;
            if(goals.Count > 0)
            {
                result = goals.Max(x => x.Id);
            }
            return result + 1;
        }

        public int GetFreeId(List<Person> people)
        {
            var result = 0;
            if (people.Count > 0)
            {
                result = people.Max(x => x.Id);
            }
            return result + 1;
        }

        public int GetFreeId(List<Department> departments)
        {
            var result = 0;
            if (departments.Count > 0)
            {
                result = departments.Max(x => x.Id);
            }
            return result + 1;
        }
    }
}
