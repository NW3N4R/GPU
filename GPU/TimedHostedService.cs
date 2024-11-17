using GPU.Helpers;
using GPU.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using GPU.Services;
using System.Data;
namespace GPU
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private int _secondsCounter = 0;
       
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            if (_secondsCounter == 0)
            {
                await WeatherCall.getWeather();
            }

            // Reset counter every hour
            _secondsCounter = (_secondsCounter + 1) % 3600;

            // Process Infos list and update CountDown
            var itemsToRemove = DbConnectionHelper.Infos;
            foreach (var item in itemsToRemove)
            {
                if (item.CountDown < 3)
                {
                    item.CountDown++;
                }
            }

            DbConnectionHelper.Infos.RemoveAll(i => i.CountDown >= 3);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
