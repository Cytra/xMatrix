using System;
using System.Collections.Generic;
using System.Text;
using xMatrix.Core.Models;

namespace xMatrix.Core.Interfaces
{
    public interface IidService
    {
        int GetFreeId(List<Goal> goals);
    }
}
