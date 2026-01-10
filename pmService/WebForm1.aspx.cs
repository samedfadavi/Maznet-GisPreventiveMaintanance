using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace pmService
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Update_PayehFFZ()
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new System.Uri("http://10.150.145.35:8181/api/");

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string contentValue = @"{""UserName"": ""pmuser"",""Password"": ""v9ChTgQqFjE="",""RequestType"":""18""}";
            System.Net.Http.HttpContent content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.PostAsync("GenerateToken", content).Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(result);
                string token = json.GetValue("Token").ToString();
                contentValue =
@"{""Token"":""{0}"",
""StartDate"":""2024-01-17"",
""EndDate"":""2024-04-02"",
""MethodType"":""List"",
""Layers"":
[
{
""GISTableCode"":""LV_POLE"",
""Properties"":[{""EnglishName"":""Mfctrd_Yr""},{""EnglishName"":""Pole_Hght""},{""EnglishName"":""Atr_Pw""}]
}
]
}";
                contentValue = contentValue.Replace("{0}", token);
                string salesakht = "0", ertefa = "0", keshesh = "0";
                content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
                messge = client.PostAsync("GetGISData", content).Result;

                if (messge.IsSuccessStatusCode)
                {
                    result = messge.Content.ReadAsStringAsync().Result;
                    json = JObject.Parse(result);
                    JArray data = JArray.Parse(json.GetValue("Data").ToString());
                    
                    foreach (JObject fileds in data)
                    {
                        string f_id = fileds.GetValue("FK_GISCode").ToString();
                        string gis_code = fileds.GetValue("GISCode").ToString();
                        string hoze = fileds.GetValue("ZoneID").ToString();
                        string globalid = fileds.GetValue("GISCode").ToString();
                        JArray props = JArray.Parse(fileds.GetValue("Properties").ToString());
                        foreach (JObject feature in props)
                        {
                            string propname = feature.GetValue("EnglishName").ToString();
                            string propval = feature.GetValue("PropertyValue").ToString();
                            switch (propname)
                            {
                                case "Mfctrd_Yr":
                                    salesakht = propval;
                                    break;
                                case "Pole_Hght":
                                    ertefa = propval;
                                    break;
                                case "Atr_Pw":
                                    keshesh = propval;
                                    break;
                                default:
                                    break;
                            }


                        }



                        contentValue = @"{""Token"":""{0}"",
""EPSG"":""4326"",
""GISTableCode"":""LV_POLE"",
""Features"":
[{""GISCode"":""{1}""}]

}";
                        contentValue = contentValue.Replace("{0}", token);
                        contentValue = contentValue.Replace("{1}", gis_code);
                        content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
                        messge = client.PostAsync("GetGeoData", content).Result;
                        if (messge.IsSuccessStatusCode)
                        {
                            result = messge.Content.ReadAsStringAsync().Result;
                            json = JObject.Parse(result);
                            JObject Data = JObject.Parse(json.GetValue("Data").ToString());
                            if (Data.GetValue("Features").ToString() != "")
                            {
                                JArray features = JArray.Parse(Data.GetValue("Features").ToString());
                                foreach (JObject feature in features)
                                {
                                    JToken geo = feature.GetValue("Geometry");
                                    JObject geobj = JObject.Parse(feature.GetValue("Geometry").ToString());
                                    string coor = geobj.GetValue("coordinates").ToString();
                                    classdata classdata = new classdata();
                                    classdata.AddWithProcedure("UpdatePayehffzONGIS", new string[] { "Name_Payeh", "Mokhtasat", "Code_FFM", "Hoze", "Salesakht", "Ertefa", "Keshesh" }, new string[] { gis_code, coor, f_id, hoze, salesakht, ertefa, keshesh });
                                }
                            }



                        }


                    }
                }

            }

        }
        protected  void Update_PayehFFM()
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new System.Uri("http://10.150.145.35:8181/api/");

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string contentValue = @"{""UserName"": ""pmuser"",""Password"": ""v9ChTgQqFjE="",""RequestType"":""18""}";
            System.Net.Http.HttpContent content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.PostAsync("GenerateToken", content).Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(result);
                string token = json.GetValue("Token").ToString();
                contentValue =
