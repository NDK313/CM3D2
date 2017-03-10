using System.IO;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     플러그인 설정을 담고 있는 클래스입니다.
    /// </summary>
    public sealed class PluginConfig : StatedAccessibleConfig
    {
        private bool extractionActivate;
        private bool translationActivateDefault;
        private string translationActivateToggleKey;
        private string translationRefreshKey;

        /// <summary>
        ///     추출의 활성화 값입니다.
        /// </summary>
        public bool ExtractionActivate { get { return this.extractionActivate; } }
        /// <summary>
        ///     번역의 활성화 기본 값입니다.
        /// </summary>
        public bool TranslationActivateDefault { get { return this.translationActivateDefault; } }
        /// <summary>
        ///     번역의 활성화 토글 키입니다.
        /// </summary>
        public string TranslationActivateToggleKey { get { return this.translationActivateToggleKey; } }
        /// <summary>
        ///     번역의 새로 고침 키입니다.
        /// </summary>
        public string TranslationRefreshKey { get { return this.translationRefreshKey; } }

        /// <summary>
        ///     PluginConfig 클래스의 새 인스턴스를 초기화 합니다.
        /// </summary>
        public PluginConfig() : base() { }

        /// <summary>
        ///     스트림에서 플러그인 설정을 읽어옵니다.
        /// </summary>
        /// <param name="stream">읽어올 스트림입니다.</param>
        public override void Read(Stream stream)
        {
            base.Read(stream);
        }
        /// <summary>
        ///     스트림으로 플러그인 설정을 출력합니다.
        /// </summary>
        /// <param name="stream">출력할 스트림입니다.</param>
        public override void Write(Stream stream)
        {
            base.Write(stream);
        }
        /// <summary>
        ///     플러그인 설정으로 새로 고침합니다.
        /// </summary>
        public override void Refresh()
        {
            this.extractionActivate = this.AccessConfig("ExtractionActivate", true).t2;
            this.translationActivateDefault = this.AccessConfig("TranslationActivateDefault", true).t2;
            this.translationActivateToggleKey = this.AccessConfig("TranslationActivateToggleKey", "f1").t2;
            this.translationRefreshKey = this.AccessConfig("TranslationRefreshKey", "r").t2;
        }
    }
}
