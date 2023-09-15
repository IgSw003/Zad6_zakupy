using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Zad6_zakupy.Data;
using Zad6_zakupy.Models;

namespace Zad6_zakupy.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly Zad6_zakupyContext context;

    public MainViewModel(Zad6_zakupyContext context)
    {
        this.context = context;
        Purchases = context.Purchase
            .LoadAsync()
             .ContinueWith(p => context.Purchase.Local.ToObservableCollection());
    }

    [RelayCommand]
    private async Task AddPurchaseAsync()
    {
        if (NewPurchase.Validate())
        {
            context.Add(NewPurchase);
            await context.SaveChangesAsync();
            NewPurchase = new Purchase();
        }
    }

    [RelayCommand]
    private async Task DeletePurchaseAsync(Purchase purchase)
    {
        context.Remove(purchase);
        await context.SaveChangesAsync();
    }



    [ObservableProperty]
    private Purchase newPurchase = new Purchase();

    private TaskNotifier<ObservableCollection<Purchase>> purchases;
    public Task<ObservableCollection<Purchase>> Purchases
    {
        get => purchases;
        set => SetPropertyAndNotifyOnCompletion(ref purchases, value);
    }
}
