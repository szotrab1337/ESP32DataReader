using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP32DataReader.Model
{
    internal class Context : GenericContext
    {
        public DbSet<Reading> Readings { get; set; }

        public Context() : base("ESP32")
        {

        }
    }
}
