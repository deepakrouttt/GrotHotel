using System.ComponentModel.DataAnnotations;

namespace GrotHotel.Models
{
    public class BlackOutDate
    {
        [Key]
        public int Id { get; set; }
        public ICollection<DateEntry> Dates { get; set; }

        public int RoomRateId { get; set; }
    }

    public class DateEntry
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
    public class BlackoutData
    {
        public int Id { get; set; }
        public List<string> BlackOutDates { get; set; }
    }
}
