namespace WareHouseManagerWebApp.Model
{
    public class userModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int EmployeeId { get; set; }

        public employeeModel Employee { get; set; }
    }
}
