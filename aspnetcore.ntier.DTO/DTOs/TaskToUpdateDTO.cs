
namespace aspnetcore.ntier.DTO.DTOs;

public class TaskToUpdateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime DateDue { get; set; }
    public DateTime DateCompleted { get; set; }

}
