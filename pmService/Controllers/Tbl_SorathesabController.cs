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
    public class Tbl_SorathesabController : ApiController
    {
        private SmModel db = new SmModel();
        [HttpGet]
        [Route("api/Tbl_Sorathesab/List_Sorathesab")]
        public IHttpActionResult GetTbl_Sorathesab()
        {
            var res=db.Tbl_Sorathesab
                        .Include(p => p.Tbl_Kala)        // Eager load Category
        .Include(p => p.Tbl_Moshtari)        // Eager load Supplier
        .AsNoTracking()                  // Performance optimization
        .Select(p => new
        {
            p.ID_Sorathesab,
            p.Code_Moshtari,
            p.Code_Kala,
            p.Mablagh,
            p.Tarikh,
            p.Shenase,
            p.Vazeiat,
            Kala = p.Tbl_Kala.Onvan,
            Moshtari = p.Tbl_Moshtari.Name_Moshtari
        }).ToList();

            return Ok(res);
        }
        // GET: api/Tbl_Sorathesab
    

        // GET: api/Tbl_Sorathesab/5
        [ResponseType(typeof(Tbl_Sorathesab))]
        public IHttpActionResult GetTbl_Sorathesab(int id)
        {
            Tbl_Sorathesab tbl_Sorathesab = db.Tbl_Sorathesab.Find(id);
            if (tbl_Sorathesab == null)
            {
                return NotFound();
            }

            return Ok(tbl_Sorathesab);
        }
        [HttpPost]
        [Route("api/Tbl_Sorathesab/Sabt")]
        public string SabteSorathesab([FromBody] Tbl_Sorathesab data)
        {
            db.Tbl_Sorathesab.Add(data);
            int result = db.SaveChanges();
            return result.ToString();

        }
        [HttpPost]
        [Route("api/Tbl_Sorathesab/Edit")]
        public string EditSorathesab([FromBody] Tbl_Sorathesab data)
        {
            if (!ModelState.IsValid)
            {
                return "-1";
            }



            db.Tbl_Sorathesab.Attach(data);
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
        // PUT: api/Tbl_Sorathesab/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_Sorathesab(int id, Tbl_Sorathesab tbl_Sorathesab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Sorathesab.ID_Sorathesab)
            {
                return BadRequest();
            }

            db.Entry(tbl_Sorathesab).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_SorathesabExists(id))
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

        // POST: api/Tbl_Sorathesab
        [ResponseType(typeof(Tbl_Sorathesab))]
        public IHttpActionResult PostTbl_Sorathesab(Tbl_Sorathesab tbl_Sorathesab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tbl_Sorathesab.Add(tbl_Sorathesab);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Sorathesab.ID_Sorathesab }, tbl_Sorathesab);
        }

        // DELETE: api/Tbl_Sorathesab/5
        [ResponseType(typeof(Tbl_Sorathesab))]
        public IHttpActionResult DeleteTbl_Sorathesab(int id)
        {
            Tbl_Sorathesab tbl_Sorathesab = db.Tbl_Sorathesab.Find(id);
            if (tbl_Sorathesab == null)
            {
                return NotFound();
            }

            db.Tbl_Sorathesab.Remove(tbl_Sorathesab);
            db.SaveChanges();

            return Ok(tbl_Sorathesab);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_SorathesabExists(int id)
        {
            return db.Tbl_Sorathesab.Count(e => e.ID_Sorathesab == id) > 0;
        }
    }
}