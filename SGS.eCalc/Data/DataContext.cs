using Microsoft.EntityFrameworkCore;
using SGS.eCalc.Models;

namespace SGS.eCalc.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<CalculationVersion> CalculationVersions { get; set; }
    }
}