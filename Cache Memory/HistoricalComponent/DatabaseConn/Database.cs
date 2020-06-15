using ModelsAndProps.Historical;
using System.Data.Entity;

namespace HistoricalComponent.DatabaseConn
{
    public class Database : DbContext
    {
        public Database() : base("ResConn")
        {

        }
        public DbSet<ListDescription> ListDescriptions { get; set; }

        public DbSet<HistoricalDescription> HistoricalDescriptions { get; set; }
        public DbSet<HistoricalProperty> HistoricalProperties { get; set; }

    }
}
