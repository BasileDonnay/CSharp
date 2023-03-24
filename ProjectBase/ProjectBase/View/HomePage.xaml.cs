namespace ProjectBase.View;

public partial class Hom : ContentView
{
	public Hom(HomeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}