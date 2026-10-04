using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCore.Infrastructure.Database
{
    
        public interface IDatabaseStrategy
        {
            bool IsAvailable();
            void Configure(DbContextOptionsBuilder optionsBuilder);
        }
    
}
