namespace SK.Identity.UI.Blazor.Route
{
    public static class Routes
    {
        public const string Identity_Dashboard = "/identity-dashboard";
        public const string Identity_Users = "/users";
        public const string Identity_Users_Create = Identity_Users + "/create";
        public const string Identity_Users_Edit = Identity_Users + "/edit/{userId}";
        public const string Identity_Users_Roles_Edit = Identity_Users + "/roles/edit/{userId}";
        public static string GetUserEditUrl(string userId) => $"/users/edit/{userId}";
        public static string GetUserRolesEditUrl(string userId) => $"/users/roles/edit/{userId}";

        public const string Identity_Manager = "/manager";
        public const string Identity_Manager_Create = Identity_Manager + "/create";
        public const string Identity_Manager_Edit = Identity_Manager + "/edit/{userId}";
        public const string Identity_Manager_Roles_Edit = Identity_Manager + "/roles/edit/{userId}";
        public static string GetManagerEditUrl(string userId) => $"/manager/edit/{userId}";
        public static string GetManagerRolesEditUrl(string userId) => $"/manager/roles/edit/{userId}";

        public const string Identity_Manager_Users_Roles = "/manager-users-roles";

    }
}
