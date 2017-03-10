namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     후크 가능한 인터페이스입니다.
    /// </summary>
    public abstract class Hooker
    {
        /// <summary>
        ///     Hooker 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        protected Hooker() { }

        /// <summary>
        ///     후크합니다.
        /// </summary>
        public abstract void DetainHook();
        /// <summary>
        ///     언 후크합니다.
        /// </summary>
        public abstract void ReleaseHook();
    }
}
