public class King
{
    public int Id { get; set; }
    public int CastleId { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public int Year { get; set; }
}

public class Castle
{
    public int Id { get; set; }
    public string? Name { get; set; }
}