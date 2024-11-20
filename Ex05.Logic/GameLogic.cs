
using System.Collections.Generic;
//logic from Ex02
namespace Ex05.Logic
{
    public static class GameLogic
    {
        public static void InitializeMiddle(int i_GameBoardDimension, GameBoard i_GameBoard)
        {
            int middleOfBoard = i_GameBoardDimension / 2;

            i_GameBoard.EditCellInGameBoard(middleOfBoard - 1, middleOfBoard - 1, Enums.eCellType.Oh);
            i_GameBoard.EditCellInGameBoard(middleOfBoard - 1, middleOfBoard, Enums.eCellType.Ex);
            i_GameBoard.EditCellInGameBoard(middleOfBoard, middleOfBoard - 1, Enums.eCellType.Ex);
            i_GameBoard.EditCellInGameBoard(middleOfBoard, middleOfBoard, Enums.eCellType.Oh);
        }

        public static void ResetBoard(int i_GameBoardDimension, GameBoard i_GameBoard)
        {
            for (int i = 0; i < i_GameBoardDimension; i++)
            {
                for(int j = 0; j < i_GameBoardDimension; j++)
                {
                    i_GameBoard.EditCellInGameBoard(i, j, Enums.eCellType.Empty);
                }
            }
        }

        public static List<KeyValuePair<int, int>?> CheckAllPossibleMoves(Enums.eCellType i_CellType, Enums.eCellType[,] i_GameBoard)
        {
            List<KeyValuePair<int, int>?> m_PossibleMoves = new List<KeyValuePair<int, int>?>();

            for (int i = 0; i < i_GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < i_GameBoard.GetLength(0); j++)
                {
                    KeyValuePair<int, int>? possibleMove = checkIfThereAreAnyValidMoves(i, j, i_CellType, i_GameBoard);
                    if (i_GameBoard[i, j] == Enums.eCellType.Empty && possibleMove != null)
                    {
                        m_PossibleMoves.Add(possibleMove);
                    }

                }
            }

            return m_PossibleMoves;
        }

        private static KeyValuePair<int, int>? checkIfThereAreAnyValidMoves(int i_CoordinateX, int i_CoordinateY,
            Enums.eCellType i_CellType, Enums.eCellType[,] i_GameBoard)
        {
            KeyValuePair<int, int>? validMove = null;
            Enums.eCellType eOppositeCellType = (i_CellType == Enums.eCellType.Ex) ? Enums.eCellType.Oh : Enums.eCellType.Ex;

            bool piecesFlipped1 = checkIfThereAreAnyValidMovesHelper(i_CoordinateX + 1, i_CoordinateY, 1, 0, i_CellType, eOppositeCellType, i_GameBoard);
            bool piecesFlipped2 = checkIfThereAreAnyValidMovesHelper(i_CoordinateX - 1, i_CoordinateY, -1, 0, i_CellType, eOppositeCellType, i_GameBoard);
            bool piecesFlipped3 = checkIfThereAreAnyValidMovesHelper(i_CoordinateX, i_CoordinateY + 1, 0, 1, i_CellType, eOppositeCellType, i_GameBoard);
            bool piecesFlipped4 = checkIfThereAreAnyValidMovesHelper(i_CoordinateX, i_CoordinateY - 1, 0, -1, i_CellType, eOppositeCellType, i_GameBoard);
            bool piecesFlipped5 = checkIfThereAreAnyValidMovesHelper(i_CoordinateX + 1, i_CoordinateY + 1, 1, 1, i_CellType, eOppositeCellType, i_GameBoard);
            bool piecesFlipped6 = checkIfThereAreAnyValidMovesHelper(i_CoordinateX - 1, i_CoordinateY - 1, -1, -1, i_CellType, eOppositeCellType, i_GameBoard);
            bool piecesFlipped7 = checkIfThereAreAnyValidMovesHelper(i_CoordinateX - 1, i_CoordinateY + 1, -1, 1, i_CellType, eOppositeCellType, i_GameBoard);
            bool piecesFlipped8 = checkIfThereAreAnyValidMovesHelper(i_CoordinateX + 1, i_CoordinateY - 1, 1, -1, i_CellType, eOppositeCellType, i_GameBoard);

            if (piecesFlipped1 || piecesFlipped2 || piecesFlipped3 || piecesFlipped4 || piecesFlipped5 || piecesFlipped6 || piecesFlipped7 || piecesFlipped8)
            {
                validMove = new KeyValuePair<int, int>(i_CoordinateX, i_CoordinateY);
            }

            return validMove;

        }

