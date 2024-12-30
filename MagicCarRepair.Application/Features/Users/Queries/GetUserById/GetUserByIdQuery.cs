using MediatR;
using Core.Packages.Application.Results;
using MagicCarRepair.Application.Features.Users.Dtos;

namespace MagicCarRepair.Application.Features.Users.Queries.GetUserById;

/// <summary>
/// ID'ye göre kullanıcı getirme sorgusu
/// </summary>
public class GetUserByIdQuery : IRequest<IDataResult<UserDto>>, IRequest<Core.Utilities.Results.IDataResult<UserDto>>
{
    /// <summary>
    /// Kullanıcı ID'si
    /// </summary>
    public Guid Id { get; set; }
} 