using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.HandleData
{
    public class DaDBContext : DbContext
    {
        public DaDBContext(DbContextOptions<DaDBContext> options) : base(options)
        {


        }
    }
}
