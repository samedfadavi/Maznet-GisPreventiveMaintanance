using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace pmService.Controllers
{
    public class QueryParameters
    {
        public string query { get; set; }
    }
    public class BazdidController : ApiController
    {
        [HttpGet]
        [Route("api/Bazdid/SabteBazdid/{query}")]
        public string SabteBazdid(string query)
        {

            try
            {
                classdata data = new classdata();
                return data.LoadOpenData(query).ToString();
            }
            catch(Exception ex)
            { return ex.Message; }


        }
        [HttpPost]
        [Route("api/Bazdid/SabteIradeTajhiz")]
        public string SabteIradeTajhiz([FromBody]string query)
        {
            
            try
            {
                classdata data = new classdata();
                
                return data.LoadOpenData(query).ToString();
            }
            catch(Exception ex)
            { return "0"; }


        }
        [HttpGet]
        [Route("api/Bazdid/SabteOlaviateIrad")]
        public string SabteOlaviateIrad([FromBody]string query)
        {

            try
            {
                classdata data = new classdata();
                return Convert.ToInt32( data.Add(query)).ToString();
            }
            catch
            { return "0"; }


        }
        [HttpPost]
        [Route("api/Bazdid/HazfeIradat")]
        public string HazfeIradat([FromBody]string query)
        {

            try
            {
                classdata data = new classdata();

                return Convert.ToInt32( data.EditAndUpdate(query)).ToString();
            }
            catch (Exception ex)
            { return "0"; }


        }
        [HttpPost]
        [Route("api/Bazdid/HazfeIradat2")]
        public string HazfeIradat2([FromBody]string query)
        {

            try
            {
                classdata data = new classdata();

                return Convert.ToInt32(data.ExecuteQuerywithresult(query)).ToString();
            }
            catch (Exception ex)
            { return "0"; }


        }
        [Route("api/Bazdid/SabteTasvireIrad")]
        [HttpPost]
        public string SabteTasvireIrad()
        {
            classdata data = new classdata();
            //WriteImage(photo.file);
            try {
                var request = HttpContext.Current.Request;
                
                var pk_id = request.Form["pk_id"];
                if(pk_id.Contains("WHERE vaziat <>2 andl"))
                  pk_id=  pk_id.Replace("WHERE vaziat <>2 andl", "WHERE vaziat <>2 and");
                string flag_enteghal = data.LoadOpenData(pk_id).ToString();
                for (int i = 0; i < request.Files.Count; i++)
                {
                    var file = request.Files[i];

                   data.AddWithProcedure("Add_TasvireIrad", new string[] { "code_iradetajhiz", "tasvire_irad" }, new object[] { flag_enteghal, file.InputStream });
                    

                }
                return flag_enteghal;
            }
            catch(Exception ex)
            { return ex.Message; }
            
            
            

            
        }
        [Route("api/Bazdid/SabteTasvireIrad2")]
        [HttpPost]
        public string SabteTasvireIrad2()
        {
            classdata data = new classdata();
            //WriteImage(photo.file);
            try
            {
                var request = HttpContext.Current.Request;
                var pk_id = request.Form["pk_id"];
                string flag_enteghal = data.LoadOpenData2(pk_id).ToString();
                for (int i = 0; i < request.Files.Count; i++)
                {
                    var file = request.Files[i];

                    data.AddWithProcedure("Add_TasvireIrad", new string[] { "code_iradetajhiz", "tasvire_irad" }, new object[] { flag_enteghal, file.InputStream });


                }
                return flag_enteghal;
            }
            catch (Exception ex)
            { return ex.Message; }





        }
        [HttpPost]
        [Route("api/Bazdid/SabteTajhizejadid")]
        public string SabteTajhizejadid([FromBody]string inputdata)                        
        {
            string[] datas=null;
            try
            {


                System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                string tarikh = p.GetYear(DateTime.Now).ToString();
                if(p.GetMonth(DateTime.Now)<10)
                     tarikh += "/0"+p.GetMonth(DateTime.Now).ToString();
                else
                    tarikh += "/"+p.GetMonth(DateTime.Now).ToString();
                if (p.GetDayOfMonth(DateTime.Now) < 10)
                    tarikh += "/0" + p.GetDayOfMonth(DateTime.Now).ToString();
                else
                    tarikh += "/" + p.GetDayOfMonth(DateTime.Now).ToString();
                datas = inputdata.Split(';');
                string globalid = datas[0];
                string code_zamanbandi = datas[1];
                string noe_tajhiz = datas[2];
                string code_ekip = datas[3];
                //string tarikh = datas[4];
                string hoze = "0";
                string ffm = "0";
                string name_tajhiz = "";
                string address_tajhiz = "";
                string code_post = "";
                if (datas.Length > 4)
                {
                    try
                    {
                        if(datas[5].Length>1)
                        hoze = datas[5];
                        if(datas[6].Length>1)
                        ffm = datas[6];
                    }
                    catch
                    {
                        hoze = "0";
                        ffm = "0";
                    }
                }
                if (datas.Length > 6)
                {
                    try
                    {
                        name_tajhiz = datas[7];
                        address_tajhiz = datas[8];
                    }
                    catch
                    { }
                }
                if (datas.Length > 8)
                {
                    try { code_post = datas[9]; }
                    catch
                    { }
                    
                    
                }
                string table_name = "";
                string code_field = "0";
                int noe = 0;
                switch (noe_tajhiz)
                {
                    case "1":
                        table_name = "tbl_pt";
                        code_field = "code_pt";
                        noe = 2;
                        break;
                    case "2":
                        table_name = "tbl_pt";
                        code_field = "code_pt";
                        noe = 2;
                        break;
                    case "3":
                        table_name = "tbl_payehffm";
                        code_field = "code_payeh"; 
                        noe = 1;
                        break;
                    case "5":
                        table_name = "tbl_payehffz";
                        code_field = "code_payeh";
                        noe = 3;
                        break;
                }
                classdata data = new classdata();
                string query = "select    "+code_field+" from " + table_name + " where globalid='" + globalid+"'";
                string code_tajhiz="";
                object obj = data.GetValue(query);
                if (obj != null)
                    code_tajhiz = obj.ToString();
                if (code_tajhiz == "")
                {
                    try {
                        if (noe_tajhiz == "1" || noe_tajhiz == "2")
                        {
                            if (datas.Length > 8)
                            {
                                code_post = datas[9];

                            }
                            code_tajhiz = data.LoadOpenData("insert into tbl_pt (qffm,hozeh,name_pt,address_pt,pelak, globalid) values(" + ffm + "," + hoze + ",'" + name_tajhiz + "','" + address_tajhiz + "','" + code_post + "','" + globalid + "') declare @code_tajhiz int= @@IDENTITY select @code_tajhiz").ToString();
                        }
                        else if (noe_tajhiz == "3")
                            code_tajhiz = data.LoadOpenData("insert into tbl_payehffm (Code_FFM,hozeh,NamePayeh,Address_Payeh, globalid) values(" + ffm + "," + hoze + ",'" + name_tajhiz + "','" + address_tajhiz + "','" + globalid + "') declare @code_tajhiz int= @@IDENTITY select @code_tajhiz").ToString();

                    }
                    catch 
                    {
                        code_tajhiz = "0";
                    }

                }
                string code_bazdid = "";
                if (code_tajhiz != "0" && code_tajhiz!="")
                {
                    if (code_zamanbandi == "0" || code_zamanbandi == "")
                    {
                        code_bazdid = data.GetValue("select top 1 code from Tbl_jozeiatezamanbandibazdid where code_tajhiz=" + code_tajhiz + " order by tarikheshoro desc").ToString();
                        if (code_bazdid == "" || code_bazdid == "0")
                        {
                            code_zamanbandi = data.GetValue("select top 1 code_Zamanbandi from Tbl_ZamanbandieBazdid where Omor="+hoze+ " order by PishbiniShoro desc").ToString();
                        }


                    }
                    if (code_bazdid == "0" || code_bazdid == "")
                    {
                        query = "INSERT INTO Tbl_jozeiatezamanbandibazdid     (code_zamanbandi, code_tajhiz, noe_tajhiz, tarikheshoro, tarikhepayan, code_ekip, vaziat) values        (" + code_zamanbandi + ", " + code_tajhiz + ", " + noe + ", " + tarikh + ", " + tarikh + ", " + code_ekip + ", 2)    declare @code_bazdid int= @@IDENTITY select @code_bazdid";

                        code_bazdid = data.GetValue(query).ToString();
                    }
                    
                    if (code_tajhiz != "" && code_bazdid != "")
                        return code_tajhiz + ";" + code_bazdid;
                    else
                        return "0";

                }
                else
                    return "0";
            }
            catch (Exception ex)
            { return inputdata+datas.Length.ToString(); }

        }
        //Api Controller Model
        public class ProfilePhotoUpload
        {
            public string pk_id { get; set; }
            
            public HttpPostedFile file { get; set; }
        }
    }
}
