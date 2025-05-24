namespace SK.Inventory.UI.Blazor.Route
{
    public static class Routes
    {
        public const string Inventory_Dashboard = "/visit-dashboard";
        public const string Inventory_Categories = "/categories";
        public const string Inventory_Category_Create = Inventory_Categories + "/create";
        public const string Inventory_Category_Edit = Inventory_Categories + "/edit/{id:int}";
        public static string GetCategoryEditUrl(int id) => $"/categories/edit/{id}";
        public const string Inventory_products = "/products";
        public const string Inventory_Product_Create = Inventory_products+"/create";
        public const string Inventory_Product_Edit = Inventory_products+ "/edit/{id:int}";
        public static string GetProductEditUrl(int id) => $"/products/edit/{id}";
    }
}
