namespace TodoListApp.WebApi.Models.UserModels
{
    using Microsoft.AspNetCore.Identity;
    using TodoListApp.WebApi.Models.Models;

    public class User : IdentityUser<int>
    {
        public string Nickname { get; set; } = string.Empty;
    }
}
