using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xispirito.Models
{
    public class Viewer : BaseUser 
    {
        public Viewer()
        {

        }

        public Viewer(int viewerId, string viewerName, string viewerEmail, string viewerPicture, string viewerEncryptedPassword, bool isActive)
        {
            Id = viewerId;
            Name = viewerName;
            Email = viewerEmail;
            Picture = viewerPicture;
            EncryptedPassword = viewerEncryptedPassword;
            IsActive = isActive;
        }

        public Viewer(string viewerName, string viewerEmail, string viewerPicture, string viewerEncryptedPassword, bool isActive)
        {
            Name = viewerName;
            Email = viewerEmail;
            Picture = viewerPicture;
            EncryptedPassword = viewerEncryptedPassword;
            IsActive = isActive;
        }
    }
}