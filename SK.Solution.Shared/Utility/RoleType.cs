namespace SK.Solution.Shared.Utility
{
    public static class RoleType
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
       
        //Visit_Viewer Module
        public const string Visit_Manager = "Visit_Manager";
        public const string Visit_Viewer = "Visit_Viewer";
        public const string Visit_Agent = "Visit_Agent";
        public const string Visit_Representative = "Visit_Representative";
        public const string Visit_Dispatcher = "Visit_Dispatcher";
        public const string Visit_Coordinator = "Visit_Coordinator";

        public static List<string> VisitRoles = new List<string>
        {
            Visit_Viewer,
            Visit_Agent,
            Visit_Representative,
            Visit_Dispatcher,
            Visit_Coordinator
        };
        //Inventory_Viewer Module
        public const string Inventory_Manager = "Inventory_Manager";
        public const string Inventory_Viewer = "Inventory_Viewer";
        public const string Inventory_Coordinator = "Inventory_Coordinator";
        public static List<string> InventoryRoles = new List<string>
        {
            Inventory_Viewer,
            Inventory_Coordinator,
        };
        //Note_Viewer Module
        public const string Crm_Manager = "Crm_Manager";
        public const string Crm_Viewer = "Crm_Viewer";
        public static List<string> CrmRoles = new List<string>
        {
            Crm_Viewer,
        };
        //Note_Viewer Module
        public const string Note_Manager = "Note_Manager";
        public const string Note_Viewer = "Note_Viewer";
        public static List<string> NoteRoles = new List<string>
        {
            Note_Viewer,
        };
        public static List<string> managerRoles = new List<string>
        {
            Visit_Manager,
            Inventory_Manager,
            Crm_Manager,
            Note_Manager,
        };
    }
}
