using NoSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSearch.Data.Resources
{
    public interface IResourceDataAccess
    {
        IEnumerable<TagModel> GetAllTags();
        IEnumerable<ResourceModel> GetAllResources();
        void AddResource(ResourceModel resource);
    }
}
