namespace pmService.Models
{
    public class BargiriPt
    {        
        public string LPPostId { get; set; }
        public double PostCapacity { get; set; }
        public string LoadDT { get; set; }
        public string LoadDateTimePersian { get; set; }
        public string LoadTime { get; set; }
        public string FazSatheMaghta { get; set; }
        public int CountFazSatheMaghta { get; set; }
        public string NolSatheMaghta { get; set; }
        public int CountNolSatheMaghta { get; set; }
        public double PostPeakCurrent { get; set; }
        public double RCurrent { get; set; }
        public double SCurrent { get; set; }
        public double TCurrent { get; set; }
        public double NolCurrent { get; set; }
        public string KelidCurrent { get; set; }
 
        
        public int vRS { get; set; }
        public int vTS { get; set; }
        public int vTR { get; set; }
        public int vTN { get; set; }
        public int vRN { get; set; }
        public int vSN { get; set; }
        
 
   

    }
}