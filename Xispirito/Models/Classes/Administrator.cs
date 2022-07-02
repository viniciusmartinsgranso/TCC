using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xispirito.Models
{
    public class Administrator : BaseUser
    {
        public Administrator()
        {

        }

        public Administrator(int administratorId, string administratorName, string administratorEmail, string administratorPicture, string administratorEncryptedPassword, bool isActive)
        {
            Id = administratorId;
            Name = administratorName;
            Email = administratorEmail;
            Picture = administratorPicture;
            EncryptedPassword = administratorEncryptedPassword;
            IsActive = isActive;
        }
    }
}