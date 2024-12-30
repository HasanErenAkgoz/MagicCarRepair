using AutoMapper;
using Core.Packages.Application.Repositories;
using Core.Utilities.Results;
using MagicCarRepair.Application.Features.Cars.Dtos;
using MagicCarRepair.Application.Security.SecuredOperation;
using MagicCarRepair.Domain.Entities;
using MediatR;

namespace MagicCarRepair.Application.Features.Cars.Queries.GetCarById
{
    [SecuredOperation(Priority = 1)]
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, IDataResult<CarDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCarByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CarDto>> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var car = await _unitOfWork.Repository<Car>().GetAsync(c => c.Id == request.Id);
            if (car == null)
                return new ErrorDataResult<CarDto>("Araç bulunamadı");

            var carDto = _mapper.Map<CarDto>(car);
            return new SuccessDataResult<CarDto>(carDto);
        }
    }
} 