using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    using DataPair = KeyValuePair<Config.DivisionCode, string>;
    using ConfigDataPair = KeyValuePair<string, KeyValuePair<Config.DivisionCode, string>>;
    using ConfigData = SortedDictionary<string, KeyValuePair<Config.DivisionCode, string>>;
    using ConfigDatasPair = KeyValuePair<DateTime, SortedDictionary<string, KeyValuePair<Config.DivisionCode, string>>>;
    using ConfigDatas = SortedDictionary<DateTime, SortedDictionary<string, KeyValuePair<Config.DivisionCode, string>>>;

    /// <summary>
    ///     접근 시간별로 정렬된 접근 가능한 설정을 담고 있는 추상 클래스입니다.
    /// </summary>
    public abstract class HistoricalAccessibleConfig : Config
    {
        private readonly PropertyConfig propertyConfig;

        private readonly ConfigDatas configDatas;
        private readonly IComparer<string> configDataStringComparer;

        private readonly DateTime nowDateTime;

        /// <summary>
        ///     HistoricalAccessibleConfig 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        private HistoricalAccessibleConfig() : base()
        {
            this.configDataStringComparer = null;

            this.nowDateTime = DateTime.UtcNow;
        }
        /// <summary>
        ///     HistoricalAccessibleConfig 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="propertyConfig">속성 설정입니다.</param>
        protected HistoricalAccessibleConfig(PropertyConfig propertyConfig) : this()
        {
            this.propertyConfig = propertyConfig;

            this.configDatas = new ConfigDatas(new DateTimeSecondUnitComparer());
            this.configDatas.Add(this.nowDateTime, new ConfigData(this.configDataStringComparer));
        }
        /// <summary>
        ///     설정을 정렬하기 위한 IComparer 클래스를 지정하여 HistoricalAccessibleConfig 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="propertyConfig">속성 설정입니다.</param>
        /// <param name="configDataStringComparer">설정을 비교할 IComparer 클래스입니다.</param>
        protected HistoricalAccessibleConfig(PropertyConfig propertyConfig, IComparer<string> configDataStringComparer) : this()
        {
            this.propertyConfig = propertyConfig;

            this.configDataStringComparer = configDataStringComparer;

            this.configDatas = new ConfigDatas(new DateTimeSecondUnitComparer());
            this.configDatas.Add(this.nowDateTime, new ConfigData(this.configDataStringComparer));
        }

        /// <summary>
        ///     스트림에서 설정을 읽어옵니다.
        /// </summary>
        /// <param name="stream">읽어올 스트림입니다.</param>
        public override void Read(Stream stream)
        {
            DateTime currentDateTime = this.nowDateTime;
            StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);

            while (!streamReader.EndOfStream)
            {
                string data = streamReader.ReadLine();

                //시간 추출
                DateTime dateTime;
                Match match = Regex.Match(data, this.propertyConfig.DateTimeRegex, RegexOptions.Compiled);
                if (match.Success && DateTime.TryParse(match.Value, out dateTime))
                {
                    currentDateTime = dateTime;
                    continue;
                }

                //분할 코드 추출
                DivisionCode currentDivisionCode = AccessibleConfig.GetDivisionCode(data);
                if (currentDivisionCode != DivisionCode.None)
                {
                    string divisionString = AccessibleConfig.GetDivisionString(currentDivisionCode);
                    int index = data.IndexOf(divisionString);

                    string key = data.Substring(0, index);
                    string value = data.Substring(index + divisionString.Length);
                    DataPair dataPair = new DataPair(currentDivisionCode, value);

                    //읽어온 값의 접근 시간이랑 저장되어있던 값의 접근 시간을 비교 후 가장 최근 접근 시간을 추출
                    DateTime latestDateTime = currentDateTime;
                    foreach (ConfigDatasPair configDatasPair in this.configDatas)
                    {
                        if (configDatasPair.Value.ContainsKey(key))
                        {
                            latestDateTime = DateTime.Compare(latestDateTime, configDatasPair.Key) == 1 ? latestDateTime : configDatasPair.Key;
                            configDatasPair.Value.Remove(key);
                        }
                    }

                    //가장 최근 접근 시간으로 읽어온 설정을 등록
                    ConfigData configData;
                    if (!this.configDatas.TryGetValue(latestDateTime, out configData))
                    {
                        configData = new ConfigData(this.configDataStringComparer);
                        this.configDatas.Add(latestDateTime, configData);
                    }
                    configData.Add(key, dataPair);
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

            foreach (ConfigDatasPair configDatasPair in this.configDatas)
            {
                if (configDatasPair.Value.Count != 0)
                {
                    streamWriter.WriteLine(this.GetDateTimeSectionString(configDatasPair.Key));
                    foreach (ConfigDataPair configDataPair in configDatasPair.Value)
                        streamWriter.WriteLine(configDataPair.Key + AccessibleConfig.GetDivisionString(configDataPair.Value.Key) + configDataPair.Value.Value);
                }
            }

            streamWriter.Flush();
        }
        /// <summary>
        ///     설정을 비웁니다.
        /// </summary>
        public override void Clear()
        {
            foreach (ConfigDatasPair configDatasPair in this.configDatas)
                configDatasPair.Value.Clear();

            this.configDatas.Clear();
            this.configDatas.Add(this.nowDateTime, new ConfigData(this.configDataStringComparer));
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

            foreach (ConfigDatasPair configDatasPair in this.configDatas)
            {
                if (configDatasPair.Value.TryGetValue(key, out dataPair))
                {
                    //접근 시간이 현재 시간이 아니면
                    if (!configDatasPair.Key.Equals(this.nowDateTime))
                    {
                        //접근 시간 갱신
                        configDatasPair.Value.Remove(key);
                        this.configDatas[this.nowDateTime].Add(key, dataPair);
                    }

                    if (dataPair.Key == DivisionCode.Disable)
                        return new Tuple<DivisionCode, string>(DivisionCode.Disable, defaultValue);
                    else
                        return new Tuple<DivisionCode, string>(dataPair);
                }
            }

            this.configDatas[this.nowDateTime].Add(key, new DataPair(DivisionCode.Relative, defaultValue));
            return this.AccessConfig(configName, defaultValue);
        }

        /// <summary>
        ///     DateTime 클래스를 섹션 문자열로 변환합니다.
        /// </summary>
        /// <param name="dateTime">변환할 DateTime 클래스입니다.</param>
        /// <returns>변환된 섹션 문자열입니다.</returns>
        private string GetDateTimeSectionString(DateTime dateTime)
        {
            if (dateTime == null)
                throw new ArgumentNullException("dateTime", "Argument can not be null");

            string dateTimeString = dateTime.ToString(this.propertyConfig.DateTimeString);
            int count = this.propertyConfig.SectionStringMaxLength - dateTimeString.Length;
            int restLength = (count) / 2;
            int remainder = (count) % 2;

            StringBuilder dateTimeName = new StringBuilder();
            dateTimeName.Append('-', restLength);
            dateTimeName.Append(dateTimeString);
            dateTimeName.Append('-', restLength + remainder);
            return dateTimeName.ToString();
        }
    }
}