@"{""Token"":""{0}"",
""StartDate"":""2024-01-17"",
""EndDate"":""2024-04-02"",
""MethodType"":""List"",
""Layers"":
[
{
""GISTableCode"":""MV_POLE"",
""Properties"":[{""EnglishName"":""Mnfctrd_Yr""},{""EnglishName"":""Pole_Hght""},{""EnglishName"":""Atr_Pw""}]
}
]
}";
                contentValue = contentValue.Replace("{0}", token);
                string salesakht = "0", ertefa = "0", keshesh = "0";
                content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
               messge = client.PostAsync("GetGISData", content).Result;

                if (messge.IsSuccessStatusCode)
                {
                    result = messge.Content.ReadAsStringAsync().Result;
                    json = JObject.Parse(result);
                    JArray data = JArray.Parse(json.GetValue("Data").ToString());
                    foreach (JObject fileds in data)
                    {
                        string f_id = fileds.GetValue("FK_GISCode").ToString();
                        string gis_code = fileds.GetValue("GISCode").ToString();
                        string hoze = fileds.GetValue("ZoneID").ToString();
                        string globalid = fileds.GetValue("GISCode").ToString();
                        JArray props = JArray.Parse(fileds.GetValue("Properties").ToString());
                        foreach (JObject feature in props)
                        {
                            string propname = feature.GetValue("EnglishName").ToString();
                            string propval = feature.GetValue("PropertyValue").ToString();
                            switch (propname)
                            {
                                case "Mnfctrd_Yr":
                                    salesakht = propval;
                                    break;
                                case "Pole_Hght":
                                    ertefa = propval;
                                    break;
                                case "Atr_Pw":
                                    keshesh = propval;
                                    break;
                                default:
                                    break;
                            }


                        }



                        contentValue = @"{""Token"":""{0}"",
""EPSG"":""4326"",
""GISTableCode"":""MV_POLE"",
""Features"":
[{""GISCode"":""{1}""}]

}";
                        contentValue = contentValue.Replace("{0}", token);
                        contentValue = contentValue.Replace("{1}", gis_code);
                        content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
                        messge = client.PostAsync("GetGeoData", content).Result;
                        if (messge.IsSuccessStatusCode)
                        {
                            result = messge.Content.ReadAsStringAsync().Result;
                            json = JObject.Parse(result);
                            JObject Data = JObject.Parse(json.GetValue("Data").ToString());
                            if (Data.GetValue("Features").ToString() != "")
                                {
                                JArray features = JArray.Parse(Data.GetValue("Features").ToString());
                                foreach (JObject feature in features)
                                {
                                    JToken geo = feature.GetValue("Geometry");
                                    JObject geobj = JObject.Parse(feature.GetValue("Geometry").ToString());
                                    string coor = geobj.GetValue("coordinates").ToString();
                                    classdata classdata = new classdata();
                                    classdata.AddWithProcedure("UpdatePayehffmONGIS", new string[] { "Name_Payeh", "Mokhtasat", "Code_FFM", "Hoze", "Salesakht", "Ertefa", "Keshesh" }, new string[] { gis_code, coor, f_id, hoze, salesakht, ertefa, keshesh });
                                }
                            }



                        }


                    }
                }

            }
            Controllers.EkipController ek = new Controllers.EkipController();
            string query = "select    code_ekip from  tbl_Ekip where tbl_ekip.UserName='q' and tbl_ekip.Password='q' ";
            string code_ekip = ek.GetAppVersion().ToString();
            /*string query = "select   *  from tbl_tasvireirad ";// where code_Zamanbandi=(select top 1 code_zamanbandi from Tbl_jozeiatezamanbandibazdid inner join tbl_Ekip on Tbl_jozeiatezamanbandibazdid.code_ekip=tbl_Ekip.Code_Ekip where tbl_ekip.UserName='bargh4' and tbl_ekip.Password='123' order by code_zamanbandi desc)  ";
            classdata data = new classdata();
            SqlDataReader  s = data.BindDataReader(query);
            int i = 4;
            while (s.Read())
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),i.ToString()+ ".png");
                i++;
               

                FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);

              Stream  data1 = s.GetStream(1);


                // Asynchronously copy the stream from the server to the file we just created
                data1.CopyTo(file);
                file.Close();
                    }
            s.Close();*/
        }

        protected void Update_PayehFFM_Feeder()
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new System.Uri("http://10.150.145.35:8181/api/");

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string contentValue = @"{""UserName"": ""pmuser"",""Password"": ""v9ChTgQqFjE="",""RequestType"":""18""}";
            System.Net.Http.HttpContent content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.PostAsync("GenerateToken", content).Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(result);
                string token = json.GetValue("Token").ToString();
                classdata cls = new classdata();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select NamePayeh,code_ffm,Hozeh from Tbl_Payehffm";
               System.Data.DataTable dt= cls.SetTable(cmd);

                string salesakht = "0", ertefa = "0", keshesh = "0";

                

                    foreach (System.Data.DataRow row in dt.Rows)
                    {

                    string gis_code = row["Namepayeh"].ToString();
                    string f_id = row["code_ffm"].ToString();
                    string hoze = row["hozeh"].ToString();

                    contentValue = @"{""Token"":""{0}"",
""EPSG"":""4326"",
""GISTableCode"":""MV_POLE"",
""Features"":
[{""GISCode"":""{1}""}]

}";
                        contentValue = contentValue.Replace("{0}", token);
                        contentValue = contentValue.Replace("{1}", gis_code);
                        content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
                        messge = client.PostAsync("GetGeoData", content).Result;
                        if (messge.IsSuccessStatusCode)
                        {
                            result = messge.Content.ReadAsStringAsync().Result;
                            json = JObject.Parse(result);
                            JObject Data = JObject.Parse(json.GetValue("Data").ToString());
                            JArray features = JArray.Parse(Data.GetValue("Features").ToString());
                            foreach (JObject feature in features)
                            {
                                JToken geo = feature.GetValue("Geometry");
                                JObject geobj = JObject.Parse(feature.GetValue("Geometry").ToString());
                                string coor = geobj.GetValue("coordinates").ToString();
                                classdata classdata = new classdata();
                                classdata.AddWithProcedure("UpdatePayehffmONGIS", new string[] { "Name_Payeh", "Mokhtasat", "Code_FFM", "Hoze", "Salesakht", "Ertefa", "Keshesh" }, new string[] { gis_code, coor, f_id, hoze, salesakht, ertefa, keshesh });
                            }



                        }


                    }
                }

            
        }
        protected void Update_Pt_Havaee()
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new System.Uri("http://10.150.145.35:8181/api/");

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string contentValue = @"{""UserName"": ""pmuser"",""Password"": ""v9ChTgQqFjE="",""RequestType"":""18""}";
            System.Net.Http.HttpContent content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.PostAsync("GenerateToken", content).Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(result);
                string token = json.GetValue("Token").ToString();
                contentValue =
