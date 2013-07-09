using System;

namespace ByteCarrot.Masslog.Core.DomainModel.Entities
{
    public class DenormalizedDateTime
    {
        public DenormalizedDateTime()
        {
        }

        public DenormalizedDateTime(DateTime date)
        {
            Date = date;
            Year = date.Year;
            Month = date.Month;
            Day = date.Day;
            Hour = date.Hour;
            Minute = date.Minute;
            Second = date.Second;
        }

        public DateTime Date { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public int Second { get; set; }
    }
}