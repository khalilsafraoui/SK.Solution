namespace SK.Solution.Shared.Entities
{
    public class StateDto
    {
        public string Code { get; set; }          // e.g., "TN-TUN"
        public string Name { get; set; }
        public string CountryCode { get; set; }

        // Optional navigation property
        public CountryDto Country { get; set; }

        public List<CityDto> States { get; set; } = new();
    }
}
