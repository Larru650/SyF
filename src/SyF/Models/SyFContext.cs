using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SyF.Models
{
    public class SyFContext : DbContext
    {
        private IConfigurationRoot _config;

        public SyFContext(IConfigurationRoot config, DbContextOptions options ) : base(options) //we inject our configuration service so we can use the connection string
        {
            _config = config;
          

        }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["Data:SyFContextConnection"]);


        }

    }
}
