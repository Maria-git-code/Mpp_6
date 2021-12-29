using System;
using System.Collections.Concurrent;
using System.IO;
using System.Timers;

namespace lab6
{
    public class LogBuffer
    {
        private ConcurrentQueue<string> Words { get; } = new();
        private readonly StreamWriter _streamWriter;

        private const int Capacity = 10;
        private const int Limit = 10000;

        public LogBuffer(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("File doesn't exists: " + filePath);
            }

            _streamWriter = new StreamWriter(filePath, true);

            var timer = new Timer(Limit);
            timer.Elapsed += checkTimer;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void Add(string item)
        {
            Words.Enqueue(item);
            CheckCapacity();
        }
        
        private void checkTimer(object source, ElapsedEventArgs e)
                {
                    while (!Words.IsEmpty)
                    {
                        Words.TryDequeue(out var message);
                        if (message != null)
                        {
                            _streamWriter.WriteLineAsync(message);
                        }
                    }
                }
        private void CheckCapacity()
        {
            if (Words.Count < Capacity)
            {
                return;
            }
            while (!Words.IsEmpty)
            {
                Words.TryDequeue(out string message);
                if (message != null)
                {
                    _streamWriter.WriteLineAsync(message);
                }
            }
        }


        public void Close()
        {
            _streamWriter.Close();
        }
    }
}