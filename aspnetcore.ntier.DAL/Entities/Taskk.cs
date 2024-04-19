﻿
using System.ComponentModel.DataAnnotations;

namespace aspnetcore.ntier.DAL.Entities
{
    public class Taskk
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status {  get; set; }
        public DateTime DateCreated {  get; set; }
        public DateTime? DateDue {  get; set; }
        public DateTime? DateCompleted { get; set; }
        public ICollection<Subtask> Subtasks { get; set; }

        public Taskk() 
        {
            Status = "undone";
            DateCreated = DateTime.Now;
        }

    }
}
