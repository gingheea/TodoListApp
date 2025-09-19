namespace TodoListApp.Services.Identity.Entities
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser<int>
    {
        public string Nickname { get; set; } = string.Empty;
    }
}
