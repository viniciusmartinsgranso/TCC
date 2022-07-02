using System.Collections.Generic;
using Xispirito.DAL;
using Xispirito.Models;

namespace Xispirito.Controller
{
    public class LectureBAL
    {
        private LectureDAL lectureDAL { get; set; }

        public LectureBAL()
        {
            lectureDAL = new LectureDAL();
        }

        public List<Lecture> GetUpcomingLecturesList()
        {
            return lectureDAL.UpcomingLecturesList();
        }

        public List<Lecture> GetUpcomingLecturesList(int maxQuantity)
        {
            return lectureDAL.UpcomingLecturesList(maxQuantity);
        }

        public List<Lecture> GetLecturesList()
        {
            return lectureDAL.LecturesList();
        }

        public List<Lecture> GetLecturesList(int maxQuantity)
        {
            return lectureDAL.LecturesList(maxQuantity);
        }

        public Lecture GetLecture(int lectureId)
        {
            return lectureDAL.Select(lectureId);
        }

        public List<Lecture> SearchLecturesByName(string search)
        {
            return lectureDAL.SearchLecturesByName(search);
        }

        public void UpdateLecture(Lecture objLecture)
        {
            lectureDAL.Update(objLecture);
        }

        public void InsertLecture(Lecture objLecture)
        {
            lectureDAL.Insert(objLecture);
        }

        public int GetNextId()
        {
            int nextId = lectureDAL.GetLastId();
            nextId++;
            return nextId;
        }

        public List<Lecture> GetAdministratorLectureList()
        {
            return lectureDAL.List();
        }

        public List<Lecture> GetAdministratorLectureList(string search)
        {
            return lectureDAL.List(search);
        }

        public void DeleteLecture(int lectureId)
        {
            lectureDAL.Delete(lectureId);
        }
    }
}