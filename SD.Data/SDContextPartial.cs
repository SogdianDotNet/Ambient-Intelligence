using SD.Repositories.Repositories;
using SD.Data.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SD.Data
{
    public partial class SDContext
    {
        private GenericRepository<Log> log;
        private GenericRepository<Teacher> teacher;
        private GenericRepository<Student> student;
        private GenericRepository<Administrator> administrator;
        private GenericRepository<Session> session;
        private GenericRepository<StudentsAttention> studentsAttention;
        private GenericRepository<Course> course;
        private GenericRepository<MediaMetaData> mediaMetaData;
        private GenericRepository<Klas> klas;
        private GenericRepository<Report> report;
        private GenericRepository<ClientFileInstallation> clientFileInstallation;
        private GenericRepository<FileDownload> fileDownload;
        private GenericRepository<EncryptedUserData> encryptedUserData;

        public GenericRepository<Report> Report
        {
            get
            {
                if (report == null)
                {
                    report = new GenericRepository<Report>(sdContext);
                }
                return report;
            }
        }

        public GenericRepository<EncryptedUserData> EncryptedUserData
        {
            get
            {
                if (encryptedUserData == null)
                {
                    encryptedUserData = new GenericRepository<EncryptedUserData>(sdContext);
                }
                return encryptedUserData;
            }
        }

        public GenericRepository<ClientFileInstallation> ClientFileInstallation
        {
            get
            {
                if (clientFileInstallation == null)
                {
                    clientFileInstallation = new GenericRepository<ClientFileInstallation>(sdContext);
                }
                return clientFileInstallation;
            }
        }

        public GenericRepository<FileDownload> FileDownload
        {
            get
            {
                if (fileDownload == null)
                {
                    fileDownload = new GenericRepository<FileDownload>(sdContext);
                }
                return fileDownload;
            }
        }
        

        public GenericRepository<Klas> Klas
        {
            get
            {
                if (klas == null)
                {
                    klas = new GenericRepository<Klas>(sdContext);
                }
                return klas;
            }
        }

        public GenericRepository<MediaMetaData> MediaMetaData
        {
            get
            {
                if (mediaMetaData == null)
                {
                    mediaMetaData = new GenericRepository<MediaMetaData>(sdContext);
                }
                return mediaMetaData;
            }
        }

        public GenericRepository<Course> Course
        {
            get
            {
                if (course == null)
                {
                    course = new GenericRepository<Course>(sdContext);
                }
                return course;
            }
        }

        public GenericRepository<StudentsAttention> StudentsAttention
        {
            get
            {
                if (studentsAttention == null)
                {
                    studentsAttention = new GenericRepository<StudentsAttention>(sdContext);
                }
                return studentsAttention;
            }
        }

        public GenericRepository<Session> Session
        {
            get
            {
                if (session == null)
                {
                    session = new GenericRepository<Session>(sdContext);
                }
                return session;
            }
        }

        public GenericRepository<Administrator> Administrator
        {
            get
            {
                if (administrator == null)
                {
                    administrator = new GenericRepository<Administrator>(sdContext);
                }
                return administrator;
            }
        }

        public GenericRepository<Student> Student
        {
            get
            {
                if (student == null)
                {
                    student = new GenericRepository<Student>(sdContext);
                }
                return student;
            }
        }

        public GenericRepository<Log> Log
        {
            get
            {
                if (log == null)
                {
                    log = new GenericRepository<Log>(sdContext);
                }
                return log;
            }
        }

        public GenericRepository<Teacher> Teacher
        {
            get
            {
                if (teacher == null)
                {
                    teacher = new GenericRepository<Teacher>(sdContext);
                }
                return teacher;
            }
        }
    }
}
