using Microsoft.EntityFrameworkCore;
using ProductOrg.Models;
using System;
using System.IO;
using Xamarin.Forms;

namespace ProductOrg.Repositories
{
    public class ProductOrgContext : DbContext
    {
        #region Constants

        private const string databaseName = "Productorg.db";

        #endregion
        #region Properties
        
        public DbSet<PomodoroConfiguration> PomodoroConfigurations { get; set; }

        #endregion
        #region Constructs

        public ProductOrgContext()
        {
            this.Database.EnsureCreated();
        }

        #endregion
        #region Overrrite

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databasePath = "";
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
                    break;
                default:
                    throw new NotImplementedException("Platform not supported");
            }

            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }

        #endregion
    }
}
