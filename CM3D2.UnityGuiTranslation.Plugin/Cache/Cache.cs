using System;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     키에 해당되는 값의 빠른 접근이 가능한 추상 클래스입니다.
    /// </summary>
    /// <typeparam name="TKey">키입니다.</typeparam>
    /// <typeparam name="TValue">값입니다.</typeparam>
    public abstract class Cache<TKey, TValue>
    {
        /// <summary>
        ///     Cache 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        protected Cache() { }

        /// <summary>
        ///     키에 해당되는 값이 캐시 되어있으면 반환하고, 없으면 함수로부터 얻어오고, 등록하고, 반환합니다.
        /// </summary>
        /// <param name="key">키입니다.</param>
        /// <param name="getValue">값을 얻어올 함수입니다.</param>
        /// <returns>키에 해당되는 값입니다.</returns>
        public TValue GetValue(TKey key, Func<TKey, TValue> getValue)
        {
            return this.GetValue(key, getValue, (TKey x, TKey y) => { return x.Equals(y); });
        }
        /// <summary>
        ///     키에 해당되는 값이 캐시 되어있으면 반환하고, 없으면 함수로부터 얻어오고, 등록하고, 반환합니다.
        /// </summary>
        /// <param name="key">키입니다.</param>
        /// <param name="getValue">값을 얻어올 함수입니다.</param>
        /// <param name="sameComparer">키와 값이 같은지 비교하는 함수입니다.</param>
        /// <returns>키에 해당되는 값입니다.</returns>
        public abstract TValue GetValue(TKey key, Func<TKey, TValue> getValue, Func<TKey, TKey, bool> sameComparer);
        /// <summary>
        ///     캐시를 비웁니다.
        /// </summary>
        public abstract void Clear();
    }
}
