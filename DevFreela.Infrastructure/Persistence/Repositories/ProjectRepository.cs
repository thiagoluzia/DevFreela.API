using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{

    public class ProjectRepository : IProjectRepositoriy
    {
        private readonly DevFreelaDbContext _dbcontext;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbcontext = dbContext;
            _configuration = configuration;

            _connectionString = _configuration.GetConnectionString("DevFreelaCs");
        }


        public async  Task AddCommentAsync(ProjectComment comment)
        {
            await _dbcontext.ProjectComments.AddAsync(comment);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task AddProject(Project project)
        {
            await _dbcontext.Projects.AddAsync(project);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<List<Project>> GetAllAsync()
        {
            #region Com DAPPER
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var script = "SELECT Id, Title, CreatedAt FROM Projects";
                var projecs = await sqlConnection.QueryAsync<Project>(script);

                return projecs.ToList();
            }
            #endregion

            #region Com EF
            //var projects = await _dbcontext.Projects.ToListAsync();
            //return projects;
            #endregion

        }

        public async Task<Project> GetByIdAsync(int id)
        {
            var project = await _dbcontext.Projects
                .Include(x => x.Client)
                .Include(x => x.Freelancer)
                .FirstOrDefaultAsync(x => x.Id == id);

            return project;
        }

        public async Task<Project> GetDetailsByIdAsync(int id)
        {
            var project = await _dbcontext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefaultAsync(p => p.Id == id);

            return project;
        }

        public async Task SaveChangesAsync()
        {
           await _dbcontext.SaveChangesAsync();
        }

        public Task StartAsync(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
