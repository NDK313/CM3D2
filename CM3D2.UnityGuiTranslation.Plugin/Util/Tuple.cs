using System.Collections.Generic;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     여러 개체를 담고 있는 클래스입니다.
    /// </summary>
    /// <typeparam name="T1">첫 번째 개체입니다.</typeparam>
    public struct Tuple<T1>
    {
        /// <summary>
        ///     첫 번째 개체입니다.
        /// </summary>
        public T1 t1;

        /// <summary>
        ///     Tuple 구조체를 초기화합니다.
        /// </summary>
        /// <param name="t1">첫 번째 개체입니다.</param>
        public Tuple(T1 t1)
        {
            this.t1 = t1;
        }
    }

    /// <summary>
    ///     여러 개체를 담고 있는 클래스입니다.
    /// </summary>
    /// <typeparam name="T1">첫 번째 개체입니다.</typeparam>
    /// <typeparam name="T2">두 번째 개체입니다.</typeparam>
    public struct Tuple<T1, T2>
    {
        /// <summary>
        ///     첫 번째 개체입니다.
        /// </summary>
        public T1 t1;
        /// <summary>
        ///     두 번째 개체입니다.
        /// </summary>
        public T2 t2;

        /// <summary>
        ///     Tuple 구조체를 초기화합니다.
        /// </summary>
        /// <param name="t1">첫 번째 개체입니다.</param>
        /// <param name="t2">두 번째 개체입니다.</param>
        public Tuple(T1 t1, T2 t2)
        {
            this.t1 = t1;
            this.t2 = t2;
        }
        /// <summary>
        ///     Tuple 구조체를 초기화합니다.
        /// </summary>
        /// <param name="pair">첫 번째와 두번 째 개체입니다.</param>
        public Tuple(KeyValuePair<T1, T2> pair) : this(pair.Key, pair.Value) { }
    }

    /// <summary>
    ///     여러 개체를 담고 있는 클래스입니다.
    /// </summary>
    /// <typeparam name="T1">첫 번째 개체입니다.</typeparam>
    /// <typeparam name="T2">두 번째 개체입니다.</typeparam>
    /// <typeparam name="T3">세 번째 개체입니다.</typeparam>
    public struct Tuple<T1, T2, T3>
    {
        /// <summary>
        ///     첫 번째 개체입니다.
        /// </summary>
        public T1 t1;
        /// <summary>
        ///     두 번째 개체입니다.
        /// </summary>
        public T2 t2;
        /// <summary>
        ///     세 번째 개체입니다.
        /// </summary>
        public T3 t3;

        /// <summary>
        ///     Tuple 구조체를 초기화합니다.
        /// </summary>
        /// <param name="t1">첫 번째 개체입니다.</param>
        /// <param name="t2">두 번째 개체입니다.</param>
        /// <param name="t3">세 번째 개체입니다.</param>
        public Tuple(T1 t1, T2 t2, T3 t3)
        {
            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;
        }
    }

    /// <summary>
    ///     여러 개체를 담고 있는 클래스입니다.
    /// </summary>
    /// <typeparam name="T1">첫 번째 개체입니다.</typeparam>
    /// <typeparam name="T2">두 번째 개체입니다.</typeparam>
    /// <typeparam name="T3">세 번째 개체입니다.</typeparam>
    /// <typeparam name="T4">네 번째 개체입니다.</typeparam>
    public struct Tuple<T1, T2, T3, T4>
    {
        /// <summary>
        ///     첫 번째 개체입니다.
        /// </summary>
        public T1 t1;
        /// <summary>
        ///     두 번째 개체입니다.
        /// </summary>
        public T2 t2;
        /// <summary>
        ///     세 번째 개체입니다.
        /// </summary>
        public T3 t3;
        /// <summary>
        ///     네 번째 개체입니다.
        /// </summary>
        public T4 t4;

        /// <summary>
        ///     Tuple 구조체를 초기화합니다.
        /// </summary>
        /// <param name="t1">첫 번째 개체입니다.</param>
        /// <param name="t2">두 번째 개체입니다.</param>
        /// <param name="t3">세 번째 개체입니다.</param>
        /// <param name="t4">네 번째 개체입니다.</param>
        public Tuple(T1 t1, T2 t2, T3 t3, T4 t4)
        {
            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;
            this.t4 = t4;
        }
    }

    /// <summary>
    ///     여러 개체를 담고 있는 클래스입니다.
    /// </summary>
    /// <typeparam name="T1">첫 번째 개체입니다.</typeparam>
    /// <typeparam name="T2">두 번째 개체입니다.</typeparam>
    /// <typeparam name="T3">세 번째 개체입니다.</typeparam>
    /// <typeparam name="T4">네 번째 개체입니다.</typeparam>
    /// <typeparam name="T5">다섯 번째 개체입니다.</typeparam>
    public struct Tuple<T1, T2, T3, T4, T5>
    {
        /// <summary>
        ///     첫 번째 개체입니다.
        /// </summary>
        public T1 t1;
        /// <summary>
        ///     두 번째 개체입니다.
        /// </summary>
        public T2 t2;
        /// <summary>
        ///     세 번째 개체입니다.
        /// </summary>
        public T3 t3;
        /// <summary>
        ///     네 번째 개체입니다.
        /// </summary>
        public T4 t4;
        /// <summary>
        ///     다섯 번째 개체입니다.
        /// </summary>
        public T5 t5;

        /// <summary>
        ///     Tuple 구조체를 초기화합니다.
        /// </summary>
        /// <param name="t1">첫 번째 개체입니다.</param>
        /// <param name="t2">두 번째 개체입니다.</param>
        /// <param name="t3">세 번째 개체입니다.</param>
        /// <param name="t4">네 번째 개체입니다.</param>
        /// <param name="t5">다섯 번째 개체입니다.</param>
        public Tuple(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
        {
            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;
            this.t4 = t4;
            this.t5 = t5;
        }
    }
}
