﻿using Binance;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceConsoleApp.Controllers
{
    public class GetTradesIn : IHandleCommand
    {
        public async Task<bool> HandleAsync(string command, CancellationToken token = default)
        {
            if (!command.StartsWith("tradesIn ", StringComparison.OrdinalIgnoreCase)
                && !command.Equals("tradesIn", StringComparison.OrdinalIgnoreCase))
                return false;

            var args = command.Split(' ');

            string symbol = Symbol.BTC_USDT;
            if (args.Length > 1)
            {
                symbol = args[1];
            }

            long startTime = 0;
            if (args.Length > 2)
            {
                long.TryParse(args[2], out startTime);
            }

            long endTime = 0;
            if (args.Length > 3)
            {
                long.TryParse(args[3], out endTime);
            }

            var trades = await Program._api.GetAggregateTradesInAsync(symbol, startTime, endTime, token: token);

            lock (Program._consoleSync)
            {
                Console.WriteLine();
                foreach (var trade in trades)
                {
                    Program.Display(trade);
                }
                Console.WriteLine();
            }

            return true;
        }
    }
}