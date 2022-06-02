using NoSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSearch.Data
{
    public interface IResourceDataAccess
    {
        IEnumerable<Resource> GetAllResources();
        void AddResource(Resource resource);
    }
}
