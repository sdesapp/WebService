namespace mService.Models
{
     public class mAccounts
    {
        
        public bool IsValid { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
		public string EmployeeId { get; set; }
        public string ConsigneeId { get; set; } //storerkey
        public string Token { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string DeviceType { get; set; }
        public string DeviceId { get; set; }
        
    }
}