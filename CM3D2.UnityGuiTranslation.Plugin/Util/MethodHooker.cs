using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     메서드를 후크하는 클래스입니다.
    /// </summary>
    /// <remarks>
    ///     ※CLR 2.0.50727.1433 에서만 작동합니다.
    /// </remarks>
    public sealed class MethodHooker : Hooker
    {
        /// <summary>
        ///     후크 방향입니다.
        /// </summary>
        public enum HookDirection
        {
            /// <summary>
            ///     없음
            /// </summary>
            None,
            /// <summary>
            ///     왼쪽으로
            /// </summary>
            Left,
            /// <summary>
            ///     오른쪽으로
            /// </summary>
            Right,
            /// <summary>
            ///     양쪽으로
            /// </summary>
            Both
        }
        private readonly HookDirection hookDirection;

        private readonly MethodBase leftMethod;
        private readonly MethodBase rightMethod;

        private readonly MethodUtil.MethodData leftMethodData;
        private readonly MethodUtil.MethodData rightMethodData;

        /// <summary>
        ///     후크 방향과 두 메서드를 등록하고 MethodHooker 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="hookDirection">후크할 방향입니다.</param>
        /// <param name="leftMethod">등록할 첫 번째 메서드입니다.</param>
        /// <param name="rightMethod">등록할 두 번째 메서드입니다.</param>
        public MethodHooker(HookDirection hookDirection, MethodBase leftMethod, MethodBase rightMethod) : base()
        {
            if (leftMethod == null)
                throw new ArgumentNullException("leftMethod", "Argument can not be null");
            if (rightMethod == null)
                throw new ArgumentNullException("rightMethod", "Argument can not be null");

            this.hookDirection = hookDirection;

            this.leftMethod = leftMethod;
            this.rightMethod = rightMethod;

            this.leftMethodData = MethodUtil.GetMethodData(leftMethod);
            this.rightMethodData = MethodUtil.GetMethodData(rightMethod);

            this.leftMethodData.MethodAccessModifier = MethodUtil.MethodData.AccessModifier.Public;
            this.rightMethodData.MethodAccessModifier = MethodUtil.MethodData.AccessModifier.Public;
        }

        /// <summary>
        ///     등록된 메서드를 후크합니다.
        /// </summary>
        public override void DetainHook()
        {
            if (this.hookDirection == HookDirection.Left || this.hookDirection == HookDirection.Both)
                MethodUtil.SetMethodData(this.leftMethod, this.rightMethodData);
            if (this.hookDirection == HookDirection.Right || this.hookDirection == HookDirection.Both)
                MethodUtil.SetMethodData(this.rightMethod, this.leftMethodData);
        }
        /// <summary>
        ///     등록된 메서드를 언 후크합니다.
        /// </summary>
        /// <remarks>
        ///     동작하지 않는 함수입니다.
        ///     사실 왜 후크 되는지도 모릅니다. XD 꺄아아 난 멀랏
        /// </remarks>
        public override void ReleaseHook()
        {
            MethodUtil.SetMethodData(this.leftMethod, this.leftMethodData);
            MethodUtil.SetMethodData(this.rightMethod, this.rightMethodData);
        }
    }
}
