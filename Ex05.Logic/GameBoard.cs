//from Ex02
using System;

namespace Ex05.Logic
{
    public class GameBoard
    {
        private readonly int r_GameBoardDimension;
        public readonly Enums.eCellType[,] r_PhysicalBoard;

        public  GameBoard(int i_GameBoardDimension)
        {
            r_GameBoardDimension = i_GameBoardDimension;
            r_PhysicalBoard = new Enums.eCellType[r_GameBoardDimension, r_GameBoardDimension];

            for (int i = 0; i < r_GameBoardDimension; i++)
            {
                for (int j = 0; j < r_GameBoardDimension; j++)
                {
                    r_PhysicalBoard[i, j] = Enums.eCellType.Empty;
                }
            }
        }

        public void EditCellInGameBoard(int i_XCoordinate, int i_YCoordinate, Enums.eCellType i_Celltype)
        {
            r_PhysicalBoard[i_XCoordinate, i_YCoordinate] = i_Celltype;
        }
    }
}
