using Common.Messaging.Models;
using Common.Messaging.Queues;
using Helpers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Users.DAL;
using Users.Model.Dtos;

namespace Users.BL
{
    public class AccountLogic : IAccountLogic
    {
        private readonly IMessagingQueueHandler _messagingQueueHandler;
        private UsersContext _userContext;

        public AccountLogic(IMessagingQueueHandler messagingQueueHandler,UsersContext userContext)
        {
            _messagingQueueHandler = messagingQueueHandler;
            _userContext = userContext;
        }
        public ResponseDetailsBase AddNewUser(UserDto userDto)
        {
            var user = new User {
                Name =userDto.Name,
                 Address=userDto.Address,
                Age =userDto.Age,
                Title = userDto.Title
            };
            _userContext.Users.Add(user);
            _userContext.SaveChanges();
            _userContext.Entry(user).GetDatabaseValues();
            _messagingQueueHandler.PushMessage(new UserCreatedMessage { Id =user.Id,Name=user.Name  });
            return new ResponseDetailsBase(CommonEnums.ResponseStatusCode.Success);
        }

        public ResponseDetailsList<UserDto> GetAllUsers()
        {
            return new ResponseDetailsList<UserDto>()
            {
                ItemsList = new List<UserDto>
                {
                  new UserDto
                  {
                      Id=1,
                      Name= "Yasser",
                      Address="Assiut",
                      Age=33,
                      Title="Developer"
                  },
                  new UserDto
                  {
                      Id=2,
                      Name= "Hani",
                      Address="Assiut2",
                      Age=10,
                      Title="Developer"
                  },
                  new UserDto{
                      Id=3,
                      Name= "Bayoush",
                      Address="Riyadh",
                      Age=35,
                      Title="Developer"
                  },
                  new UserDto{
                      Id=4,
                      Name= "Bayoush01",
                      Address="Riyadh01",
                      Age=35,
                      Title="Developer"
                  },
                  new UserDto{
                      Id=58,
                      Name= "Bayoush02",
                      Address="Riyadh012",
                      Age=35,
                      Title="Developer"
                  },
                }

            };

        }
    }
}
