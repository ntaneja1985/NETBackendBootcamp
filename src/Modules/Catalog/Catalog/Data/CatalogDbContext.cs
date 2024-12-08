namespace Catalog.Data
{
    public class CatalogDbContext: DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options):base(options)
        {
            
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            //For Data Isolation, we use separate schema model, so we define a schema here
            builder.HasDefaultSchema("catalog");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
