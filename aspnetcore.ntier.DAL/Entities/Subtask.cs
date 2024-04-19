
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspnetcore.ntier.DAL.Entities
{
    public class Subtask
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } 
        public DateTime DateCreated {  get; set; } 
        public DateTime? DateCompleted { get; set; }
        public int TaskId {  get; set; }

        public Taskk Task { get; set; }

        public Subtask()
        {
            Status = "undone";
            DateCreated = DateTime.Now;
        }

    }
}