        private static bool checkIfThereAreAnyValidMovesHelper(int i_X, int i_Y, int i_DirectionX,
            int i_directionY, Enums.eCellType i_CellType, Enums.eCellType i_OppositeCellType, Enums.eCellType[,] i_GameBoard)
        {
            bool hasFlippedCoins = false;
            int steps = 0;
            int boardSize = i_GameBoard.GetLength(0);

            while (i_X >= 0 && i_X < boardSize && i_Y >= 0 && i_Y < boardSize && i_GameBoard[i_X, i_Y] == i_OppositeCellType)
            {
                steps++;
                i_X += i_DirectionX;
                i_Y += i_directionY;
            }

            if (i_X >= 0 && i_X < boardSize && i_Y >= 0 && i_Y < boardSize && i_GameBoard[i_X, i_Y] == i_CellType && steps > 0)
            {
                i_X -= i_DirectionX;
                i_Y -= i_directionY;

                while (steps > 0)
                {
                    i_X -= i_DirectionX;
                    i_Y -= i_directionY;
                    steps--;
                }

                hasFlippedCoins = true;

            }

            return hasFlippedCoins;
        }

        public static void DoTurn(KeyValuePair<int, int>? i_Coordinates, Enums.eCellType i_CellType, Enums.eCellType[,] i_GameBoard)
        {
            //we are checkig which cell needs to be flipped which is the opposite cell
            Enums.eCellType eOppositeCellType = (i_CellType == Enums.eCellType.Ex) ? Enums.eCellType.Oh : Enums.eCellType.Ex;

            int i_x = i_Coordinates.Value.Key;
            int i_y = i_Coordinates.Value.Value;

            //check all from the current spot that the user chose(up, down,up left and so on...)
            bool piecesFlipped1 = doTurnHelper(i_x + 1, i_y, 1, 0, i_CellType, eOppositeCellType, i_GameBoard);
            bool piecesFlipped2 = doTurnHelper(i_x - 1, i_y, -1, 0, i_CellType, eOppositeCellType, i_GameBoard);
            bool piecesFlipped3 = doTurnHelper(i_x, i_y + 1, 0, 1, i_CellType, eOppositeCellType, i_GameBoard);
            bool piecesFlipped4 = doTurnHelper(i_x, i_y - 1, 0, -1, i_CellType, eOppositeCellType, i_GameBoard);
            bool piecesFlipped5 = doTurnHelper(i_x + 1, i_y + 1, 1, 1, i_CellType, eOppositeCellType, i_GameBoard);
            bool piecesFlipped6 = doTurnHelper(i_x - 1, i_y - 1, -1, -1, i_CellType, eOppositeCellType, i_GameBoard);
            bool piecesFlipped7 = doTurnHelper(i_x - 1, i_y + 1, -1, 1, i_CellType, eOppositeCellType, i_GameBoard);
            bool piecesFlipped8 = doTurnHelper(i_x + 1, i_y - 1, 1, -1, i_CellType, eOppositeCellType, i_GameBoard);

            if (piecesFlipped1 || piecesFlipped2 || piecesFlipped3 || piecesFlipped4 || piecesFlipped5 || piecesFlipped6 || piecesFlipped7 || piecesFlipped8)
            {
                //if we flipped any coins in any of the directions then flip the starting point aswell
                i_GameBoard[i_x, i_y] = i_CellType; ;
            }

        }

        private static bool doTurnHelper(int i_X, int i_Y, int i_DirectionX, int i_DirectionY,
            Enums.eCellType i_CellType, Enums.eCellType i_OppositeCellType, Enums.eCellType[,] i_GameBoard)
        {
            bool hasFlippedCoins = false;
            int steps = 0;
            int boardSize = i_GameBoard.GetLength(0);
            //count the amount of coins that we can flip while checking for bounds 
            while (i_X >= 0 && i_X < boardSize && i_Y >= 0 && i_Y < boardSize && i_GameBoard[i_X, i_Y] == i_OppositeCellType)
            {
                steps++;
                i_X += i_DirectionX;
                i_Y += i_DirectionY;
            }
            //if not out of bounds and if we counted any coins that need to be flipped then go backwards and flip the coins while backtracking
            if (i_X >= 0 && i_X < boardSize && i_Y >= 0 && i_Y < boardSize && i_GameBoard[i_X, i_Y] == i_CellType && steps > 0)
            {
                i_X -= i_DirectionX;
                i_Y -= i_DirectionY;

                while (steps > 0)
                {
                    i_GameBoard[i_X, i_Y] = i_CellType;
                    i_X -= i_DirectionX;
                    i_Y -= i_DirectionY;
                    steps--;
                }
                hasFlippedCoins = true;

            }

            return hasFlippedCoins;
        }

        public static int GetAmountTypeOnBoard(Enums.eCellType[,] i_GameBoard, Enums.eCellType i_CellType)
        {
            int ExOccurences = 0;

            for (int i = 0; i < i_GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < i_GameBoard.GetLength(0); j++)
                {
                    if (i_GameBoard[i, j] == i_CellType)
                    {
                        ExOccurences++;
                    }

                }
            }

            return ExOccurences;
        }
        

    }
}
