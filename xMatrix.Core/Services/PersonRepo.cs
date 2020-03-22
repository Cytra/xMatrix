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
    public class PersonRepo : IPersonRepo
    {
        private const string _personListName = "Person";
        private readonly string _fileLocation;
        public PersonRepo()
        {
            _fileLocation = $"{AppDomain.CurrentDomain.BaseDirectory}/{_personListName}.json";
        }
        //public List<Person> GetAllPeople()
        //{
        //    var result = new List<Person>();

        //    if (File.Exists(_fileLocation))
        //    {
        //        var personString = File.ReadAllText(_fileLocation);
        //        result = JsonConvert.DeserializeObject<List<Person>>(personString);
        //        result = result.Where(x => x.Deleted == false).ToList();
        //    }

        //    return result;
        //}

        //public void SavePeople(List<Person> people)
        //{
        //    File.WriteAllText(_fileLocation, JsonConvert.SerializeObject(people));
        //}
    }
}
