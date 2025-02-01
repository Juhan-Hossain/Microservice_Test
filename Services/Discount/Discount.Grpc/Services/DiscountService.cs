namespace Discount.Grpc.Services;
public class DiscountService
    (DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        if (coupon == null)
            coupon = new Coupon    
            {
                ProductName = "No Product Name",
                Amount = 0,
                Description = "No coupoin was found"
            };

        logger.LogInformation("Discount is retrived for productName : {productName}, Request: {Request}", request.ProductName, request);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override  async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid argument have passed"));

        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully updated for productName : {productName}, Request: {Request}", request.Coupon.ProductName, request);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid argument have passed"));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully updated for productName : {productName}, Request: {Request}", request.Coupon.ProductName, request);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {

        var coupon = await dbContext
             .Coupons
             .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        if (coupon == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with this {request.ProductName} not found"));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully deleted for productName : {productName}, Request: {Request}", request.ProductName, request);

        return new DeleteDiscountResponse { Success = true };
    }
}

