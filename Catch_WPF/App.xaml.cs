using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Catch_Wpf.ViewModel;
using Catch_Wpf.Model;
using Catch_Wpf.View;
using Catch_Wpf.Persistence;
using System.Windows.Threading;
using System.ComponentModel;

namespace Catch_Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string def11 = "Catch_WPF/Catch_Wpf/def11.txt";
        private string def15 = "Catch_WPF/Catch_Wpf/def15.txt";
        private string def21 = "Catch_WPF/Catch_Wpf/def21.txt";
        private string saved11 = "Catch_WPF/Catch_Wpf/11.txt";
        private string saved15 = "Catch_WPF/Catch_Wpf/15.txt";
        private string saved21 = "Catch_WPF/Catch_Wpf/21.txt";

        private DataAccess _dataAccess;
        private GameModel _model;
        private MainWindow _view;
        private GameViewModel _viewModel;
        private DispatcherTimer _timer;

        public App()
        {
            Startup += new StartupEventHandler(AppStartUp);
        }

        private void AppStartUp(object sender, StartupEventArgs e)
        {
            _view = new MainWindow();
            Initalize(0, def15);
        }

        #region New Game Handlers
        private void ViewModel_Saved21(object? sender, EventArgs e)
        {
            _timer.Stop();
            Initalize(1, saved21);
        }

        private void ViewModel_Saved15(object? sender, EventArgs e)
        {
            _timer.Stop();
            Initalize(1, saved15);
        }

        private void ViewModel_Saved11(object? sender, EventArgs e)
        {
            _timer.Stop();
            Initalize(1, saved11);
        }

        private void ViewModel_New21(object? sender, EventArgs e)
        {
            _timer.Stop();
            Initalize(0, def21);
        }

        private void ViewModel_New15(object? sender, EventArgs e)
        {
            _timer.Stop();
            Initalize(0, def15);
        }

        private void ViewModel_New11(object? sender, EventArgs e)
        {
            _timer.Stop();
            Initalize(0, def11);
        }

        #endregion

        #region View Model Handlers
        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (_viewModel.IsGame)
            {
                _model.AdvanceTime(true);
                if (_model.E1)
                {
                    _model.Enemy1Move(1);
                }
                if (_model.E2)
                {
                    _model.Enemy2Move(1);
                }
                _viewModel.RefreshTable();
            }
        }
        private void ViewModel_Save(object? sender, EventArgs e)
        {
            if (_viewModel.Size == 11)
            {
                _model.Save(saved11, _model.Table);
            }else if (_viewModel.Size == 15)
            {
                _model.Save(saved15, _model.Table);
            }else if (_viewModel.Size == 21)
            {
                _model.Save(saved21, _model.Table);
            }
        }

        private void ViewModel_Pause(object? sender, EventArgs e)
        {
            if (_viewModel.IsGame)
            {
                _viewModel.IsGame = false;
                _timer.Stop();
            }
            else
            {
                _viewModel.IsGame = true;
                _timer.Start();
            }
        }

        #endregion

        #region Model Handlers
        private void Modell_GameOver(object? sender, GameModelEventArgs e)
        {
            _viewModel.RefreshTable();
            string message;
            string caption;
            _timer.Stop();
            //onGame = false;
            if (e.WinLose == 1)
            {
                message = "Nyertél, szeretnél új játékot kezdeni?";
                caption = "GameOver";
            }
            else if (e.WinLose == 0)
            {
                message = "Vesztettél, mert elkaptak.\nSzeretnél új játékot kezdeni?";
                caption = "GameOver";
            }
            else
            {
                message = "Vesztettél, mert aknára léptél.\nSzeretnél új játékot kezdeni?";
                caption = "GameOver";
            }
            var x = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
            if (x == MessageBoxResult.No)
            {
                _view.Close();
            }
        }
        #endregion

        private void Initalize(int i, string s)
        {
            _dataAccess = new DataAccess();
            _model = new GameModel(_dataAccess);
            if (i == 0)
            {
                _model.Loaddef(s);
            }
            else
            {
                _model.Load(s);
            }
            _viewModel = new GameViewModel(_model);
            _timer = new DispatcherTimer();

            _model.GameOver += new EventHandler<GameModelEventArgs>(Modell_GameOver);

            _viewModel.New11Game += new EventHandler(ViewModel_New11);
            _viewModel.New15Game += new EventHandler(ViewModel_New15);
            _viewModel.New21Game += new EventHandler(ViewModel_New21);
            _viewModel.Saved11Game += new EventHandler(ViewModel_Saved11);
            _viewModel.Saved15Game += new EventHandler(ViewModel_Saved15);
            _viewModel.Saved21Game += new EventHandler(ViewModel_Saved21);
            _viewModel.SaveGame += new EventHandler(ViewModel_Save);
            //_viewModel.ExitGame += new EventHandler(ViewModel_Exit);
            _viewModel.PauseGame += new EventHandler(ViewModel_Pause);

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();

            _view.DataContext = _viewModel;
            //_view.Closing += new System.ComponentModel.CancelEventHandler(View_Closing); // eseménykezelés a bezáráshoz
            _view.Show();
        }
    }
}
