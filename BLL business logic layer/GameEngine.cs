using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using BE_BusinessEntities;
using DA_DataAccesLayer;

namespace BL_business_logic_layer
{
    public class GameEngine
    {
        private Board board = new Board();
        public BE_GameColor actualPlayer { get; private set; }
        private List<BE_Position> validActualMovs;
        private BE_Position? selectedSquare;
        private List<BE_User> _players = null;
        private DA_Game DB_Acces = new DA_Game();
        private BE_GameRecord record;
        private Stopwatch stopwatch = new Stopwatch();
        public Board Board { get { return board; } }
        public BE_GameState ActualGameState { get; private set; } = BE_GameState.InGame;

        public event DelInitSquare InitSquare;
        public event DelUpdateImage UpdateImage;
        public event Action<BE_GameState, BE_GameColor> OnGameOver;
        public event Action OnCheck;
        public event Action<BE_GameColor> OnNextMove;


        public void AssingPlayers(List<BE_User> players)
        {
            if (_players == null)
            {
                _players = players;
            }
        }
        public void InitGame()
        {
            if (_players != null)
            {
                record = DB_Acces.Newgame(_players);
                board.InitializeBoard();
                stopwatch.Start();

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        InitSquare?.Invoke(board.Squares[y, x], y, x);
                    }
                }

                actualPlayer = BE_GameColor.White;
                ActualGameState = BE_GameState.InGame;
                validActualMovs = null;
            }
        }

        public List<BE_Position> SelectSquare(BE_Position pos)
        {
            // Bloqueo total si el juego ya terminó: no se devuelven movimientos ni se selecciona nada
            if (ActualGameState == BE_GameState.CheckMate || ActualGameState == BE_GameState.Tablas)
            {
                return null;
            }

            selectedSquare = pos;
            validActualMovs = board.GetLegalMoves(pos, actualPlayer);
            return validActualMovs;
        }

        public bool MakeMov(BE_Position to)
        {
            // Bloqueo total si el juego ya terminó
            if (ActualGameState == BE_GameState.CheckMate || ActualGameState == BE_GameState.Tablas)
            {
                selectedSquare = null;
                validActualMovs = null;
                return false;
            }

            if (validActualMovs != null && validActualMovs.Contains(new BE_Position(to.Y, to.X)))
            {
                if (selectedSquare == null) return false;
                BE_Position SelSqr = (BE_Position)selectedSquare;

                ActualGameState = board.MakeMove(SelSqr, to);

                switch (ActualGameState)
                {
                    case BE_GameState.Check:
                        OnCheck?.Invoke();
                        nextMove();
                        return true;

                    case BE_GameState.CheckMate:
                        // La partida finalizó: gana el jugador que ejecutó el mate
                        OnGameOver?.Invoke(BE_GameState.CheckMate, actualPlayer);
                        GameOver(false);
                        return false;

                    case BE_GameState.Tablas:
                        // La partida finalizó en empate
                        OnGameOver?.Invoke(BE_GameState.Tablas, actualPlayer);
                        GameOver(true);
                        return false;

                    case BE_GameState.InGame:
                        nextMove();
                        return true;
                }

                // Limpiar selección actual tras mover o finalizar
                selectedSquare = null;
                validActualMovs = null;

            }


            return false;
        }

        private void nextMove()
        {
            actualPlayer = (actualPlayer == BE_GameColor.White) ? BE_GameColor.Black : BE_GameColor.White;
            OnNextMove?.Invoke(actualPlayer);
        }

        private void GameOver(bool draw)
        {
            stopwatch.Stop();
            record.Duration = record.Duration + stopwatch.Elapsed;
            if (draw)
            {
                DB_Acces.GameOverDraw(record);
            }
            else
            { 
                DB_Acces.GameOverCheck(record, _players.FirstOrDefault(p => p.PlayerColor == actualPlayer).Id);
            }
        }
    }
}