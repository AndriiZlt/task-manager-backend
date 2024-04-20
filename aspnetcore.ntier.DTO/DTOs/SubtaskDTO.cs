

namespace aspnetcore.ntier.DTO.DTOs;

public class SubtaskDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime DateCreated { get; set; } 
    public DateTime? DateCompleted { get; set; }
    public int TaskId { get; set; }

}
