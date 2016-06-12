using SD.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.BusinessLayer.API_Builders
{
    /// <summary>
    /// class LoginAPIBuilder
    /// </summary>
    public class LoginAPIBuilder
    {
        /// <summary>
        /// Logging in if the username and pincode matches with the data in the database. 
        /// It sends back a JSON formatted boolean.
        /// This is for the Android application login.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pincode"></param>
        /// <returns></returns>
        public bool Login(string username, int pincode)
        {
            if (!String.IsNullOrEmpty(username))
            {
                using (SDContext sdContext = new SDContext())
                {
                    var encryptedUser = sdContext.EncryptedUserData.Get(x => x.Pincode == pincode);
                    var teacher = sdContext.Teacher.Get(x => x.Email == username);
                    if (encryptedUser != null && teacher != null && encryptedUser.Pincode == pincode && teacher.Email == username)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
