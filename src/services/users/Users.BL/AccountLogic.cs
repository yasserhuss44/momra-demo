using Helpers.Models;
using System;
using System.Collections.Generic;
using Users.Model.Dtos;

namespace Users.BL
{
    public class AccountLogic
    {
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
