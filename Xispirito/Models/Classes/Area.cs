using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xispirito.Models
{
    public class Area : BaseEntity
    {
        private int AreaId { get; set; }
        private string AreaName { get; set; }
        private string AreaPicture { get; set; }

        public Area(int areaId, string areaName, string areaPicture, bool isActive)
        {
            AreaId = areaId;
            AreaName = areaName;
            AreaPicture = areaPicture;
            IsActive = isActive;
        }

        public int GetId()
        {
            return AreaId;
        }

        public string GetName()
        {
            return AreaName;
        }

        public string GetPicture()
        {
            return AreaPicture;
        }

        public void SetPicture(string pictureDirectory)
        {
            AreaPicture = pictureDirectory;
        }
    }
}