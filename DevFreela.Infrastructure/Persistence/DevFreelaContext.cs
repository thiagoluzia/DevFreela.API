using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaContext
    {
        #region Simulando base de dados com lista em memoria

        public DevFreelaContext()
        {
            Projects = new List<Project>
            {
                new Project("Meu PRojeto 01", "Minha descrição 01",1,1,1000),
                new Project("Meu PRojeto 01", "Minha descrição 01",1,1,1000),
                new Project("Meu PRojeto 01", "Minha descrição 01",1,1,1000)
            };

            Users = new List<User>
            {
                new User("Thiago Moura","thiago@gmail.com",new DateTime(1989,8,23)),
                new User("Moura Luzia","moura@gmail.com",new DateTime(1989,9,23)),
                new User("Henrique  Moura","Henrique@gmail.com",new DateTime(1989,10,23))
            };

            Skills = new List<Skill>
            {
                new Skill(".Net Core"),
                new Skill("C#"),
                new Skill("SQL")
            };
        }
        public List<Project> Projects { get; set; }
        public List<Skill> Skills { get; set; }
        public List<User> Users { get; set; }

        #endregion
    }
}
