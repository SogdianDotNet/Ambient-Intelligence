using SD.BusinessLayer.Logging;
using SD.Commons.Shared;
using SD.Data;
using SD.Data.Entities.Entities;
using SD.Security.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.BusinessLayer.Builders
{
    /// <summary>
    /// class EncryptedUserBuilder.
    /// </summary>
    public class EncryptedUserBuilder
    {
        /// <summary>
        /// Insert a new Encrypted user in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Insert(RegisterViewModel model, string userId)
        {
            try
            {
                if (model != null && !string.IsNullOrEmpty(userId))
                {
                    using (SDContext sdContext = new SDContext())
                    {
                        sdContext.EncryptedUserData.Insert(new EncryptedUserData
                        {
                            Id = Guid.NewGuid(),
                            Email = HashData.ComputeHash(model.Email, "SHA256", null),
                            Password = HashData.ComputeHash(model.Password, "SHA256", null),
                            PhoneNumber = HashData.ComputeHash(model.PhoneNumber, "SHA256", null),
                            UserId = userId
                        });
                        sdContext.Save();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return false;
            }
        }

        /// <summary>
        /// Deletes the EncryptedUserData entity from the database.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Delete(string userId)
        {
            if (!String.IsNullOrEmpty(userId))
            {
                using (SDContext sdContext = new SDContext())
                {
                    if (sdContext.EncryptedUserData.Get(x=>x.UserId == userId) != null)
                    {
                        var user = sdContext.EncryptedUserData.Get(x => x.UserId == userId);
                        sdContext.EncryptedUserData.Delete(user.Id);
                        sdContext.Save();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                Logger.Write("EncryptedDataUser entity hasn't been deleted.", "Check the method of Delete(string userId) in EncryptedUserBuilder.class");
                return false;
            }
        }
    }
}
