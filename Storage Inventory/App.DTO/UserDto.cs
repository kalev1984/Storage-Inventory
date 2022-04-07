namespace App.DTO;

public class UserDto
{
    public int UserCount { get; set; }

    public ICollection<StatisticsDto>? Statistics { get; set; }
}