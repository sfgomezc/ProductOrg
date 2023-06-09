using Xamarin.Forms;
using ProductOrg.Models;
using System;
using System.Collections.ObjectModel;
using ProductOrg.Repositories;

namespace ProductOrg.ViewModels
{
    public class TodoViewModel : BaseViewModel
    {
        #region Attributes

        private ObservableCollection<SelectableData<TodoModel>> _listTodo;

        #endregion
        #region Properties

        public ObservableCollection<SelectableData<TodoModel>> ListTodo
        {
            get { return _listTodo; }
            set
            {
                _listTodo = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Commands

        public Command SaveCommand { get; set; }
        public Command ResetCommand { get; set; }

        #endregion
        #region Constructs

        public TodoViewModel(INavigation navigation)
        {
            Navigation = navigation;
            ListTodo = new ObservableCollection<SelectableData<TodoModel>>();

            SaveCommand = new Command(Save);
            ResetCommand = new Command(Reset);
            LoadTodos();
        }

        #endregion
        #region Process

        private async void Save()
        {
            var repo = new PomodoroConfigRepository();

            var configuracion = new PomodoroConfiguration()
            {
            };

            try
            {
                repo.SaveAsync(configuracion);
            }
            catch (Exception ex)
            {
                //await dialogService.DisplayAlertAsync(Languages.Pomodoro, Languages.ErrorSaveconfiguration, Languages.Accept);
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                return;
            }

            //await dialogService.DisplayAlertAsync(Languages.Pomodoro, Languages.SuccessSaveConfiguration, Languages.Accept);
            await App.Current.MainPage.DisplayAlert("Info", "Configuration update!!", "Ok");
            await Navigation.PopAsync();

        }

        private async void Reset()
        {
            //var answer = await dialogService.DisplayAlertAsync(Languages.Pomodoro, Languages.QuestionDeleteHistory, Languages.Yes, Languages.No);
            //if (answer)
            //{
            //    var repo = new HistoryRepository();
            //    try
            //    {
            //        repo.DeleteStores();
            //    }
            //    catch (Exception ex)
            //    {
            //        Crashes.TrackError(ex);
            //        await dialogService.DisplayAlertAsync(Languages.Pomodoro, Languages.ErrorDeleteHistory, Languages.Accept);
            //        return;
            //    }

            //    await dialogService.DisplayAlertAsync(Languages.Pomodoro, Languages.SuccessDeleteHistory, Languages.Accept);
            //}
        }

        #endregion
        #region Private Methods

        private void LoadTodos()
        {
            var repo = new TodoRepository();
            var todos = repo.LoadAsync();

            foreach (TodoModel todo in todos)
            {
                SelectableData<TodoModel> selectableData = new SelectableData<TodoModel>() { Data = todo, Selected = todo.IsDone };
                ListTodo.Add(selectableData);
            }
        }

        #endregion
    }
}
