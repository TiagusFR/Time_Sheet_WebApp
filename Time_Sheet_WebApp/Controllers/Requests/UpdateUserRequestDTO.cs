namespace Time_Sheet_WebApp.Controllers.Requests
{
    public class  UpdateUserRequestDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsDeleted { get; set; }
    }
}
