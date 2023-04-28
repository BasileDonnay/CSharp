
using ProjectBase.Model;

namespace ProjectBase.View;

public partial class RacePage : ContentPage
{
    public RacePage(RaceViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}