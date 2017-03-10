using System;
using UnityEngine;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     UnityEngine.GUI 클래스를 대체하기 위한 클래스입니다.
    /// </summary>
    public class GUI
    {
        /// <summary>
        ///     GUI 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <remarks>
        ///     GUI 클래스가 생성되는 일은 없습니다.
        /// </remarks>
        private GUI() { }

        /// <summary>
        ///     GUIContent 클래스를 번역한 뒤 원본 함수를 호출합니다.
        /// </summary>
        /// <param name="position">원본 함수의 첫 번째 개체입니다.</param>
        /// <param name="content">번역할 GUIContent 클래스입니다.</param>
        /// <param name="style">원본 함수의 세 번째 개체입니다.</param>
        private static void DoLabel(Rect position, GUIContent content, IntPtr style)
        {
            GUI.DoLabel(position, UnityGuiTranslation.Instance.TranslationGUIContent(content), style);

            /*
            GUI.INTERNAL_CALL_DoLabel(ref position, content, style);
            */
        }

#pragma warning disable CS3001 // 인수 형식이 CLS 규격이 아닙니다.
        /// <summary>
        ///     GUIContent 클래스를 번역한 뒤 원본 함수를 호출합니다.
        /// </summary>
        /// <param name="position">원본 함수의 첫 번째 개체입니다.</param>
        /// <param name="content">번역할 GUIContent 클래스입니다.</param>
        /// <param name="style">원본 함수의 세 번째 개체입니다.</param>
        public static void Box(Rect position, GUIContent content, GUIStyle style)
#pragma warning restore CS3001 // 인수 형식이 CLS 규격이 아닙니다.
        {
            GUI.Box(position, UnityGuiTranslation.Instance.TranslationGUIContent(content), style);

            /*
            GUIUtility.CheckOnGUI();
            int controlId = GUIUtility.GetControlID(GUI.boxHash, FocusType.Passive);
            if (Event.current.type != EventType.Repaint)
                return;
            style.Draw(position, content, controlId);
            */
        }

        /// <summary>
        ///     GUIContent 클래스를 번역한 뒤 원본 함수를 호출합니다.
        /// </summary>
        /// <param name="position">원본 함수의 첫 번째 개체입니다.</param>
        /// <param name="content">번역할 GUIContent 클래스입니다.</param>
        /// <param name="style">원본 함수의 세 번째 개체입니다.</param>
        /// <returns>원본 함수의 반환 값입니다.</returns>
        private static bool DoButton(Rect position, GUIContent content, IntPtr style)
        {
            return GUI.DoButton(position, UnityGuiTranslation.Instance.TranslationGUIContent(content), style);

            /*
            return GUI.INTERNAL_CALL_DoButton(ref position, content, style);
            */
        }

        /// <summary>
        ///     GUIContent 클래스를 번역한 뒤 원본 함수를 호출합니다.
        /// </summary>
        /// <param name="position">원본 함수의 첫 번째 개체입니다.</param>
        /// <param name="content">번역할 GUIContent 클래스입니다.</param>
        /// <param name="style">원본 함수의 세 번째 개체입니다.</param>
        /// <param name="focusType">원본 함수의 네 번째 개체입니다.</param>
        /// <returns>원본 함수의 반환 값입니다.</returns>
        private static bool DoRepeatButton(Rect position, GUIContent content, GUIStyle style, FocusType focusType)
        {
            return GUI.DoRepeatButton(position, UnityGuiTranslation.Instance.TranslationGUIContent(content), style, focusType);

            /*
            GUIUtility.CheckOnGUI();
            int controlId = GUIUtility.GetControlID(GUI.repeatButtonHash, focusType, position);
            switch (Event.current.GetTypeForControl(controlId))
            {
                case EventType.MouseDown:
                    if (position.Contains(Event.current.mousePosition))
                    {
                        GUIUtility.hotControl = controlId;
                        Event.current.Use();
                    }
                    return false;
                case EventType.MouseUp:
                    if (GUIUtility.hotControl != controlId)
                        return false;
                    GUIUtility.hotControl = 0;
                    Event.current.Use();
                    return position.Contains(Event.current.mousePosition);
                case EventType.Repaint:
                    style.Draw(position, content, controlId);
                    if (controlId == GUIUtility.hotControl)
                        return position.Contains(Event.current.mousePosition);
                    return false;
                default:
                    return false;
            }
            */
        }

        /// <summary>
        ///     GUIContent 클래스를 번역한 뒤 원본 함수를 호출합니다.
        /// </summary>
        /// <param name="position">원본 함수의 첫 번째 개체입니다.</param>
        /// <param name="id">원본 함수의 두 번째 개체입니다.</param>
        /// <param name="content">번역할 GUIContent 클래스입니다.</param>
        /// <param name="multiline">원본 함수의 네 번째 개체입니다.</param>
        /// <param name="maxLength">원본 함수의 다섯 번째 개체입니다.</param>
        /// <param name="style">원본 함수의 여섯 번째 개체입니다.</param>
        internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style)
        {
            GUI.DoTextField(position, id, UnityGuiTranslation.Instance.TranslationGUIContent(content), multiline, maxLength, style);

            /*
            if (maxLength >= 0 && content.text.Length > maxLength)
                content.text = content.text.Substring(0, maxLength);
            GUIUtility.CheckOnGUI();
            TextEditor editor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), id);
            editor.content.text = content.text;
            editor.SaveBackup();
            editor.position = position;
            editor.style = style;
            editor.multiline = multiline;
            editor.controlID = id;
            editor.ClampPos();
            if (GUIUtility.keyboardControl == id && Event.current.type != EventType.Layout)
                editor.UpdateScrollOffsetIfNeeded();
            GUI.HandleTextFieldEventForDesktop(position, id, content, multiline, maxLength, style, editor);
            */
        }

        /// <summary>
        ///     GUIContent 클래스를 번역한 뒤 원본 함수를 호출합니다.
        /// </summary>
        /// <param name="position">원본 함수의 첫 번째 개체입니다.</param>
        /// <param name="id">원본 함수의 두 번째 개체입니다.</param>
        /// <param name="value">원본 함수의 세 번째 개체입니다.</param>
        /// <param name="content">번역할 GUIContent 클래스입니다.</param>
        /// <param name="style">원본 함수의 다섯 번째 개체입니다.</param>
        /// <returns>원본 함수의 반환 값입니다.</returns>
        internal static bool DoToggle(Rect position, int id, bool value, GUIContent content, IntPtr style)
        {
            return GUI.DoToggle(position, id, value, UnityGuiTranslation.Instance.TranslationGUIContent(content), style);

            /*
            return GUI.INTERNAL_CALL_DoToggle(ref position, id, value, content, style);
            */
        }

        /// <summary>
        ///     GUIContent 클래스를 번역한 뒤 원본 함수를 호출합니다.
        /// </summary>
        /// <param name="position">원본 함수의 첫 번째 개체입니다.</param>
        /// <param name="selected">원본 함수의 두 번째 개체입니다.</param>
        /// <param name="contents">번역할 GUIContent 클래스들입니다.</param>
        /// <param name="xCount">원본 함수의 네 번째 개체입니다.</param>
        /// <param name="style">원본 함수의 다섯 번째 개체입니다.</param>
        /// <param name="firstStyle">원본 함수의 여섯 번째 개체입니다.</param>
        /// <param name="midStyle">원본 함수의 일곱 번째 개체입니다.</param>
        /// <param name="lastStyle">원본 함수의 여덟 번째 개체입니다.</param>
        /// <returns>원본 함수의 반환 값입니다.</returns>
        private static int DoButtonGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
        {
            GUIContent[] translatedGUIContents = new GUIContent[contents.Length];

            for (int i = 0; i < translatedGUIContents.Length; ++i)
                translatedGUIContents[i] = UnityGuiTranslation.Instance.TranslationGUIContent(contents[i]);

            return GUI.DoButtonGrid(position, selected, translatedGUIContents, xCount, style, firstStyle, midStyle, lastStyle);

            /*
            GUIUtility.CheckOnGUI();
            int length = contents.Length;
            if (length == 0)
                return selected;
            if (xCount <= 0)
            {
                Debug.LogWarning((object)"You are trying to create a SelectionGrid with zero or less elements to be displayed in the horizontal direction. Set xCount to a positive value.");
                return selected;
            }
            int controlId = GUIUtility.GetControlID(GUI.buttonGridHash, FocusType.Native, position);
            int num1 = length / xCount;
            if (length % xCount != 0)
                ++num1;
            float num2 = (float)GUI.CalcTotalHorizSpacing(xCount, style, firstStyle, midStyle, lastStyle);
            float num3 = (float)(Mathf.Max(style.margin.top, style.margin.bottom) * (num1 - 1));
            float elemWidth = (position.width - num2) / (float)xCount;
            float elemHeight = (position.height - num3) / (float)num1;
            if ((double)style.fixedWidth != 0.0)
                elemWidth = style.fixedWidth;
            if ((double)style.fixedHeight != 0.0)
                elemHeight = style.fixedHeight;
            switch (Event.current.GetTypeForControl(controlId))
            {
                case EventType.MouseDown:
                    if (position.Contains(Event.current.mousePosition) && GUI.GetButtonGridMouseSelection(GUI.CalcMouseRects(position, length, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false), Event.current.mousePosition, true) != -1)
                    {
                        GUIUtility.hotControl = controlId;
                        Event.current.Use();
                        break;
                    }
                    break;
                case EventType.MouseUp:
                    if (GUIUtility.hotControl == controlId)
                    {
                        GUIUtility.hotControl = 0;
                        Event.current.Use();
                        int gridMouseSelection = GUI.GetButtonGridMouseSelection(GUI.CalcMouseRects(position, length, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false), Event.current.mousePosition, true);
                        GUI.changed = true;
                        return gridMouseSelection;
                    }
                    break;
                case EventType.MouseDrag:
                    if (GUIUtility.hotControl == controlId)
                    {
                        Event.current.Use();
                        break;
                    }
                    break;
                case EventType.Repaint:
                    GUIStyle guiStyle1 = (GUIStyle)null;
                    GUIClip.Push(position, Vector2.zero, Vector2.zero, false);
                    position = new Rect(0.0f, 0.0f, position.width, position.height);
                    Rect[] buttonRects = GUI.CalcMouseRects(position, length, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false);
                    int gridMouseSelection1 = GUI.GetButtonGridMouseSelection(buttonRects, Event.current.mousePosition, controlId == GUIUtility.hotControl);
                    GUIUtility.mouseUsed |= position.Contains(Event.current.mousePosition);
                    for (int index = 0; index < length; ++index)
                    {
                        GUIStyle guiStyle2 = index == 0 ? firstStyle : midStyle;
                        if (index == length - 1)
                            guiStyle2 = lastStyle;
                        if (length == 1)
                            guiStyle2 = style;
                        if (index != selected)
                            guiStyle2.Draw(buttonRects[index], contents[index], index == gridMouseSelection1 && (GUI.enabled || controlId == GUIUtility.hotControl) && (controlId == GUIUtility.hotControl || GUIUtility.hotControl == 0), controlId == GUIUtility.hotControl && GUI.enabled, false, false);
                        else
                            guiStyle1 = guiStyle2;
                    }
                    if (selected < length && selected > -1)
                        guiStyle1.Draw(buttonRects[selected], contents[selected], selected == gridMouseSelection1 && (GUI.enabled || controlId == GUIUtility.hotControl) && (controlId == GUIUtility.hotControl || GUIUtility.hotControl == 0), controlId == GUIUtility.hotControl, true, false);
                    if (gridMouseSelection1 >= 0)
                        GUI.tooltip = contents[gridMouseSelection1].tooltip;
                    GUIClip.Pop();
                    break;
            }
            return selected;
            */
        }

