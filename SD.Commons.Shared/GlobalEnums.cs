using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared
{
    public enum UserRoles
    {
        Administrator,
        Teacher,
        Student,
        All
    }

    public enum KlassenEnum
    {
        INF1A,
        INF1B,
        INF1C,
        INF2A,
        INF2B,
        INF2C,
        INF3A,
        INF3B,
        INF3C
    }

    public enum LokalenEnum
    {
        [Display(Name = "H.4.206")]
        h4206,
        [Display(Name = "H.4.306")]
        h4306,
        [Display(Name = "H.4.406")]
        h4308
    }

    public enum CoursesEnum
    {
        [Display(Name = "INFSEN01-1")]
        INFSEN011,
        [Display(Name = "INFSEN01-2")]
        INFSEN012,
        [Display(Name = "INFMAN01-1")]
        INFMAN011,
        [Display(Name = "INFMAN01-2")]
        INFMAN012,
        INFLAB01,
        INFLAB02,
        [Display(Name = "INFDTA01-1")]
        INFDTA011,
        [Display(Name = "INFDTA01-2")]
        INFDTA012        
    }
}
