using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catch_Wpf.Model;

namespace Catch_Wpf.ViewModel 
{
    public class GameViewModel : ViewModelBase
    {
        private GameModel _model;
        private string _Pcolor = "orange";
        private string _Ecolor = "blue";
        private string _Fieldcolor = "green";
        private string _Minecolor = "black";
        private bool _onGame;

        #region New Game Commands and Handlers
        public DelegateCommand NewGame11Command { get; private set; }
        public DelegateCommand NewGame15Command { get; private set; }
        public DelegateCommand NewGame21Command { get; private set; }
        public DelegateCommand Load11Command { get; private set; }
        public DelegateCommand Load15Command { get; private set; }
        public DelegateCommand Load21Command { get; private set; }

        public event EventHandler New11Game;
        public event EventHandler New15Game;
        public event EventHandler New21Game;
        public event EventHandler Saved11Game;
        public event EventHandler Saved15Game;
        public event EventHandler Saved21Game;

        #endregion

        #region Other Commands And Handlers
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand PauseCommand { get; private set; }

        public event EventHandler SaveGame;
        public event EventHandler PauseGame;

        #endregion

        public ObservableCollection<Field> Fields { get; set; }
        public int Size { get { return _model.Table.M; } }
        public string GameTime { get { return _model.Time.ToString(); } }
        public bool IsGame { get { return _onGame; } set { _onGame = value; } }

        public GameViewModel(GameModel m)
        {
            _model = m;
            NewGame11Command = new DelegateCommand(p => OnNew11());
            NewGame15Command = new DelegateCommand(p => OnNew15());
            NewGame21Command = new DelegateCommand(p => OnNew21());
            Load11Command = new DelegateCommand(p => OnSaved11());
            Load15Command = new DelegateCommand(p => OnSaved15());
            Load21Command = new DelegateCommand(p => OnSaved21());

            SaveCommand = new DelegateCommand(p => OnSave());
            PauseCommand = new DelegateCommand(p => OnPause());

            Fields = new ObservableCollection<Field>();
            Random r = new Random();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Fields.Add(new Field
                    {
                        Color = GetColor(i, j),
                        X = i,
                        Y = j,
                        Number = i * Size + j,
                        StepCommand = new DelegateCommand(p => StepGame(Convert.ToInt32(p)))
                    }) ;
                }
            }
            _onGame = true;
            RefreshTable();
        }

        #region New Game Handler Methods
        private void OnSaved21()
        {
            Saved21Game.Invoke(this, EventArgs.Empty);
        }

        private void OnSaved15()
        {
            Saved15Game.Invoke(this, EventArgs.Empty);
        }

        private void OnSaved11()
        {
            Saved11Game.Invoke(this, EventArgs.Empty);
        }

        private void OnNew21()
        {
            New21Game.Invoke(this, EventArgs.Empty);
        }

        private void OnNew15()
        {
            New15Game.Invoke(this, EventArgs.Empty);
        }

        private void OnNew11()
        {
            New11Game.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Other Handler Methods
        private void OnSave()
        {
            SaveGame.Invoke(this, EventArgs.Empty);
        }

        private void OnPause()
        {
            PauseGame.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Methods
        private string GetColor(int i, int j)
        {
            switch (_model.Table.Tábla[i, j])
            {
                case 0: return _Fieldcolor;
                case 1: return _Minecolor;
                case 2: return _Ecolor;
                case 3: return _Pcolor;
                case 4: return _Ecolor;
                case 5: return _Minecolor;
                case 6: return _Minecolor;
                case 7: return _Minecolor;
                default: return _Fieldcolor;
            }
        }
        public void RefreshTable()
        {
            foreach (Field field in Fields)
            {
                field.Color = GetColor(field.X, field.Y);
            }
            OnPropertyChanged("GameTime");
            OnPropertyChanged("GameStepCount");
        }

        private void StepGame(int v)
        {
            if (_onGame)
            {
                Field field = Fields[v];
                _model.MovePlayer(field.X, field.Y);
                RefreshTable();
                OnPropertyChanged("GameStepCount");
            }
        }
        #endregion
    }
}
