using Xamarin.Forms;
using ProductOrg.Models;
using System;
using System.Collections.ObjectModel;
using ProductOrg.Repositories;
using ProductOrg.Views;

namespace ProductOrg.ViewModels
{
    public class TodoItemViewModel : BaseViewModel
    {
        #region Attributes

        private TodoModel _todoItem;
        private string _title;
        private string _description;
        private string _messageError;

        #endregion
        #region Properties

        public TodoModel TodoItem
        {
            get { return _todoItem; }
            set
            {
                _todoItem = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public string MessageError
        {
            get { return _messageError; }
            set
            {
                _messageError = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Commands

        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }

        #endregion
        #region Constructs

        public TodoItemViewModel(INavigation navigation)
        {
            Navigation = navigation;

            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
            LoadTodos();
        }

        #endregion
        #region Process

        private async void Save()
        {
            var repo = new TodoRepository();
            ShowMessage("");

            try
            {
                if (Title == null || Title.Length < 5)
                    ShowMessage("The title is required!");
                else if (Description == null || Description.Length < 5)
                    ShowMessage("The description is required!");

                if (MessageError.Length > 0)
                    return;

                var item = new TodoModel()
                {
                    Title = Title,
                    Description = Description,
                    IsDone = false
                };

                repo.SaveAsync(item);
            }
            catch (Exception ex)
            {
                //await dialogService.DisplayAlertAsync(Languages.Pomodoro, Languages.ErrorSaveconfiguration, Languages.Accept);
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                return;
            }

            await Navigation.PopAsync();

        }

        private async void Cancel()
        {
            await Navigation.PopAsync();
        }

        #endregion
        #region Private Methods

        private void ShowMessage(string message)
        {
            MessageError = message;
        }

        private void LoadTodos()
        {
            //var repo = new TodoRepository();
            //var todos = repo.LoadAsync();

            //foreach (TodoModel todo in todos)
            //{
            //    SelectableData<TodoModel> selectableData = new SelectableData<TodoModel>() { Data = todo, Selected = todo.IsDone };
            //    ListTodo.Add(selectableData);
            //}
        }

        #endregion
    }
}
