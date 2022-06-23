using NoSearch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSearch.Data.Resources
{
    public class Tag : TagModel
    {
        public Tag(string name) : base(name) { }

        public Tag(string name, int id) : base(name) 
        {
            Id = id;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
