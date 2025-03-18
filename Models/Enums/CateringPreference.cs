using System.ComponentModel;

namespace WinLab4.Models.Enums;

public enum CateringPreference
{
    [Description("Bez preferencji")]
    None = 1,

    [Description("Wegetariańskie")]
    Vegetarian = 2,

    [Description("Bez glutenu")]
    GlutenFree = 3,
}
