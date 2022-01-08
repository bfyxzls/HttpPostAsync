using System;

namespace Fuck.Core.User
{
    public interface IUserBlanceService
    {
        #region 接口
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="val"></param>
        void incream(int userId, decimal val);
        /// <summary>
        /// 减少
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="val"></param>
        void decream(int userId, decimal val);
        #endregion

        #region 事件
        /// <summary>
        /// 增加事件
        /// </summary>
        event Action<int, decimal> OnIncream;
        /// <summary>
        /// 减少事件
        /// </summary>
        event Action<int, decimal> OnDecream;
        #endregion


    }
}
