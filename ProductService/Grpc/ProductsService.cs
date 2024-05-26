using Grpc.Core;
using MediatR;
using ProductService.Grpc;
using ProductService.Application.Commands;
using static ProductService.Grpc.ProductsService;

namespace ProductService.Grpc
{
    public class ProductsService : Products.ProductsBase
    {
        private readonly IMediator _mediator;

        public ProductsService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {
            var command = new CreateProductCommand { Name = request.Name, Price = (decimal)request.Price };
            var productId = await _mediator.Send(command);
            return new CreateProductResponse { Id = productId };
        }
    }
}
