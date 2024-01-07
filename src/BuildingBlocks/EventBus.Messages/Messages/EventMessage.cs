namespace EventBus.Messages.Messages;

/// <summary>
/// Класс для отправки событий через rabbit
/// </summary>
public class EventMessage
{
    // Тип передаваемого события (для десериализации)
    public string EventType { get; set; }
    
    // Сообщение в виде json 
    public string Message { get; set; }
}