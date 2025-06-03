namespace CoffeeManagement.ResponseModel.Account
{
   
    public class AccountResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string RoleId { get; set; }  //khong hieu tai sao ko set duoc RoleType
    }
}
