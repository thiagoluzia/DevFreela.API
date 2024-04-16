using DevFreela.Core.Entities;

namespace DevFreela.API.Models
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate, DateTime createdAt, bool active, string password, string role)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            CreatedAt = createdAt;
            Active = active;
            Password = password;
            Role = role;

            Skills = new List<UserSkill>();
            OwnerdProjects = new List<Project>();
            FreelancerProjects = new List<Project>();
            Comments = new List<ProjectComment>();
        }

        public string FullName { get;  private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        public List<UserSkill> Skills { get; private set; }
        public List<Project> OwnerdProjects { get; private set; }
        public List<Project> FreelancerProjects { get; private set; }
        public List<ProjectComment> Comments { get; private set; }

    }
}
