namespace TaskManager.Models
{
    public class Project1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<int> TasksId { get; set; }
        public IEnumerable<int> Members { get; set; }
    }
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public IEnumerable<string> TaskTitle { get; set; }
        public IEnumerable<string> Membernames { get; set; }
    }
}
