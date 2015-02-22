using System;


namespace TAlex.Common.Consoles
{
    /// <summary>
    /// Represents the additional (helper) methods for console applications.
    /// </summary>
    public static class ConsoleEx
    {
        /// <summary>
        /// Writes the colored text representation of the specified array of objects to the standard
        /// output stream using the specified format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="color">A console color for displayed text.</param>
        /// <param name="args">An array of objects to write using format.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurred.</exception>
        /// <exception cref="System.ArgumentNullException">format or arg is null.</exception>
        /// <exception cref="System.FormatException">The format specification in format is invalid.</exception>
        public static void Write(string format, ConsoleColor color = ConsoleColor.Gray, params object[] args)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.Write(format, args);

            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Writes the colored text representation of the specified array of objects, followed
        /// by the current line terminator, to the standard output stream using the specified
        /// format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="color">A console color for displayed text.</param>
        /// <param name="args">An array of objects to write using format.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurred.</exception>
        /// <exception cref="System.ArgumentNullException">format or arg is null.</exception>
        /// <exception cref="System.FormatException">The format specification in format is invalid.</exception>
        public static void WriteLine(string format, ConsoleColor color = ConsoleColor.Gray, params object[] args)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.WriteLine(format, args);

            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Writes the error text (red color) representation of the specified array of objects
        /// to the standard output stream using the specified format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurred.</exception>
        /// <exception cref="System.ArgumentNullException">format or arg is null.</exception>
        /// <exception cref="System.FormatException">The format specification in format is invalid.</exception>
        public static void WriteError(string format, params object[] args)
        {
            Write(format, ConsoleColor.Red, args);
        }

        /// <summary>
        /// Writes the error text (red color) representation of the specified array of objects, followed
        /// by the current line terminator, to the standard output stream using the specified format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurred.</exception>
        /// <exception cref="System.ArgumentNullException">format or arg is null.</exception>
        /// <exception cref="System.FormatException">The format specification in format is invalid.</exception>
        public static void WriteErrorLine(string format, params object[] args)
        {
            WriteLine(format, ConsoleColor.Red, args);
        }

        /// <summary>
        /// Writes the warning text (yellow color) representation of the specified array of objects
        /// to the standard output stream using the specified format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurred.</exception>
        /// <exception cref="System.ArgumentNullException">format or arg is null.</exception>
        /// <exception cref="System.FormatException">The format specification in format is invalid.</exception>
        public static void WriteWarning(string format, params object[] args)
        {
            Write(format, ConsoleColor.Yellow, args);
        }

        /// <summary>
        /// Writes the warning text (yellow color) representation of the specified array of objects, followed
        /// by the current line terminator, to the standard output stream using the specified format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurred.</exception>
        /// <exception cref="System.ArgumentNullException">format or arg is null.</exception>
        /// <exception cref="System.FormatException">The format specification in format is invalid.</exception>
        public static void WriteWarningLine(string format, params object[] args)
        {
            WriteLine(format, ConsoleColor.Yellow, args);
        }

        /// <summary>
        /// Writes the information text (white color) representation of the specified array of objects
        /// to the standard output stream using the specified format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurred.</exception>
        /// <exception cref="System.ArgumentNullException">format or arg is null.</exception>
        /// <exception cref="System.FormatException">The format specification in format is invalid.</exception>
        public static void WriteInformation(string format, params object[] args)
        {
            Write(format, ConsoleColor.White, args);
        }

        /// <summary>
        /// Writes the information text (white color) representation of the specified array of objects, followed
        /// by the current line terminator, to the standard output stream using the specified format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurred.</exception>
        /// <exception cref="System.ArgumentNullException">format or arg is null.</exception>
        /// <exception cref="System.FormatException">The format specification in format is invalid.</exception>
        public static void WriteInformationLine(string format, params object[] args)
        {
            WriteLine(format, ConsoleColor.White, args);
        }
    }
}
