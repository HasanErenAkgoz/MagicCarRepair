using AutoMapper;
using Core.Packages.Application.Repositories;
using Core.Utilities.Results;
using MagicCarRepair.Application.Security.SecuredOperation;
using MagicCarRepair.Domain.Entities;
using MediatR;

namespace MagicCarRepair.Application.Features.Cars.Commands.CreateCar
{
    [SecuredOperation(Priority = 1)]
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Car>(request);
            await _unitOfWork.Repository<Car>().AddAsync(car);
            await _unitOfWork.SaveChangesAsync();
            
            return new SuccessResult("Araç başarıyla eklendi");
        }
    }
} 