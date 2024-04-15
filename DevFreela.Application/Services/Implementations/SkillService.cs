using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _dbContext;

        public SkillService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SkillViewModel> GetAll()
        {
            //Buscar no banco de dados
            var skills = _dbContext.Skills;

            //mapeamento do retorno 
            var skillViewModel = skills
                .Select(x => new SkillViewModel(x.Id, x.Description))
                .ToList();

            //Retornar
            return skillViewModel;
        }
    }
}
