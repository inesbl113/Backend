using System.ComponentModel.DataAnnotations.Schema;

namespace CLONETRELLOBACK.models
{
    public class Tasks
    {

        public int Id { get; set; }  // Identifiant unique de la tâche
        public string Title { get; set; } // Titre de la tâche
        public string Categorie { get; set; } // Categorie de la tâche
        public string Description { get; set; } // Description de la tâche
        public DateTime CreatedAt { get; set; } // Date de création de la tâche
        public int ListId { get; set; } // Clé étrangère vers la liste
        public bool IsCompleted { get; set; } // Statut de la tâche
        
        //Navigation properties
        [ForeignKey("ListId")]
        public Lists? List { get; set; } // Liste à laquelle appartient la tâche
        public ICollection<Comments> Comments { get; set; } // Collection de commentaires sur la tâche

         public Tasks()
        {
            CreatedAt = DateTime.Now;
        }
    }
   
}