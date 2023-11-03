using Microsoft.AspNetCore.Authorization;

namespace SignalRAss
{
    public class EmailAuthorizeAttribute : AuthorizeAttribute
    {
        public EmailAuthorizeAttribute(string email)
        {
        
            if (email == "admin@gmail.com")
            {
                Roles = "Admin";
            }
        }
    }
}
