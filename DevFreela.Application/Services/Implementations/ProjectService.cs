using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;//Com Dapper

        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");//Com Dapper

        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Finish();
            _dbContext.SaveChanges();
        }

        //public List<ProjectViewModel> GetAll(string query)
        //{
        //    #region Com EF

        //    var projects = _dbContext.Projects;

        //    var projectsViewModel = projects
        //        .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
        //        .ToList();

        //    return projectsViewModel;

        //    #endregion

        //    #region Com Dapper

        //    //using (var sqlConnection = new SqlConnection(_connectionString))
        //    //{
        //    //    var script = "SELECT Id, Title, CreatedAt FROM Projects";

        //    //    return sqlConnection.Query<ProjectViewModel>(script).ToList();
        //    //}

        //    #endregion
        //}

        //public ProjectDetailsViewModel GetById(int id)
        //{
        //    var project = _dbContext.Projects
        //        .Include(p => p.Client)
        //        .Include(p => p.Freelancer)
        //        .SingleOrDefault(p => p.Id == id);

        //    if (project == null)
        //    {
        //        return null;
        //    }

        //    var projectDetailsViewModel = new ProjectDetailsViewModel(
        //        project.Id,
        //        project.Title,
        //        project.Description,
        //        project.TotalCost,
        //        project.StartedAt,
        //        project.FinishedAt,
        //        project.Client.FullName,
        //        project.Freelancer.FullName
        //        );

        //    return projectDetailsViewModel;
        //}

        public void Start(int id)
        {
            #region Com EF Core
            //var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            //project.Start();
            //_dbContext.SaveChanges();

            #endregion

            #region Com Dapper

            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            project.Start();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";

                sqlConnection.Execute(script, new { status = project.Status, startedAt = project.StartedAt, id });

            }

            #endregion
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);

            project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
            _dbContext.SaveChanges();

        }
   
    }
}
