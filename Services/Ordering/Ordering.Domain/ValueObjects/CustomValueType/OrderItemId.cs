namespace Ordering.Domain.ValueObjects.CustomValueType
{
    public record OrderItemId
    {
        public Guid Value { get; }
        private OrderItemId(Guid value) => Value = value;
        public static OrderItemId Of(Guid value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value.ToString());
            if (value == Guid.Empty)
            {
                throw new DomainException("OrderItem Id can not be empty");

            }
            return new OrderItemId(value);
        }
    }
}