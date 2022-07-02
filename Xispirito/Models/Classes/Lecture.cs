using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xispirito.Models
{
    public class Lecture : BaseEntity
    {
        private int LectureId { get; set; }
        private string LectureName { get; set; }
        private string LecturePicture { get; set; }
        private List<Area> AreaList { get; set; }
        private int LectureTime { get; set; }
        private DateTime LectureDate { get; set; }
        private string LectureDescription { get; set; }
        private string LectureModality { get; set; }
        private string LectureAddress { get; set; }
        private int LectureLimit { get; set; }
        private List<Speaker> SpeakerList { get; set; }
        private List<Viewer> ViewerList { get; set; }

        public Lecture()
        {

        }

        public Lecture(int lectureId, string lectureName, string lecturePicture, int lectureTime, DateTime lectureDate, string lectureDescription, string lectureModality, string lectureAddress, int lectureLimit, bool isActive)
        {
            LectureId = lectureId;
            LectureName = lectureName;
            LecturePicture = lecturePicture;
            LectureTime = lectureTime;
            LectureDate = lectureDate;
            LectureDescription = lectureDescription;
            LectureModality = lectureModality;
            LectureAddress = lectureAddress;
            LectureLimit = lectureLimit;
            IsActive = isActive;
        }

        public Lecture(int lectureId, string lectureName, string lecturePicture, List<Area> areaList, int lectureTime, DateTime lectureDate, string lectureDescription, string lectureModality, string lectureAddress, int lectureLimit, List<Speaker> speakerList, List<Viewer> viewerList, bool isActive)
        {
            LectureId = lectureId;
            LectureName = lectureName;
            LecturePicture = lecturePicture;
            AreaList = areaList;
            LectureTime = lectureTime;
            LectureDate = lectureDate;
            LectureDescription = lectureDescription;
            LectureModality = lectureModality;
            LectureAddress = lectureAddress;
            LectureLimit = lectureLimit;
            SpeakerList = speakerList;
            ViewerList = viewerList;
            IsActive = isActive;
        }

        public int GetId()
        {
            return LectureId;
        }

        public void SetId(int lectureId)
        {
            LectureId = lectureId;
        }

        public string GetName()
        {
            return LectureName;
        }

        public void SetName(string lectureName)
        {
            LectureName = lectureName;
        }

        public string GetPicture()
        {
            return LecturePicture;
        }

        public void SetPicture(string picturePath)
        {
            LecturePicture = picturePath;
        }

        public List<Area> GetAreaList()
        {
            return AreaList;
        }

        public int GetTime()
        {
            return LectureTime;
        }

        public void SetTime(int lectureTime)
        {
            LectureTime = lectureTime;
        }

        public DateTime GetDate()
        {
            return LectureDate;
        }

        public void SetDate(DateTime date)
        {
            LectureDate = date;
        }

        public string GetDescription()
        {
            return LectureDescription;
        }

        public void SetDescription(string description)
        {
            LectureDescription = description;
        }

        public string GetModality()
        {
            return LectureModality;
        }

        public void SetModality(string modality)
        {
            LectureModality = modality;
        }

        public string GetAddress()
        {
            return LectureAddress;
        }

        public void SetAddress(string address)
        {
            LectureAddress = address;
        }

        public int GetLimit()
        {
            return LectureLimit;
        }

        public void SetLimit(int limit)
        {
            LectureLimit = limit;
        }

        public List<Speaker> GetSpeakerList()
        {
            return SpeakerList;
        }

        public List<Viewer> GetViewerList()
        {
            return ViewerList;
        }
    }
}