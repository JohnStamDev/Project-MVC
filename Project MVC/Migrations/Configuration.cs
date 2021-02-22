namespace Project_MVC.Migrations
{
    using Project_MVC.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Project_MVC.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Project_MVC.DAL.ApplicationDbContext context)
        {
            var trainers = new List<Trainer>
           {
               new Trainer{ID= 1,FirstName="Panos",LastName="Bozas",Subject="C#"},
               new Trainer{ID= 2,FirstName="Freddie",LastName="Mercury",Subject="Java"},
               new Trainer{ID= 3,FirstName="Jimi",LastName="Hendrix",Subject="Javascript"},
               new Trainer{ID= 4,FirstName="James",LastName="Brown",Subject="PHP"},
               new Trainer{ID= 5,FirstName="Steven",LastName="Tyler",Subject="SQL"},
               new Trainer{ID= 6,FirstName="Evlis",LastName="Presley",Subject="Swift"},
               new Trainer{ID= 7,FirstName="Axl",LastName="Rose",Subject="Java"},
               new Trainer{ID= 8,FirstName="Jim",LastName="Morrison",Subject="Python"}

           };
            context.Trainers.AddOrUpdate(trainers.ToArray());
        }
    }
}
