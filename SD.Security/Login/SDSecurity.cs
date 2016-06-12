using SD.Commons.Shared;
using SD.Data;
using SD.Data.Entities.Entities;
using SD.Security.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Security.Login
{
    /// <summary>
    /// class SDSecurity.
    /// </summary>
    public class SDSecurity
    {
        private static string SHA256 = "SHA256";

        /// <summary>
        /// Checks if the user exists in the database.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string email, string password)
        {
            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
            {
                string userId = "";
                string phoneNumber = "";
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var user = context.Users.FirstOrDefault(x => x.Email == email && x.UserName == email);
                    if (user == null)
                    {
                        return false;
                    }
                    userId = user.Id;
                    phoneNumber = user.PhoneNumber;
                }

                if (VerifyEncryptedUser(email, password, userId, phoneNumber))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Verifying the encrypted user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static bool VerifyEncryptedUser(string email, string password, string userId, string phoneNumber)
        {
            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
            {
                using (SDContext sdContext = new SDContext())
                {
                    EncryptedUserData encUser = sdContext.EncryptedUserData.GetFirstOrDefault(x => x.UserId == userId);
                    if (encUser == null)
                    {
                        return false;
                    }

                    if (!HashData.VerifyHash(email, SHA256, encUser.Email) && !HashData.VerifyHash(password, SHA256, encUser.Password) && !HashData.VerifyHash(phoneNumber, SHA256, encUser.PhoneNumber))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }
    }
}
