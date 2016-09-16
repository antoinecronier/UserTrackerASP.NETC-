using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UsersTracker.Models
{
    public class UsersTrackerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public UsersTrackerContext() : base("name=UsersTrackerContext")
        {
        }

        public System.Data.Entity.DbSet<UsersTracker.Entities.Product> Products { get; set; }

        public System.Data.Entity.DbSet<UsersTracker.Entities.Client> Clients { get; set; }

        public System.Data.Entity.DbSet<UsersTracker.Entities.Enseigne> Enseignes { get; set; }

        public System.Data.Entity.DbSet<UsersTracker.Entities.Compte> Comptes { get; set; }

        public System.Data.Entity.DbSet<UsersTracker.Entities.ClientProduct> ClientProducts { get; set; }

        public System.Data.Entity.DbSet<UsersTracker.Entities.ClientEnseigne> ClientEnseignes { get; set; }
    }
}
