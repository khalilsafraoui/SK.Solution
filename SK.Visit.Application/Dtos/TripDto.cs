using SK.Visit.Application.Enum;

namespace SK.Visit.Application.Dtos
{
    public class TripDto : DestinationDto
    {
        
        public string? ArrivalTime;

        public string? FinishTime;

        public string? Note { get; set; } = string.Empty;
        public string? SkipReason { get; set; } = string.Empty;

        public List<string>? PreviousNotes { get; set; } = new List<string>();

        public TripStatusDto Status { get; set; } = TripStatusDto.Pending;
        
    }
}
