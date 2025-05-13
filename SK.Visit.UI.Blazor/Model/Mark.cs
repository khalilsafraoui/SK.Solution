namespace SK.Visit.UI.Blazor.Model
{
    public class Mark
    {
        public double Lat { get; set; }
        public double Lng { get; set; }

        public string Label { get; set; }

        public string Title { get; set; }

        public Mark(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }

        public Mark(double lat, double lng, string title, string label)
        {
            Lat = lat;
            Lng = lng;
            Label = label;
            Title = title;
        }
    }
}
