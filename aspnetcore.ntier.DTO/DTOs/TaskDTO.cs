﻿using System.ComponentModel.DataAnnotations;

namespace aspnetcore.ntier.DTO.DTOs;

public class TaskDTO
{
    public int TaskId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }

}
