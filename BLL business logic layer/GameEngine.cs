using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BE_BusinessEntities;
using BL_BusinessLogic;
using DA_DataAccesLayer;
using DAL_DataAccesLayer;

namespace BL_business_logic_layer
{
    public class GameEngine
    {
        private readonly Board board = new Board();
        public BE_GameColor actualPlayer { get; private set; }
        private List<BE_MoveResult> validActualMovs;
        private BE_Position? selectedSquare;
        private List<BE_User> _players = null;
        private IDA_Game DB_Acces = new DA_Game();
        private BE_GameRecord record;
        private Stopwatch stopwatch = new Stopwatch();
        private BE_GameState ActualGameState = BE_GameState.InGame;

        public event DelInitSquare InitSquare;
        public event Action<BE_GameState, BE_GameColor> OnGameStateChanged;
        public event Action<BE_GameColor> OnNextMove;

        public GameEngine() : this(new DA_Game()) { }
        public GameEngine(IDA_Game db) { DB_Acces = db; }
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
            if (ActualGameState == BE_GameState.CheckMate || ActualGameState == BE_GameState.Draw)
            {
                return null;
            }

            selectedSquare = pos;
            validActualMovs = board.GetLegalMoves(pos, actualPlayer);

            List<BE_Position> movs = (from v in validActualMovs select v.Position).ToList();
            return movs;
        }

        public bool MakeMov(BE_Position to)
        {
            // Bloqueo total si el juego ya terminó
            if (ActualGameState == BE_GameState.CheckMate || ActualGameState == BE_GameState.Draw)
            {
                selectedSquare = null;
                validActualMovs = null;
                return false;
            }

            var movElegido = validActualMovs?.FirstOrDefault(m => m.Position.X == to.X && m.Position.Y == to.Y);

            if (movElegido != null)
            {
                if (selectedSquare == null) return false;
                BE_Position SelSqr = selectedSquare.Value;

                record.History.Push(new BE_MovementRecord(record.History.Count + 1, SelSqr.ToNotation(), to.ToNotation(), movElegido.Value.MovType));
                ActualGameState = board.MakeMove(SelSqr, to);

                switch (ActualGameState)
                {
                    case BE_GameState.Check:
                        OnGameStateChanged?.Invoke(ActualGameState, (actualPlayer == BE_GameColor.White) ? BE_GameColor.Black : BE_GameColor.White);
                        nextMove();
                        return true;

                    case BE_GameState.CheckMate:
                        // La partida finalizó: gana el jugador que ejecutó el mate
                        OnGameStateChanged?.Invoke(ActualGameState, (actualPlayer == BE_GameColor.White) ? BE_GameColor.Black : BE_GameColor.White);
                        GameOver(false);
                        return false;

                    case BE_GameState.Draw:
                        // La partida finalizó en empate
                        OnGameStateChanged?.Invoke(ActualGameState, (actualPlayer == BE_GameColor.White) ? BE_GameColor.Black : BE_GameColor.White);
                        GameOver(true);
                        return false;

                    case BE_GameState.InGame:
                        nextMove();
                        return true;
                }
            }


            return false;
        }

        private void nextMove()
        {
            selectedSquare = null;
            validActualMovs = null;
            actualPlayer = (actualPlayer == BE_GameColor.White) ? BE_GameColor.Black : BE_GameColor.White;
            OnNextMove?.Invoke(actualPlayer);
        }

        private void GameOver(bool draw)
        {
            DA_XML_GameRecorder GameRecorder = new DA_XML_GameRecorder();
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

            GameRecorder.SaveGameHistory(record);
        }

        public void ResumeGame(BE_GameRecord existingRecord, List<BE_User> players)
        {
            _players = players;
            record = existingRecord;
            List<BE_MovementRecord> moves = existingRecord.History.Reverse().ToList();

            board.InitializeBoard();

            record.History.Clear();


            foreach (BE_MovementRecord mov in moves)
            {
                BE_Position from = BE_Position.FromNotation(mov.FromSquare);
                BE_Position to = BE_Position.FromNotation(mov.ToSquare);

                board.ApplyMoveDirectly(from, to);

                record.History.Push(mov);
            }

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    InitSquare?.Invoke(board.Squares[y, x], y, x);
                }
            }

            actualPlayer = (moves.Count % 2 == 0) ? BE_GameColor.White : BE_GameColor.Black;
            ActualGameState = BE_GameState.InGame;

            stopwatch.Restart();

            OnNextMove?.Invoke(actualPlayer);
                        
        }
        ~GameEngine()
        {
            DA_XML_GameRecorder GameRecorder = new DA_XML_GameRecorder();
            stopwatch.Stop();
            record.Duration = record.Duration + stopwatch.Elapsed;
            DB_Acces.SaveGameState(record);
            GameRecorder.SaveGameHistory(record);            
        }
    }
}