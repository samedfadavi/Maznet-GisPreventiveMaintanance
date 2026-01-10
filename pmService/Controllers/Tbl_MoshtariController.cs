using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

using pmService.Models;

namespace pmService.Controllers
{
    public class Tbl_MoshtariController : ApiController
    {
        private SmModel db = new SmModel();

        // GET: Tbl_Moshtari
        [HttpGet]
        [Route("api/Tbl_Moshtari/List_Moshtari")]
        public IQueryable<Tbl_Moshtari> GetTbl_Moshtari()
        {
            return (IQueryable<Tbl_Moshtari>)db.Tbl_Moshtari;
        }
      
        [HttpPost]
        [Route("api/Tbl_Moshtari/Sabt")]
        public string SabteMoshtari([FromBody] Tbl_Moshtari data)
        {
            db.Tbl_Moshtari.Add(data);
           int result= db.SaveChanges();
            return result.ToString();

        }

        [HttpPost]
        [Route("api/Tbl_Moshtari/Edit")]
        public string EditMoshtari([FromBody] Tbl_Moshtari data)
        {
            if (!ModelState.IsValid)
            {
                return "-1";
            }


          
            db.Tbl_Moshtari.Attach(data);
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
        [Route("api/Tbl_Moshtari/Delete")]
        public string DeleteMoshtari([FromBody] Tbl_Moshtari data)
        {
            if (!ModelState.IsValid)
            {
                return "-1";
            }


            Tbl_Moshtari moshtari = db.Tbl_Moshtari.Find(data.Code_Moshtari);
            db.Tbl_Moshtari.Remove(moshtari);

            try
            {
                return db.SaveChanges().ToString();
            }
            catch
            {
                return "-1";
            }

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
