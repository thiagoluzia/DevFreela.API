namespace DevFreela.Application.Dtos_Thiago.OutputModels
{
    public class ProjectDetailsOutputModel
    {
        public ProjectDetailsOutputModel(int id, string title, string description, decimal totalCost, DateTime? startedAt, DateTime? finishedAt)
        {
            Id = id;
            Title = title;
            Description = description;
            TotalCost = totalCost;
            StartedAt = startedAt;
            FinishedAt = finishedAt;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? FinishedAt { get; set; }
    }
}
