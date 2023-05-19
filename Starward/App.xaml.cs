﻿// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Starward.Helper;
using System;
using System.IO;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Starward;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
        RequestedTheme = ApplicationTheme.Dark;
        UnhandledException += App_UnhandledException;
        InitializeConsoleOutput();
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Starward", "crash");
        Directory.CreateDirectory(folder);
        var file = Path.Combine(folder, $"crash_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        File.WriteAllText(file, e.Exception.ToString());
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        m_window = new MainWindow();
        m_window.Activate();
    }

    private Window m_window;



    private void InitializeConsoleOutput()
    {
        _ = Task.Run(() =>
        {
            if (AppConfig.EnableConsole)
            {
                ConsoleHelper.Alloc();
                ConsoleHelper.Show();
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Welcome to Starward v{AppConfig.AppVersion}");
            Console.WriteLine(DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            Console.WriteLine(Environment.CommandLine);
            Console.WriteLine();
            Console.ResetColor();
        });
    }



}