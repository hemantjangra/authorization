namespace Authorization.Models
{
    public class Login
    {
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public bool RememberLogin { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}