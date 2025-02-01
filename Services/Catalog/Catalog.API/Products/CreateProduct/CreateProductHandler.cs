namespace Catalog.API.Products.CreateProduct;
public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductsCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductsCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Products name should not be empty");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Products category should not be empty");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Products ImageFile should not be empty");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Products prize should not be zero");

    }
}

internal class CreateProductHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Create Product entity from command object
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };
        //Save the DB
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        //return CreateProductResult result
        return new CreateProductResult(product.Id);
    }
}