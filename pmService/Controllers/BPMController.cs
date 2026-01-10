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
    public class BPMController : ApiController
    {
        private MaznetModel db = new MaznetModel();

 
        [HttpGet]
        [Route("api/BPM/List_Farayand")]
        public Task<List<Dictionary<string, object>>> List_Farayand()
        {

            //var items = JArray.Parse(jsonData); // Assuming jsonData is an array of JSON objects
       

            return new classdata().ExecuteSql("select * from Tbl_Farayand");

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