@"{""Token"":""{0}"",
""StartDate"":""2024-07-17"",
""EndDate"":""2024-08-19"",
""MethodType"":""List"",
""Layers"":
[
{
""GISTableCode"":""Pl_MDSUB"",
""Properties"":[{""EnglishName"":""Pl_MDS_Nam""},{""EnglishName"":""Post_Type""},{""EnglishName"":""Mnfctrd_Yr""},{""EnglishName"":""Loc_Adres""}] 
}
]
}";
                contentValue = contentValue.Replace("{0}", token);
                string salesakht = "0", address = "0", onvan = "0";
                content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
                messge = client.PostAsync("GetGISData", content).Result;

                if (messge.IsSuccessStatusCode)
                {
                    result = messge.Content.ReadAsStringAsync().Result;
                    json = JObject.Parse(result);
                    JArray data = JArray.Parse(json.GetValue("Data").ToString());
                    foreach (JObject fileds in data)
                    {
                        string f_id = fileds.GetValue("FK_GISCode").ToString();
                        string gis_code = fileds.GetValue("GISCode").ToString();
                        string hoze = fileds.GetValue("ZoneID").ToString();
                        string globalid = fileds.GetValue("GISCode").ToString();
                        JArray props = JArray.Parse(fileds.GetValue("Properties").ToString());
                        foreach (JObject feature in props)
                        {
                            string propname = feature.GetValue("EnglishName").ToString();
                            string propval = feature.GetValue("PropertyValue").ToString();
                            switch (propname)
                            {
                                case "Mnfctrd_Yr":
                                    salesakht = propval;
                                    break;
                                case "Loc_Adres":
                                    address = propval;
                                    break;
                                case "Pl_MDS_Nam":
                                    onvan = propval;
                                    break;
                                default:
                                    break;
                            }


                        }



                        contentValue = @"{""Token"":""{0}"",
""EPSG"":""4326"",
""GISTableCode"":""PL_MDSUB"",
""Features"":
[{""GISCode"":""{1}""}]

}";
                        contentValue = contentValue.Replace("{0}", token);
                        contentValue = contentValue.Replace("{1}", gis_code);
                        content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
                        messge = client.PostAsync("GetGeoData", content).Result;
                        if (messge.IsSuccessStatusCode)
                        {
                            result = messge.Content.ReadAsStringAsync().Result;
                            json = JObject.Parse(result);
                            JObject Data = JObject.Parse(json.GetValue("Data").ToString());
                            if (Data.GetValue("Features").ToString() != "")
                            {
                                JArray features = JArray.Parse(Data.GetValue("Features").ToString());
                                foreach (JObject feature in features)
                                {
                                    JToken geo = feature.GetValue("Geometry");
                                    JObject geobj = JObject.Parse(feature.GetValue("Geometry").ToString());
                                    string coor = geobj.GetValue("coordinates").ToString();
                                    classdata classdata = new classdata();
                                    classdata.AddWithProcedure("UpdatePtONGIS", new string[] { "Code_Post", "Mokhtasat", "Code_FFM", "Hoze", "Onvan", "Address" }, new string[] { gis_code, coor, f_id, hoze, onvan, address });
                                }
                            }


                        }


                    }
                }

            }
            
            /*string query = "select   *  from tbl_tasvireirad ";// where code_Zamanbandi=(select top 1 code_zamanbandi from Tbl_jozeiatezamanbandibazdid inner join tbl_Ekip on Tbl_jozeiatezamanbandibazdid.code_ekip=tbl_Ekip.Code_Ekip where tbl_ekip.UserName='bargh4' and tbl_ekip.Password='123' order by code_zamanbandi desc)  ";
            classdata data = new classdata();
            SqlDataReader  s = data.BindDataReader(query);
            int i = 4;
            while (s.Read())
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),i.ToString()+ ".png");
                i++;
               

                FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);

              Stream  data1 = s.GetStream(1);


                // Asynchronously copy the stream from the server to the file we just created
                data1.CopyTo(file);
                file.Close();
                    }
            s.Close();*/
        }
        protected void Update_Pt_Zamini()
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new System.Uri("http://10.150.145.35:8181/api/");

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string contentValue = @"{""UserName"": ""pmuser"",""Password"": ""v9ChTgQqFjE="",""RequestType"":""18""}";
            System.Net.Http.HttpContent content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.PostAsync("GenerateToken", content).Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(result);
                string token = json.GetValue("Token").ToString();
                contentValue =
