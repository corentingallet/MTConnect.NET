// Copyright (c) 2023 TrakHound Inc., All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using MTConnect.Configurations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MTConnect.Adapters.Shdr
{
    /// <summary>
    /// An Adapter class for communicating with an MTConnect Agent using the SHDR protocol.
    /// Supports multiple concurrent Agent connections.
    /// Uses a queue to collect changes to Observations and sends the entire buffer at the specified interval.
    /// </summary>
    public class ShdrIntervalQueueAdapter : ShdrQueueAdapter
    {
        /// <summary>
        /// The interval (in milliseconds) at which new data is sent to the Agent
        /// </summary>
        public int Interval { get; set; }


        public ShdrIntervalQueueAdapter(int port = 7878, int heartbeat = 10000) : base(port, heartbeat)
        {
            Init();
        }

        public ShdrIntervalQueueAdapter(string deviceKey, int port = 7878, int heartbeat = 10000) : base(deviceKey, port, heartbeat)
        {
            Init();
        }

        public ShdrIntervalQueueAdapter(ShdrAdapterConfiguration configuration) : base(configuration)
        {
            Init();
        }

        private void Init()
        {
            Interval = 100;
        }


        protected override void OnStart()
        {
            // Start Write Queue
            _ = Task.Run(() => Worker(StopToken.Token));
        }


        private async Task Worker(CancellationToken cancellationToken)
        {
            try
            {
                do
                {
                    int interval = Math.Max(1, Interval); // Set Minimum of 1ms to prevent high CPU usage

                    var stpw = System.Diagnostics.Stopwatch.StartNew();

                    // Call Overridable Method
                    OnIntervalElapsed();

                    stpw.Stop();

                    if (stpw.ElapsedMilliseconds < interval)
                    {
                        var waitInterval = interval - (int)stpw.ElapsedMilliseconds;

                        await Task.Delay(waitInterval, cancellationToken);
                    }

                } while (!cancellationToken.IsCancellationRequested);
            }
            catch (TaskCanceledException) { }
            catch (Exception) { }
        }

        protected virtual void OnIntervalElapsed()
        {
            // Send the buffered items
            SendBuffer();
        }
    }
}
