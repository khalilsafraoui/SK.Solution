namespace SK.Solution.Shared.Entities
{
    public class CountryDto
    {
        public string Code { get; set; }   // ISO Alpha-2 code (e.g., "TN", "BE")
        public string Name { get; set; }

        // Optional navigation property if needed
        public List<StateDto> States { get; set; } = new();
    }
}