@"{""Token"":""{0}"",
""StartDate"":""2024-01-17"",
""EndDate"":""2024-04-02"",
""MethodType"":""List"",
""Layers"":
[
{
""GISTableCode"":""Pd_MDSUB"",
""Properties"":[{""EnglishName"":""Pd_MDS_Nam""},{""EnglishName"":""Post_Type""},{""EnglishName"":""Mnfctrd_Yr""},{""EnglishName"":""Loc_Adr""}] 
}
]
}";
                contentValue = contentValue.Replace("{0}", token);
                string salesakht = "0", address = "0", onvan = "0";
                content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
                messge = client.PostAsync("GetGISData", content).Result;

                if (messge.IsSuccessStatusCode)
                {
                    result = messge.Content.ReadAsStringAsync().Result;
                    json = JObject.Parse(result);
                    JArray data = JArray.Parse(json.GetValue("Data").ToString());
                    foreach (JObject fileds in data)
                    {
                        string f_id = fileds.GetValue("FK_GISCode").ToString();
                        string gis_code = fileds.GetValue("GISCode").ToString();
                        string hoze = fileds.GetValue("ZoneID").ToString();
                        string globalid = fileds.GetValue("GISCode").ToString();
                        JArray props = JArray.Parse(fileds.GetValue("Properties").ToString());
                        foreach (JObject feature in props)
                        {
                            string propname = feature.GetValue("EnglishName").ToString();
                            string propval = feature.GetValue("PropertyValue").ToString();
                            switch (propname)
                            {
                                case "Mnfctrd_Yr":
                                    salesakht = propval;
                                    break;
                                case "Loc_Adr":
                                    address = propval;
                                    break;
                                case "Pd_MDS_Nam":
                                    onvan = propval;
                                    break;
                                default:
                                    break;
                            }


                        }



                        contentValue = @"{""Token"":""{0}"",
""EPSG"":""4326"",
""GISTableCode"":""Pd_MDSUB"",
""Features"":
[{""GISCode"":""{1}""}]

}";
                        contentValue = contentValue.Replace("{0}", token);
                        contentValue = contentValue.Replace("{1}", gis_code);
                        content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
                        messge = client.PostAsync("GetGeoData", content).Result;
                        if (messge.IsSuccessStatusCode)
                        {
                            result = messge.Content.ReadAsStringAsync().Result;
                            json = JObject.Parse(result);
                            JObject Data = JObject.Parse(json.GetValue("Data").ToString());
                            JArray features = JArray.Parse(Data.GetValue("Features").ToString());
                            foreach (JObject feature in features)
                            {
                                JToken geo = feature.GetValue("Geometry");
                                JObject geobj = JObject.Parse(feature.GetValue("Geometry").ToString());
                                string coor = geobj.GetValue("coordinates").ToString();
                                classdata classdata = new classdata();
                                classdata.AddWithProcedure("UpdatePtONGIS2", new string[] { "Code_Post", "Mokhtasat", "Code_FFM", "Hoze", "Onvan", "Address" }, new string[] { gis_code, coor, f_id, hoze, onvan, address });
                            }



                        }


                    }
                }

            }

            /*string query = "select   *  from tbl_tasvireirad ";// where code_Zamanbandi=(select top 1 code_zamanbandi from Tbl_jozeiatezamanbandibazdid inner join tbl_Ekip on Tbl_jozeiatezamanbandibazdid.code_ekip=tbl_Ekip.Code_Ekip where tbl_ekip.UserName='bargh4' and tbl_ekip.Password='123' order by code_zamanbandi desc)  ";
            classdata data = new classdata();
            SqlDataReader  s = data.BindDataReader(query);
            int i = 4;
            while (s.Read())
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),i.ToString()+ ".png");
                i++;
               

                FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);

              Stream  data1 = s.GetStream(1);


                // Asynchronously copy the stream from the server to the file we just created
                data1.CopyTo(file);
                file.Close();
                    }
            s.Close();*/
        }
        protected void Update_Feeder()
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new System.Uri("http://10.150.145.35:8181/api/");

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string contentValue = @"{""UserName"": ""pmuser"",""Password"": ""v9ChTgQqFjE="",""RequestType"":""18""}";
            System.Net.Http.HttpContent content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.PostAsync("GenerateToken", content).Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(result);
                string token = json.GetValue("Token").ToString();
                contentValue =
