namespace Ordering.Domain.ValueObjects.CustomValueType
{
    public record OrderId
    {
        public Guid Value { get; }
        private OrderId(Guid value) => Value = value;
        public static OrderId Of(Guid value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value.ToString());
            if (value == Guid.Empty)
            {
                throw new DomainException("OrderId can not be empty");

            }
            return new OrderId(value);
        }
    }
}