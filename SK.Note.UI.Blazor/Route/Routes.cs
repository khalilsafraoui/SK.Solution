namespace SK.Note.UI.Blazor.Route
{
    public static class Routes
    {
        public const string Note_Dashboard = "/note-dashboard";
        public const string Note_Notes = "/notes";
        public const string Note_Notes_Create = Note_Notes + "/create";
        public const string Note_Notes_Edit = Note_Notes + "/edit/{id:int}";

        public static string GetNoteEditUrl(int id) => $"/notes/edit/{id}";

        public const string Note_Notes_Categories = Note_Notes+"/categories";
        public const string Note_Notes_Category_Create = Note_Notes_Categories + "/create";
        public const string Note_Notes_Category_Edit = Note_Notes_Categories + "/edit/{id:int}";
        public static string GetNoteCategoryEditUrl(int id) => $"/notes/categories/edit/{id}";
    }
}
