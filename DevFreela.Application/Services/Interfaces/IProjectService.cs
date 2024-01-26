using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {
        int Create(NewProjectInputModel inputModel);
        void Update(UpdateProjectInputModel inputModel);
        void Delete(int id);
        List<ProjectViewModel> GetAll(string query);
        ProjectDetailsViewModel GetById(int id);
       
        void Start(int id);
        void Finish(int id);

        //void CreateComment(CreateCommentInputModel inputModel);

    }
}
