using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP32DataReader.Model
{
    internal class GenericContext : DbContext
    {
        public GenericContext(string ConnStr) : base (ConnStr)
        {
            Database.CommandTimeout = 10;
        }
    }
}
