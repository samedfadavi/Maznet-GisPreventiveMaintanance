using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pmService.Models
{
    public class UserTask
    {
        System.Globalization.PersianCalendar PersianCalendar = new System.Globalization.PersianCalendar();
        public string id { get; set; }
        public string name { get; set; }
        public string assignee { get; set; }
        public DateTime created { get; set; }
        public string createdshamsi { get { return PersianCalendar.GetYear(created).ToString() + "/" + PersianCalendar.GetMonth(created).ToString() + "/" + PersianCalendar.GetDayOfMonth(created).ToString() + " " + created.Hour.ToString() + ":" + created.Minute.ToString(); } }
        public object due { get; set; }
        public object followUp { get; set; }
        public DateTime lastUpdated { get; set; }
        public object delegationState { get; set; }
        public object description { get; set; }
        public string executionId { get; set; }
        public object owner { get; set; }
        public object parentTaskId { get; set; }
        public int priority { get; set; }
        public string processDefinitionId { get; set; }
        public string processInstanceId { get; set; }
        public string taskDefinitionKey { get; set; }
        public object caseExecutionId { get; set; }
        public object caseInstanceId { get; set; }
        public object caseDefinitionId { get; set; }
        public bool suspended { get; set; }
        public string formKey { get; set; }
        public object camundaFormRef { get; set; }
        public object tenantId { get; set; }
    }

}
