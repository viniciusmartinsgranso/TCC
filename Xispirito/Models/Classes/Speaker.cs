using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xispirito.Models
{
    public class Speaker : BaseUser
    {
        private string SpeakerProfession { get; set; }

        public Speaker()
        {

        }

        public Speaker(int speakerId, string speakerName, string speakerEmail, string speakerPicture, string speakerProfession, string speakerEncryptedPassword, bool isActive)
        {
            Id = speakerId;
            Name = speakerName;
            Email = speakerEmail;
            Picture = speakerPicture;
            SpeakerProfession = speakerProfession;
            EncryptedPassword = speakerEncryptedPassword;
            IsActive = isActive;
        }

        public string GetSpeakerProfession()
        {
            return SpeakerProfession;
        }
    }
}