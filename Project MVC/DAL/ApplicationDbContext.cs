using Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_MVC.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("ProjectDb")
        {

        }
        public DbSet<Trainer> Trainers { get; set; }
    }
}