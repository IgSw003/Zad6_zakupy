using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad6_zakupy.Models;

public partial class Purchase : ObservableValidator
{
    [ObservableProperty]
    private int id;

    [ObservableProperty]
    [Required(ErrorMessage ="Jest wymagana")]
    private DateTime date = DateTime.Today;

    [ObservableProperty]
    [Required(ErrorMessage = "Jest wymagana")]
    private string name;

    [ObservableProperty]
    [Required(ErrorMessage = "Jest wymagana")]
    [NotifyPropertyChangedFor(nameof(GrossPrice))]
    private decimal price;

    [Required(ErrorMessage = "Jest wymagana")]
    [NotifyPropertyChangedFor(nameof(GrossPrice))]
    [ObservableProperty]
    [Range(0.0,9999.99, ErrorMessage ="błędna wartość")]
    [Column(TypeName = "decimal(6, 2)")]
    private decimal quantity;

    public decimal GrossPrice => Quantity * Price;

    public bool Validate()
    {
        ValidateAllProperties();
        return !HasErrors;
    }
}
