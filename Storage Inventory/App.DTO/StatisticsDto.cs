namespace App.DTO;

public class StatisticsDto
{
    public string UserEmail { get; set; } = default!;

    public int ItemsTotal { get; set; }

    public int StorageLevelTotal { get; set; }

    public int ItemsUniqueTotal { get; set; }
}