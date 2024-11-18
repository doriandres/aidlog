namespace AidLog.Models;

public class Ticket
{
    public string Id { get; set; } = "";
    public string CompanyId { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public bool EnableNotifications { get; set; }
}
