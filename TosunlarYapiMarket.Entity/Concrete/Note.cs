using TosunlarYapiMarket.Entity.Abstract;

namespace TosunlarYapiMarket.Entity.Concrete
{
    public class Note:EntityBase,IEntity
    {
        public string? NoteTitle { get; set; }
        public string? NoteDescription { get; set; }
        public DateTime NoteDate { get; set; } = DateTime.Now;
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public AppUser? AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}
