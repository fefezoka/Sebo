using FluentResults;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using SEBO.Domain.Dto.DTO.IdentityDTO.Account;
using SEBO.Domain.Entities.IdentityAggregate;
using SEBO.Domain.Interface.Repository.IdentityAggregate;
using SEBO.Services.Identity;

namespace SEBO.Tests
{
    public class UserServiceTest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        [Fact]
        public async Task TestGetUserByIdAsync()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var userService = new UserService(userRepositoryMock.Object, _httpContextAccessor);

            // criando um usuario pra ser retornado pelo repositorio
            var user = new ApplicationUser();
            user.Id = 1;
            user.UserName = "fefezoka";
            user.FirstName = "Felipe";
            user.LastName = "Brito";
            user.Active = true;

            // definindo que o mock do repositorio vai retornar o user acima
            userRepositoryMock
                .Setup(x => x.GetUserByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Result.Ok(), user));

            // criando o DTO a ser retornado pelo Mapper
            var readUserDTO = new ReadUserDTO();
            readUserDTO.Id = 1;
            readUserDTO.FirstName = user.FirstName;
            readUserDTO.LastName = user.LastName;
            readUserDTO.UserName = user.UserName;
            readUserDTO.Active = user.Active;

            // act
            var response = await userService.GetUserById(1);
            var obj1Str = JsonConvert.SerializeObject(response.Content.FirstOrDefault());
            var obj2Str = JsonConvert.SerializeObject(readUserDTO);

            // assert
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Content);
            Assert.Equal(obj1Str, obj2Str);
        }
    }
}