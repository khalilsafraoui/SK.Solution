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
        public const string Crm_Customer_Order_Confirmation = Crm_Customer_Orders + "/confirmation/{session_id}";
        public const string Crm_Customer_Order_Confirmed = Crm_Customer_Orders + "/confirmed/{orderId}/{userId}";
        public static string GetCustomerOrderDetailsUrl(Guid id) => $"/customers/orders/details/{id}";
        public static string GetCustomerOrderConfirmationUrl(string baseUri) => $"{baseUri}customers/orders/confirmation/{{CHECKOUT_SESSION_ID}}";
        public static string GetCustomerOrderConfirmedUrl(Guid orderId, string userId) => $"/customers/orders/confirmed/{orderId}/{userId}";

        public const string Crm_Orders = "/orders";
        public const string Crm_Order_Create = Crm_Orders + "/create";
        public const string Crm_Order_Edit = Crm_Orders + "/edit/{id}";
        public const string Crm_Order_Details = Crm_Orders + "/details/{id}";
        

        public static string GetOrderEditUrl(Guid id) => $"/orders/edit/{id}";
        public static string GetOrderDetailsUrl(Guid id) => $"/orders/details/{id}";
       


        public const string Crm_Cart = "/cart";
        public static string GetCartUrl(string baseUri) => $"{baseUri}cart";

        public const string Crm_Customer_Quotes = "/customers/quotes";
        public const string Crm_Customer_Quote_Details = Crm_Customer_Quotes + "/details/{id}";

        public static string GetCustomerQuoteDetailsUrl(Guid id) => $"/customers/quotes/details/{id}";

        public const string Crm_Quotes = "/quotes";
        public const string Crm_Quote_Create = Crm_Quotes + "/create";
        public const string Crm_Quote_Edit = Crm_Quotes + "/edit/{id}";
        public const string Crm_Quote_Details = Crm_Quotes + "/details/{id}";


        public static string GetQuoteEditUrl(Guid id) => $"/quotes/edit/{id}";
        public static string GetQuoteDetailsUrl(Guid id) => $"/quotes/details/{id}";
    }
}
