using System;
using System.Collections.Generic;
using System.Text;
using Dockka.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Dockka.Data.Context
{
    public class DockkaContext:DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public DockkaContext(DbContextOptions<DockkaContext> options) : base(options)
        {
        }
    }
}
