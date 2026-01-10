using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using pmService.Models;

namespace pmService.Controllers
{
    public class ViewPardakhtMNsController : ApiController
    {
        private SmModel db = new SmModel();

        // GET: api/ViewPardakhtMNs
        [HttpGet]
        [Route("api/ViewPardakhtMNs/List_Sorathesab")]
        public IQueryable<ViewPardakhtMN> GetViewPardakhtMNs( string datefrom,  string dateto)
        {
            return  (IQueryable<ViewPardakhtMN>) db.ViewPardakhtMNs.Where(p => p.state!=null &&  string.Compare( p.datepardakht,datefrom)>=0 && string.Compare(p.datepardakht,dateto)<=0).OrderByDescending(p=> p.datepardakht);
        }
        [HttpPost]
        [Route("api/ViewPardakhtMNs/Sabt")]
        public string GSabteViewPardakhtMNs([FromBody] ViewPardakhtMN data)
        {
           
            return data.codemeli;

        }




        // GET: api/ViewPardakhtMNs/5
        [ResponseType(typeof(ViewPardakhtMN))]
        public IHttpActionResult GetViewPardakhtMN(decimal id)
        {
            ViewPardakhtMN viewPardakhtMN = db.ViewPardakhtMNs.Find(id);
            if (viewPardakhtMN == null)
            {
                return NotFound();
            }

            return Ok(viewPardakhtMN);
        }

        // PUT: api/ViewPardakhtMNs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutViewPardakhtMN(decimal id, ViewPardakhtMN viewPardakhtMN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != viewPardakhtMN.Idrow)
            {
                return BadRequest();
            }

            db.Entry(viewPardakhtMN).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViewPardakhtMNExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ViewPardakhtMNs
        [ResponseType(typeof(ViewPardakhtMN))]
        public IHttpActionResult PostViewPardakhtMN(ViewPardakhtMN viewPardakhtMN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ViewPardakhtMNs.Add(viewPardakhtMN);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = viewPardakhtMN.Idrow }, viewPardakhtMN);
        }

        // DELETE: api/ViewPardakhtMNs/5
        [ResponseType(typeof(ViewPardakhtMN))]
        public IHttpActionResult DeleteViewPardakhtMN(decimal id)
        {
            ViewPardakhtMN viewPardakhtMN = db.ViewPardakhtMNs.Find(id);
            if (viewPardakhtMN == null)
            {
                return NotFound();
            }

            db.ViewPardakhtMNs.Remove(viewPardakhtMN);
            db.SaveChanges();

            return Ok(viewPardakhtMN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ViewPardakhtMNExists(decimal id)
        {
            return db.ViewPardakhtMNs.Count(e => e.Idrow == id) > 0;
        }
        [HttpOptions]
        public IHttpActionResult Options()
        {
            Request.Headers.Add("Access-Control-Allow-Origin", "*");
            Request.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            Request.Headers.Add("Access-Control-Allow-Headers", "Content-Type");

            return Ok();
        }
    }
}