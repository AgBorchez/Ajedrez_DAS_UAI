using System;
using System.Collections.Generic;
using Ajedrez_DAS.BE;

namespace Ajedrez_DAS.BLL
{
    public class Game
    {
        private Board board = new Board();
        private GameColor actualPlayer;
        private List<Position> validActualMovs;
        private Square selectedSquare;

        public event DelInitSquare InitSquare;
        public event DelUpdateImage UpdateImage;
        public GameState ActualGameState { get; private set; } = GameState.InGame;
        public event Action<GameState, GameColor> OnGameOver;
        public event Action OnCheck;

        // Propiedad que indica si el juego ya concluyó definitivamente
        public bool isGameOver => ActualGameState == GameState.CheckMate || ActualGameState == GameState.Tablas;

        public GameColor Color { get { return actualPlayer; } }
        public Board Board { get { return board; } }

        public void InitGame()
        {
            board.InitializeBoard();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    InitSquare?.Invoke(board.Squares[y, x], y, x);
                }
            }

            actualPlayer = GameColor.White;
            ActualGameState = GameState.InGame;
            selectedSquare = null;
            validActualMovs = null;
        }

        public List<Position> SelectSquare(Square c)
        {
            // Bloqueo total si el juego ya terminó: no se devuelven movimientos ni se selecciona nada
            if (isGameOver)
            {
                return null;
            }

            if (c.SquarePiece != null && c.SquarePiece.color == actualPlayer)
            {
                selectedSquare = c;
                validActualMovs = board.GetLegalMoves(c);
                return validActualMovs;
            }

            return null;
        }

        public void MakeMov(Square to)
        {
            // Bloqueo total si el juego ya terminó
            if (isGameOver)
            {
                selectedSquare = null;
                validActualMovs = null;
                return;
            }

            if (validActualMovs != null && validActualMovs.Contains(new Position(to.y, to.x)))
            {
                ActualGameState = board.MakeMove(selectedSquare, to);
                UpdateImage?.Invoke();

                switch (ActualGameState)
                {
                    case GameState.Check:
                        OnCheck?.Invoke();
                        nextMove();
                        break;

                    case GameState.CheckMate:
                        // La partida finalizó: gana el jugador que ejecutó el mate
                        OnGameOver?.Invoke(GameState.CheckMate, actualPlayer);
                        break;

                    case GameState.Tablas:
                        // La partida finalizó en empate
                        OnGameOver?.Invoke(GameState.Tablas, actualPlayer);
                        break;

                    case GameState.InGame:
                        nextMove();
                        break;
                }

                // Limpiar selección actual tras mover o finalizar
                selectedSquare = null;
                validActualMovs = null;
            }
        }

        private void nextMove()
        {
            actualPlayer = (actualPlayer == GameColor.White) ? GameColor.Black : GameColor.White;
        }
    }
}