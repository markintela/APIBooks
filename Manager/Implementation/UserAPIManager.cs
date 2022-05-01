using AutoMapper;
using Core.Domain;
using Core.Shared.ModelViews.UserAPI;
using Manager.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Implementation
{
    public class UserAPIManager : IUserAPIManager
    {

        private readonly IUserAPIRepository _userAPIRepository;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;

        public UserAPIManager(IUserAPIRepository userAPIRepository, IMapper mapper, IJWTService jWTService)
        {
            _userAPIRepository = userAPIRepository;
            _mapper = mapper;
            _jwtService = jWTService;

        }
        public async Task<IEnumerable<UserAPIView>> GetAsync()
        {
            return _mapper.Map<IEnumerable<UserAPI>, IEnumerable<UserAPIView>>( await _userAPIRepository.GetAsync());
        }

        public async Task<UserAPIView> GetAsync(string login)
        {
            return _mapper.Map<UserAPIView>(await _userAPIRepository.GetAsync(login));
        }

        public async Task<UserAPIView> InsertAsync(NewUserAPI newUserAPI)
        {
            var userAPIToInsert = _mapper.Map<UserAPI>(newUserAPI);
            PasswordHashConverterHashAsync(userAPIToInsert);
            return _mapper.Map<UserAPIView>(await _userAPIRepository.InsertAsync(userAPIToInsert));
        }

        public async Task<UserAPIView> UpdateUserAPIAsync(UserAPI userAPI)
        {
            throw new NotImplementedException();
        }

        public async Task<UserAPILogged> ValidaUserAPIEGeraTokenAsync(UserAPI userAPI)
        {
           var userAPISearched = await _userAPIRepository.GetAsync(userAPI.Login);

            if (userAPISearched == null)
            {
                return null;
            }

            var validationUserAPI  = await PasswordValidationAndUpdateHashAsync(userAPI, userAPISearched.Password);
            if (validationUserAPI)
            {
                var userLogged = _mapper.Map<UserAPILogged>(userAPISearched);
                userLogged.Token =  _jwtService.CreateToken(userAPISearched);
                return userLogged;
            }

            return null;
        }

        private void PasswordHashConverterHashAsync(UserAPI userAPI)
        {
            var password = new PasswordHasher<UserAPI>();
            userAPI.Password = password.HashPassword(userAPI, userAPI.Password);

        }
        private async Task<bool> PasswordValidationAndUpdateHashAsync(UserAPI userAPI, string hash)
        {
            var passwordHasher = new PasswordHasher<UserAPI>();
            var status = passwordHasher.VerifyHashedPassword(userAPI, hash, userAPI.Password);
            switch (status)
            {
                case PasswordVerificationResult.Failed:
                    return false;

                case PasswordVerificationResult.Success:
                    return true;

                case PasswordVerificationResult.SuccessRehashNeeded:
                    await UpdateUserAPIAsync(userAPI);
                    return true;

                default:
                    throw new InvalidOperationException();

            }
        }
    }
}
