namespace SK.Identity.Application.Dtos
{
    public class UserRolesDto
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public Dictionary<string, bool> RoleSelections { get; set; } = new();

        public Dictionary<string, List<string>> AllRoles { get; set; } = new();
    }
}
