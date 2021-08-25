using SrmApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SrmApi.Models
{
    public class TaskEntity : BaseEntity
    {
        public string Description { get; set; }
        public int Status {get;set;}
    }
}