#pragma warning disable CS3001 // 인수 형식이 CLS 규격이 아닙니다.
        /// <summary>
        ///     GUIContent 클래스를 번역한 뒤 원본 함수를 호출합니다.
        /// </summary>
        /// <param name="position">원본 함수의 첫 번째 개체입니다.</param>
        /// <param name="content">번역할 GUIContent 클래스입니다.</param>
        /// <param name="style">원본 함수의 세 번째 개체입니다.</param>
        public static void BeginGroup(Rect position, GUIContent content, GUIStyle style)
#pragma warning restore CS3001 // 인수 형식이 CLS 규격이 아닙니다.
        {
            GUI.BeginGroup(position, UnityGuiTranslation.Instance.TranslationGUIContent(content), style);

            /*
            GUIUtility.CheckOnGUI();
            int controlId = GUIUtility.GetControlID(GUI.beginGroupHash, FocusType.Passive);
            if (content != GUIContent.none || style != GUIStyle.none)
            {
                if (Event.current.type == EventType.Repaint)
                    style.Draw(position, content, controlId);
                else if (position.Contains(Event.current.mousePosition))
                    GUIUtility.mouseUsed = true;
            }
            GUIClip.Push(position, Vector2.zero, Vector2.zero, false);
            */
        }

        /// <summary>
        ///     GUIContent 클래스를 번역한 뒤 원본 함수를 호출합니다.
        /// </summary>
        /// <param name="id">원본 함수의 첫 번째 개체입니다.</param>
        /// <param name="clientRect">원본 함수의 두 번째 개체입니다.</param>
        /// <param name="func">원본 함수의 세 번째 개체입니다.</param>
        /// <param name="content">번역할 GUIContent 클래스입니다.</param>
        /// <param name="style">원본 함수의 다섯 번째 개체입니다.</param>
        /// <param name="skin">원본 함수의 여섯 번째 개체입니다.</param>
        /// <returns>원본 함수의 반환 값입니다.</returns>
        private static Rect DoModalWindow(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, GUIContent content, GUIStyle style, GUISkin skin)
        {
            return GUI.DoModalWindow(id, clientRect, func, UnityGuiTranslation.Instance.TranslationGUIContent(content), style, skin);

            /*
            return GUI.INTERNAL_CALL_DoModalWindow(id, ref clientRect, func, content, style, skin);
            */
        }

        /// <summary>
        ///     GUIContent 클래스를 번역한 뒤 원본 함수를 호출합니다.
        /// </summary>
        /// <param name="id">원본 함수의 첫 번째 개체입니다.</param>
        /// <param name="clientRect">원본 함수의 두 번째 개체입니다.</param>
        /// <param name="func">원본 함수의 세 번째 개체입니다.</param>
        /// <param name="title">번역할 GUIContent 클래스입니다.</param>
        /// <param name="style">원본 함수의 다섯 번째 개체입니다.</param>
        /// <param name="skin">원본 함수의 여섯 번째 개체입니다.</param>
        /// <param name="forceRectOnLayout">원본 함수의 일곱 번째 개체입니다.</param>
        /// <returns>원본 함수의 반환 값입니다.</returns>
        private static Rect DoWindow(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, GUIContent title, GUIStyle style, GUISkin skin, bool forceRectOnLayout)
        {
            return GUI.DoWindow(id, clientRect, func, UnityGuiTranslation.Instance.TranslationGUIContent(title), style, skin, forceRectOnLayout);

            /*
            return GUI.INTERNAL_CALL_DoWindow(id, ref clientRect, func, title, style, skin, forceRectOnLayout);
            */
        }
    }
}
