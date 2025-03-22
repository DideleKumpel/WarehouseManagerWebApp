

namespace WareHouseManagerWebApp.Model
{
    public class taskModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime FinishDate { get; set; }
        public  string RampName { get; set; }
        public  int EmployeeId { get; set; }
        public int LocationId { get; set; }
        public string ProductBarcode { get; set; }

        public employeeModel Employee { get; set; }
        public locationModel Location { get; set; }
        public productModel Product { get; set; }
        public rampModel Ramp { get; set; }
    }
}
