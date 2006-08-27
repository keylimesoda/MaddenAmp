//	LumenWorks.Framework.IO.CSV.CsvReader
//	Copyright (c) 2005 Sébastien Lorion
//
//	Permission is hereby granted, free of charge, to any person obtaining a copy
//	of this software and associated documentation files (the "Software"), to deal
//	in the Software without restriction, including without limitation the rights 
//	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//	of the Software, and to permit persons to whom the Software is furnished to do so, 
//	subject to the following conditions:
//
//	The above copyright notice and this permission notice shall be included in all 
//	copies or substantial portions of the Software.
//
//	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
//	INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
//	PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
//	FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//	ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using LumenWorks.Framework.IO.Csv.Resources;

#endregion

namespace LumenWorks.Framework.IO.Csv
{
	/// <summary>
	/// Represents a reader that provides fast, non-cached, forward-only access to CSV data.  
	/// </summary>
	public partial class CsvReader
		: IEnumerable<string[]>, IDisposable
	{
		#region Constants

		/// <summary>
		/// Defines the default buffer size.
		/// </summary>
		public const int DefaultBufferSize = 0x1000;

		/// <summary>
		/// Defines the default delimiter character separating each field.
		/// </summary>
		public const char DefaultDelimiter = ',';

		/// <summary>
		/// Defines the default quote character wrapping every field.
		/// </summary>
		public const char DefaultQuote = '"';

		/// <summary>
		/// Defines the default escape character letting insert quotation characters inside a quoted field.
		/// </summary>
		public const char DefaultEscape = '"';

		/// <summary>
		/// Defines the default comment character indicating that a line is commented out.
		/// </summary>
		public const char DefaultComment = '#';

		#endregion

		#region Fields

		#region Settings

		/// <summary>
		/// Contains the <see cref="T:TextReader"/> pointing to the CSV file.
		/// </summary>
		private TextReader _reader;

		/// <summary>
		/// Contains the buffer size.
		/// </summary>
		private int _bufferSize;

		/// <summary>
		/// Contains the comment character indicating that a line is commented out.
		/// </summary>
		private char _comment;

		/// <summary>
		/// Contains the escape character letting insert quotation characters inside a quoted field.
		/// </summary>
		private char _escape;

		/// <summary>
		/// Contains the delimiter character separating each field.
		/// </summary>
		private char _delimiter;

		/// <summary>
		/// Contains the quotation character wrapping every field.
		/// </summary>
		private char _quote;

		/// <summary>
		/// Indicates if spaces at the start and end of a field are trimmed.
		/// </summary>
		private bool _trimSpaces;

		/// <summary>
		/// Indicates if field names are located on the first non commented line.
		/// </summary>
		private bool _hasHeaders;

		#endregion

		#region State

		/// <summary>
		/// Indicates if the class is initialized.
		/// </summary>
		private bool _initialized;

		/// <summary>
		/// Contains the dictionary of field headers. The key is the field name and the value is its index.
		/// </summary>
		private Dictionary<string, int> _fieldHeaders;

		/// <summary>
		/// Contains the current record index in the CSV file.
		/// A value of <see cref="M:Int32.MinValue"/> means that the reader has not been initialized yet.
		/// Otherwise, a negative value means that no record has been read yet.
		/// </summary>
		private int _currentRecordIndex;

		/// <summary>
		/// Contains the starting position of the next unread field.
		/// </summary>
		private int _nextFieldStart;

		/// <summary>
		/// Contains the index of the next unread field.
		/// </summary>
		private int _nextFieldIndex;

		/// <summary>
		/// Contains the array of the field values for the current record.
		/// A null value indicates that the field have not been parsed.
		/// </summary>
		private string[] _fields; //_currentRecord

		/// <summary>
		/// Contains the maximum number of fields to retrieve for each record.
		/// </summary>
		private int _fieldCount;

		/// <summary>
		/// Contains the read buffer.
		/// </summary>
		private char[] _buffer;

		/// <summary>
		/// Contains the current read buffer length.
		/// </summary>
		private int _bufferLength;

		/// <summary>
		/// Indicates if the end of the reader has been reached.
		/// </summary>
		private bool _eof;

		#endregion

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CsvReader class.
		/// </summary>
		/// <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
		/// <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
		/// <exception cref="T:ArgumentNullException">
		///		<paramref name="reader"/> is a <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:ArgumentException">
		///		Cannot read from <paramref name="reader"/>.
		/// </exception>
		public CsvReader(TextReader reader, bool hasHeaders)
			: this(reader, hasHeaders, DefaultBufferSize)
		{
		}

		/// <summary>
		/// Initializes a new instance of the CsvReader class.
		/// </summary>
		/// <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
		/// <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
		/// <param name="bufferSize">The buffer size in bytes.</param>
		/// <exception cref="T:ArgumentNullException">
		///		<paramref name="reader"/> is a <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:ArgumentException">
		///		Cannot read from <paramref name="reader"/>.
		/// </exception>
		public CsvReader(TextReader reader, bool hasHeaders, int bufferSize)
			: this(reader, hasHeaders, DefaultDelimiter, DefaultQuote, DefaultEscape, DefaultComment, true, bufferSize)
		{
		}

		/// <summary>
		/// Initializes a new instance of the CsvReader class.
		/// </summary>
		/// <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
		/// <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
		/// <param name="delimiter">The delimiter character separating each field (default is ',').</param>
		/// <param name="quote">The quotation character wrapping every field (default is ''').</param>
		/// <param name="escape">
		/// The escape character letting insert quotation characters inside a quoted field (default is '\').
		/// If no escape character, set to '\0' to gain some performance.
		/// </param>
		/// <param name="comment">The comment character indicating that a line is commented out (default is '#').</param>
		/// <param name="trimSpaces"><see langword="true"/> if spaces at the start and end of a field are trimmed, otherwise, <see langword="false"/>. Default is <see langword="true"/>.</param>
		/// <exception cref="T:ArgumentNullException">
		///		<paramref name="reader"/> is a <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:ArgumentException">
		///		Cannot read from <paramref name="reader"/>.
		/// </exception>
		public CsvReader(TextReader reader, bool hasHeaders, char delimiter, char quote, char escape, char comment, bool trimSpaces)
			: this(reader, hasHeaders, delimiter, quote, escape, comment, trimSpaces, DefaultBufferSize)
		{
		}

		/// <summary>
		/// Initializes a new instance of the CsvReader class.
		/// </summary>
		/// <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
		/// <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
		/// <param name="delimiter">The delimiter character separating each field (default is ',').</param>
		/// <param name="quote">The quotation character wrapping every field (default is ''').</param>
		/// <param name="escape">
		/// The escape character letting insert quotation characters inside a quoted field (default is '\').
		/// If no escape character, set to '\0' to gain some performance.
		/// </param>
		/// <param name="comment">The comment character indicating that a line is commented out (default is '#').</param>
		/// <param name="trimSpaces"><see langword="true"/> if spaces at the start and end of a field are trimmed, otherwise, <see langword="false"/>. Default is <see langword="true"/>.</param>
		/// <param name="bufferSize">The buffer size in bytes.</param>
		/// <exception cref="T:ArgumentNullException">
		///		<paramref name="reader"/> is a <see langword="null"/>.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		///		<paramref name="bufferSize"/> must be 1 or more.
		/// </exception>
		public CsvReader(TextReader reader, bool hasHeaders, char delimiter, char quote, char escape, char comment, bool trimSpaces, int bufferSize)
		{
			if (reader == null)
				throw new ArgumentNullException("reader");

			if (bufferSize <= 0)
				throw new ArgumentOutOfRangeException("bufferSize", bufferSize, "Buffer size must be 1 or more.");

			_reader = reader;
			_bufferSize = bufferSize;
			_delimiter = delimiter;
			_quote = quote;
			_escape = escape;
			_comment = comment;

			_hasHeaders = hasHeaders;
			_trimSpaces = trimSpaces;

			_currentRecordIndex = -1;
		}

		#endregion

		#region Properties

		#region Settings

		/// <summary>
		/// Gets the comment character indicating that a line is commented out.
		/// </summary>
		/// <value>The comment character indicating that a line is commented out.</value>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public char Comment
		{
			get
			{
				CheckDisposed();
				return _comment;
			}
		}

		/// <summary>
		/// Gets the escape character letting insert quotation characters inside a quoted field.
		/// </summary>
		/// <value>The escape character letting insert quotation characters inside a quoted field.</value>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public char Escape
		{
			get
			{
				CheckDisposed();
				return _escape;
			}
		}

		/// <summary>
		/// Gets the delimiter character separating each field.
		/// </summary>
		/// <value>The delimiter character separating each field.</value>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public char Delimiter
		{
			get
			{
				CheckDisposed();
				return _delimiter;
			}
		}

		/// <summary>
		/// Gets the quotation character wrapping every field.
		/// </summary>
		/// <value>The quotation character wrapping every field.</value>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public char Quote
		{
			get
			{
				CheckDisposed();
				return _quote;
			}
		}

		/// <summary>
		/// Indicates if field names are located on the first non commented line.
		/// </summary>
		/// <value><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</value>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public bool HasHeaders
		{
			get
			{
				CheckDisposed();
				return _hasHeaders;
			}
		}

		/// <summary>
		/// Indicates if spaces at the start and end of a field are trimmed.
		/// </summary>
		/// <value><see langword="true"/> if spaces at the start and end of a field are trimmed, otherwise, <see langword="false"/>.</value>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public bool TrimSpaces
		{
			get
			{
				CheckDisposed();
				return _trimSpaces;
			}
		}

		/// <summary>
		/// Gets the buffer size.
		/// </summary>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public int BufferSize
		{
			get
			{
				CheckDisposed();
				return _bufferSize;
			}
		}

		#endregion

		#region State

		/// <summary>
		/// Gets the maximum number of fields to retrieve for each record.
		/// </summary>
		/// <value>The maximum number of fields to retrieve for each record.</value>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public int FieldCount
		{
			get
			{
				CheckDisposed();
				return _fieldCount;
			}
		}

		/// <summary>
		/// Gets a value that indicates whether the current stream position is at the end of the stream.
		/// </summary>
		/// <value><see langword="true"/> if the current stream position is at the end of the stream; otherwise <see langword="false"/>.</value>
		public virtual bool EndOfStream
		{
			get { return _eof; }
		}

		/// <summary>
		/// Gets the dictionary of field headers. The key is the field name and the value is its index.
		/// </summary>
		/// <value>The dictionary of field headers. The key is the field name and the value is its index.</value>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public Dictionary<string, int> FieldHeaders
		{
			get
			{
				CheckDisposed();

				if (_fieldHeaders == null)
				{
					if (_hasHeaders)
					{
						Debug.Assert(!_initialized, "Field headers should be set once reader is initialized.");

						if (ReadNextRecord())
							_currentRecordIndex--;
						else if (_fieldHeaders == null)
							_fieldHeaders = new Dictionary<string, int>();
					}
					else
						_fieldHeaders = new Dictionary<string, int>();
				}

				Debug.Assert(_fieldHeaders != null, "Field headers must be non null.");

				return _fieldHeaders;
			}
		}

		/// <summary>
		/// Gets the current record index in the CSV file.
		/// </summary>
		/// <value>The current record index in the CSV file.</value>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public virtual int CurrentRecordIndex
		{
			get
			{
				CheckDisposed();
				return _currentRecordIndex;
			}
		}

		#endregion

		#endregion

		#region Indexers

		/// <summary>
		/// Gets the field with the specified name and record position. <see cref="M:hasHeaders"/> must be <see langword="true"/>.
		/// </summary>
		/// <value>
		/// The field with the specified name and record position.
		/// A <see langword="null"/> is returned if the field cannot be found for the record.
		/// </value>
		/// <exception cref="T:ArgumentNullException">
		///		<paramref name="field"/> is a <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:ArgumentException">
		///		<paramref name="field"/> not found.
		/// </exception>
		/// <exception cref="T:InvalidOperationException">
		///		Cannot move to a previous record in forward-only mode.
		/// </exception>
		/// <exception cref="T:EndOfStreamException">
		///		Cannot read record at <paramref name="record"/>.
		///	</exception>
		///	<exception cref="T:MalformedCsvException">
		///		The CSV appears to be corrupt at the current position.
		/// </exception>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public string this[int record, string field]
		{
			get
			{
				CheckDisposed();

				if (field == null)
					throw new ArgumentNullException("field");

				int index;

				if (!_fieldHeaders.TryGetValue(field, out index))
					throw new ArgumentException(string.Format(ExceptionMessage.FieldHeaderNotFound, field), "field");

				return this[record, index];
			}
		}

		/// <summary>
		/// Gets the field with the specified name. <see cref="M:hasHeaders"/> must be <see langword="true"/>.
		/// </summary>
		/// <value>
		/// The field with the specified name.
		/// A <see langword="null"/> is returned if the field cannot be found for the current record.
		/// </value>
		/// <exception cref="T:ArgumentNullException">
		///		<paramref name="field"/> is a <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:ArgumentException">
		///		<paramref name="field"/> not found.
		/// </exception>
		/// <exception cref="T:MalformedCsvException">
		///		The CSV appears to be corrupt at the current position.
		/// </exception>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public string this[string field]
		{
			get
			{
				CheckDisposed();

				if (field == null)
					throw new ArgumentNullException("field");

				int index;

				if (!_fieldHeaders.TryGetValue(field, out index))
					throw new ArgumentException(string.Format(ExceptionMessage.FieldHeaderNotFound, field), "field");

				return this[index];
			}
		}

		/// <summary>
		/// Gets the field at the specified index and record position.
		/// </summary>
		/// <value>
		/// The field at the specified index and record position.
		/// A <see langword="null"/> is returned if the field cannot be found for the record.
		/// </value>
		/// <exception cref="T:ArgumentOutOfRangeException">
		///		<paramref name="field"/> must be included in [0, <see cref="M:FieldCount"/>[.
		/// </exception>
		/// <exception cref="T:InvalidOperationException">
		///		Cannot move to a previous record in forward-only mode.
		/// </exception>
		/// <exception cref="T:EndOfStreamException">
		///		Cannot read record at <paramref name="record"/>.
		/// </exception>
		/// <exception cref="T:MalformedCsvException">
		///		The CSV appears to be corrupt at the current position.
		/// </exception>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public string this[int record, int field]
		{
			get
			{
				CheckDisposed();

				MoveTo(record);

				return this[field];
			}
		}

		/// <summary>
		/// Gets the field at the specified index.
		/// </summary>
		/// <value>The field at the specified index.</value>
		/// <exception cref="T:ArgumentOutOfRangeException">
		///		<paramref name="field"/> must be included in [0, <see cref="M:FieldCount"/>[.
		/// </exception>
		/// <exception cref="T:InvalidOperationException">
		///		No record read yet. Call ReadLine() first.
		/// </exception>
		/// <exception cref="T:MalformedCsvException">
		///		The CSV appears to be corrupt at the current position.
		/// </exception>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public virtual string this[int field]
		{
			get
			{
				CheckDisposed();

				return ReadField(field, false, false);
			}
		}

		#endregion

		#region Methods

		#region CopyTo

		/// <summary>
		/// Copies the field array of the current record to a one-dimensional array, starting at the beginning of the target array.
		/// </summary>
		/// <param name="array"> The one-dimensional <see cref="T:Array"/> that is the destination of the fields of the current record.</param>
		/// <exception cref="T:ArgumentNullException">
		///		<paramref name="array"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="ArgumentException">
		///		The number of fields in the record is greater than the available space from <paramref name="index"/> to the end of <paramref name="array"/>.
		/// </exception>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public void CopyCurrentRecordTo(string[] array)
		{
			CopyCurrentRecordTo(array, 0);
		}

		/// <summary>
		/// Copies the field array of the current record to a one-dimensional array, starting at the beginning of the target array.
		/// </summary>
		/// <param name="array"> The one-dimensional <see cref="T:Array"/> that is the destination of the fields of the current record.</param>
		/// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
		/// <exception cref="T:ArgumentNullException">
		///		<paramref name="array"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:ArgumentOutOfRangeException">
		///		<paramref name="index"/> is les than zero or is equal to or greater than the length <paramref name="array"/>. 
		/// </exception>
		/// <exception cref="ArgumentException">
		///		The number of fields in the record is greater than the available space from <paramref name="index"/> to the end of <paramref name="array"/>.
		/// </exception>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public void CopyCurrentRecordTo(string[] array, int index)
		{
			CheckDisposed();

			if (array == null)
				throw new ArgumentNullException("array");

			if (index < 0 || index >= array.Length)
				throw new ArgumentOutOfRangeException("array", index, "");

			if (array.Length - index < _fieldCount)
				throw new ArgumentException(ExceptionMessage.NotEnoughSpaceInArray, "index");

			for (int i = 0; i < _fieldCount; i++)
				array[index + i] = this[i];
		}

		#endregion

		#region GetCurrentRawData

		/// <summary>
		/// Gets the current raw CSV data.
		/// </summary>
		/// <remarks>Used for exception handling purpose.</remarks>
		/// <returns>The current raw CSV data.</returns>
		private string GetCurrentRawData()
		{
			if (_buffer != null && _bufferLength > 0)
				return new string(_buffer, 0, _bufferLength);
			else
				return string.Empty;
		}

		#endregion

		#region IsWhiteSpace

		/// <summary>
		/// Indicates whether the specified Unicode character is categorized as white space.
		/// </summary>
		/// <param name="c">A Unicode character.</param>
		/// <returns><see langword="true"/> if <paramref name="c"/> is white space; otherwise, <see langword="false"/>.</returns>
		private static bool IsWhiteSpace(char c)
		{
			// See char.IsLatin1(char c) in Reflector
			if (c <= '\x00ff')
				return (c == ' ' || c == '\t');
			else
				return (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.SpaceSeparator);
		}

		#endregion

		#region Move

		/// <summary>
		/// Moves to the specified record index.
		/// </summary>
		/// <param name="record">The record index.</param>
		/// <exception cref="T:ArgumentOutOfRangeException">
		///		Record index must be > 0.
		/// </exception>
		/// <exception cref="T:InvalidOperationException">
		///		Cannot move to a previous record in forward-only mode.
		/// </exception>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public virtual void MoveTo(int record)
		{
			CheckDisposed();

			if (record < 0)
				throw new ArgumentOutOfRangeException("record", record, ExceptionMessage.RecordIndexLessThanZero);

			if (record < _currentRecordIndex)
				throw new InvalidOperationException(ExceptionMessage.CannotMovePreviousRecordInForwardOnly);

			// Get number of record to read

			int offset = record - _currentRecordIndex;

			if (offset > 0)
			{
				bool ok = true;

				// Skip to the last record before the specified one
				while (ok && offset > 1)
				{
					ok = ReadNextRecord();
					offset--;
				}

				if (!ok || !ReadNextRecord())
					throw new EndOfStreamException(string.Format(ExceptionMessage.CannotReadRecordAtIndex, _currentRecordIndex - offset));
			}
		}

		#endregion

		#region ParseNewLine

		/// <summary>
		/// Parses a new line delimiter.
		/// </summary>
		/// <param name="pos">The starting position of the parsing. Will contain the resulting end position.</param>
		/// <returns><see langword="true"/> if a new line delimiter was found; otherwise, <see langword="false"/>.</returns>
		private bool ParseNewLine(ref int pos)
		{
			Debug.Assert(pos <= _bufferLength);

			// Check if already at the end of the buffer
			if (pos == _bufferLength)
			{
				pos = 0;

				if (!ReadBuffer())
					return false;
			}

			char c = _buffer[pos];

			// Treat \r as new line only if it's not the delimiter

			if (c == '\r' && _delimiter != '\r')
			{
				pos++;

				// Skip following \n (if there is one)

				if (pos < _bufferLength)
				{
					if (_buffer[pos] == '\n')
						pos++;
				}
				else
				{
					if (ReadBuffer())
					{
						if (_buffer[0] == '\n')
							pos = 1;
						else
							pos = 0;
					}
				}

				return true;
			}
			else if (c == '\n')
			{
				pos++;
				return true;
			}

			return false;
		}

		#endregion

		#region ReadBuffer

		/// <summary>
		/// Fills the buffer with data from the reader.
		/// </summary>
		/// <returns><see langword="true"/> if data was successfully read; otherwise, <see langword="false"/>.</returns>
		private bool ReadBuffer()
		{
			_bufferLength = _reader.Read(_buffer, 0, _bufferSize);

			if (_bufferLength > 0)
				return true;
			else
			{
				_eof = true;
				_buffer = null;

				return false;
			}
		}

		#endregion

		#region ReadField

		/// <summary>
		/// Reads the field at the specified index.
		/// Any unread fields with an inferior index will also be read as part of the required parsing.
		/// </summary>
		/// <param name="field">The field index.</param>
		/// <param name="initializing">Indicates if the reader is currently initializing.</param>
		/// <param name="discardValue">Indicates if the value(s) are discarded.</param>
		/// <returns>The field at the specified index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		///		<paramref name="field"/> is out of range.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		///		There is no current record.
		/// </exception>
		/// <exception cref="MalformedCsvException">
		///		The CSV data appears to be malformed.
		/// </exception>
		private string ReadField(int field, bool initializing, bool discardValue)
		{
			CheckDisposed();

			if (!initializing)
			{
				if (field < 0 || field >= _fieldCount)
					throw new ArgumentOutOfRangeException("field", field, string.Format(ExceptionMessage.FieldIndexOutOfRange, field));

				if (_currentRecordIndex < 0)
					throw new InvalidOperationException(ExceptionMessage.NoCurrentRecord);
			}

			// Directly return field if cached
			if (_fields[field] != null)
				return _fields[field];

			int index = _nextFieldIndex;

			while (index < field + 1)
			{
				// Handle case where stated start of field is past buffer
				// This can occur because _nextFieldStart is simply 1 + last char position of previous field
				if (_nextFieldStart == _bufferLength)
				{
					_nextFieldStart = 0;

					// Possible EOF will be handled later
					ReadBuffer();
				}

				string value = null;
				bool eol = false;

				if (_nextFieldStart == _bufferLength)
				{
					// If current field is the requested field, then the value of the field is "" as in "f1,f2,f3,(\s*)"
					// otherwise, the CSV is malformed

					if (index == field)
					{
						if (!discardValue)
						{
							value = string.Empty;
							_fields[index] = value;
						}
					}
					else
						throw new MalformedCsvException(GetCurrentRawData(), _currentRecordIndex, _nextFieldStart);
				}
				else
				{
					// Trim spaces at start
					if (_trimSpaces)
						SkipWhiteSpaces(ref _nextFieldStart);

					if (_buffer[_nextFieldStart] != _quote)
					{
						// Non-quoted field

						int start = _nextFieldStart;
						int pos = _nextFieldStart;

						for (; ; )
						{
							while (pos < _bufferLength)
							{
								char c = _buffer[pos];

								if (c == _delimiter)
								{
									_nextFieldStart = pos + 1;

									break;
								}
								else if (c == '\r' || c == '\n')
								{
									_nextFieldStart = pos;
									eol = true;

									break;
								}
								else
									pos++;
							}

							if (pos < _bufferLength)
								break;
							else
							{
								if (!discardValue)
									value += new string(_buffer, start, pos - start);

								start = 0;
								pos = 0;
								_nextFieldStart = 0;

								if (!ReadBuffer())
								{
									if (index == field)
										break;
									else
										throw new MalformedCsvException(GetCurrentRawData(), _currentRecordIndex, _nextFieldStart);
								}
							}
						}

						if (!discardValue)
						{
							if (!_trimSpaces)
							{
								if (!_eof && pos > start)
									value += new string(_buffer, start, pos - start);
							}
							else
							{
								if (!_eof && pos > start)
								{
									// Do the trimming
									pos--;
									while (pos > -1 && IsWhiteSpace(_buffer[pos]))
										pos--;
									pos++;

									if (pos > 0)
										value += new string(_buffer, start, pos - start);
								}
								else
									pos = -1;

								// If pos < 0, that means the trimming went past buffer start,
								// and the concatenated value needs to be trimmed too.
								if (pos < 0)
								{
									pos = (value == null ? -1 : value.Length - 1);

									// Do the trimming
									while (pos > -1 && IsWhiteSpace(value[pos]))
										pos--;

									pos++;

									if (pos > 0 && pos != value.Length)
										value = value.Substring(0, pos);
								}
							}

							if (value == null)
								value = string.Empty;

							_fields[index] = value;
						}

						if (eol)
						{
							// Reaching a new line is ok as long as the parser is initializing or it is the last field
							if (initializing || index == _fieldCount - 1)
								ParseNewLine(ref _nextFieldStart);
							else
								throw new MalformedCsvException(GetCurrentRawData(), _currentRecordIndex, _nextFieldStart);
						}
					}
					else
					{
						// Quoted field

						// Skip quote
						int start = _nextFieldStart + 1; 
						int pos = start;

						bool quoted = true;
						bool escaped = false;

						for ( ;; )
						{
							while (pos < _bufferLength)
							{
								char c = _buffer[pos];

								if (escaped)
								{
									escaped = false;
									start = pos;
								}
								// IF current char is escape AND (escape and quote are different OR next char is a quote)
								else if (c == _escape && (_escape != _quote || (pos + 1 < _bufferLength && _buffer[pos + 1] == _quote) || (pos + 1 == _bufferLength && _reader.Peek() == _quote)))
								{
									if (!discardValue)
										value += new string(_buffer, start, pos - start);

									escaped = true;
								}
								else if (c == _quote)
								{
									quoted = false;
									break;
								}

								pos++;
							}

							if (!quoted)
								break;
							else
							{
								if (!discardValue && !escaped)
									value += new string(_buffer, start, pos - start);

								start = 0;
								pos = 0;
								_nextFieldStart = 0;

								if (!ReadBuffer())
								{
									// Reaching the buffer end is ok as long as the quotes are matched and it is the requested field
									if (!quoted && index == field)
										break;
									else
										throw new MalformedCsvException(GetCurrentRawData(), _currentRecordIndex, _nextFieldStart);
								}
							}
						}

						if (!_eof)
						{
							// Append remaining parsed buffer content
							if (!discardValue && pos > start)
								value += new string(_buffer, start, pos - start);

							// Skip quote
							_nextFieldStart = pos + 1;

							// Skip whitespaces between the quote and the delimiter/eol
							SkipWhiteSpaces(ref _nextFieldStart);

							// Skip delimiter
							if (_nextFieldStart < _bufferLength && _buffer[_nextFieldStart] == _delimiter)
								_nextFieldStart++;

							// Skip new line delimiter if initializing or last field
							// (if the next field is missing, it will be caught when parsed)
							if (!_eof && (initializing || index == _fieldCount - 1))
								eol = ParseNewLine(ref _nextFieldStart);
						}

						if (!discardValue)
						{
							if (value == null)
								value = string.Empty;

							_fields[index] = value;
						}
					}
				}

				if (initializing || index < _fieldCount - 1)
					_nextFieldIndex = index + 1;
				else
					_nextFieldIndex = 0;

				if (index == field)
				{
					// If initializing, return null to signify the last field has been reached

					if (initializing && (eol || _eof))
						return null;
					else
						return value;
				}

				index++;
			}

			// Getting here is bad ...
			throw new MalformedCsvException(GetCurrentRawData(), _currentRecordIndex, _nextFieldStart);
		}

		#endregion

		#region ReadNextRecord

		/// <summary>
		/// Reads the next record.
		/// </summary>
		/// <returns><see langword="true"/> if a record has been successfully reads; otherwise, <see langword="false"/>.</returns>
		public virtual bool ReadNextRecord()
		{
			if (_eof)
				return false;

			if (!_initialized)
			{
				_buffer = new char[_bufferSize];

				if (!ReadBuffer())
					return false;

				if (!SkipBlankAndCommentedLines(ref _nextFieldStart))
					return false;

				// Keep growing _fields array until the last field has been found
				// and then resize it to its final correct size

				_fieldCount = 0;
				_fields = new string[16];

				while (ReadField(_fieldCount, true, false) != null)
				{
					_fieldCount++;

					if (_fieldCount == _fields.Length)
						Array.Resize<string>(ref _fields, (_fieldCount + 1) * 2);
				}

				// _fieldCount contains the last field index, but it must contains the field count,
				// so increment by 1
				_fieldCount++;

				if (_fields.Length != _fieldCount)
					Array.Resize<string>(ref _fields, _fieldCount);

				_currentRecordIndex = 0;
				_initialized = true;

				// If headers are present, call ReadNextRecord again
				if (_hasHeaders)
				{
					_fieldHeaders = new Dictionary<string, int>(_fieldCount);

					for (int i = 0; i < _fields.Length; i++)
						_fieldHeaders.Add(_fields[i], i);

					// Don't count first record as it was the headers
					_currentRecordIndex = -1;

					// Proceed to first record
					return ReadNextRecord();
				}
			}
			else
			{
				// Advance to last field
				if (_currentRecordIndex > -1)
					ReadField(_fieldCount - 1, false, true);

				if (!SkipBlankAndCommentedLines(ref _nextFieldStart))
					return false;

				Array.Clear(_fields, 0, _fields.Length);

				_nextFieldIndex = 0;

				_currentRecordIndex++;
			}

			return true;
		}

		#endregion

		#region SkipBlankAndCommentedLines

		/// <summary>
		/// Skips blank and commented lines.
		/// If the end of the buffer is reached, its content be discarded and filled again from the reader.
		/// </summary>
		/// <param name="pos">
		/// The position in the buffer where to start parsing. 
		/// Will contains the resulting position after the operation.
		/// </param>
		/// <returns><see langword="true"/> if the end of the reader has not been reached; otherwise, <see langword="false"/>.</returns>
		private bool SkipBlankAndCommentedLines(ref int pos)
		{
			if (pos < _bufferLength)
				DoSkipBlankAndCommentedLines(ref pos);

			while (pos >= _bufferLength && !_eof)
			{
				if (ReadBuffer())
				{
					pos = 0;
					DoSkipBlankAndCommentedLines(ref pos);
				}
				else
					return false;
			}

			return !_eof;
		}

		/// <summary>
		/// <para>Worker method.</para>
		/// <para>Skips blank and commented lines.</para>
		/// </summary>
		/// <param name="pos">
		/// The position in the buffer where to start parsing. 
		/// Will contains the resulting position after the operation.
		/// </param>
		private void DoSkipBlankAndCommentedLines(ref int pos)
		{
			while (pos < _bufferLength)
			{
				if (_buffer[pos] == _comment)
				{
					pos++;

					// ((pos = 0) == 0) is a little trick to reset position inline
					while ((pos < _bufferLength || (ReadBuffer() && ((pos = 0) == 0))) && !ParseNewLine(ref pos))
						pos++;
				}
				else if (ParseNewLine(ref pos))
					continue;
				else
					break;
			}
		}

		#endregion

		#region SkipWhiteSpaces

		/// <summary>
		/// Skips whitespace characters.
		/// </summary>
		/// <param name="pos">The starting position of the parsing. Will contain the resulting end position.</param>
		/// <returns><see langword="true"/> if the end of the reader has not been reached; otherwise, <see langword="false"/>.</returns>
		private bool SkipWhiteSpaces(ref int pos)
		{
			for (; ; )
			{
				while (pos < _bufferLength && IsWhiteSpace(_buffer[pos]))
					pos++;

				if (pos < _bufferLength)
					break;
				else
				{
					pos = 0;

					if (!ReadBuffer())
						return false;
				}
			}

			return true;
		}

		#endregion

		#endregion

		#region IEnumerable<string[]> Members

		/// <summary>
		/// Returns an <see cref="T:RecordEnumerator"/>  that can iterate through CSV records.
		/// </summary>
		/// <returns>An <see cref="T:RecordEnumerator"/>  that can iterate through CSV records.</returns>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		public CsvReader.RecordEnumerator GetEnumerator()
		{
			CheckDisposed();
			return new CsvReader.RecordEnumerator(this);
		}

		/// <summary>
		/// Returns an <see cref="T:System.Collections.Generics.IEnumerator"/>  that can iterate through CSV records.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.Generics.IEnumerator"/>  that can iterate through CSV records.</returns>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		IEnumerator<string[]> IEnumerable<string[]>.GetEnumerator()
		{
			CheckDisposed();
			return this.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		/// <summary>
		/// Returns an <see cref="T:System.Collections.IEnumerator"/>  that can iterate through CSV records.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator"/>  that can iterate through CSV records.</returns>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///		The instance has been disposed of.
		/// </exception>
		IEnumerator IEnumerable.GetEnumerator()
		{
			CheckDisposed();
			return GetEnumerator();
		}

		#endregion

		#region IDisposable members

		/// <summary>
		/// Contains a dummy object for locking purpose.
		/// </summary>
		private object _dummyLock = new object();

		/// <summary>
		/// Contains the disposed status flag.
		/// </summary>
		private bool _isDisposed = false;

		/// <summary>
		/// Occurs when the instance is disposed of.
		/// </summary>
		public event EventHandler Disposed;

		/// <summary>
		/// <para>
		///	Gets a value indicating whether the instance has been disposed of.
		/// </para>
		/// </summary>
		/// <value>
		///		<see langword="true"/> if the instance has been disposed of; otherwise, <see langword="false"/>.
		/// </value>
		[System.ComponentModel.Browsable(false)]
		public bool IsDisposed
		{
			get { return _isDisposed; }
		}

		/// <summary>
		/// Raises the <see cref="M:Disposed"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected virtual void OnDisposed(EventArgs e)
		{
			if (Disposed != null)
				Disposed(this, e);
		}

		/// <summary>
		///	Checks if the instance has been disposed of, and if it has, throws an <see cref="T:System.ComponentModel.ObjectDisposedException"/>; otherwise, does nothing.
		/// </summary>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		///	The instance has been disposed of.
		/// </exception>
		/// <remarks>
		///	Derived classes should call this method at the start of all methods and properties that should not be accessed after a call to <see cref="M:Dispose()"/>.
		/// </remarks>
		protected void CheckDisposed()
		{
			if (_isDisposed)
				throw new ObjectDisposedException(this.GetType().FullName, string.Format(ExceptionMessage.ObjectDisposed, this.GetType()));
		}

		/// <summary>
		/// Releases all resources used by the instance.
		/// </summary>
		/// <remarks>
		/// Calls <see cref="M:Dispose(bool)"/> with the disposing parameter set to <see langword="true"/> to free unmanaged and managed resources.
		/// </remarks>
		public void Dispose()
		{
			if (!_isDisposed)
			{
				Dispose(true);

				// Because the cleanup code executed at dispose-time is a superset of the code executed at the finalize-time,
				// there is no need to call the finalize-time code during object finalization after the object has been disposed.
				// Moreover, keeping objects that don't need to be finalized in the finalization queue has a cost associated with it.
				// This is why the Dispose() method should call GC.SuppressFinalize, 
				// which removes the object from the finalization queue and thus prevents unnecessary finalization.
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// Releases the unmanaged resources used by this instance and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">
		/// 	<see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			// Refer to http://www.gotdotnet.com/team/libraries/whitepapers/resourcemanagement/resourcemanagement.aspx for guidelines.

			// After Dispose(bool) is called, objects are free to throw ObjectDisposedException from any instance method except Dispose().
			// Dispose() can be called multiple times and should never throw ObjectDisposedException.
			if (!_isDisposed)
			{
				try
				{
					// Acquire a lock on the object while disposing.

					// The protected Dispose(bool) method cannot be called from both the user thread and the finalizer thread at the same time,
					// but it can be called from multiple user threads, though this is not common.
					lock (_dummyLock)
					{
						// Dispose-time code should call Dispose() on all owned objects that implement the IDisposable interface. 
						// "owned" means objects whose lifetime is solely controlled by the container. 
						// In cases where ownership is not as straightforward, techniques such as Handle Collector can be used.  

						// Dispose-time code should also set references of all owned objects to null, after disposing them. This will allow the referenced objects to be garbage collected even if not all references to the "parent" are released. It may be a significant memory consumption win if the referenced objects are large, such as big arrays, collections, etc. 
						if (disposing)
						{
							// put code to dispose managed resources

							if (_reader != null)
							{
								_reader.Close();
								((IDisposable) _reader).Dispose();
								_reader = null;
							}

							_fieldHeaders = null;
							_fields = null;
						}

						// put code to free unmanaged resources here

						// the flag should be set while the object is locked
						_isDisposed = true;
					}
				}
				finally
				{
					// ensure that the flag is set
					_isDisposed = true;

					// catch any issues about firing an event on an already disposed object
					try
					{
						OnDisposed(EventArgs.Empty);
					}
					catch { }
				}
			}
		}

		/// <summary>
		/// 	<para>
		/// 		Releases unmanaged resources and performs other cleanup operations before the instance is reclaimed by garbage collection.
		/// 	</para>
		/// 	<para>
		/// 		In C#, finalizers are expressed using destructor syntax.
		/// 	</para>
		/// </summary>
		~CsvReader()
		{
			Dispose(false);
		}

		#endregion
	}
}
