using System;
using System.Collections.Generic;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     DateTime 클래스를 초 단위까지만 비교하는 클래스입니다.
    /// </summary>
    public sealed class DateTimeSecondUnitComparer : IComparer<DateTime>
    {
        /// <summary>
        ///     두 개체를 비교한 다음 한 개체가 다른 개체보다 작은지, 큰지 또는 두 개체가 같은지 여부를 나타내는 값을 반환합니다.
        /// </summary>
        /// <param name="x">비교할 첫 번째 개체입니다.</param>
        /// <param name="y">비교할 두 번째 개체입니다.</param>
        /// <returns>값 조건 0보다 작음x는 y보다 작습니다.0x는 y와 같습니다.0보다 큼x는 y보다 큽니다.</returns>
        public int Compare(DateTime x, DateTime y)
        {
            long xticks = this.GetSecondUnitTicks(x.Ticks);
            long yticks = this.GetSecondUnitTicks(y.Ticks);

            if (xticks == yticks)
                return 0;
            else
                return xticks < yticks ? -1 : 1;
        }

        /// <summary>
        ///     Tick 단위를 초 단위까지 잘라냅니다.
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        private long GetSecondUnitTicks(long ticks)
        {
            return (ticks / 10000000) * 10000000;
        }
    }
}
