using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsAndProps;
using ModelsAndProps.Historical;

namespace HistoricalComponent.DatabaseConn
{
    public class Database : DbContext
    {
        public Database() :base("ResConn")
        {

        }
        public DbSet<ListDescription> ListDescription1 { get; set; }
        public DbSet<ListDescription> ListDescription2 { get; set; }
        public DbSet<ListDescription> ListDescription3 { get; set; }
        public DbSet<ListDescription> ListDescription4 { get; set; }
        public DbSet<ListDescription> ListDescription5 { get; set; }
    }
}
