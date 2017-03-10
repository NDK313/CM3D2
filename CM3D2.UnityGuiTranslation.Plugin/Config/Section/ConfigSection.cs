using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     설정 파일들을 섹션으로 분리해서 담은 클래스입니다.
    /// </summary>
    public sealed class ConfigSection : IStorable
    {
        private readonly PropertyConfig propertyConfig;

        private const string configEnd = "ConfigEnd";

        /// <summary>
        ///     설정 파일들을 분리하기 위한 Section 구조체입니다.
        /// </summary>
        public struct Section
        {
            private readonly string configName;
            private readonly Config config;

            /// <summary>
            ///     설정 이름입니다.
            /// </summary>
            public string ConfigName { get { return this.configName; } }
            /// <summary>
            ///     설정입니다.
            /// </summary>
            public Config Config { get { return this.config; } }

            /// <summary>
            ///     Section 구조체의 새 인스턴스를 초기화합니다.
            /// </summary>
            /// <param name="configName">설정 이름입니다.</param>
            /// <param name="config">설정입니다.</param>
            public Section(string configName, Config config)
            {
                this.configName = configName;
                this.config = config;
            }
        }
        private Section[] sections;

        /// <summary>
        ///     섹션들을 등록하고 ConfigSection 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="propertyConfig">속성 설정입니다.</param>
        /// <param name="sections">등록할 섹션들입니다.</param>
        public ConfigSection(PropertyConfig propertyConfig, Section[] sections)
        {
            this.propertyConfig = propertyConfig;

            this.sections = sections;
        }

        /// <summary>
        ///     스트림에서 섹션을 읽어옵니다.
        /// </summary>
        /// <param name="stream">읽어올 스트림입니다.</param>
        public void Read(Stream stream)
        {
            StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);

            while (!streamReader.EndOfStream)
            {
                Section section = default(Section);
                string configName = this.GetConfigName(streamReader.ReadLine());

                foreach (Section sec in this.sections)
                {
                    if (sec.ConfigName.Equals(configName))
                        section = sec;
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    StreamWriter streamWriter = new StreamWriter(memoryStream);

                    while (!streamReader.EndOfStream)
                    {
                        string data = this.GetConfigName(streamReader.ReadLine());
                        if (data == ConfigSection.configEnd)
                        {
                            streamWriter.Flush();
                            memoryStream.Seek(0, SeekOrigin.Begin);
                            if (!section.Equals(default(Section)))
                            {
                                section.Config.Read(memoryStream);
                                section.Config.Refresh();
                            }

                            break;
                        }
                        streamWriter.WriteLine(data);
                    }
                }
            }
        }
        /// <summary>
        ///     스트림으로 섹션을 출력합니다.
        /// </summary>
        /// <param name="stream">출력할 스트림입니다.</param>
        public void Write(Stream stream)
        {
            StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8);
            streamWriter.AutoFlush = true;

            foreach (Section sec in this.sections)
            {
                streamWriter.WriteLine(this.GetSectionString(sec.ConfigName));
                sec.Config.Write(streamWriter.BaseStream);
                streamWriter.WriteLine(this.GetSectionString(ConfigSection.configEnd));
            }
        }

        /// <summary>
        ///     등록된 섹션들의 설정들을 비웁니다.
        /// </summary>
        public void ClearConfigs()
        {
            foreach (Section sec in this.sections)
                sec.Config.Clear();
        }
        /// <summary>
        ///     등록된 섹션들의 설정들을 새로 고침합니다.
        /// </summary>
        public void RefreshConfigs()
        {
            foreach (Section sec in this.sections)
                sec.Config.Refresh();
        }

        /// <summary>
        ///     섹션 문자열에서 설정 이름을 추출합니다.
        ///     없으면 섹션 문자열을 반환합니다.
        /// </summary>
        /// <param name="sectionString">섹션 문자열입니다.</param>
        /// <returns>설정 이름 혹은 추출된 섹션 문자열입니다.</returns>
        private string GetConfigName(string sectionString)
        {
            Match match = Regex.Match(sectionString, @"=+&(\w+)&=+", RegexOptions.Compiled);

            if (match.Groups.Count >= 2 && match.Groups[1].Success)
                return match.Groups[1].Value;
            else
                return sectionString;
        }
        /// <summary>
        ///     설정 이름을 섹션 문자열로 변환합니다.
        /// </summary>
        /// <param name="configName">변환할 설정 이름입니다.</param>
        /// <returns>변환된 섹션 문자열입니다.</returns>
        private string GetSectionString(string configName)
        {
            int configNameLength = configName.Length + 2;
            int count = this.propertyConfig.SectionStringMaxLength - configNameLength;
            int restLength = (count) / 2;
            int remainder = (count) % 2;

            StringBuilder sectionString = new StringBuilder();
            sectionString.Append('=', restLength);
            sectionString.Append('&');
            sectionString.Append(configName);
            sectionString.Append('&');
            sectionString.Append('=', restLength + remainder);
            return sectionString.ToString();
        }
    }
}
