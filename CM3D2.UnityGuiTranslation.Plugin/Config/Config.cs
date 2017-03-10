using System;
using System.ComponentModel;
using System.IO;
using UnityEngine;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     설정을 담고 있는 추상 클래스입니다.
    /// </summary>
    public abstract class Config : IStorable
    {
        /// <summary>
        ///     키와 값의 지정 코드 종류입니다.
        /// </summary>
        public enum DivisionCode
        {
            /// <summary>
            ///     없음 코드입니다.
            /// </summary>
            None = -1,
            /// <summary>
            ///     절대 지정 코드입니다.
            /// </summary>
            Relative,
            /// <summary>
            ///     상대 지정 코드입니다.
            /// </summary>
            Absolute,
            /// <summary>
            ///     사용 안 함 코드입니다.
            /// </summary>
            Disable,
        }
        private static readonly string[] divisionStrings = new string[]
        {
             " =#> ",
             " ==> ",
             " =/> "
        };

        /// <summary>
        ///     Config 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        protected Config() : base() { }

        /// <summary>
        ///     스트림에서 설정을 읽어옵니다.
        /// </summary>
        /// <param name="stream">읽어올 스트림입니다.</param>
        public abstract void Read(Stream stream);
        /// <summary>
        ///     스트림으로 설정을 출력합니다.
        /// </summary>
        /// <param name="stream">출력할 스트림입니다.</param>
        public abstract void Write(Stream stream);
        /// <summary>
        ///     설정을 비웁니다.
        /// </summary>
        public abstract void Clear();
        /// <summary>
        ///     설정으로 새로 고침합니다.
        /// </summary>
        public abstract void Refresh();

        /// <summary>
        ///     설정의 이름에 해당하는 값이 있으면 반환하고, 없으면 기본 값을 등록하고, 반환합니다.
        /// </summary>
        /// <param name="configName">찾을 설정의 이름입니다.</param>
        /// <param name="defaultValue">설정의 기본 값입니다.</param>
        /// <returns>찾은 설정의 분할 코드와 값입니다.</returns>
        public abstract Tuple<DivisionCode, string> AccessConfig(string configName, string defaultValue);
        /// <summary>
        ///     설정의 이름에 해당하는 값이 있으면 반환하고, 없으면 기본 값을 등록하고, 반환합니다.
        /// </summary>
        /// <param name="configName">찾을 설정의 이름입니다.</param>
        /// <param name="defaultValue">설정의 기본 값입니다.</param>
        /// <returns>찾은 설정의 분할 코드와 값입니다.</returns>
        public Tuple<DivisionCode, T> AccessConfig<T>(string configName, T defaultValue) where T : struct
        {
            Tuple<DivisionCode, string> tuple = this.AccessConfig(configName, defaultValue.ToString());

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null && converter.IsValid(tuple.t2))
                return new Tuple<DivisionCode, T>(tuple.t1, (T)converter.ConvertFromString(tuple.t2));
            else
            {
                Debug.LogError("Can't convert config to " + typeof(T).FullName.ToLower() + " format : ConfigName : " + configName + ", ConfigValue : " + tuple.t2);

                return new Tuple<DivisionCode, T>(tuple.t1, defaultValue);
            }
        }

        /// <summary>
        ///     문자열에 해당하는 분할 코드를 반환합니다.
        /// </summary>
        /// <param name="divisionString">문자열입니다.</param>
        /// <returns>물자열에 해당하는 분할 코드입니다.</returns>
        protected static DivisionCode GetDivisionCode(string divisionString)
        {
            if (divisionString == null)
                throw new ArgumentNullException("divisionString", "Argument can not be null");

            DivisionCode divisionCode = DivisionCode.None;
            divisionCode = divisionString.Contains(AccessibleConfig.GetDivisionString(DivisionCode.Relative)) ? DivisionCode.Relative : divisionCode;
            divisionCode = divisionString.Contains(AccessibleConfig.GetDivisionString(DivisionCode.Absolute)) ? DivisionCode.Absolute : divisionCode;
            divisionCode = divisionString.Contains(AccessibleConfig.GetDivisionString(DivisionCode.Disable)) ? DivisionCode.Disable : divisionCode;
            return divisionCode;
        }
        /// <summary>
        ///     분할 코드에 해당하는 문자열을 반환합니다.
        /// </summary>
        /// <param name="divisionCode">분할 코드입니다.</param>
        /// <returns>분할 코드에 해당하는 문자열입니다.</returns>
        protected static string GetDivisionString(DivisionCode divisionCode)
        {
            return Config.divisionStrings[(int)divisionCode];
        }
    }
}
