using System.IO;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     속성 설정을 담고 있는 클래스입니다.
    /// </summary>
    public sealed class PropertyConfig : StatedAccessibleConfig
    {
        private int sectionStringMaxLength;

        private string wordRegex;
        private int wordMinByteLength;

        private string numberRegex;
        private string numberReplacementCharacter;

        private string dateTimeRegex;
        private string dateTimeString;

        /// <summary>
        ///     섹션 문자열의 최대 길이입니다.
        /// </summary>
        public int SectionStringMaxLength { get { return this.sectionStringMaxLength; } }

        /// <summary>
        ///     단어을 문자열에서 추출하는 정규식 패턴입니다.
        /// </summary>
        public string WordRegex { get { return this.wordRegex; } }
        /// <summary>
        ///     단어의 최소 Byte 길이입니다.
        /// </summary>
        public int WordMinByteLength { get { return this.wordMinByteLength; } }

        /// <summary>
        ///     숫자를 문자열에서 추출하는 정규식 패턴입니다.
        /// </summary>
        public string NumberRegex { get { return this.numberRegex; } }
        /// <summary>
        ///     숫자를 대체할 문자입니다.
        /// </summary>
        public string NumberReplacementCharacter { get { return this.numberReplacementCharacter; } }

        /// <summary>
        ///     DateTime 을 문자열에서 추출하는 정규식 패턴입니다.
        /// </summary>
        public string DateTimeRegex { get { return this.dateTimeRegex; } }
        /// <summary>
        ///     DateTime 클래스를 문자열로 출력할 패턴입니다.
        /// </summary>
        public string DateTimeString { get { return this.dateTimeString; } }


        /// <summary>
        ///     PropertyConfig 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        public PropertyConfig() : base() { }

        /// <summary>
        ///     스트림에서 속성 설정을 읽어옵니다.
        /// </summary>
        /// <param name="stream">읽어올 스트림입니다.</param>
        public override void Read(Stream stream)
        {
            base.Read(stream);
        }
        /// <summary>
        ///     스트림으로 속성 설정을 출력합니다.
        /// </summary>
        /// <param name="stream">출력할 스트림입니다.</param>
        public override void Write(Stream stream)
        {
            base.Write(stream);
        }
        /// <summary>
        ///     속성 설정으로 새로 고침합니다.
        /// </summary>
        public override void Refresh()
        {
            this.sectionStringMaxLength = this.AccessConfig("SectionStringMaxLength", 100).t2;

            this.wordRegex = this.AccessConfig("WordRegex", @"[\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Lm}]+").t2;
            this.wordMinByteLength = this.AccessConfig("WordMinByteLength", 3).t2;

            this.dateTimeRegex = this.AccessConfig("DateTimeRegex", @"\d+-\d+-\d+\s+\d+:\d+:\d+").t2;
            this.dateTimeString = this.AccessConfig("DateTimeString", @"yyyy-MM-dd HH:mm:ss").t2;

            this.numberRegex = this.AccessConfig("NumberRegex", @"\d+").t2;
            this.numberReplacementCharacter = this.AccessConfig("NumberReplacementCharacter", @"&d&").t2;
        }
    }
}
