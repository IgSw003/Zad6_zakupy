using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Zad6_zakupy.Data;
using Zad6_zakupy.Models;
using Zad6_zakupy.ViewModels;
using Zad6_zakupy.Views;

namespace Zad6_zakupy;

public partial class App : Application
{
    public new static App Current => (App)Application.Current;
    public IServiceProvider Services { get; private set; }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddDbContext<Zad6_zakupyContext>(options => options.UseSqlite("Data Source=Zad6_zakupy.db"));
        services.AddTransient<MainViewModel>();
        return services.BuildServiceProvider();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        Services = ConfigureServices();
        if(!Design.IsDesignMode)
        {
            using (var scope = Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Zad6_zakupyContext>();
                context.Database.Migrate();
                if (!context.Purchase.Any())
                {
                    context.Add(new Purchase()
                    {
                        Name = "Papier toaletowy",
                        Date = DateTime.Now,
                        Price = 8.8m,
                        Quantity = 1m
                    });
                    context.SaveChanges();
                }
            }
        }
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Services.GetRequiredService<MainViewModel>()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = Services.GetRequiredService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
