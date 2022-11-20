using System.Diagnostics;
using ToDoList.DataServices;
using ToDoList.Models;

namespace ToDoList.Pages;

[QueryProperty(nameof(Todo), "Todo")]
public partial class ManageTodoPage : ContentPage
{
    private readonly IRestDataService _restDataService;
    private Todo _todo;
    private bool _isNew;

    public Todo Todo
    {
        get => _todo;
        set
        {
            _isNew = IsNew(value);
            _todo = value;
            OnPropertyChanged();
        }
    }
    public ManageTodoPage(IRestDataService restDataService)
	{
		InitializeComponent();
        _restDataService = restDataService;
        BindingContext = this;
    }

    private bool IsNew(Todo todo)
    {
        if(todo.Id == 0)
        {
            return true;
        }
        return false;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        if(_isNew)
        {
            Debug.WriteLine("Add new item");
            await _restDataService.AddTodoAsync(Todo);
        }
        else
        {
            Debug.WriteLine("Update an item");
            await _restDataService.UpdateTodoAsync(Todo);
        }

        await Shell.Current.GoToAsync("..");
    }

    private async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        await _restDataService.DeleteTodoAsync(Todo.Id);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}