namespace ProjectBase.View;

public partial class UserPage : ContentPage
{
    UserViewModel viewModel;
	public UserPage(UserViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel;
		this.viewModel = viewModel;
    }

    private async void OnAjouterClicked(object sender, EventArgs e)
    {
        string nom = NomEntry.Text;
        string mdp = MDPEntry.Text;
        string isAdmin = IsAdminEntry.Text;
        await viewModel.InsertUser(nom, mdp, isAdmin);
    }
}