@"{""Token"":""{0}"",
""StartDate"":""2023-07-12"",
""EndDate"":""2023-10-27"",
""MethodType"":""List"",
""Layers"":
[
{
""GISTableCode"":""Feeder"",
""Properties"":[{""EnglishName"":""ALL""}]
}
]
}";
                contentValue = contentValue.Replace("{0}", token);
                string noe_feeder = "0", name_feeder = "0", toihat = "0";
                content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
                messge = client.PostAsync("GetGISData", content).Result;

                if (messge.IsSuccessStatusCode)
                {
                    result = messge.Content.ReadAsStringAsync().Result;
                    json = JObject.Parse(result);
                    JArray data = JArray.Parse(json.GetValue("Data").ToString());
                    foreach (JObject fileds in data)
                    {
                        string f_id = fileds.GetValue("FK_GISCode").ToString();
                        string gis_code = fileds.GetValue("GISCode").ToString();
                        string hoze = fileds.GetValue("ZoneID").ToString();
                        string globalid = fileds.GetValue("GISCode").ToString();
                        string ActionType = fileds.GetValue("ActionType").ToString();
                        JArray props = JArray.Parse(fileds.GetValue("Properties").ToString());
                        foreach (JObject feature in props)
                        {
                            string propname = feature.GetValue("EnglishName").ToString();
                            string propval = feature.GetValue("PropertyValue").ToString();
                            switch (propname)
                            {
                                case "Fed_Class":
                                    noe_feeder = propval;
                                    break;
                                case "Fed_nam":
                                    name_feeder = propval;
                                    break;
                                case "Dscription":
                                    toihat = propval;
                                    break;
                                default:
                                    break;
                            }


                        }


                        if (ActionType == "1" && noe_feeder!= "LvFeeder" && f_id!="")
                        {
                            contentValue = @"{""Token"":""{0}"",
""EPSG"":""4326"",
""GISTableCode"":""Feeder"",
""Features"":
[{""GISCode"":""{1}""}]

}";
                            contentValue = contentValue.Replace("{0}", token);
                            contentValue = contentValue.Replace("{1}", gis_code);
                            content = new StringContent(contentValue, UTF8Encoding.UTF8, "application/json");
                            messge = client.PostAsync("GetGeoData", content).Result;
                            if (messge.IsSuccessStatusCode)
                            {
                                result = messge.Content.ReadAsStringAsync().Result;
                                json = JObject.Parse(result);
                                JObject Data = JObject.Parse(json.GetValue("Data").ToString());
                                
                                JArray features = JArray.Parse(Data.GetValue("Features").ToString());
                                foreach (JObject feature in features)
                                {
                                    JToken geo = feature.GetValue("Geometry");
                                    JObject geobj = JObject.Parse(feature.GetValue("Geometry").ToString());
                                    string coor = geobj.GetValue("coordinates").ToString();
                                    classdata classdata = new classdata();
                                    //classdata.AddWithProcedure("UpdatePayehffmONGIS", new string[] { "Name_Payeh", "Mokhtasat", "Code_FFM", "Hoze", "Salesakht", "Ertefa", "Keshesh" }, new string[] { gis_code, coor, f_id, hoze, salesakht, ertefa, keshesh });
                                }



                            }
                        }


                    }
                }

            }
            
            string query = "select    code_ekip from  tbl_Ekip where tbl_ekip.UserName='q' and tbl_ekip.Password='q' ";
            //string code_ekip = ek.GetAppVersion().ToString();
            /*string query = "select   *  from tbl_tasvireirad ";// where code_Zamanbandi=(select top 1 code_zamanbandi from Tbl_jozeiatezamanbandibazdid inner join tbl_Ekip on Tbl_jozeiatezamanbandibazdid.code_ekip=tbl_Ekip.Code_Ekip where tbl_ekip.UserName='bargh4' and tbl_ekip.Password='123' order by code_zamanbandi desc)  ";
            classdata data = new classdata();
            SqlDataReader  s = data.BindDataReader(query);
            int i = 4;
            while (s.Read())
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),i.ToString()+ ".png");
                i++;
               

                FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);

              Stream  data1 = s.GetStream(1);


                // Asynchronously copy the stream from the server to the file we just created
                data1.CopyTo(file);
                file.Close();
                    }
            s.Close();*/
        }
        protected async void Page_Load(object sender, EventArgs e)
        {

            //Update_Pt_Havaee();
            //Update_Pt_Zamini();
          
            Controllers.Derakht_TajhizatController ekip = new Controllers.Derakht_TajhizatController();
           var res=await ekip.ExecuteDynamicQueryAsync(new Models.MaznetModel(),"tbl_payehffm", "  where Code_FFM in ( select Code_QFFM from tbl_QFFM where Fider in (select Code_FFM from tbl_FFM where Pft in (select Code_pft from tbl_PFT where Omoor in (select Code_Omoor from tbl_Omoor where Code_Omoor in(88)))))");
            
        }
    }
}