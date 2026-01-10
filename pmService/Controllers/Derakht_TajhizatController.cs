using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using pmService.Data;
using pmService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;

namespace pmService.Controllers
{
    public class Derakht_TajhizatController : ApiController
    {
        private MaznetModel db = new MaznetModel();

        [HttpPost]
        [Route("api/Derakht_Tajhizat/List_Iradat")]
        public Task<List<Dictionary<string, object>>> List_Iradat([FromBody] JArray items)
        {

            //var items = JArray.Parse(jsonData); // Assuming jsonData is an array of JSON objects
            string result = "";
            int i = 0;
            foreach (var item in items)
            {
                try
                {
                    var table_name = item["Name_Jadval_Pm"]?.Value<string>();
                    var field_name = item["Name_Pm"]?.Value<string>();
                    var field_code = item["Code_Pm"]?.Value<string>();
                    var id = item["ID"]?.Value<int>();
                    string where = item["where"]?.Value<string>();
                    string whereirad = item["whereirad"]?.Value<string>();
                    if (i++ > 0)
                        result += " union ";
                    result += " SELECT TBL_Bazdid_Shode.*, "+ table_name+"."+ field_name + " collate Arabic_CI_AI as name_tajhiz " +
                        ", (tbl_irad.onvan_irad+' '+ ISNULL(Tbl_IradatCheck.name_irad, ' ')+' '+tbl_olaviat_goroh.name_o) as onvane_irad, tbl_olaviat_goroh.shomare,  " +
" tbl_olaviat_goroh.code_o, tbl_irad.code_irad, Tbl_IradatCheck.code_irad AS code_rizirad " +
                        " FROM TBL_Bazdid_Shode INNER JOIN  Tbl_jozeiatezamanbandibazdid ON TBL_Bazdid_Shode.Code_Bazdid = Tbl_jozeiatezamanbandibazdid.code" +
                        " INNER JOIN "+table_name+" ON Tbl_jozeiatezamanbandibazdid.code_tajhiz = "+table_name+"."+ field_code+

                        " INNER JOIN tbl_irad ON TBL_Bazdid_Shode.Kharabi = tbl_irad.code_irad INNER JOIN "+
                        " tbl_olaviat_goroh ON TBL_Bazdid_Shode.olaviat = tbl_olaviat_goroh.code_o LEFT  JOIN"+
                        " Tbl_IradatCheck ON TBL_Bazdid_Shode.Code_rizIrad = Tbl_IradatCheck.code_irad";

                    if (where.Length > 0 && !where.Contains("where"))
                        where =" where "+ table_name + "." + field_code + " in(" + where + ")";
                    if (where.Length < 4)
                        result += " where ";
                    else
                        result += where+" and ";
                    if (whereirad.Length > 1)
                        result += " TBL_Bazdid_Shode.kharabi in("+whereirad+") and ";
                    result += " TBL_Bazdid_Shode.Noe_Tajhiz=" + id.ToString() + " and TBL_Bazdid_Shode.Flag=0 and Tarikh like '1404%' ";
             
                

                }
                catch
                { }
                //var id = item["id"]?.Value<int>();

                // Process the id and name values as needed
            }

            return new classdata().ExecuteSql(result);

        }
        [HttpGet]
        [Route("api/Derakht_Tajhizat/List_Iradat_Noe_Tajhiz")]
        public Task<List<Dictionary<string, object>>> List_Iradat_Noe_Tajhiz( int ID_Tajhiz)
        {

            //var items = JArray.Parse(jsonData); // Assuming jsonData is an array of JSON objects
       

            return new classdata().ExecuteStoredProcedure("cmms_get_list_irad_noe_tajhiz",new[] {new SqlParameter("@ID_Tajhiz",ID_Tajhiz) });

        }

        public async Task<List<dynamic>> ExecuteDynamicQueryAsync(MaznetModel context, string entityName, string sqlQuery)
        {

            // Get the entity type by name
            Type entityType = context.GetType().GetProperties()
                .Where(p => p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .Select(p => p.PropertyType.GetGenericArguments()[0])
                .FirstOrDefault(t => t.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));

            if (entityType == null)
                throw new ArgumentException($"Entity type {entityName} not found in context.");

            // Use the dynamic DbSet to create and execute the query
            var dbSet = context.Set(entityType);

            // Execute the raw SQL query and convert results to a list
            var results = await dbSet.SqlQuery("select * from "+ entityName+" "+ sqlQuery).ToListAsync(); // Use SqlQuery from EF6
            
            return results.Select(item => (dynamic)item).ToList();
        }
        [HttpGet]
        [Route("api/Derakht_Tajhizat/GetList_Tajhiz/{entityName}/{sqlQuery}")]
        public async Task<List<dynamic>> GetList_Tajhiz( string entityName, string sqlQuery)
        {
            return await ExecuteDynamicQueryAsync(new MaznetModel(), entityName, sqlQuery);
        }
        // GET: api/Derakht_Tajhizat
        [HttpGet]
        [Route("api/Derakht_Tajhizat/GetDerakht_Tajhizat")]
        public IQueryable<Tbl_Derakht_Tajhizat> GetTbl_Derakht_Tajhizat()
        {
            //JToken json = JToken.Parse(JsonConvert.SerializeObject(cls_BargiriPt));
            return db.Tbl_Derakht_Tajhizat;
        }

        // GET: api/Derakht_Tajhizat/5
        [ResponseType(typeof(Tbl_Derakht_Tajhizat))]
        public IHttpActionResult GetTbl_Derakht_Tajhizat(int id)
        {
            Tbl_Derakht_Tajhizat tbl_Derakht_Tajhizat = db.Tbl_Derakht_Tajhizat.Find(id);
            if (tbl_Derakht_Tajhizat == null)
            {
                return NotFound();
            }

            return Ok(tbl_Derakht_Tajhizat);
        }

        // PUT: api/Derakht_Tajhizat/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_Derakht_Tajhizat(int id, Tbl_Derakht_Tajhizat tbl_Derakht_Tajhizat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Derakht_Tajhizat.ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Derakht_Tajhizat).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_Derakht_TajhizatExists(id))
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

        // POST: api/Derakht_Tajhizat
        [ResponseType(typeof(Tbl_Derakht_Tajhizat))]
        public IHttpActionResult PostTbl_Derakht_Tajhizat(Tbl_Derakht_Tajhizat tbl_Derakht_Tajhizat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tbl_Derakht_Tajhizat.Add(tbl_Derakht_Tajhizat);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Tbl_Derakht_TajhizatExists(tbl_Derakht_Tajhizat.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_Derakht_Tajhizat.ID }, tbl_Derakht_Tajhizat);
        }

        // DELETE: api/Derakht_Tajhizat/5
        [ResponseType(typeof(Tbl_Derakht_Tajhizat))]
        public IHttpActionResult DeleteTbl_Derakht_Tajhizat(int id)
        {
            Tbl_Derakht_Tajhizat tbl_Derakht_Tajhizat = db.Tbl_Derakht_Tajhizat.Find(id);
            if (tbl_Derakht_Tajhizat == null)
            {
                return NotFound();
            }

            db.Tbl_Derakht_Tajhizat.Remove(tbl_Derakht_Tajhizat);
            db.SaveChanges();

            return Ok(tbl_Derakht_Tajhizat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_Derakht_TajhizatExists(int id)
        {
            return db.Tbl_Derakht_Tajhizat.Count(e => e.ID == id) > 0;
        }
    }
}