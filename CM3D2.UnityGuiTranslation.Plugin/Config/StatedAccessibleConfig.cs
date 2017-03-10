using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    using DataPair = KeyValuePair<Config.DivisionCode, string>;
    using ConfigDataPair = KeyValuePair<string, KeyValuePair<Config.DivisionCode, string>>;
    using ConfigData = SortedDictionary<string, KeyValuePair<Config.DivisionCode, string>>;

    /// <summary>
    ///     정해진 설정만 접근 가능한 설정을 담고 있는 추상 클래스입니다.
    /// </summary>
    public abstract class StatedAccessibleConfig : Config
    {
        private readonly ConfigData configData;

        /// <summary>
        ///     AccessibleConfig 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        protected StatedAccessibleConfig() : base()
        {
            this.configData = new ConfigData();
        }

        /// <summary>
        ///     설정을 정렬하기 위한 IComparer 클래스를 지정하여 AccessibleConfig 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="configDataComparer">설정을 비교할 IComparer 클래스입니다.</param>
        protected StatedAccessibleConfig(IComparer<string> configDataComparer) : base()
        {
            this.configData = new ConfigData(configDataComparer);
        }

        /// <summary>
        ///     스트림에서 설정을 읽어옵니다.
        /// </summary>
        /// <param name="stream">읽어올 스트림입니다.</param>
        public override void Read(Stream stream)
        {
            StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);

            while (!streamReader.EndOfStream)
            {
                string data = streamReader.ReadLine();

                //분할 코드 추출
                DivisionCode currentDivisionCode = AccessibleConfig.GetDivisionCode(data);
                if (currentDivisionCode != DivisionCode.None)
                {
                    string divisionString = AccessibleConfig.GetDivisionString(currentDivisionCode);
                    int index = data.IndexOf(divisionString);

                    string key = data.Substring(0, index);
                    string value = data.Substring(index + divisionString.Length);
                    DataPair dataPair = new DataPair(currentDivisionCode, value);

                    //읽어온 설정을 등록
                    if (this.configData.ContainsKey(key))
                    {
                        this.configData.Remove(key);
                        this.configData.Add(key, dataPair);
                    }
                }
            }
        }
        /// <summary>
        ///     스트림으로 설정을 출력합니다.
        /// </summary>
        /// <param name="stream">출력할 스트림입니다.</param>
        public override void Write(Stream stream)
        {
            StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8);

            foreach (ConfigDataPair configDataPair in this.configData)
                streamWriter.WriteLine(configDataPair.Key + AccessibleConfig.GetDivisionString(configDataPair.Value.Key) + configDataPair.Value.Value);

            streamWriter.Flush();
        }
        /// <summary>
        ///     설정을 비웁니다.
        /// </summary>
        public override void Clear()
        {
            this.configData.Clear();
        }

        /// <summary>
        ///     설정의 이름에 해당하는 값이 있으면 반환하고, 없으면 기본 값을 등록하고, 반환합니다.
        /// </summary>
        /// <param name="configName">찾을 설정의 이름입니다.</param>
        /// <param name="defaultValue">설정의 기본 값입니다.</param>
        /// <returns>찾은 설정의 분할 코드와 값입니다.</returns>
        public override Tuple<DivisionCode, string> AccessConfig(string configName, string defaultValue)
        {
            if (configName == null)
                throw new ArgumentNullException("configName", "Argument can not be null");

            string key = configName.Trim();
            DataPair dataPair;

            if (this.configData.TryGetValue(key, out dataPair))
            {
                if (dataPair.Key == DivisionCode.Disable)
                    return new Tuple<DivisionCode, string>(DivisionCode.Disable, defaultValue);
                else
                    return new Tuple<DivisionCode, string>(dataPair);
            }
            else
            {
                this.configData.Add(key, new DataPair(DivisionCode.Relative, defaultValue));
                return this.AccessConfig(configName, defaultValue);
            }
        }
    }
}
