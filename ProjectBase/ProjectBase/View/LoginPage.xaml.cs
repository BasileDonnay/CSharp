namespace ProjectBase.View;

public partial class LoginPage : ContentPage
{
    LoginViewModel viewModel;
	public LoginPage(LoginViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel;
		this.viewModel = viewModel;
    }

    private async void OnClicked(object sender, EventArgs e)
    {
        string nom = NomEntry.Text;
        string mdp = MDPEntry.Text;
        await viewModel.LoginUser(nom, mdp);
    }
}