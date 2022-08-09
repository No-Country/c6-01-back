namespace UniversityAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        //likes
        
        public int CareerId { get; set; }

        public Career Career { get; set; }
    }
}
