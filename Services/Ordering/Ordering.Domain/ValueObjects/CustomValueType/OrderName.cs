namespace Ordering.Domain.ValueObjects.CustomValueType;
public record OrderName
{
    private const int DefaultLength = 5;
    public string Value { get; }
    private OrderName(string value) => Value = value;
    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length,DefaultLength);
        if (string.IsNullOrEmpty(value))
        {
            throw new DomainException("Customer Id can not be empty");

        }
        return new OrderName(value);
    }
}