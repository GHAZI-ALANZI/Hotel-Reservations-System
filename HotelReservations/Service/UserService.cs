using HotelReservations.Model;
using HotelReservations.Repositories;
using System.Collections.Generic;

namespace HotelReservations.Service
{
    public class UserService
    {
        IUserRepository userRepository;
        public UserService()
        {
            userRepository = new UserRepositoryDB();
        }

        public List<User> GetAllUsers()
        {
            return Hotel.GetInstance().Users;
        }

        public void SaveUser(User user)
        {
            if (user.user_id == 0)
            {
                Hotel.GetInstance().Users.Add(user);
                user.user_id = userRepository.Insert(user);
            }
            else
            {
                var index = Hotel.GetInstance().Users.FindIndex(u => u.user_id == user.user_id);
                Hotel.GetInstance().Users[index] = user;
                userRepository.Update(user);
            }
        }

        public void MakeUserInactive(User user)
        {
            var makeUserInactive = Hotel.GetInstance().Users.Find(u => u.user_id == user.user_id);
            makeUserInactive.user_is_active = false;
            userRepository.Delete(user.user_id);
        }
    }
}
