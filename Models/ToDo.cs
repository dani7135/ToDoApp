using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class ToDo
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Plaese enter a description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Plaese enter a dúe date")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Plaese enter a categpry")]
        public string CategoryId { get; set; } = string.Empty;

        [ValidateNever]
        public Category Category { get; set; } = new Category();

        [Required(ErrorMessage = "Plaese enter a status")]
        public string StatusId { get; set; } = string.Empty;
        [ValidateNever]
        public Status Status { get; set; } = new Status();

        public bool Overdue => StatusId == "open" && DueDate < DateTime.Today;


    }
}
