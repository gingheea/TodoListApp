namespace TodoListApp.WebApi.Models.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Nickname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
