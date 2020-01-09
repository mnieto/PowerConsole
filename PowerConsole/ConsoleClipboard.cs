using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using SysConsole = System.Console;

namespace PowerConsole
{

    /// <summary>
    /// Allow copy and restore content of the 
    /// </summary>
    public class ConsoleClipboard {
        private const int STD_OUTPUT_HANDLE = -11;
        private CHAR_INFO[] ClipBuffer = null;
        private COORD Size;
        private SMALL_RECT Rect;

        /// <summary>
        /// <see cref="Rect"/> of the copied area
        /// </summary>
        public Rect ClipArea {
            get {
                return new Rect {
                    X = Rect.Left,
                    Y = Rect.Top,
                    Width = (short)(Rect.Right - Rect.Left + 1),
                    Height = (short)(Rect.Bottom - Rect.Top + 1)
                };
            }
        }

        /// <summary>
        /// Gives access, by index, to the char_info.attributes
        /// </summary>
        public Attributes ColorAttributes { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ConsoleClipboard() {
            ColorAttributes = new Attributes(this);
        }

        /// <summary>
        /// Copy a region of a console buffer
        /// </summary>
        /// <param name="x">Zero based origin coordinate</param>
        /// <param name="y">Zero based origin coordinate</param>
        /// <param name="width">With of the area to be copied</param>
        /// <param name="height">Height of the area to be copied</param>
        public void Copy(short x, short y, short width, short height) {
            IntPtr buffer = Marshal.AllocHGlobal(width * height * Marshal.SizeOf(typeof(CHAR_INFO)));
            ClipBuffer = new CHAR_INFO[width * height];

            if (buffer == null)
                throw new OutOfMemoryException();

            try {
                SetStrutures(x, y, width, height);
                if (!ReadConsoleOutput(GetStdHandle(STD_OUTPUT_HANDLE), buffer, Size, new COORD(), ref Rect)) {
                    // 'Not enough storage is available to process this command' may be raised for buffer size > 64K (see ReadConsoleOutput doc.)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                IntPtr ptr = buffer;
                for (int i = 0; i < ClipBuffer.Length; i++) {
                    ClipBuffer[i] = Marshal.PtrToStructure<CHAR_INFO>(ptr);
                    ptr += Marshal.SizeOf(typeof(CHAR_INFO));
                }

            } finally {
                Marshal.FreeHGlobal(buffer);
            }
        }

        /// <summary>
        /// Restore the previously saved content in the same place
        /// </summary>
        public void Paste() {
            if (ClipBuffer == null)
                throw new InvalidOperationException("No content available to paste");

            IntPtr buffer = Marshal.AllocHGlobal(Size.X * Size.Y * Marshal.SizeOf(typeof(CHAR_INFO)));
            if (buffer == null)
                throw new OutOfMemoryException();

            try {
                IntPtr ptr = buffer;
                for (int i = 0; i < ClipBuffer.Length; i++) {
                    Marshal.StructureToPtr(ClipBuffer[i], ptr, false);
                    ptr += Marshal.SizeOf(typeof(CHAR_INFO));
                }

                bool result = WriteConsoleOutput(GetStdHandle(STD_OUTPUT_HANDLE), ClipBuffer, Size, new COORD(), ref Rect);
                if (!result)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

            } finally {
                Marshal.FreeHGlobal(buffer);
            }
        }


        /// <summary>
        /// Return th copied content as a list of texts
        /// </summary>
        public IEnumerable<string> ToText() {
            if (ClipBuffer == null)
                throw new InvalidOperationException("No content available");
            for (int r = 0; r < ClipArea.Height; r++) {
                var sb = new StringBuilder();
                for (int c = 0; c < ClipArea.Width; c++) {
                    int i = r * ClipArea.Width + c;
                    char[] chars = SysConsole.OutputEncoding.GetChars(ClipBuffer[i].charData);
                    sb.Append(chars[0]);
                }
                yield return sb.ToString();
            }
        }

        /// <summary>
        /// Return the copied content as text
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return string.Join(Environment.NewLine, ToText());
        }

        /// <summary>
        /// Restore the previously saved content in another place
        /// </summary>
        /// <param name="x">New x origin</param>
        /// <param name="y">New y origin</param>
        public void Paste(short x, short y) {
            
            if (ClipBuffer == null)
                throw new InvalidOperationException("No content available to paste");

            SetStrutures(x, y, Size.X, Size.Y);
            Paste();
        }

        private void SetStrutures(short x, short y, short width, short height) {
            Size = new COORD {
                X = width,
                Y = height
            };
            Rect = new SMALL_RECT {
                Left = x,
                Top = y,
                Right = (short)(x + width - 1),
                Bottom = (short)(y + height - 1)
            };
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CHAR_INFO
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] charData;
            public short attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COORD
        {
            public short X;
            public short Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SMALL_RECT
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadConsoleOutput(IntPtr hConsoleOutput, IntPtr lpBuffer, COORD dwBufferSize, COORD dwBufferCoord, ref SMALL_RECT lpReadRegion);

        [DllImport("kernel32.dll", /*EntryPoint = "WriteConsoleOutputW", CharSet = CharSet.Unicode,*/ SetLastError = true)]
        private static extern bool WriteConsoleOutput(
        IntPtr hConsoleOutput,
        [MarshalAs(UnmanagedType.LPArray), In] CHAR_INFO[] lpBuffer,
        COORD dwBufferSize,
        COORD dwBufferCoord,
        ref SMALL_RECT lpWriteRegion);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);


        /// <summary>
        /// Nested class to access by index to the char_info.attributes
        /// </summary>
        public class Attributes
        {
            private ConsoleClipboard instance;
            internal Attributes(ConsoleClipboard consoleClipboard) {
                instance = consoleClipboard;
            }

            /// <summary>
            /// Indexer
            /// </summary>
            /// <param name="x">x coordinate of the char respect of the copied buffer</param>
            /// <param name="y">y coordinate of the char respect of the copied buffer</param>
            /// <returns></returns>
            public short this[short x, short y] {
                get {
                    if (instance.ClipBuffer == null)
                        throw new InvalidOperationException("No content available");
                    return instance.ClipBuffer[x * instance.ClipArea.Width + y].attributes;
                }
                set {
                    if (instance.ClipBuffer == null)
                        throw new InvalidOperationException("No content available");
                    instance.ClipBuffer[x * instance.ClipArea.Width + y].attributes = value;
                }
            }
        }
    }

    /// <summary>
    /// Rectangle structure
    /// </summary>
    public class Rect
    {
        /// <summary>Top coordinate</summary>
        public short X;
        /// <summary>Left coordinate</summary>
        public short Y;
        /// <summary>Width of the rectangle</summary>
        public short Width;
        /// <summary>Height of the rectangle</summary>
        public short Height;
    }
}
