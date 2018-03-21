namespace DAL.DTO
{
    public class TfsWorkItem
    {
        public int Id { get; set; }
        public string IterationPath { get; set; }
        public string Title { get; set; }
        public string WorkItemType { get; set; }
        public string AssignedTo { get; set; }
        public string State { get; set; }
    }
}