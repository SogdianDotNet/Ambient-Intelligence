using Microsoft.AspNet.Identity.EntityFramework;
using SD.Data.Entities;
using SD.Data.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Data
{
    public partial class SmartEntities : DbContext
    {
        public SmartEntities()
            : base("name=database2")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<StudentsAttention> StudentsAttention { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<MediaMetaData> MediaMetaData { get; set; }
        public DbSet<Klas> Klas { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<LogType> LogType { get; set; }
        public DbSet<LogTypeSub> LogTypeSub { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<ClientFileInstallation> ClientFileInstallation { get; set; }
        public DbSet<FileDownload> FileDownload { get; set; }
        public DbSet<EncryptedUserData> EncryptedUserData { get; set; }
        public DbSet<LogClient> LogClient { get; set; }
    }
}
