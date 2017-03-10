using System;
using System.Collections.Generic;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     키에 해당되는 값의 빠른 순차적 접근이 가능한 추상 클래스입니다.
    /// </summary>
    /// <typeparam name="TKey">키입니다.</typeparam>
    /// <typeparam name="TValue">값입니다.</typeparam>
    public sealed class SequenceCache<TKey, TValue> : Cache<TKey, TValue>
    {
        private readonly List<KeyValuePair<TKey, TValue>> cacheList;
        private int sequenceCacheIndex;

        /// <summary>
        ///     SequenceCache 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        public SequenceCache() : base()
        {
            this.cacheList = new List<KeyValuePair<TKey, TValue>>();
            this.sequenceCacheIndex = 0;
        }

        /// <summary>
        ///     키에 해당되는 값이 캐시 되어있으면 반환하고, 없으면 함수로부터 얻어오고, 등록하고, 반환합니다.
        /// </summary>
        /// <param name="key">키입니다.</param>
        /// <param name="getValue">값을 얻어올 함수입니다.</param>
        /// <param name="sameComparer">키와 값이 같은지 비교하는 함수입니다.</param>
        /// <returns>키에 해당되는 값입니다.</returns>
        public override TValue GetValue(TKey key, Func<TKey, TValue> getValue, Func<TKey, TKey, bool> sameComparer)
        {
            if (key == null)
                throw new ArgumentNullException("key", "Argument can not be null");
            if (getValue == null)
                throw new ArgumentNullException("getValue", "Argument can not be null");
            if (sameComparer == null)
                throw new ArgumentNullException("sameComparer", "Argument can not be null");

            while (this.cacheList.Count <= this.sequenceCacheIndex)
                this.cacheList.Add(new KeyValuePair<TKey, TValue>(key, getValue(key)));

            KeyValuePair<TKey, TValue> cache = this.cacheList[this.sequenceCacheIndex];
            if (sameComparer(cache.Key, key))
            {
                this.sequenceCacheIndex += 1;
                return cache.Value;
            }
            else
            {
                this.cacheList[this.sequenceCacheIndex] = new KeyValuePair<TKey, TValue>(key, getValue(key));
                return this.GetValue(key, getValue);
            }
        }
        /// <summary>
        ///     캐시를 비웁니다.
        /// </summary>
        public override void Clear()
        {
            this.cacheList.Clear();
            this.Reset();
        }

        /// <summary>
        ///     순차적 접근 위치를 초기화합니다.
        /// </summary>
        public void Reset()
        {
            this.sequenceCacheIndex = 0;
        }
    }
}
