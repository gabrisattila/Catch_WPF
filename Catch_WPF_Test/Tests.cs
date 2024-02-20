using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catch_Wpf.Model;
using Catch_Wpf.Persistence;
using System;

namespace Test
{
    [TestClass]
    public class Test
    {
        private DataAccess _dataAccess;
        private GameTable _testTable;
        private GameModel _model;

        #region 11 teszt
        [TestMethod]
        public void Initialize11()
        {
            _dataAccess = new DataAccess();
            int m = 11;
            int[,] t = new int[m, m];
            _testTable = new GameTable(m, t);
            Random r = new Random();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    _testTable.Tábla[i, j] = r.Next(0, 2);
                }
            }
            _testTable.setP(false);
            _testTable.setE1(false);
            _testTable.setE2(false);

            _model = new GameModel(_dataAccess);
        }
        [TestMethod]
        public void LoadTest_11()
        {
            Initialize11();
            _model.Loaddef("D:/Learn/7. félév/EVA/7.félév/BEAD/Catch_Wpf/Catch_Wpf/def11.txt");
            Assert.AreEqual(_testTable.Tábla[_model.PLAYER.Pos[0], _model.PLAYER.Pos[1]],
                            _model.Table.Tábla[_model.PLAYER.Pos[0], _model.PLAYER.Pos[1]]);

            Assert.AreEqual(_testTable.Tábla[_model.ENEMY1.Pos[0], _model.ENEMY1.Pos[1]],
                            _model.Table.Tábla[_model.ENEMY1.Pos[0], _model.ENEMY1.Pos[1]]);

            Assert.AreEqual(_testTable.Tábla[_model.ENEMY2.Pos[0], _model.ENEMY2.Pos[1]],
                            _model.Table.Tábla[_model.ENEMY2.Pos[0], _model.ENEMY2.Pos[1]]);
        }

        [TestMethod]
        public void MovePlayer_And_PlayerDie_GameOver_Test_11()
        {
            Initialize11();
            _model.Loaddef("D:/Learn/7. félév/EVA/7.félév/BEAD/Catch_Wpf/Catch_Wpf/def11.txt");
            _model.MovePlayer(0,5);
            Assert.AreEqual(_model.Table.Tábla[0, 5], 3);
            // ugye felfelé nem tud mozogni, így az eredeti helyén kell, hogy maradjon
            _model.MovePlayer(2,5);
            Assert.AreEqual(_model.Table.Tábla[1, 5], 3);
            _model.MovePlayer(1,6);
            Assert.AreEqual(_model.Table.Tábla[1, 6], 3);
            _model.MovePlayer(1,5);
            Assert.AreEqual(_model.Table.Tábla[1, 5], 3);
            _model.MovePlayer(1,4);
            Assert.AreEqual(_model.Table.Tábla[1, 4], 3);
            _model.MovePlayer(0,4);
            Assert.IsTrue(_model.IsGameOver); // aknára lépett 
        }

        [TestMethod]
        public void EnemiesMove_And_Die_GameOver_Test_11()
        {
            Initialize11();
            _model.Loaddef("D:/Learn/7. félév/EVA/7.félév/BEAD/Catch_Wpf/Catch_Wpf/def11.txt");
            _model.Enemy1Move(1);
            _model.Enemy2Move(1);
            Assert.AreEqual(_model.Table.Tábla[9, 0], 2);
            Assert.AreEqual(_model.Table.Tábla[9, 10], 4);
            _model.Enemy1Move(1);
            Assert.IsFalse(_model.ENEMY1.Alive);
            _model.Enemy2Move(1);
            _model.Enemy2Move(1);
            Assert.IsFalse(_model.ENEMY2.Alive);
            Assert.IsTrue(_model.IsGameOver);
        }
        #endregion

        #region 15 teszt

        [TestMethod]
        public void Initialize15()
        {
            _dataAccess = new DataAccess();
            int m = 15;
            int[,] t = new int[m, m];
            _testTable = new GameTable(m, t);
            Random r = new Random();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    _testTable.Tábla[i, j] = r.Next(0, 2);
                }
            }
            _testTable.setP(false);
            _testTable.setE1(false);
            _testTable.setE2(false);

            _model = new GameModel(_dataAccess);
        }
        [TestMethod]
        public void LoadTest_15()
        {
            Initialize15();
            _model.Loaddef("D:/Learn/7. félév/EVA/7.félév/BEAD/Catch_Wpf/Catch_Wpf/def15.txt");
            Assert.AreEqual(_testTable.Tábla[_model.PLAYER.Pos[0], _model.PLAYER.Pos[1]],
                            _model.Table.Tábla[_model.PLAYER.Pos[0], _model.PLAYER.Pos[1]]);

            Assert.AreEqual(_testTable.Tábla[_model.ENEMY1.Pos[0], _model.ENEMY1.Pos[1]],
                            _model.Table.Tábla[_model.ENEMY1.Pos[0], _model.ENEMY1.Pos[1]]);

            Assert.AreEqual(_testTable.Tábla[_model.ENEMY2.Pos[0], _model.ENEMY2.Pos[1]],
                            _model.Table.Tábla[_model.ENEMY2.Pos[0], _model.ENEMY2.Pos[1]]);
        }

        [TestMethod]
        public void MovePlayer_And_PlayerDie_GameOver_Test_15()
        {
            Initialize15();
            _model.Loaddef("D:/Learn/7. félév/EVA/7.félév/BEAD/Catch_Wpf/Catch_Wpf/def15.txt");
            _model.MovePlayer(1,7);
            Assert.AreEqual(_model.Table.Tábla[1, 7], 3);
            _model.MovePlayer(1,8);
            Assert.IsTrue(_model.IsGameOver); // aknára lépett 
        }

        [TestMethod]
        public void EnemiesMove_And_Die_GameOver_Test_15()
        {
            Initialize15();
            _model.Loaddef("D:/Learn/7. félév/EVA/7.félév/BEAD/Catch_Wpf/Catch_Wpf/def15.txt");
            _model.Enemy1Move(1);
            _model.Enemy2Move(1);
            Assert.AreEqual(_model.Table.Tábla[13, 0], 2);
            Assert.AreEqual(_model.Table.Tábla[13, 14], 4);
            _model.Enemy1Move(1);
            Assert.IsFalse(_model.ENEMY1.Alive);
            _model.Enemy2Move(1);
            Assert.IsFalse(_model.ENEMY2.Alive);
            Assert.IsTrue(_model.IsGameOver);
        }
        #endregion

        #region 21 teszt
        [TestMethod]
        public void Initialize21()
        {
            _dataAccess = new DataAccess();
            int m = 21;
            int[,] t = new int[m, m];
            _testTable = new GameTable(m, t);
            Random r = new Random();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    _testTable.Tábla[i, j] = r.Next(0, 2);
                }
            }
            _testTable.setP(false);
            _testTable.setE1(false);
            _testTable.setE2(false);

            _model = new GameModel(_dataAccess);
        }
        [TestMethod]
        public void LoadTest_21()
        {
            Initialize21();
            _model.Loaddef("D:/Learn/7. félév/EVA/7.félév/BEAD/Catch_Wpf/Catch_Wpf/def21.txt");
            Assert.AreEqual(_testTable.Tábla[_model.PLAYER.Pos[0], _model.PLAYER.Pos[1]],
                            _model.Table.Tábla[_model.PLAYER.Pos[0], _model.PLAYER.Pos[1]]);

            Assert.AreEqual(_testTable.Tábla[_model.ENEMY1.Pos[0], _model.ENEMY1.Pos[1]],
                            _model.Table.Tábla[_model.ENEMY1.Pos[0], _model.ENEMY1.Pos[1]]);

            Assert.AreEqual(_testTable.Tábla[_model.ENEMY2.Pos[0], _model.ENEMY2.Pos[1]],
                            _model.Table.Tábla[_model.ENEMY2.Pos[0], _model.ENEMY2.Pos[1]]);
        }

        [TestMethod]
        public void MovePlayer_And_PlayerDie_GameOver_Test_21()
        {
            Initialize11();
            _model.Loaddef("D:/Learn/7. félév/EVA/7.félév/BEAD/Catch_Wpf/Catch_Wpf/def21.txt");
            _model.MovePlayer(1,10);
            Assert.IsTrue(_model.IsGameOver); // aknára lépett 
        }

        [TestMethod]
        public void EnemiesMove_And_Die_GameOver_Test_21()
        {
            Initialize15();
            _model.Loaddef("D:/Learn/7. félév/EVA/7.félév/BEAD/Catch_Wpf/Catch_Wpf/def21.txt");
            _model.Enemy1Move(1);
            _model.Enemy2Move(1);
            Assert.AreEqual(_model.Table.Tábla[19, 0], 2);
            Assert.AreEqual(_model.Table.Tábla[19, 20], 4);
            _model.Enemy1Move(1);
            Assert.IsFalse(_model.ENEMY1.Alive);
            _model.Enemy2Move(1);
            Assert.IsFalse(_model.ENEMY2.Alive);
            Assert.IsTrue(_model.IsGameOver);
        }

        #endregion

    }
}