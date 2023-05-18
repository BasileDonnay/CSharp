using Microsoft.Extensions.DependencyInjection;
using ProjectBase.View;
using ProjectBase.ViewModel;

namespace ProjectBase;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


		builder.Services.AddSingleton<MainViewModel>(); // unique et permanente
		builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<DetailsViewModel>(); // transitoire 
        builder.Services.AddTransient<DetailsPage>();

        builder.Services.AddTransient<RaceViewModel>();
        builder.Services.AddTransient<RacePage>();

        builder.Services.AddTransient<UserViewModel>();
        builder.Services.AddTransient<UserPage>();
        builder.Services.AddTransient<CreateUserTables>();
        builder.Services.AddTransient<UserManagementServices>();

        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<LoginPage>();

        builder.Services.AddSingleton<CourseService>();


        return builder.Build();
	}
}
