namespace backend.src.Dtos;

public record TaskDTO(string Title, string? Description, string TaskStatus, DateTime EndDate);
