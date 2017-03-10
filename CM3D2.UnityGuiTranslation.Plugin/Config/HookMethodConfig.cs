using System.IO;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     후크 설정을 담고 있는 클래스입니다.
    /// </summary>
    public sealed class HookMethodConfig : StatedAccessibleConfig
    {
        private bool hookDoLabel;
        private bool hookBox;
        private bool hookDoButton;
        private bool hookDoRepeatButton;
        private bool hookDoTextField;
        private bool hookDoToggle;
        private bool hookDoButtonGrid;
        private bool hookBeginGroup;
        private bool hookDoModalWindow;
        private bool hookDoWindow;

        /// <summary>
        ///     DoLabel 메소드의 후크 설정입니다.
        /// </summary>
        public bool HookDoLabel { get { return this.hookDoLabel; } }
        /// <summary>
        ///     Box 메소드의 후크 설정입니다.
        /// </summary>
        public bool HookBox { get { return this.hookBox; } }
        /// <summary>
        ///     DoButton 메소드의 후크 설정입니다.
        /// </summary>
        public bool HookDoButton { get { return this.hookDoButton; } }
        /// <summary>
        ///     DoRepeatButton 메소드의 후크 설정입니다.
        /// </summary>
        public bool HookDoRepeatButton { get { return this.hookDoRepeatButton; } }
        /// <summary>
        ///     DoTextField 메소드의 후크 설정입니다.
        /// </summary>
        public bool HookDoTextField { get { return this.hookDoTextField; } }
        /// <summary>
        ///     DoToggle 메소드의 후크 설정입니다.
        /// </summary>
        public bool HookDoToggle { get { return this.hookDoToggle; } }
        /// <summary>
        ///     DoButtonGrid 메소드의 후크 설정입니다.
        /// </summary>
        public bool HookDoButtonGrid { get { return this.hookDoButtonGrid; } }
        /// <summary>
        ///     BeginGroup 메소드의 후크 설정입니다.
        /// </summary>
        public bool HookBeginGroup { get { return this.hookBeginGroup; } }
        /// <summary>
        ///     DoModalWindow 메소드의 후크 설정입니다.
        /// </summary>
        public bool HookDoModalWindow { get { return this.hookDoModalWindow; } }
        /// <summary>
        ///     DoWindow 메소드의 후크 설정입니다.
        /// </summary>
        public bool HookDoWindow { get { return this.hookDoWindow; } }

        /// <summary>
        ///     PluginConfig 클래스의 새 인스턴스를 초기화 합니다.
        /// </summary>
        public HookMethodConfig() : base() { }

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
            this.hookDoLabel = this.AccessConfig("HookDoLabel", true).t2;
            this.hookBox = this.AccessConfig("HookBox", true).t2;
            this.hookDoButton = this.AccessConfig("HookDoButton", true).t2;
            this.hookDoRepeatButton = this.AccessConfig("HookDoRepeatButton", true).t2;
            this.hookDoTextField = this.AccessConfig("HookDoTextField", false).t2;
            this.hookDoToggle = this.AccessConfig("HookDoToggle", true).t2;
            this.hookDoButtonGrid = this.AccessConfig("HookDoButtonGrid", true).t2;
            this.hookBeginGroup = this.AccessConfig("HookBeginGroup", true).t2;
            this.hookDoModalWindow = this.AccessConfig("HookDoModalWindow", true).t2;
            this.hookDoWindow = this.AccessConfig("HookDoWindow", true).t2;
        }
    }
}
