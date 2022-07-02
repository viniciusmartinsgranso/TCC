using System.Collections.Generic;
using Xispirito.DAL;
using Xispirito.Models;

namespace Xispirito.Controller
{
    public class AreaBAL
    {
        private AreaDAL areaDAL { get; set; }

        public AreaBAL()
        {
            areaDAL = new AreaDAL();
        }

        public List<Area> GetAreaList(int areaQuantity)
        {
            List<Area> areaList = new List<Area>();
            areaList = areaDAL.List(areaQuantity);

            return areaList;
        }

    }
}