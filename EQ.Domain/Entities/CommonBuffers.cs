using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EQ.Domain.Entities
{
    // --- [1] 범용 고정 크기 버퍼 (Generic Inline Arrays) ---

    [InlineArray(16)]
    public struct Buffer16<T>
    {
        private T _element0;
    }

    [InlineArray(32)]
    public struct Buffer32<T>
    {
        private T _element0;
    }

    [InlineArray(64)]
    public struct Buffer64<T>
    {
        private T _element0;
    }

    [InlineArray(128)]
    public struct Buffer128<T>
    {
        private T _element0;
    }

    // --- [2] 편의성 확장 메서드 (Helper) ---
    public static class BufferExtensions
    {
        // BufferN<char> -> string 변환
        public static void SetText<TBuffer>(this ref TBuffer buffer, string text) where TBuffer : struct
        {
            // 구조체를 Span<char>로 캐스팅 (InlineArray 특성 활용)
            Span<char> span = MemoryMarshal.CreateSpan(ref Unsafe.As<TBuffer, char>(ref buffer), Unsafe.SizeOf<TBuffer>() / sizeof(char));

            span.Clear();
            if (string.IsNullOrEmpty(text)) return;

            int maxLen = span.Length - 1; // Null Terminator 공간 확보
            int copyLen = Math.Min(text.Length, maxLen);

            text.AsSpan(0, copyLen).CopyTo(span);
            span[copyLen] = '\0';
        }

        public static string GetText<TBuffer>(this ref TBuffer buffer) where TBuffer : struct
        {
            ReadOnlySpan<char> span = MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<TBuffer, char>(ref buffer), Unsafe.SizeOf<TBuffer>() / sizeof(char));

            int nullIndex = span.IndexOf('\0');
            if (nullIndex == 0) return string.Empty;
            if (nullIndex > 0) return span.Slice(0, nullIndex).ToString();

            return span.ToString();
        }
    }
}