using System;
using System.IO;

namespace ByteCarrot.Masslog.Core.Logging
{
    public class OutputFilterStream : Stream
    {
        private readonly Stream _innerStream;
        private readonly MemoryStream _copyStream;

        public OutputFilterStream(Stream inner)
        {
            _innerStream = inner;
            _copyStream = new MemoryStream();
        }

        public override string ToString()
        {
            lock (_innerStream)
            {
                if (_copyStream.Length <= 0L || !_copyStream.CanRead || !_copyStream.CanSeek)
                {
                    return String.Empty;
                }

                var pos = _copyStream.Position;
                _copyStream.Position = 0L;
                try
                {
                    return new StreamReader(_copyStream).ReadToEnd();
                }
                finally
                {
                    try
                    {
                        _copyStream.Position = pos;
                    }
                    catch { }
                }
            }
        }

        public override bool CanRead
        {
            get { return _innerStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _innerStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _innerStream.CanWrite; }
        }

        public override void Flush()
        {
            _innerStream.Flush();
        }

        public override long Length
        {
            get { return _copyStream.Length; }
        }

        public override long Position
        {
            get { return _innerStream.Position; }
            set { _copyStream.Position = _innerStream.Position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _innerStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            _copyStream.Seek(offset, origin);
            return _innerStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _copyStream.SetLength(value);
            _innerStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _copyStream.Write(buffer, offset, count);
            _innerStream.Write(buffer, offset, count);
        }
    }
}