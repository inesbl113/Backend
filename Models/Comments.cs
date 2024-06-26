using System;

namespace CLONETRELLOBACK.models
{
    public class Comments
    {
        public int Id { get; set; } // Identifiant unique du commentaire
        public string Text { get; set; } // Contenu du commentaire
        public string Author { get; set; } // Auteur du commentaire
        public DateTime CreatedAt { get; set; }  // Date de création du commentaire

        
        public int TaskId { get; set; } // Clé étrangère vers la tâche associée au commentaire
        public Tasks? Task { get; set; } // Tâche associée au commentaire

        public Comments()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
