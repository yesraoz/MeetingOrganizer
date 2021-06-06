using MeetingOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetingOrganizer.Controllers
{
    public class ParticipantController : Controller
    {
        private MeetingOrganizerDBEntities2 db = new MeetingOrganizerDBEntities2();


        public ActionResult Index()
        {
            return View(db.tbl_Participant.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tbl_Participant Participant)
        {
            try
            {
                db.tbl_Participant.Add(Participant);
                db.SaveChanges();
                return RedirectToAction("Index", "Meeting");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}