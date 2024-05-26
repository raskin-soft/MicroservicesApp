using MediatR;
using ProductService.Core.Entities;
using ProductService.Infrastructure.Data;

namespace ProductService.Application.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly ProductDbContext _context;

        public CreateProductCommandHandler(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product { Name = request.Name, Price = request.Price };
            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
}
