using Microsoft.EntityFrameworkCore;
using Smarty.Data.Models;

namespace Smarty.Data.SmartyDBContext
{
    public class SmartyDbContext : DbContext
    {
        public SmartyDbContext(DbContextOptions<SmartyDbContext> contextOptions) : base(contextOptions)
        {

        }
        
    }
}
