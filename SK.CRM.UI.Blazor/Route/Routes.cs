namespace SK.CRM.UI.Blazor.Route
{
    public static class Routes
    {
        public const string Crm_Dashboard = "/crm-dashboard";
        public const string Crm_Prospects = "/prospects";
        public const string Crm_Prospect_Create = Crm_Prospects + "/create";
        public const string Crm_Prospect_Edit = Crm_Prospects + "/edit/{id}";
        public static string GetProspectEditUrl(Guid id) => $"/prospects/edit/{id}";


        public const string Crm_Customers = "/customers";
        public const string Crm_Customer_Create = Crm_Customers + "/create";
        public const string Crm_Customer_Edit = Crm_Customers + "/edit/{id}";
        public static string GetCustomerEditUrl(Guid id) => $"/customers/edit/{id}";

        public const string Crm_Customer_Orders = "/customers/orders";
        public const string Crm_Customer_Order_Details = Crm_Customer_Orders + "/details/{id}";
        public static string GetCustomerOrderDetailsUrl(Guid id) => $"/customers/orders/details/{id}";

        public const string Crm_Orders = "/orders";
        public const string Crm_Order_Create = Crm_Orders + "/create";
        public const string Crm_Order_Edit = Crm_Orders + "/edit/{id}";
        public const string Crm_Order_Details = Crm_Orders + "/details/{id}";
        public const string Crm_Order_Confirmation = Crm_Orders + "/confirmation/{session_id}";
        public const string Crm_Order_Confirmed = Crm_Orders + "/confirmed/{orderId}/{userId}";

        public static string GetOrderEditUrl(Guid id) => $"/orders/edit/{id}";
        public static string GetOrderDetailsUrl(Guid id) => $"/orders/details/{id}";
        public static string GetOrderConfirmationUrl(string sessionId) => $"/orders/confirmation/{sessionId}";
        public static string GetOrderConfirmedUrl(Guid orderId, string userId) => $"/orders/confirmed/{orderId}/{userId}";


        public const string Crm_Cart = "/cart";
    }
}
