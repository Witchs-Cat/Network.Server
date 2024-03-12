using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Network.Server.Request
{
    public class RequestEnumerator : IAsyncEnumerator<byte>
    {
        private Socket _handler;

        private byte[] _buffer;
        private int _receiveSize;
        private int _currentIndex;

        private SemaphoreSlim _semaphore;

        public RequestEnumerator(Socket handler)
        {
            _handler = handler;
            _semaphore = new SemaphoreSlim(1);

            _buffer = new byte[4096];
            _receiveSize = 0;
            _currentIndex = 0;
        }

        public byte Current => _buffer[_currentIndex];


        private async Task ListenNextAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                _receiveSize = await _handler.ReceiveAsync(_buffer);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<string> ReadLineAsync()
        {
            StringBuilder line = new();
            while (await MoveNextAsync())
            {
                char chr = Convert.ToChar(Current);
                line.Append(chr);

                if (chr == '\n')
                    return line.ToString();
            }
            return line.ToString();
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            if (_currentIndex != _receiveSize)
            {
                _currentIndex++;
                return true;
            }

            _currentIndex = 0;
            await ListenNextAsync();

            return true;
        }

        public ValueTask DisposeAsync()
        {
            _semaphore.Dispose();
            return ValueTask.CompletedTask;
        }
    }
}
