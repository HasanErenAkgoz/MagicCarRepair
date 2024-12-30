using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using MagicCarRepair.Application.Features.Permissions.Queries.GetGroupedPermissions;

namespace MagicCarRepair.Application.Tests.Features.Permissions.Queries
{
    public class GetGroupedPermissionsQueryTests
    {
        [Fact]
        public async Task Should_Return_Grouped_Permissions()
        {
            // Arrange
            // ...

            // Act
            var result = await handler.Handle(new GetGroupedPermissionsQuery(), CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            // ...
        }
    }
} 