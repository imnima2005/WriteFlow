using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WriteFlow.DataLayer.Entities;

namespace WriteFlow.DataLayer.Context
{
    public class WriteFlowContext : DbContext
    {
        public WriteFlowContext(DbContextOptions<WriteFlowContext> options) : base(options){}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComment { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>().HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDelete);
        }
    }

    
}
