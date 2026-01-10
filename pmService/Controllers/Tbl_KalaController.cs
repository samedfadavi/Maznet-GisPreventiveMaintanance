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
    public class Tbl_KalaController : ApiController
    {
        private SmModel db = new SmModel();

       
        public IQueryable<Tbl_Moshtari> GetTbl_Moshtari()
        {
            return (IQueryable<Tbl_Moshtari>)db.Tbl_Moshtari;
        }
        // GET: api/Tbl_Kala
        [HttpGet]
        [Route("api/Tbl_Kala/List_Kala")]
        public IQueryable<Tbl_Kala> GetTbl_Kala()
        {
            return db.Tbl_Kala;
        }
        [HttpPost]
        [Route("api/Tbl_Kala/Sabt")]
        public string SabteKala([FromBody] Tbl_Kala data)
        {
            db.Tbl_Kala.Add(data);
            int result = db.SaveChanges();
            return result.ToString();

        }

        [HttpPost]
        [Route("api/Tbl_Kala/Edit")]
        public string EditKala([FromBody] Tbl_Kala data)
        {
            if (!ModelState.IsValid)
            {
                return "-1";
            }



            db.Tbl_Kala.Attach(data);
            db.Entry(data).State = EntityState.Modified;

            try
            {

                return db.SaveChanges().ToString();

            }
            catch
            {
                return "-1";
            }

        }
        [HttpPost]
        [Route("api/Tbl_Kala/Delete")]
        public string DeleteKala([FromBody] Tbl_Kala data)
        {
            if (!ModelState.IsValid)
            {
                return "-1";
            }


            Tbl_Kala kala = db.Tbl_Kala.Find(data.Code_Kala);
            db.Tbl_Kala.Remove(kala);

            try
            {
                return db.SaveChanges().ToString();
            }
            catch
            {
                return "-1";
            }

        }
        // GET: api/Tbl_Kala/5
        [ResponseType(typeof(Tbl_Kala))]
        public IHttpActionResult GetTbl_Kala(int id)
        {
            Tbl_Kala tbl_Kala = db.Tbl_Kala.Find(id);
            if (tbl_Kala == null)
            {
                return NotFound();
            }

            return Ok(tbl_Kala);
        }

        // PUT: api/Tbl_Kala/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_Kala(int id, Tbl_Kala tbl_Kala)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Kala.Code_Kala)
            {
                return BadRequest();
            }

            db.Entry(tbl_Kala).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_KalaExists(id))
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

        // POST: api/Tbl_Kala
        [ResponseType(typeof(Tbl_Kala))]
        public IHttpActionResult PostTbl_Kala(Tbl_Kala tbl_Kala)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tbl_Kala.Add(tbl_Kala);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Kala.Code_Kala }, tbl_Kala);
        }

        // DELETE: api/Tbl_Kala/5
        [ResponseType(typeof(Tbl_Kala))]
        public IHttpActionResult DeleteTbl_Kala(int id)
        {
            Tbl_Kala tbl_Kala = db.Tbl_Kala.Find(id);
            if (tbl_Kala == null)
            {
                return NotFound();
            }

            db.Tbl_Kala.Remove(tbl_Kala);
            db.SaveChanges();

            return Ok(tbl_Kala);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_KalaExists(int id)
        {
            return db.Tbl_Kala.Count(e => e.Code_Kala == id) > 0;
        }
    }
}