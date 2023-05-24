using Xamarin.Forms;
using ProductOrg.Models;
using System;
using System.Collections.ObjectModel;
using ProductOrg.Repositories;

namespace ProductOrg.ViewModels
{
    public class PomodoroConfigViewModel : BaseViewModel
    {
        #region Attributes

        private short selectedWorking;
        private short selectedPomodoros;
        private short selectedLongBreaks;
        private short selectedShortBreaks;
        private ObservableCollection<short> working;
        private ObservableCollection<short> pomodoros;
        private ObservableCollection<short> longBreaks;
        private ObservableCollection<short> shortBreaks;

        #endregion
        #region Properties

        public ObservableCollection<short> Working
        {
            get { return working; }
            set { working = value; SetProperty(ref working, value); }
        }

        public short SelectedWorking
        {
            get { return selectedWorking; }
            set { selectedWorking = value; SetProperty(ref selectedWorking, value); }
        }

        public ObservableCollection<short> ShortBreaks
        {
            get { return shortBreaks; }
            set { shortBreaks = value; SetProperty(ref shortBreaks, value); }
        }

        public short SelectedShortBreaks
        {
            get { return selectedShortBreaks; }
            set { selectedShortBreaks = value; SetProperty(ref selectedShortBreaks, value); }
        }

        public ObservableCollection<short> LongBreaks
        {
            get { return longBreaks; }
            set { longBreaks = value; SetProperty(ref longBreaks, value); }
        }

        public short SelectedLongBreaks
        {
            get { return selectedLongBreaks; }
            set { selectedLongBreaks = value; SetProperty(ref selectedLongBreaks, value); }
        }

        public ObservableCollection<short> Pomodoros
        {
            get { return pomodoros; }
            set { pomodoros = value; SetProperty(ref pomodoros, value); }
        }

        public short SelectedPomodoros
        {
            get { return selectedPomodoros; }
            set { selectedPomodoros = value; SetProperty(ref selectedPomodoros, value); }
        }

        #endregion
        #region Commands

        public Command SaveCommand { get; set; }
        public Command ResetCommand { get; set; }

        #endregion
        #region Injects

        //private INavigationService navigationService;
        //private IPageDialogService dialogService;

        #endregion
        #region Constructs

        public PomodoroConfigViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Working = new ObservableCollection<short>();
            ShortBreaks = new ObservableCollection<short>();
            LongBreaks = new ObservableCollection<short>();
            Pomodoros = new ObservableCollection<short>();

            SaveCommand = new Command(Save);
            ResetCommand = new Command(Reset);
            LoadControls();
            LoadConfiguracion();
        }

        #endregion
        #region Process

        private async void Save()
        {
            var repo = new PomodoroConfigRepository();

            var configuracion = new PomodoroConfiguration()
            {
                Working = SelectedWorking,
                ShortBreak = SelectedShortBreaks,
                LongBreak = SelectedLongBreaks,
                Pomorodos = SelectedPomodoros
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

        private void LoadControls()
        {
            Working = new ObservableCollection<short>(new short[] { 5, 10, 15, 20, 25, 30, 45, 60 });
            ShortBreaks = new ObservableCollection<short>(new short[] { 5, 10, 15, 20, 25, 30, 45, 60 });
            LongBreaks = new ObservableCollection<short>(new short[] { 5, 10, 15, 20, 25, 30, 45, 60 });
            Pomodoros = new ObservableCollection<short>(new short[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        }

        private void LoadConfiguracion()
        {
            var repo = new PomodoroConfigRepository();
            var config = repo.LoadAsync();

            if (config == null)
            {
                config = new PomodoroConfiguration()
                {
                    Working = 25,
                    ShortBreak = 5,
                    LongBreak = 20,
                    Pomorodos = 4
                };
            }

            SelectedWorking = config.Working;
            SelectedShortBreaks = config.ShortBreak;
            SelectedLongBreaks = config.LongBreak;
            SelectedPomodoros = config.Pomorodos;
        }

        #endregion
    }
}
