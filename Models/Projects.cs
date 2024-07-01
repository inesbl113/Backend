namespace CLONETRELLOBACK.models
{
    public class Projects
    {
        public int Id { get; set; } // Identifiant unique du projet
        public string Title { get; set; } // Nom du projet
        public string Description { get; set; } // Description du projet
        public DateTime CreatedAt { get; set; } // Date de création du projet

        // Navigation properties
        public ICollection<Lists> Lists { get; set; } // Collection de listes associées au projet

        public Projects()
        {
            CreatedAt = DateTime.Now;
            Lists = new List<Lists>();
        }
    }
}
