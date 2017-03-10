using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityInjector;
using UnityInjector.Attributes;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     UnityGuiTranslation 플러그인의 메인 클래스입니다.
    /// </summary>
    [PluginName("UnityGuiTranslation")]
    [PluginVersion("1.5.1")]
    [PluginFilter("CM3D2x86")]
    [PluginFilter("CM3D2x64")]
    //OH 버전은 모르지만 통상 버전이랑 비슷할 것이다?
    [PluginFilter("CM3D2OHx86")]
    [PluginFilter("CM3D2OHx64")]
    //VR 버전이랑 OHVR 버전은 전혀 모르므로... 알아서 컴파일 ㅎㅎ
    //[PluginFilter("CM3D2VRx64")]
    //[PluginFilter("CM3D2OHVRx64")]
#pragma warning disable CS3009 // 기본 형식이 CLS 규격이 아닙니다.
    public sealed class UnityGuiTranslation : PluginBase
#pragma warning restore CS3009 // 기본 형식이 CLS 규격이 아닙니다.
    {
        private struct MethodInfo
        {
            private readonly string methodName;
            private readonly BindingFlags bindingFlag;
            private readonly Type[] types;

            public string MethodName { get { return this.methodName; } }
            public BindingFlags BindingFlag { get { return this.bindingFlag; } }
            public Type[] Types { get { return this.types; } }

            public MethodInfo(string methodName, BindingFlags bindingFlag, Type[] types)
            {
                this.methodName = methodName;
                this.bindingFlag = bindingFlag;
                this.types = types;
            }
        }

        private static UnityGuiTranslation instance = null;

        private readonly PluginConfig pluginConfig;
        private readonly PropertyConfig propertyConfig;
        private readonly HookMethodConfig hookMethodConfig;
        private readonly SentenceTranslationConfig sentenceTranslationConfig;
        private readonly WordTranslationConfig wordTranslationConfig;
        private readonly ConfigSection configSection;

        private readonly SequenceCache<GUIContent, GUIContent> guicontentCache;
        private readonly SequenceCache<string, string> stringCache;

        private readonly List<Hooker> methodHookerList;

        private Regex wordRegex;

        private Regex numberRegex;
        private Regex numberReplacementRegex;

        private bool translationActivate;

        /// <summary>
        ///     UnityGuiTranslation 클래스의 최초로 생성된 인스턴스입니다.
        /// </summary>
        public static UnityGuiTranslation Instance
        {
            private set
            {
                if (UnityGuiTranslation.instance == null)
                    UnityGuiTranslation.instance = value;
            }
            get
            {
                return UnityGuiTranslation.instance;
            }
        }

        /// <summary>
        ///     UnityGuiTranslation 플러그인의 PluginConfig 입니다.
        /// </summary>
        public PluginConfig PluginConfig { get { return this.pluginConfig; } }
        /// <summary>
        ///     UnityGuiTranslation 플러그인의 PropertyConfig 입니다.
        /// </summary>
        public PropertyConfig PropertyConfig { get { return this.propertyConfig; } }
        /// <summary>
        ///     UnityGuiTranslation 플러그인의 HookMethodConfig 입니다.
        /// </summary>
        public HookMethodConfig HookMethodConfig { get { return this.hookMethodConfig; } }
        /// <summary>
        ///     UnityGuiTranslation 플러그인의 WordTranslationConfig 입니다.
        /// </summary>
        public WordTranslationConfig WordTranslationConfig { get { return this.wordTranslationConfig; } }
        /// <summary>
        ///     UnityGuiTranslation 플러그인의 SentenceTranslationConfig 입니다.
        /// </summary>
        public SentenceTranslationConfig SentenceTranslationConfig { get { return this.sentenceTranslationConfig; } }

        /// <summary>
        ///     UnityGuiTranslation 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        private UnityGuiTranslation()
        {
            UnityGuiTranslation.Instance = this;

            //CLR 버전 출력
            Debug.Log("CLR Version : " + Environment.Version);

            //ConfigSection 파일 초기화
            this.pluginConfig = new PluginConfig();
            this.propertyConfig = new PropertyConfig();
            this.hookMethodConfig = new HookMethodConfig();
            this.wordTranslationConfig = new WordTranslationConfig(this.PropertyConfig);
            this.sentenceTranslationConfig = new SentenceTranslationConfig(this.PropertyConfig);
            ConfigSection.Section[] sections = new ConfigSection.Section[]
            {
                new ConfigSection.Section("PluginConfig", this.PluginConfig),
                new ConfigSection.Section("PropertyConfig", this.PropertyConfig),
                new ConfigSection.Section("HookMethodConfig", this.HookMethodConfig),
                new ConfigSection.Section("WordTranslationConfig", this.WordTranslationConfig),
                new ConfigSection.Section("SentenceTranslationConfig", this.SentenceTranslationConfig)
            };
            this.configSection = new ConfigSection(this.PropertyConfig, sections);
            this.configSection.RefreshConfigs();
            this.RefreshConfigSectionFile();

            this.guicontentCache = new SequenceCache<GUIContent, GUIContent>();
            this.stringCache = new SequenceCache<string, string>();

            this.methodHookerList = new List<Hooker>();
            //후크할 메서드의 정보
            {
                List<MethodInfo> methodInfoList = new List<MethodInfo>();

                if (this.HookMethodConfig.HookDoLabel) methodInfoList.Add(new MethodInfo("DoLabel", BindingFlags.NonPublic | BindingFlags.Static, new Type[] { typeof(Rect), typeof(GUIContent), typeof(IntPtr) }));
                if (this.HookMethodConfig.HookBox) methodInfoList.Add(new MethodInfo("Box", BindingFlags.Public | BindingFlags.Static, new Type[] { typeof(Rect), typeof(GUIContent), typeof(GUIStyle) }));
                if (this.HookMethodConfig.HookDoButton) methodInfoList.Add(new MethodInfo("DoButton", BindingFlags.NonPublic | BindingFlags.Static, new Type[] { typeof(Rect), typeof(GUIContent), typeof(IntPtr) }));
                if (this.HookMethodConfig.HookDoRepeatButton) methodInfoList.Add(new MethodInfo("DoRepeatButton", BindingFlags.NonPublic | BindingFlags.Static, new Type[] { typeof(Rect), typeof(GUIContent), typeof(GUIStyle), typeof(FocusType) }));
                if (this.HookMethodConfig.HookDoTextField) methodInfoList.Add(new MethodInfo("DoTextField", BindingFlags.NonPublic | BindingFlags.Static, new Type[] { typeof(Rect), typeof(int), typeof(GUIContent), typeof(bool), typeof(int), typeof(GUIStyle) }));
                if (this.HookMethodConfig.HookDoToggle) methodInfoList.Add(new MethodInfo("DoToggle", BindingFlags.NonPublic | BindingFlags.Static, new Type[] { typeof(Rect), typeof(int), typeof(bool), typeof(GUIContent), typeof(IntPtr) }));
                if (this.HookMethodConfig.HookDoButtonGrid) methodInfoList.Add(new MethodInfo("DoButtonGrid", BindingFlags.NonPublic | BindingFlags.Static, new Type[] { typeof(Rect), typeof(int), typeof(GUIContent[]), typeof(int), typeof(GUIStyle), typeof(GUIStyle), typeof(GUIStyle), typeof(GUIStyle) }));
                if (this.HookMethodConfig.HookBeginGroup) methodInfoList.Add(new MethodInfo("BeginGroup", BindingFlags.Public | BindingFlags.Static, new Type[] { typeof(Rect), typeof(GUIContent), typeof(GUIStyle) }));
                if (this.HookMethodConfig.HookDoModalWindow) methodInfoList.Add(new MethodInfo("DoModalWindow", BindingFlags.NonPublic | BindingFlags.Static, new Type[] { typeof(int), typeof(Rect), typeof(UnityEngine.GUI.WindowFunction), typeof(GUIContent), typeof(GUIStyle), typeof(GUISkin) }));
                if (this.HookMethodConfig.HookDoWindow) methodInfoList.Add(new MethodInfo("DoWindow", BindingFlags.NonPublic | BindingFlags.Static, new Type[] { typeof(int), typeof(Rect), typeof(UnityEngine.GUI.WindowFunction), typeof(GUIContent), typeof(GUIStyle), typeof(GUISkin), typeof(bool) }));

                foreach (MethodInfo methodInfo in methodInfoList)
                {
                    MethodBase srcMethod = typeof(UnityEngine.GUI).GetMethod(methodInfo.MethodName, methodInfo.BindingFlag, null, methodInfo.Types, null);
                    MethodBase newMethod = typeof(GUI).GetMethod(methodInfo.MethodName, methodInfo.BindingFlag, null, methodInfo.Types, null);

                    this.methodHookerList.Add(new MethodHooker(MethodHooker.HookDirection.Both, srcMethod, newMethod));
                }
            }
        }
        /// <summary>
        ///     UnityGuiTranslation 클래스의 인스턴스를 해제합니다.
        /// </summary>
        ~UnityGuiTranslation()
        {
            if (UnityGuiTranslation.Instance == this)
                UnityGuiTranslation.instance = null;
        }

        ///<summary>
        ///     UnityGuiTranslation 플러그인이 활성화될 때 ConfigSection 파일을 불러오고, 관련 메서드를 후크하고, UnityGuiTranslation 클래스의 내부 변수를 초기화합니다.
        ///</summary>
        internal void OnEnable()
        {
            //후크
            foreach (MethodHooker methodHooker in this.methodHookerList)
                methodHooker.DetainHook();

            this.configSection.RefreshConfigs();

            this.RefreshConfigSectionFile();

            this.wordRegex = new Regex(this.PropertyConfig.WordRegex, RegexOptions.Compiled);

            this.numberRegex = new Regex(this.PropertyConfig.NumberRegex, RegexOptions.Compiled);
            this.numberReplacementRegex = new Regex(this.PropertyConfig.NumberReplacementCharacter, RegexOptions.Compiled);

            this.translationActivate = this.PluginConfig.TranslationActivateDefault;
        }
        ///<summary>
        ///     UnityGuiTranslation 플러그인이 비활성화될 때 ConfigSection 파일을 저장하고, 관련 메서드를 언 후크합니다.
        ///</summary>
        internal void OnDisable()
        {
            //언 후크
            foreach (MethodHooker methodHooker in this.methodHookerList)
                methodHooker.ReleaseHook();

            this.RefreshConfigSectionFile();

            this.configSection.ClearConfigs();
        }

        ///<summary>
        ///     프레임이 업데이트될 때 키 입력을 받고, 해당되는 행동을 합니다.
        ///</summary>
        internal void Update()
        {
            //토글 키
            if (Input.GetKeyDown(this.PluginConfig.TranslationActivateToggleKey))
            {
                this.translationActivate = !this.translationActivate;

                this.RefreshConfigSectionFile();

                if (!this.translationActivate)
                    this.configSection.ClearConfigs();

                Debug.Log("UnityGuiTranslation Activate : " + this.translationActivate);

                this.guicontentCache.Clear();
                this.stringCache.Clear();
            }
            //번역 활성화되었을 때만
            if (this.translationActivate)
            {
                //새로 고침
                if (Input.GetKeyDown(this.PluginConfig.TranslationRefreshKey))
                {
                    this.RefreshConfigSectionFile();

                    Debug.Log("UnityGuiTranslation Refresh");

                    this.guicontentCache.Clear();
                    this.stringCache.Clear();
                }

                //프레임마다 캐시 위치 초기화
                this.guicontentCache.Reset();
                this.stringCache.Reset();
            }
        }

        ///<summary>
        ///     ConfigSection 파일을 저장합니다.
        ///</summary>
        private void SaveConfigSectionFile()
        {
            using (FileStream fileStream = new FileStream(this.GetConfigSectionFilePath(), FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                BufferedStream bufferedStream = new BufferedStream(fileStream);

                try
                {
                    this.configSection.Write(bufferedStream);
                    bufferedStream.Flush();

                    fileStream.SetLength(fileStream.Position);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                    Debug.LogException(e);
                }
            }
        }
        ///<summary>
        ///     ConfigSection 파일을 불러옵니다.
        ///</summary>
        private void LoadConfigSectionFile()
        {
            using (FileStream fileStream = new FileStream(this.GetConfigSectionFilePath(), FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                BufferedStream bufferedStream = new BufferedStream(fileStream);

                try
                {
                    this.configSection.Read(bufferedStream);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                    Debug.LogException(e);
                }
            }
        }
        ///<summary>
        ///     ConfigSection 파일을 새로 고침합니다.
        ///</summary>
        public void RefreshConfigSectionFile()
        {
            if (!this.PluginConfig.ExtractionActivate)
            {
                this.configSection.ClearConfigs();
                this.configSection.RefreshConfigs();
            }

            this.LoadConfigSectionFile();
            this.SaveConfigSectionFile();
        }

#pragma warning disable CS3002 // 반환 형식이 CLS 규격이 아닙니다.
#pragma warning disable CS3001 // 인수 형식이 CLS 규격이 아닙니다.
        /// <summary>
        ///     GUIContent 클래스를 번역하고 반환합니다.
        /// </summary>
        /// <param name="guicontent">번역할 GUIContent 클래스입니다.</param>
        /// <returns>번역된 GUIContent 클래스입니다.</returns>
        public GUIContent TranslationGUIContent(GUIContent guicontent)
#pragma warning restore CS3001 // 인수 형식이 CLS 규격이 아닙니다.
#pragma warning restore CS3002 // 반환 형식이 CLS 규격이 아닙니다.
        {
            if (this.translationActivate && guicontent != null)
            {
                return this.guicontentCache.GetValue(guicontent, (GUIContent content) =>
                {
                    return new GUIContent(this.TranslationString(guicontent.text), guicontent.image, guicontent.tooltip);
                },
                (GUIContent x, GUIContent y) =>
                {
                    return x.text.Equals(y);
                });
            }
            else
                return guicontent;
        }
        /// <summary>
        ///     문자열을 번역하고 반환합니다.
        /// </summary>
        /// <param name="str">번역할 문자열입니다.</param>
        /// <returns>번역된 문자열입니다.</returns>
        public string TranslationString(string str)
        {
            if (this.translationActivate && str != null)
            {
                //캐시에 저장되어 있는 경우 캐시에서 반환, 아니면 함수에서 얻은 값을 캐시에 등록, 반환
                return this.stringCache.GetValue(str, (string s) =>
                {
                    //숫자 치환
                    MatchCollection numberMatchCollection = this.numberRegex.Matches(s);
                    s = this.numberRegex.Replace(s, this.PropertyConfig.NumberReplacementCharacter);

                    //문장 번역
                    Tuple<Config.DivisionCode, string> sentencePair = this.SentenceTranslationConfig.AccessConfig(s, s);
                    s = sentencePair.t2;

                    //상대 지정일 경우에만 단어 번역
                    if (sentencePair.t1 == Config.DivisionCode.Relative)
                    {
                        MatchCollection stringMatchCollection = this.wordRegex.Matches(s);
                        foreach (Match stringMatch in stringMatchCollection)
                        {
                            if (stringMatch.Success && Encoding.UTF8.GetBytes(stringMatch.Value).Length >= this.PropertyConfig.WordMinByteLength)
                                s = s.Replace(stringMatch.Value, this.WordTranslationConfig.AccessConfig(stringMatch.Value, stringMatch.Value).t2);
                        }
                    }

                    //숫자 원상복구
                    foreach (Match numberMatch in numberMatchCollection)
                    {
                        if (numberMatch.Success)
                            s = this.numberReplacementRegex.Replace(s, numberMatch.Value, 1);
                    }

                    return s;
                });
            }
            else
                return str;
        }

        /// <summary>
        ///     UnityGuiTranslation 플러그인이 로드된 디렉터리 경로를 반환합니다.
        /// </summary>
        /// <returns>DLL의 디렉터리 경로</returns>
        private string GetCurrentDirectoryPath()
        {
            string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            return Regex.Replace(currentDirectory, @"^(file\:\\)", string.Empty) + @"\";
        }
        /// <summary>
        ///     UnityGuiTranslation 플러그인의 Config 디렉터리 경로를 반환합니다.
        /// </summary>
        /// <returns>Config 디렉터리 경로</returns>
        private string GetConfigDirectoryPath()
        {
            return this.GetCurrentDirectoryPath() + @"Config\";
        }
        /// <summary>
        ///     UnityGuiTranslation 플러그인의 ConfigSection 파일 경로를 반환합니다.
        /// </summary>
        /// <returns>ConfigSection 파일 경로</returns>
        private string GetConfigSectionFilePath()
        {
            return this.GetConfigDirectoryPath() + @"UnityGuiTranslation.txt";
        }
    }
}
