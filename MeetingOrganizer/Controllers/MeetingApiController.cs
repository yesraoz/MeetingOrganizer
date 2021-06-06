using MeetingOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MeetingOrganizer.Controllers
{
    public class MeetingApiController : ApiController
    {
        MeetingOrganizerDBEntities2 db = new MeetingOrganizerDBEntities2();


        public HttpResponseMessage Get(int id)
        {
            var meeting = db.tbl_Meeting.FirstOrDefault(x => x.ID == id);
            if (meeting == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Id'si {id} olan toplantı bulunamadı.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, meeting);
        }

        public HttpResponseMessage Post(tbl_Meeting meeting)
        {
            try
            {
                db.tbl_Meeting.Add(meeting);

                if (db.SaveChanges() > 0)
                {
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, meeting);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + meeting.ID);

                    return message;

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Ekleme Yapılamadı");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);


            }
        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var meeting = db.tbl_Meeting.FirstOrDefault(e => e.ID == id);
                if (meeting == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Meeting id: " + id);
                }
                else
                {
                    db.tbl_Meeting.Remove(meeting);

                    if (db.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, "meeting id: " + id);



                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Kayıt Silinemedi Meeting Id: " + id);
                    }


                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ex);

            }
        }

    }
}
