using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLONETRELLOBACK.models
{
    public class Tasks
    {
        public int Id { get; set; } // Identifiant unique de la tâche
        public string Title { get; set; } // Titre de la tâche
        public string Categorie { get; set; } // Catégorie de la tâche
        public string Description { get; set; } // Description de la tâche
        public DateTime CreatedAt { get; set; } // Date de création de la tâche
        public DateTime DueDate { get; set; } // Date de fin de la tâche
        public int ListId { get; set; } // Clé étrangère vers la liste
        public bool IsCompleted { get; set; } // Statut de la tâche

        // Propriétés de navigation
        [ForeignKey("ListId")]
        public Lists? List { get; set; } // Liste à laquelle appartient la tâche
        public ICollection<Comments> Comments { get; set; } = new List<Comments>(); // Collection de commentaires sur la tâche

        public Tasks()
        {
            CreatedAt = DateTime.Now;
        }
    }
}