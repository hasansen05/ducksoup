﻿#region

using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using API.Command;
using API.Server;
using API.ServiceFactory;
using DuckSoup.Library;
using DuckSoup.Library.Commands;
using DuckSoup.Library.Database;
using DuckSoup.Library.Event;
using DuckSoup.Library.Party;
using DuckSoup.Library.Plugins;
using DuckSoup.Library.Server;
using DuckSoup.Library.Services;
using DuckSoup.Library.Settings;
using DuckSoup.Library.Webserver;
using Serilog;
using Serilog.Core;
using Serilog.Events;

#endregion

namespace DuckSoup;

public static class Program
{
    public static LoggingLevelSwitch LoggingLevelSwitch { get; } = new LoggingLevelSwitch();

    private static void Main()
    {
        ThreadPool.SetMinThreads(1024 * 32, 1024 * 32);
        LoggingLevelSwitch.MinimumLevel = LogEventLevel.Debug; 

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(LoggingLevelSwitch)
            .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day, 
                outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();

        Log.Debug("Testing: Debug");
        Log.Information("Testing: Information");
        Log.Warning("Testing: Warning");
        Log.Error("Testing: Error");
        Log.Fatal("Testing: Fatal");

        // prints out logo + version
        Log.Information("\n\n" +
                        ",--.          .   .---.             \n" +
                        "|   \\ . . ,-. | , \\___  ,-. . . ,-. \n" +
                        "|   / | | |   |<      \\ | | | | | | \n" +
                        "^--'  `-^ `-' ' ` `---' `-' `-^ |-' \n" +
                        "                                |   \n" +
                        "         Version  {0}         ' \n" +
                        "\n",
            FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location)
                .ProductVersion);
        Console.Title = "Starting up...";

        try
        {
            var settingsManager = new SettingsManager();
            var databaseManager = new DatabaseManager();

            var sharedObjects = new SharedObjects();
            var userService = new UserService();
            var authService = new AuthService();
            var partyManager = new PartyManager();
            var serverManager = new ServerManager();
            var webserverManager = new WebserverManager();
            var commandManager = new CommandManager();
            var pluginManager = new PluginManager();
            var eventManager = new EventManager();

            // Make sure we start the command loop in order to not exit the application
            ServiceFactory.Load<ICommandManager>(typeof(ICommandManager)).StartCommandLoop();
        }
        catch (Exception exception)
        {
            Log.Error("Program.cs Main| {0}", exception.Message);
            Log.Error("Program.cs Main| {0}", exception.StackTrace);
        }
    }

    public static void Stop()
    {
        try
        {
            ServiceFactory.Load<IServerManager>(typeof(IServerManager)).Dispose();
            ServiceFactory.Load<ICommandManager>(typeof(ICommandManager)).Dispose();
        }
        catch (Exception exception)
        {
            Log.Error("Program.cs Stop| {0}", exception.Message);
        }
    }
}