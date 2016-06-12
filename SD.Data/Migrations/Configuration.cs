namespace SD.Data.Migrations
{
    using Commons.Shared;
    using Entities.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SD.Data.SmartEntities>
    {
        //SD.Commons.Shared.ApplicationDbContext
        //SD.Data.SmartEntities
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SD.Data.SmartEntities context)
        {
            
        }
    }
}
