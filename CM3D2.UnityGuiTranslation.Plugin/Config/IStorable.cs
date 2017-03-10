using System.IO;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     저장 가능한 인터페이스입니다.
    /// </summary>
    public interface IStorable
    {
        /// <summary>
        ///     스트림에서 읽어옵니다.
        /// </summary>
        /// <param name="stream">읽어올 스트림입니다.</param>
        void Read(Stream stream);
        /// <summary>
        ///     스트림으로 출력합니다.
        /// </summary>
        /// <param name="stream">출력할 스트림입니다.</param>
        void Write(Stream stream);
    }
}
