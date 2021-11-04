using ClickHouse.Client.ADO;
using FreeSql;
using FreeSql.Aop;
using FreeSql.Custom;
using Microsoft.Extensions.DependencyInjection;
using SinoFramework.ClickHouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ClickHouseExtensions
    {
        public static IServiceCollection UseClickHouse(this IServiceCollection services, string connectionString)
        {

            IFreeSql fsql = new FreeSqlBuilder()
                .UseConnectionFactory(FreeSql.DataType.Custom, ()=> new ClickHouseConnection(connectionString))
#if DEBUG
                .UseMonitorCommand(cmd =>
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("SQL：");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(cmd.CommandText);
                    Console.ForegroundColor = ConsoleColor.White;
                })
                .UseNoneCommandParameter(true)
#endif
                .Build();

            fsql.SetCustomAdapter(new ClickHouseAdapter());

            services.AddSingleton<IFreeSql>(fsql);

            return services;
        }        
    }
}
