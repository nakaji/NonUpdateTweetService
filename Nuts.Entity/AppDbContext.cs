﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nuts.Entity.Migrations;

namespace Nuts.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("NutsConnection") { }

        public virtual IDbSet<User> Users { get; set; }
    }


}
