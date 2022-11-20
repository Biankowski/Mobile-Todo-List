using System.Diagnostics;
using ToDoList.DataServices;
using ToDoList.Models;
using ToDoList.Pages;

namespace ToDoList;

public partial class MainPage : ContentPage
{
	private readonly IRestDataService _restDataService;
	public MainPage(IRestDataService restDataService)
	{
		InitializeComponent();
		_restDataService = restDataService;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();

		collectionView.ItemsSource = await _restDataService.GetAllTodosAsync();
		Debug.WriteLine("WTF");
	}
	
	private async void OnAddTodoClicked(object sender, EventArgs e)
	{
		Debug.WriteLine("Add button clicked");

		var navigationParameter = new Dictionary<string, object>
		{
			{nameof(Todo), new Todo() }
		};

		await Shell.Current.GoToAsync(nameof(ManageTodoPage), navigationParameter);
	}

	private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		Debug.WriteLine("Item changed clicked");

        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(Todo),e.CurrentSelection.FirstOrDefault() as Todo }
        };

        await Shell.Current.GoToAsync(nameof(ManageTodoPage), navigationParameter);
    }
}

