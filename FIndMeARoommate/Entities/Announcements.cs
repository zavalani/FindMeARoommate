namespace FIndMeARoommate.Entities
{
    public class Announcements
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
