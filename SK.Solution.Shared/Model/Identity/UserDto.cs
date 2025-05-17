﻿namespace SK.Solution.Shared.Model.Identity
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
