using System.Collections.ObjectModel;
using Firebase.Database;
using Firebase.Database.Query;
using MauiFirebaseDemo.Model;

namespace MauiFirebaseDemo;

public partial class MainPage : ContentPage
{
    FirebaseClient _firebase = new FirebaseClient("MY_FIREBASE_REALTIME_DATABASE");    
    public ObservableCollection<TodoItem> TodoItems { get; set; } = new ObservableCollection<TodoItem>();
    
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;

        var collection = _firebase.Child("Todo")
            .AsObservable<TodoItem>()
            .Subscribe((item) =>
            {
                if (item.Object != null)
                    TodoItems.Add(item.Object);
            });
    }


    private void OnAddTodoClicked(object? sender, EventArgs e)
    {
        var todo = new TodoItem
        {
            Title = TitleEntry.Text,
        };
        _firebase.Child("Todo").PostAsync(todo);
        
        TitleEntry.Text = string.Empty;
    }
}
