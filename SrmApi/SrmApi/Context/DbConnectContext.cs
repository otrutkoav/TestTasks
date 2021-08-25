using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SrmApi.Context
{
    public class DbConnectContext : DbContext
    {
        public DbSet<SrmApi.Models.TaskEntity> Tasks { get; set; }

        public DbConnectContext(DbContextOptions<DbConnectContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
