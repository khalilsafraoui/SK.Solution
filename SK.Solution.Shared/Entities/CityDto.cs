namespace SK.Solution.Shared.Entities
{
    public class CityDto
    {
        public string Code { get; set; }          // e.g., "TN-TUN-MRS"
        public string Name { get; set; }
        public string StateCode { get; set; }

        // Optional navigation property
        public StateDto State { get; set; }
    }
}
