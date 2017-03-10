using System;
using System.Reflection;

namespace CM3D2.UnityGuiTranslation.Plugin
{
    /// <summary>
    ///     메서드와 관련된 유틸 전역 클래스입니다.
    /// </summary>
    /// <remarks>
    ///     ※CLR 2.0.50727.1433 에서만 작동합니다.
    /// </remarks>
    public static class MethodUtil
    {
        /// <summary>
        ///     메서드의 데이터 구조체입니다.
        /// </summary>
        public struct MethodData
        {
            /// <summary>
            ///     메서드의 데이터 시작 지점입니다.
            /// </summary>
            public const byte dataBegin = 4;
            /// <summary>
            ///     메서드의 데이터 길이입니다.
            /// </summary>
            public const byte dataLength = 8;

            /// <summary>
            ///     메서드의 접근 제한자의 종류입니다.
            /// </summary>
            public enum AccessModifier
            {
                /// <summary>
                ///     없음
                /// </summary>
                None = 0,
                /// <summary>
                ///     Private 접근 제한자
                /// </summary>
                Private = 0x91,
                /// <summary>
                ///     Internal 접근 제한자
                /// </summary>
                Internal = 0x93,
                /// <summary>
                ///     ProtectedInternal 접근 제한자
                /// </summary>
                ProtectedInternal = 0x93,
                /// <summary>
                ///     Protected 접근 제한자
                /// </summary>
                Protected = 0x94,
                /// <summary>
                ///     Public 접근 제한자
                /// </summary>
                Public = 0x96
            }
            private AccessModifier methodAccessModifier;
            private byte[] data;

            /// <summary>
            ///     메서드의 접근 제한자입니다.
            /// </summary>
            public AccessModifier MethodAccessModifier { set { this.methodAccessModifier = value; } get { return this.methodAccessModifier; } }
            /// <summary>
            ///     메서드의 데이터입니다.
            /// </summary>
            public byte[] Data { set { this.data = value; } get { return this.data; } }

            /// <summary>
            ///     MethodData 구조체의 새 인스턴스를 초기화합니다.
            /// </summary>
            /// <param name="methodAccessModifier">저장할 메서드의 접근 제한자입니다.</param>
            /// <param name="data">저장할 메서드의 데이터입니다.</param>
            public MethodData(AccessModifier methodAccessModifier, byte[] data)
            {
                this.methodAccessModifier = methodAccessModifier;
                this.data = data;
            }
        }

        /// <summary>
        ///     강제로 메서드에 메서드 데이터를 저장합니다.
        /// </summary>
        /// <param name="methodBase">메서드 데이터를 저장할 메서드입니다.</param>
        /// <param name="methodData">저장된 메서드 데이터입니다.</param>
        public static void SetMethodData(MethodBase methodBase, MethodData methodData)
        {
            if (methodBase == null)
                throw new ArgumentNullException("methodBase", "Argument can not be null");

            unsafe
            {
                IntPtr address = methodBase.MethodHandle.Value;
                *(byte*)address.ToPointer() = (byte)methodData.MethodAccessModifier;
                for (int i = 0; i < MethodData.dataLength; ++i)
                    *((byte*)address.ToPointer() + MethodData.dataBegin + i) = methodData.Data[i];
            }
        }
        /// <summary>
        ///     강제로 메서드에서 메서드 데이터를 불러옵니다.
        /// </summary>
        /// <param name="methodBase">메서드 데이터를 불러올 메서드입니다.</param>
        /// <returns>불러온 메서드 데이터입니다.</returns>
        public static MethodData GetMethodData(MethodBase methodBase)
        {
            if (methodBase == null)
                throw new ArgumentNullException("methodBase", "Argument can not be null");

            MethodData methodData = new MethodData();

            unsafe
            {
                IntPtr address = methodBase.MethodHandle.Value;
                methodData.MethodAccessModifier = (MethodData.AccessModifier)(*(byte*)address.ToPointer());
                methodData.Data = new byte[MethodData.dataLength];
                for (int i = 0; i < MethodData.dataLength; ++i)
                    methodData.Data[i] = *((byte*)address.ToPointer() + MethodData.dataBegin + i);
            }

            return methodData;
        }
    }
}
