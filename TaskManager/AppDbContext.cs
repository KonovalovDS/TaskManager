using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace TaskManager {
    public class AppDbContext : DbContext {
        public DbSet<Entity> MyEntities { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }
    }
}
