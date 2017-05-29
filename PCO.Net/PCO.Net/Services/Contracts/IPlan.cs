using System;

namespace PCO.Net
{
    public interface IPlan
    {
        int Id { get; }
        string Title { get; }
        string SeriesTitle { get; }
        DateTime Time { get; }
    }
}
