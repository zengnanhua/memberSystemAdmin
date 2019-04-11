//using AdminSystem.Domain.AggregatesModel.UserAggregate;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AdminSystem.Api
//{
//    public class DDDContext : DbContext
//    {
//        public DbSet<ApplicationUser> dddddUser { get; set; }
//        public DDDContext(DbContextOptions<DDDContext> options) : base(options)
//        {

//        }
//        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        //{
//        //    optionsBuilder.UseMySql(
//        //        "server=www.zengnanhua.club;port=3306;user=nanhua;password=sa123; database=AdminSystem;");
//        //}
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//        }
//    }
//    public class DDDContextFactory : IDesignTimeDbContextFactory<DDDContext>
//    {
//        public DDDContext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<DDDContext>()
//                 .UseMySql("server=www.zengnanhua.club;port=3306;user=nanhua;password=sa123; database=AdminSystem;");

//            return new DDDContext(optionsBuilder.Options);
//        }
//    }
//}
