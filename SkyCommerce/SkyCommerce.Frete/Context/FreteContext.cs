using Microsoft.EntityFrameworkCore;
using SkyCommerce.Fretes.Model;

namespace SkyCommerce.Fretes.Context
{
    public class FreteContext : DbContext
    {

        public FreteContext(DbContextOptions<FreteContext> options)
            : base(options)
        {
        }

        public DbSet<Frete> Fretes { get; set; }


        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureFreteContext();
        }
    }
}
