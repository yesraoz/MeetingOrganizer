using MeetingOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetingOrganizer.Controllers
{
    public class MeetingController : Controller
    {
        private MeetingOrganizerDBEntities2 db = new MeetingOrganizerDBEntities2();

        // GET: Meeting
        public ActionResult Index()
        {
            var meetingList = db.tbl_Meeting.OrderByDescending(x => x.ID).ToList();

            return View(meetingList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tbl_Meeting meeting)
        {
            try
            {

                db.tbl_Meeting.Add(meeting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public ActionResult Delete(int id)
        {

            try
            {
                var meeting = db.tbl_Meeting.Where(m => m.ID == id).SingleOrDefault();

                if (meeting == null)
                {
                    return HttpNotFound();
                }

                foreach (var i in meeting.tbl_MeetingParticipant.ToList())
                {
                    db.tbl_MeetingParticipant.Remove(i);
                }
                db.tbl_Meeting.Remove(meeting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult Edit(int id)
        {
            var meeting = db.tbl_Meeting.Where(m => m.ID == id).SingleOrDefault();
            if (meeting == null)
            {
                return HttpNotFound();
            }


            return View(meeting);
        }

        [HttpPost]
        public ActionResult Edit(int id, tbl_Meeting meeting)
        {
            try
            {
                var meetings = db.tbl_Meeting.Where(m => m.ID == id).SingleOrDefault();

                meetings.Subject = meeting.Subject;
                meetings.Date = meeting.Date;
                meetings.StartTime = meeting.StartTime;
                meetings.EndTime = meeting.EndTime;

                db.SaveChanges();

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}