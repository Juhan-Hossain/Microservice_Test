﻿namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext context)
        : IQueryHandler<GetOrderByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await context.Orders
                                .Include(o => o.OrderItems)
                                .AsNoTracking()
                                .Where(o => o.OrderName.Value.Contains(query.Name))
                                .OrderBy(o => o.OrderName)
                                .ToListAsync(cancellationToken);

            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }
    }
}
