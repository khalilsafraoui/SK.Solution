namespace SK.Solution.Shared.Model.Crm
{
    public class SharedMarkDto
    {
        public double Lat { get; set; }
        public double Lng { get; set; }

        public string Label { get; set; }

        public string Title { get; set; }

        public SharedMarkDto(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }

        public SharedMarkDto(double lat, double lng, string title, string label)
        {
            Lat = lat;
            Lng = lng;
            Label = label;
            Title = title;
        }
    }
}
