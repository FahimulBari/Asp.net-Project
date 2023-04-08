using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ZeroHunger.Models;

namespace ZeroHunger.Context
{
    public class MyDatabase : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Task> tasks { get; set; }
    }
}