namespace CLONETRELLOBACK.models
{
    public class Lists
    {

        public int Id { get; set; }  // Identifiant unique de la liste
        public string Title { get; set; } // Nom de la liste
        public int ProjectId { get; set; } // Clé étrangère vers le projet
        public DateTime CreatedAt { get; set; } // Date de création de la liste
        
        // Navigation properties
         public Projects? Project { get; set; } // Projet auquel appartient la liste
         public ICollection<Tasks> Tasks { get; set; } // Collection de tâches dans la liste


         public Lists()
        {
            CreatedAt = DateTime.Now;
        }
    }
   
}