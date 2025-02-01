namespace Ordering.Domain.ValueObjects.CustomValueType
{
    public record ProductId
    {
        public Guid Value { get;  }
        private ProductId(Guid value) => Value = value;
        public static ProductId Of(Guid value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value.ToString());
            if (value == Guid.Empty)
            {
                throw new DomainException("ProductId can not be empty");

            }
            return new ProductId(value);
        }
    }
}