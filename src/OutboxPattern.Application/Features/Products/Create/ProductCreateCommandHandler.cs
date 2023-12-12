using MediatR;
using OutboxPattern.Application.Interfaces.Repositories;
using OutboxPattern.Application.Utilities.Results;
using OutboxPattern.Domain.Entities;

namespace OutboxPattern.Application.Features.Products.Create;

public sealed class ProductCreateCommandHandler(IProductRepository productRepository) : IRequestHandler<ProductCreateCommand, Result>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Result> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        var product = new ProductEntity
        {
            Name = request.Name, 
            Price = request.Price,
            StockAmount = request.StockAmount,
            Description = request.Description           
        };
        
        await _productRepository.AddAsync(product);

        return await Result.Success();
    }
}
