using System.IO;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     문장의 번역 설정을 담고 있는 클래스입니다.
    /// </summary>
    public sealed class SentenceTranslationConfig : HistoricalAccessibleConfig
    {
        /// <summary>
        ///     SentenceTranslationConfig 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="propertyConfig">속성 설정입니다.</param>
        public SentenceTranslationConfig(PropertyConfig propertyConfig) : base(propertyConfig, new StringLengthComparer()) { }

        /// <summary>
        ///     스트림에서 번역 설정을 읽어옵니다.
        /// </summary>
        /// <param name="stream">읽어올 스트림입니다.</param>
        public override void Read(Stream stream)
        {
            base.Read(stream);
        }
        /// <summary>
        ///     스트림으로 번역 설정을 출력합니다.
        /// </summary>
        /// <param name="stream">출력할 스트림입니다.</param>
        public override void Write(Stream stream)
        {
            base.Write(stream);
        }
        /// <summary>
        ///     번역 설정으로 새로 고침합니다.
        /// </summary>
        public override void Refresh() { }
    }
}
