namespace ProductManagement.DAL.Dto
{
    public class UserViewModelDto
	{
        public int UserId { get; set; }
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
        public int CompanyId { get; set; }
        public int RoleId { get; set; }
        public bool Valid { get; set; }
    }
}
