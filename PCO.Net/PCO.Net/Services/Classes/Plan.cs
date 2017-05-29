using System;
namespace PCO.Net
{
    public class Plan : IPlan
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string SeriesTitle { get; set; }
    }
}
