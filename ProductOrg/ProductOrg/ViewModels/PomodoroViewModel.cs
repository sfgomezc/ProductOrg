using System.Timers;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ProductOrg.Models;
using System;
using ProductOrg.Services;

namespace ProductOrg.ViewModels
{
    public class PomodoroViewModel : BaseViewModel
    {
        #region Attributes

        private Timer timer;
        private TimeSpan _elapsed;
        private Activity _currentActivity;
        private int _pomodoros = 1;
        private float _progressValue = 1;   //percentage 0-1
        private bool _isRunning;
        private bool _isWorking;
        private int _durations;
        private string _menuMessage;
        private bool _showMenuMessage;
        private Configuration config;

        #endregion
        #region Objetos

        public float ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                OnPropertyChanged();
            }
        }

        public int Pomodoros
        {
            get { return _pomodoros; }
            set
            {
                _pomodoros = value;
                OnPropertyChanged();
            }
        }

        public int Durations
        {
            get { return _durations; }
            set
            {
                _durations = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Elapsed
        {
            get { return _elapsed; }
            set
            {
                _elapsed = value;
                OnPropertyChanged();
            }
        }

        private Activity CurrentActivity
        {
            get { return _currentActivity; }
            set
            {
                _currentActivity = value;
                IsWorking = value == Activity.Working;
                //eventAggregator.GetEvent<UpdateNavBarEvent>().Publish(IsWorking);
                soundService.ChangeActivity();
            }
        }

        public bool IsWorking
        {
            get { return _isWorking; }
            set
            {
                _isWorking = value;
                OnPropertyChanged();
            }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                OnPropertyChanged();
            }
        }

        public string MenuMessage
        {
            get { return _menuMessage; }
            set
            {
                _menuMessage = value;
                OnPropertyChanged();
            }
        }

        public bool ShowMenuMessage
        {
            get { return _showMenuMessage; }
            set
            {
                _showMenuMessage = value;
                OnPropertyChanged();
            }
        }

        public string AppVersion { get; set; }

        #endregion
        #region Comandos

        public ICommand startPauseCmd { get; set; }
        public ICommand logoutCmd { get; set; }

        #endregion
        #region Services

        private SoundService soundService;

        #endregion
        #region Constructors

        public PomodoroViewModel(INavigation navigation)
        {
            Navigation = navigation;
            startPauseCmd = new Command(async () => await StartPauseAsync());
            logoutCmd = new Command(async () => await LogoutAsync());
            //AppVersion = ((App)App.Current).appVersion;
            this.soundService = new SoundService();
            LoadConfiguracion();
            ConfigureTimer();
        }

        #endregion
        #region Procesos

        private async Task StartPauseAsync()
        {
            if (IsRunning)
                Pause();
            else
                Start();
        }

        private async Task LogoutAsync()
        {
            var result = await App.Current.MainPage.DisplayAlert("Información", "Desea cerrar la sesión actual?", "Si", "No");
            if (result) await Navigation.PopAsync();
        }

        #endregion
        #region Metodos Privados

        private void ShowMessage(string message)
        {
            MenuMessage = message;
            ShowMenuMessage = true;
        }

        public void ChangeState(Activity activity)
        {
            if (activity == Activity.LongBreak)
            {
                Durations = config.LongBreak * 60;
                Pomodoros = 1;
            }
            else if (activity == Activity.ShortBreak)
                Durations = config.ShortBreak * 60;
            else
                Durations = config.Working * 60;

            CurrentActivity = activity;
            Elapsed = new TimeSpan(0, 0, Durations);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Elapsed = Elapsed.Add(TimeSpan.FromSeconds(-1));
            ProgressValue = (float)Elapsed.TotalSeconds / Durations;

            if (CurrentActivity == Activity.Working && Elapsed.TotalSeconds == 0)
            {
                //AddHistory(Elapsed.TotalMinutes, true);
                if (Pomodoros == config.Pomorodos)
                    ChangeState(Activity.LongBreak);
                else
                    ChangeState(Activity.ShortBreak);
            }

            if ((CurrentActivity == Activity.ShortBreak && Elapsed.TotalSeconds == 0)
               || (CurrentActivity == Activity.LongBreak && Elapsed.TotalSeconds == 0))
            {
                //AddHistory(Elapsed.TotalMinutes, false);
                ChangeState(Activity.Working);
                Pomodoros++;
            }
        }

        private void ConfigureTimer()
        {
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
        }

        private void LoadConfiguracion()
        {
            CurrentActivity = Activity.Working;
            //var repo = new ConfigurationRepository();
            //config = repo.LoadAsync();
            config = new Configuration()
            {
                Id = 1,
                Working = 2,
                Pomorodos = 4,
                ShortBreak = 5,
                LongBreak = 15
            };

            Elapsed = new TimeSpan(0, 0, config.Working * 60);
            Durations = config.Working * 60;
        }

        private void Start()
        {
            timer.Start();
            IsRunning = true;
        }

        private void Pause()
        {
            timer.Stop();
            IsRunning = false;
        }
        #endregion
    }
}
