using Marten.Schema;

namespace Basket.API.Data
{
    public class BasketInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            var session = store.LightweightSession();

            if (await session.Query<ShoppingCart>().AnyAsync()) return;

            //Marten UPSERT will cater for existing record
            session.Store<ShoppingCart>(GetPreconfiguredCarts());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<ShoppingCart> GetPreconfiguredCarts() => new List<ShoppingCart>()
            {
                new ShoppingCart()
                
                
            };
    }
}
