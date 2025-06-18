namespace SK.CRM.Application.Enums
{
    public static class QuoteStatus
    {
        public static string StatusDraft = "Draft";
        public static string StatusSubmitted = "Submitted"; // e.g., when The customer submitted the quote request; waiting for supplier response.
        public static string StatusUnderReview = "UnderReview"; // e.g., when The supplier is reviewing the request.
        public static string StatusQuoted = "Quoted"; // e.g., when The supplier has responded with pricing and sent the official quote.


        public static string StatusCompleted = "Completed"; // e.g., Quote fulfilled through order creation.
        public static string StatusAccepted = "Accepted"; // e.g., when the customer accepts the quote
        public static string StatusRejected = "Rejected"; // e.g., when the customer rejects the quote
        public static string StatusCancelled = "Cancelled"; // e.g., when the quote is cancelled by the seller or customer
        public static string StatusExpired = "Expired"; // e.g., when the quote is no longer valid after a certain period

        //public static string StatusPending = "Pending"; // e.g., when the quote is created but not yet sent to the customer
        //public static string StatusDraft = "Draft";
        //public static string StatusApproved = "Sent"; // e.g., when the quote is sent to the customer for review
        //public static string StatusClosed = "Closed"; // e.g., when the quote is finalized and no further actions are needed
        //public static string StatusArchived = "Archived"; // e.g., when the quote is archived for record-keeping purposes
        //public static string StatusVoid = "Void"; // e.g., when the quote is voided and should not be considered valid anymore, possibly due to an error or change in circumstances
        //public static string StatusOnHold = "On Hold"; // e.g., when the quote is temporarily put on hold for some reason
        //public static string StatusRevised = "Revised"; // e.g., when the quote is revised after being sent to the customer, possibly due to feedback or changes in requirements

    }
}
