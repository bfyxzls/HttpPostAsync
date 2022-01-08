using System;

namespace Fuck.Core.User
{
    public class UserBlanceService : IUserBlanceService
    {
        public event Action<int, decimal> OnIncream;
        public event Action<int, decimal> OnDecream;

        public void decream(int userId, decimal val)
        {
            Console.WriteLine("decream,userId:{0}", userId);
            OnDecream(userId, val);
        }

        public void incream(int userId, decimal val)
        {
            Console.WriteLine("incream,userId:{0}", userId);
            OnIncream(userId, val);

        }
    }
}
