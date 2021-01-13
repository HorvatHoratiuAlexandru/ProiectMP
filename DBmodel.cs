namespace ContactApp
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBmodel : DbContext
    {
        public DBmodel()
            : base("name=DBmodel")
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Contact>()
                .Property(e => e.LastName)
                .IsFixedLength();

            modelBuilder.Entity<Contact>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<Contact>()
                .Property(e => e.Mail)
                .IsFixedLength();
        }
    }
}
