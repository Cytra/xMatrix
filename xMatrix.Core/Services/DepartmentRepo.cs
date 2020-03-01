using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using xMatrix.Core.Interfaces;
using xMatrix.Core.Models;

namespace xMatrix.Core.Services
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private const string _departmentListName = "Department";
        private readonly string _fileLocation;
        public DepartmentRepo()
        {
            _fileLocation = $"{AppDomain.CurrentDomain.BaseDirectory}/{_departmentListName}.json";
        }
        public List<Department> GetAllDepartments()
        {
            var result = new List<Department>();
            if (File.Exists(_fileLocation))
            {
                var goalString = File.ReadAllText(_fileLocation);
                result = JsonConvert.DeserializeObject<List<Department>>(goalString);
                result = result.Where(x => x.Deleted == false).ToList();
            }
            return result;
        }

        public void SaveDepartments(List<Department> department)
        {
            File.WriteAllText(_fileLocation, JsonConvert.SerializeObject(department));
        }
    }
}
