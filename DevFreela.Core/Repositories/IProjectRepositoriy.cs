using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepositoriy
    {
        Task<List<Project>> GetAllAsync();
        Task<Project> GetDetailsByIdAsync(int id);
        Task<Project> GetByIdAsync(int id);
        Task AddProject(Project project);
        Task StartAsync(Project project);
        Task AddCommentAsync(ProjectComment comment);
        Task SaveChangesAsync();
    }
}
