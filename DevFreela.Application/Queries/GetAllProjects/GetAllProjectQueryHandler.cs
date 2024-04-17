using Dapper;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly IProjectRepositoriy _repository;
        public GetAllProjectQueryHandler(DevFreelaDbContext dbContext, IConfiguration configuration, IProjectRepositoriy repository) 
        { 
            _dbContext = dbContext;
            _configuration = configuration;
            _repository = repository;

            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public GetAllProjectQueryHandler(IProjectRepositoriy repository)
        {
            _repository = repository;
        }
        public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            #region Com EF

            //var projects = _dbContext.Projects;

            //var projectsViewModel = await projects
            //    .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
            //    .ToListAsync();

            //return projectsViewModel;

            #endregion

            #region Com Dapper

            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    var script = "SELECT Id, Title, CreatedAt FROM Projects";
            //    var projectViewModel = await sqlConnection.QueryAsync<ProjectViewModel>(script);

            //    return projectViewModel.ToList();
            //}

            #endregion

            #region Repository

            var projects = await _repository.GetAllAsync();

            var projectsViewModel = projects
                .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))//covertendo o Project para um ProjectViewModel
                .ToList();

            return projectsViewModel;

            #endregion
        }
    }
}
