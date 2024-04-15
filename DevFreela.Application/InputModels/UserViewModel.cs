using DevFreela.Core.Entities;

namespace DevFreela.Application.InputModels
{
    public class UserViewModel
    {
        public UserViewModel(string fullName, string email, List<UserSkill> skill)
        {
            FullName = fullName;
            Email = email;

            Skills = skill;
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public List<UserSkill> Skills { get; private set; }
    }
}
