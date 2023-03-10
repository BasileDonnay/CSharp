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
		
		builder.Services.AddSingleton<StudentService>();


        return builder.Build();
	}
}
