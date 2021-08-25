using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SrmApi.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public string Name { get; set; }
    }
}
