using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsAndProps;
using ModelsAndProps.Historical;

namespace Cache_Memory.Database
{
    public class Database : DbContext
    {
        public Database() :base("ResConn")
        {

        }
        public DbSet<ListDescription> ListDescriptions { get; set; }
    }
}
