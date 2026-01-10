using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web;
using System.Data;
using Newtonsoft.Json.Linq;
using pmService.Models;

namespace pmService.Controllers
{
 
    public class EkipController : ApiController
    {
        [HttpGet]
        [Route("api/Ekip/GetChecklistFile")]
        public string GetChecklistFile()
        {
           
            try
            {
              

                return "http://10.150.129.33:8080/datafiles/pm_android.db";
            }
            catch (Exception ex)
            { return "0"; }

        }
    
        [HttpGet]
        [Route("api/Ekip/GetEkipInfo/{userName}/{Password}")]
        public string GetEkipInfo(string userName, string Password)
        {
            
            object code_ekip = "0";
            try
            {
                classdata data = new classdata();
                string query = "select    code_ekip from  tbl_Ekip where tbl_ekip.UserName='" + userName + "' and tbl_ekip.Password='" + Password + "' ";
                code_ekip = data.GetValue(query);
                if (code_ekip == null)
                    return "-2";
                 query = "select   filepath  from Tbl_ZamanbandieBazdid where code_Zamanbandi=(select top 1 code_zamanbandi from Tbl_jozeiatezamanbandibazdid where code_ekip="+code_ekip.ToString()+" order by code_zamanbandi desc)";
                object filename = data.GetValue(query);
                if (filename == null || filename.ToString().Length<1)
                    return "-1";
          
                
                if (HttpContext.Current.Request.UserHostAddress.ToString().Contains("10.126.128"))
                {
                    data.GetValue("insert into tbl_log_temp values('172.30.83.104:8080datafiles" + filename.ToString() + "pm_android.db')");
                    return "http://172.30.83.104:8080/datafiles/" + filename.ToString() + "/pm_android.db";
                }
                
                 else
                 return "http://10.150.129.33:8080/datafiles/" + filename.ToString()  + "/pm_android.db";
            }
            catch (Exception ex)
            { return code_ekip.ToString(); }
          
        }
        [HttpGet]
        [Route("api/Ekip/GetEkipTasks/{userName}/{Password}")]
        public string GetEkipTasks(string userName, string Password)
        {

            object code_ekip = "0";
            try
            {
                classdata data = new classdata();
                string query = "select    code_ekip from  tbl_Ekip where tbl_ekip.UserName='" + userName + "' and tbl_ekip.Password='" + Password + "' ";
                code_ekip = data.GetValue(query);
                if (code_ekip == null)
                    return "-2";
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.BaseAddress = new System.Uri("http://localhost:8090/engine-rest/");

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var res = client.GetAsync("task?assignee=" + code_ekip + "&priority=1&sortBy=created&sortOrder=desc");
                var response = res.Result;
                var jsonString = response.Content.ReadAsStringAsync();
                return jsonString.Result;
            }
            catch (Exception ex)
            { return "0"; }

        }
        [Route("api/Ekip/GetAppVersionBargiri")]
        public string GetAppVersionBargiri()
        {
            try
            {
                classdata data = new classdata();

                string query = "select   versioncode  from Tbl_appversionbar";
                return data.GetValue(query).ToString();

            }
            catch (SqlException ex)
            {
                return "1";
            }
            catch (Exception ex)
            {

                return "1";
            }


        }
        [HttpGet]
        [Route("api/Ekip/GetAppVersion")]
        public string GetAppVersion()
        {
            try
            {
                classdata data = new classdata();

                string query = "select   versioncode  from Tbl_appversion";
                return data.GetValue(query).ToString();

            }
            catch (SqlException ex)
            {
                return "1";
            }
            catch (Exception ex)
            {

                return "1";
            }


        }
        [HttpGet]
        [Route("api/Ekip/GetEkipMojriInfo/{userName}/{Password}")]
        public string GetEkipMojriInfo(string userName, string Password)
        {
            object code_ekip = "0";
            try
            {
                classdata data = new classdata();
                string query = "select    code_ekip from  tbl_Ekip where tbl_ekip.UserName='" + userName + "' and tbl_ekip.Password='" + Password + "' ";
                code_ekip = data.GetValue(query);
                if (code_ekip == null)
                    return "-2";
                query = "select top 1   filepath  from DS_Project_EnteghalMojri where   code_ekip=" + code_ekip.ToString() + " order by ZamanEnteghal desc";
                object filename = data.GetValue(query);
                if (filename == null || filename.ToString().Length < 1)
                    return "-1";

                return "http://10.150.129.33:8080/datafiles/pro/Mojri/" + filename.ToString() + "/pm_android.db";
            }
            catch (Exception ex)
            { return code_ekip.ToString(); }

        }
        [HttpGet]
        [Route("api/Ekip/GetEkipNazerInfo/{userName}/{Password}")]
        public List<Models.Project> GetEkipNazerInfo(string userName, string Password)
        {
            

            List<Models.Project> Projects = new List<Models.Project>();
            object code_ekip = "0";
            try
            {
                classdata data = new classdata();
                string query = "select    code_ekip from  tbl_Ekip where tbl_ekip.UserName='" + userName + "' and tbl_ekip.Password='" + Password + "' ";
                code_ekip = data.GetValue(query);
                if (code_ekip == null)
                    return null;
                query = "SELECT   top 1    DS_Project.projectID, DS_Project.name,DS_Project.filepath ,DS_Project.Flag_NezareatBarBazdid,DS_Project.Flag_NezareatBarTamir FROM         DS_Project  where (not Flag_NezareatBarBazdid is null or not  Flag_NezareatBarTamir is null ) and   not filepath is null and  ( Code_Ekip_NT in(select    code_ekip from  tbl_Ekip where tbl_ekip.UserName='" + userName + "' and tbl_ekip.Password='" + Password + "') or  Code_Ekip_NB in(select    code_ekip from  tbl_Ekip where tbl_ekip.UserName='" + userName + "' and tbl_ekip.Password='" + Password + "')) order by ZamanEnteghal_Pro desc ";
                System.Data.DataTable projects = data.GetTable(new SqlCommand(query));
                if (projects == null)
                    return null;
                if (projects.Rows.Count == 0)
                    return null;
                for (int i = 0; i < projects.Rows.Count; i++)
                {
                    Models.Project project = new Models.Project();
                    project.Projectid = projects.Rows[i].ItemArray[0].ToString();
                    project.Projectname = projects.Rows[i].ItemArray[1].ToString();
                    if(projects.Rows[i].ItemArray[4].ToString()!="")
                    project.Filepath = "http://10.150.129.33:8080/datafiles/pro/NazerTamir/" + projects.Rows[i].ItemArray[2].ToString();
                    else
                        project.Filepath = "http://10.150.129.33:8080/datafiles/pro/NazerBazdid/" + projects.Rows[i].ItemArray[2].ToString();
                    Projects.Add(project);
                }

                return Projects;
            }
            catch (Exception ex)
            { return null; }

        }
        [HttpGet]
        [Route("api/Ekip/GetEkipBargiriInfo/{userName}/{Password}")]
        public string GetEkipBargiriInfo(string userName, string Password)
        {
            object code_ekip = "0";
            try
            {
                classdata data = new classdata();
                string query = "select    code_ekip from  BG_EkipBargiri where UserName='" + userName + "' and Password='" + Password + "' ";
                code_ekip = data.GetValue(query);
                if (code_ekip == null)
                    return "-2";
                query = "select   filepath  from Tbl_ZamanbandieBazdid where no_zamanbandi=2 and code_Zamanbandi=(select top 1 code_zamanbandi from Tbl_jozeiatezamanbandibazdid where code_ekip in(select    code_ekip from  BG_EkipBargiri where UserName='" + userName + "' and Password='" + Password + "' ) order by code_zamanbandi desc)";
                object filename = data.GetValue(query);
                if (filename == null || filename.ToString().Length <= 1)
                    return "-1";

                return "http://10.150.129.33:8080/datafiles/Bargiri/" + filename.ToString() + "/pm_android.db";
            }
            catch (Exception ex)
            { return code_ekip.ToString(); }

        }
        [HttpGet]
        [Route("api/Ekip/GetAppVersionMojri")]
        public string GetAppVersionMojri()
        {
            try
            {
                classdata data = new classdata();

                string query = "select   versioncode  from Tbl_appversionmojri";
                return data.GetValue(query).ToString();

            }
            catch (SqlException ex)
            {
                return "1";
            }
            catch (Exception ex)
            {

                return "1";
            }


        }
        [HttpGet]
        [Route("api/Ekip/GetNezaratAppVersion")]
        public string GetNezaratAppVersion()
        {
            try
            {
                classdata data = new classdata();

                string query = "select   versioncode  from Tbl_appversionnezarat";
                return data.GetValue(query).ToString();

            }
            catch (SqlException ex)
            {
                return "1";
            }
            catch (Exception ex)
            {

                return "1";
            }


        }
        [HttpGet]
        [Route("api/Ekip/GetChecklistVersion")]
        public string GetChecklistVersion()
        {
            try
            {
                classdata data = new classdata();

                string query = "select   ver_num  from tbl_checklist_version";
                return data.GetValue(query).ToString();

            }
            catch (SqlException ex)
            {
                return "1";
            }
            catch (Exception ex)
            {

                return "1";
            }


        }
        [HttpGet]
        [Route("api/Liste_Bargiri_Post/{Tarikh_Bargiri_Az}/{Tarikh_Bargiri_Ta}")]
        public JArray GetBargiriPost(string Tarikh_Bargiri_Az, string Tarikh_Bargiri_Ta)
        {
            Tarikh_Bargiri_Az = Tarikh_Bargiri_Az.Substring(0, 4) + "/" + Tarikh_Bargiri_Az.Substring(4, 2) + "/" + Tarikh_Bargiri_Az.Substring(6, 2);
            Tarikh_Bargiri_Ta = Tarikh_Bargiri_Ta.Substring(0, 4) + "/" + Tarikh_Bargiri_Ta.Substring(4, 2) + "/" + Tarikh_Bargiri_Ta.Substring(6, 2);
            JArray data = new JArray();
            classdata db = new classdata();
            string query1 = string.Format(@"SELECT       Code_bargiri, tbl_PT.Pelak as LPPostId,tbl_PT.Ghodrat_PT as PostCapacity
                                            , dbo.ShamsiToMiladi(TBL_Bargiri.Tarikh_Bargiri) as LoadDT,TBL_Bargiri.Tarikh_Bargiri as LoadDateTimePersian,TBL_Bargiri.Saat_Bargiri as LoadTime
                                            ,TBL_Bargiri_Fider_Post.SathMaghta1 as FazSatheMaghtaId,TBL_Bargiri_Fider_Post.Tedad_Madar as CountFazSatheMaghta,'0' as NolSatheMaghtaId,'0' as CountNolSatheMaghta,
                                             Bar_Motevaset as PostPeakCurrent,Amparj_FazNol_R as RCurrent,Amparj_FazNol_S as SCurrent,Amparj_FazNol_T as TCurrent,
                                             Amparj_FazNol_N as NolCurrent,TBL_Bargiri_Fider_Post.Ghodrat_Kilid as KelidCurrent,
                                             Voltaj_Khat_RS as vRS,Voltaj_Khat_ST as vTS, Voltaj_Khat_TR as vTR,Voltaj_Faz_R as vrn,Voltaj_Faz_T as vTN,Voltaj_Faz_S as vSN,
                                             TBL_Bargiri_Fider_Post.Name_fider as LPFeederName,TBL_Bargiri_Fider_Post.ID_Fider,Code_Fider121
                                             FROM    TBL_Bargiri INNER JOIN
                                             TBL_Bargiri_Fider_Post ON TBL_Bargiri.ID_Fider = TBL_Bargiri_Fider_Post.ID_Fider INNER JOIN
                                             tbl_PT ON TBL_Bargiri_Fider_Post.Code_Post = tbl_PT.Code_PT
						                     where Tarikh_Bargiri>='{0}'   and Tarikh_Bargiri<='{1}' and Code_ZamanBandi is not  null  and TBL_Bargiri_Fider_Post.No=1
                                             ORDER BY TBL_Bargiri.Tarikh_Bargiri DESC", Tarikh_Bargiri_Az.ToString(), Tarikh_Bargiri_Ta.ToString());
            //and (TBL_Bargiri.Flag_ErsalBe121 is  null or  TBL_Bargiri.Flag_ErsalBe121<>1)
            DataTable dt_Bargiri = db.SetTable(new System.Data.SqlClient.SqlCommand(query1));
            List < Models.BargiriPt > BargiriPt = new List< Models.BargiriPt > ();
            for (int i = 0; i < dt_Bargiri.Rows.Count; i++)
            {
                Models.BargiriPt cls_BargiriPt = new  Models.BargiriPt();

                //cls_BargiriPt.Code_bargiri = Convert.ToInt32(dt_Bargiri.Rows[i]["Code_bargiri"]);
                cls_BargiriPt.LPPostId = dt_Bargiri.Rows[i]["LPPostId"].ToString();
                cls_BargiriPt.PostCapacity = Convert.ToDouble(dt_Bargiri.Rows[i]["PostCapacity"]);
                System.Globalization.PersianCalendar calende = new System.Globalization.PersianCalendar();
                
                cls_BargiriPt.LoadDT = dt_Bargiri.Rows[i]["LoadDT"].ToString();
                cls_BargiriPt.LoadDateTimePersian = dt_Bargiri.Rows[i]["LoadDateTimePersian"].ToString();
                cls_BargiriPt.LoadTime = dt_Bargiri.Rows[i]["LoadTime"].ToString();
                cls_BargiriPt.FazSatheMaghta =dt_Bargiri.Rows[i]["FazSatheMaghtaId"].ToString();
                cls_BargiriPt.CountFazSatheMaghta = Convert.ToInt32(dt_Bargiri.Rows[i]["CountFazSatheMaghta"]);
                cls_BargiriPt.NolSatheMaghta = dt_Bargiri.Rows[i]["NolSatheMaghtaId"].ToString();
                cls_BargiriPt.CountNolSatheMaghta = Convert.ToInt32(dt_Bargiri.Rows[i]["CountNolSatheMaghta"]);
                cls_BargiriPt.PostPeakCurrent = Convert.ToDouble(dt_Bargiri.Rows[i]["PostPeakCurrent"]);
                cls_BargiriPt.RCurrent = Convert.ToDouble(dt_Bargiri.Rows[i]["RCurrent"].ToString());
                cls_BargiriPt.SCurrent = Convert.ToDouble(dt_Bargiri.Rows[i]["SCurrent"]);
                cls_BargiriPt.TCurrent = Convert.ToDouble(dt_Bargiri.Rows[i]["TCurrent"]);
                cls_BargiriPt.NolCurrent = Convert.ToDouble(dt_Bargiri.Rows[i]["NolCurrent"]);
                cls_BargiriPt.KelidCurrent = dt_Bargiri.Rows[i]["KelidCurrent"].ToString();
                //cls_BargiriPt.DEDT = dt_Bargiri.Rows[i]["DEDT"].ToString();
                //cls_BargiriPt.DEDatePersian = dt_Bargiri.Rows[i]["DEDatePersian"].ToString();
                //cls_BargiriPt.DETime = dt_Bargiri.Rows[i]["DETime"].ToString();
                //  cls_BargiriPt.AreaUserId = Convert.ToInt32(dt_Bargiri.Rows[i]["AreaUserId"]);
                cls_BargiriPt.vRS = Convert.ToInt32(dt_Bargiri.Rows[i]["vRS"]);
                cls_BargiriPt.vTS = Convert.ToInt32(dt_Bargiri.Rows[i]["vTS"]);
                cls_BargiriPt.vTR = Convert.ToInt32(dt_Bargiri.Rows[i]["vTR"]);
                cls_BargiriPt.vRN = Convert.ToInt32(dt_Bargiri.Rows[i]["vRN"]);
                cls_BargiriPt.vTN = Convert.ToInt32(dt_Bargiri.Rows[i]["vTN"]);
                cls_BargiriPt.vSN = Convert.ToInt32(dt_Bargiri.Rows[i]["vSN"]);
                //cls_BargiriPt.IsTakFaze = Convert.ToInt32(dt_Bargiri.Rows[i]["IsTakFaze"]);
                //cls_BargiriPt.EarthValue = Convert.ToDouble(dt_Bargiri.Rows[i]["EarthValue"].ToString());
                //cls_BargiriPt.EarthValueE = Convert.ToDouble(dt_Bargiri.Rows[i]["EarthValueE"]);
                //cls_BargiriPt.vTS = Convert.ToInt32(dt_Bargiri.Rows[i]["vTS"]);
                //cls_BargiriPt.IsLowCasualties = Convert.ToInt32(dt_Bargiri.Rows[i]["IsLowCasualties"]);
                //cls_BargiriPt.LPFeederName = dt_Bargiri.Rows[i]["LPFeederName"].ToString();
                //cls_BargiriPt.LPFeederID = dt_Bargiri.Rows[i]["Code_Fider121"].ToString();
                JToken json = JToken.Parse (JsonConvert.SerializeObject(cls_BargiriPt));
                BargiriPt.Add(cls_BargiriPt);
                data.Add(json);

                string query = string.Format("DECLARE @Status bit BEGIN TRY BEGIN TRANSACTION UPDATE TBL_Bargiri SET  Flag_ErsalBe121= {0} WHERE [Code_bargiri] = {1} UPDATE TBL_Bargiri_Fider_Post SET  Flag_ErsalBe121= {0} WHERE [ID_Fider] = {2} COMMIT SET @Status = 1 END TRY BEGIN CATCH SET @Status = 0 IF(@@TRANCOUNT > 0) ROLLBACK END CATCH SELECT @Status", 1, dt_Bargiri.Rows[i]["Code_bargiri"].ToString(), Convert.ToInt32(dt_Bargiri.Rows[i]["ID_Fider"]));

                bool status = db.GetBoolean(new System.Data.SqlClient.SqlCommand(query));
                if (status)
                {

                }

                // db.DoExecute(new System.Data.SqlClient.SqlCommand(string.Format("UPDATE TBL_Bargiri SET  Flag_ErsalBe121= {0} WHERE [Code_bargiri] = {1}", 1, Convert.ToInt32(dt_Bargiri.Rows[i]["Code_bargiri"]))));
            }
            
            return data;
        }
        [HttpGet]
        [Route("api/Liste_Bargiri_Lp/{Tarikh_Bargiri_Az}/{Tarikh_Bargiri_Ta}")]
        public JArray GetBargiriLp(string Tarikh_Bargiri_Az, string Tarikh_Bargiri_Ta)
        {
            Tarikh_Bargiri_Az = Tarikh_Bargiri_Az.Substring(0, 4) + "/" + Tarikh_Bargiri_Az.Substring(4, 2) + "/" + Tarikh_Bargiri_Az.Substring(6, 2);
            Tarikh_Bargiri_Ta = Tarikh_Bargiri_Ta.Substring(0, 4) + "/" + Tarikh_Bargiri_Ta.Substring(4, 2) + "/" + Tarikh_Bargiri_Ta.Substring(6, 2);
            JArray data = new JArray();
            classdata db = new classdata();
            string query1 = string.Format(@"SELECT       Code_bargiri, tbl_PT.Pelak as LPPostId,tbl_PT.Ghodrat_PT as PostCapacity
                                            , dbo.ShamsiToMiladi(TBL_Bargiri.Tarikh_Bargiri) as LoadDT,TBL_Bargiri.Tarikh_Bargiri as LoadDateTimePersian,TBL_Bargiri.Saat_Bargiri as LoadTime
                                            ,TBL_Bargiri_Fider_Post.SathMaghta1 as FazSatheMaghtaId,TBL_Bargiri_Fider_Post.Tedad_Madar as CountFazSatheMaghta,'0' as NolSatheMaghtaId,'0' as CountNolSatheMaghta,
                                             Bar_Motevaset as PostPeakCurrent,Amparj_FazNol_R as RCurrent,Amparj_FazNol_S as SCurrent,Amparj_FazNol_T as TCurrent,
                                             Amparj_FazNol_N as NolCurrent,TBL_Bargiri_Fider_Post.Ghodrat_Kilid as KelidCurrent,
                                             Voltaj_Khat_RS as vRS,Voltaj_Khat_ST as vTS, Voltaj_Khat_TR as vTR,Voltaj_Faz_R as vrn,Voltaj_Faz_T as vTN,Voltaj_Faz_S as vSN,
                                             TBL_Bargiri_Fider_Post.Name_fider as LPFeederName,TBL_Bargiri_Fider_Post.ID_Fider,Code_Fider121
											 ,TBL_Bargiri_Fider_Post.Amparj_Fiuz_Mansobe_R as RFuse,TBL_Bargiri_Fider_Post.Amparj_Fiuz_Mansobe_S as SFuse,TBL_Bargiri_Fider_Post.Amparj_Fiuz_Mansobe_T as TFuse
                                             FROM    TBL_Bargiri INNER JOIN
                                             TBL_Bargiri_Fider_Post ON TBL_Bargiri.ID_Fider = TBL_Bargiri_Fider_Post.ID_Fider INNER JOIN
                                             tbl_PT ON TBL_Bargiri_Fider_Post.Code_Post = tbl_PT.Code_PT
						                     where Tarikh_Bargiri>='{0}'   and Tarikh_Bargiri<='{1}' and Code_ZamanBandi is not  null  and TBL_Bargiri_Fider_Post.No=2
                                             ORDER BY TBL_Bargiri.Tarikh_Bargiri DESC", Tarikh_Bargiri_Az.ToString(), Tarikh_Bargiri_Ta.ToString());
            //and (TBL_Bargiri.Flag_ErsalBe121 is  null or  TBL_Bargiri.Flag_ErsalBe121<>1)
            DataTable dt_Bargiri = db.SetTable(new System.Data.SqlClient.SqlCommand(query1));
            List<Models.BargiriLp> BargiriPt = new List<Models.BargiriLp>();
            for (int i = 0; i < dt_Bargiri.Rows.Count; i++)
            {
                Models.BargiriLp cls_BargiriPt = new Models.BargiriLp();

                //cls_BargiriPt.Code_bargiri = Convert.ToInt32(dt_Bargiri.Rows[i]["Code_bargiri"]);
                cls_BargiriPt.LPPostId = dt_Bargiri.Rows[i]["LPPostId"].ToString();
                cls_BargiriPt.PostCapacity = Convert.ToDouble(dt_Bargiri.Rows[i]["PostCapacity"]);
                System.Globalization.PersianCalendar calende = new System.Globalization.PersianCalendar();

                cls_BargiriPt.LoadDT = dt_Bargiri.Rows[i]["LoadDT"].ToString();
                cls_BargiriPt.LoadDateTimePersian = dt_Bargiri.Rows[i]["LoadDateTimePersian"].ToString();
                cls_BargiriPt.LoadTime = dt_Bargiri.Rows[i]["LoadTime"].ToString();
                cls_BargiriPt.FazSatheMaghta =dt_Bargiri.Rows[i]["FazSatheMaghtaId"].ToString();
                cls_BargiriPt.CountFazSatheMaghta = Convert.ToInt32(dt_Bargiri.Rows[i]["CountFazSatheMaghta"]);
                cls_BargiriPt.NolSatheMaghta = dt_Bargiri.Rows[i]["NolSatheMaghtaId"].ToString();
                cls_BargiriPt.CountNolSatheMaghta = Convert.ToInt32(dt_Bargiri.Rows[i]["CountNolSatheMaghta"]);
                cls_BargiriPt.PostPeakCurrent = Convert.ToDouble(dt_Bargiri.Rows[i]["PostPeakCurrent"]);
                cls_BargiriPt.RCurrent = Convert.ToDouble(dt_Bargiri.Rows[i]["RCurrent"].ToString());
                cls_BargiriPt.SCurrent = Convert.ToDouble(dt_Bargiri.Rows[i]["SCurrent"]);
                cls_BargiriPt.TCurrent = Convert.ToDouble(dt_Bargiri.Rows[i]["TCurrent"]);
                cls_BargiriPt.NolCurrent = Convert.ToDouble(dt_Bargiri.Rows[i]["NolCurrent"]);
                cls_BargiriPt.KelidCurrent = dt_Bargiri.Rows[i]["KelidCurrent"].ToString();
                cls_BargiriPt.RFuse = dt_Bargiri.Rows[i]["RFuse"].ToString();
                cls_BargiriPt.SFuse = dt_Bargiri.Rows[i]["SFuse"].ToString();
                cls_BargiriPt.TFuse = dt_Bargiri.Rows[i]["TFuse"].ToString();
                //cls_BargiriPt.DEDT = dt_Bargiri.Rows[i]["DEDT"].ToString();
                //cls_BargiriPt.DEDatePersian = dt_Bargiri.Rows[i]["DEDatePersian"].ToString();
                //cls_BargiriPt.DETime = dt_Bargiri.Rows[i]["DETime"].ToString();
                //  cls_BargiriPt.AreaUserId = Convert.ToInt32(dt_Bargiri.Rows[i]["AreaUserId"]);
                cls_BargiriPt.vRS = Convert.ToInt32(dt_Bargiri.Rows[i]["vRS"]);
                cls_BargiriPt.vTS = Convert.ToInt32(dt_Bargiri.Rows[i]["vTS"]);
                cls_BargiriPt.vTR = Convert.ToInt32(dt_Bargiri.Rows[i]["vTR"]);
                cls_BargiriPt.vRN = Convert.ToInt32(dt_Bargiri.Rows[i]["vRN"]);
                cls_BargiriPt.vTN = Convert.ToInt32(dt_Bargiri.Rows[i]["vTN"]);
                cls_BargiriPt.vSN = Convert.ToInt32(dt_Bargiri.Rows[i]["vSN"]);
                //cls_BargiriPt.IsTakFaze = Convert.ToInt32(dt_Bargiri.Rows[i]["IsTakFaze"]);
                //cls_BargiriPt.EarthValue = Convert.ToDouble(dt_Bargiri.Rows[i]["EarthValue"].ToString());
                //cls_BargiriPt.EarthValueE = Convert.ToDouble(dt_Bargiri.Rows[i]["EarthValueE"]);
                //cls_BargiriPt.vTS = Convert.ToInt32(dt_Bargiri.Rows[i]["vTS"]);
                //cls_BargiriPt.IsLowCasualties = Convert.ToInt32(dt_Bargiri.Rows[i]["IsLowCasualties"]);
                cls_BargiriPt.LPFeederName = dt_Bargiri.Rows[i]["LPFeederName"].ToString();
                cls_BargiriPt.LPFeederID = dt_Bargiri.Rows[i]["Code_Fider121"].ToString();
                JToken json = JToken.Parse(JsonConvert.SerializeObject(cls_BargiriPt));
                BargiriPt.Add(cls_BargiriPt);
                data.Add(json);

                string query = string.Format("DECLARE @Status bit BEGIN TRY BEGIN TRANSACTION UPDATE TBL_Bargiri SET  Flag_ErsalBe121= {0} WHERE [Code_bargiri] = {1} UPDATE TBL_Bargiri_Fider_Post SET  Flag_ErsalBe121= {0} WHERE [ID_Fider] = {2} COMMIT SET @Status = 1 END TRY BEGIN CATCH SET @Status = 0 IF(@@TRANCOUNT > 0) ROLLBACK END CATCH SELECT @Status", 1, dt_Bargiri.Rows[i]["Code_bargiri"].ToString(), Convert.ToInt32(dt_Bargiri.Rows[i]["ID_Fider"]));

                bool status = db.GetBoolean(new System.Data.SqlClient.SqlCommand(query));
                if (status)
                {

                }

                // db.DoExecute(new System.Data.SqlClient.SqlCommand(string.Format("UPDATE TBL_Bargiri SET  Flag_ErsalBe121= {0} WHERE [Code_bargiri] = {1}", 1, Convert.ToInt32(dt_Bargiri.Rows[i]["Code_bargiri"]))));
            }

            return data;
        }
    }
}
