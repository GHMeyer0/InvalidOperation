using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvalidOperation.Api.Models;
using Action = InvalidOperation.Api.Models.Action;

namespace InvalidOperation.Api.Data
{
    public class InvalidOperationApiContext : DbContext
    {
        public InvalidOperationApiContext(DbContextOptions<InvalidOperationApiContext> options)
            : base(options)
        {
        }

        public DbSet<ActionRule> ActionRule { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionRule>()
              .OwnsOne(ruler => ruler.Parameters, ownedNavigationBuilder =>
              {
                  ownedNavigationBuilder.ToJson();
                  ownedNavigationBuilder.OwnsMany(
                        paramters => paramters.Actions, ownedOwnedNavigationBuilder =>
                          {
                              ownedOwnedNavigationBuilder.OwnsOne(action => action.EMailParameter);
                              ownedOwnedNavigationBuilder.OwnsOne(action => action.SMSParameter);
                          });
              });
        }
    }
}
