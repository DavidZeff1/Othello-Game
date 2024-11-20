using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Image = System.Drawing.Image;
using Ex05.Logic;

namespace Ex05.UI
{
    public partial class GameBoardScreen : Form
    {
        private int m_Turn = 0;
        private readonly GameBoard r_GameBoard;
        private readonly int r_BoardSize;
        private readonly PictureBoxWithCoordinates[,] r_ButtonsMatrix;
        private int m_Player1TotalScore = 0;
        private int m_Player2TotalScore = 0;
        private readonly string r_Player1Name = "Yellow Coin";
        private readonly string r_Player2Name = "Red Coin";
        private readonly KeyValuePair<Enums.eCellType, Enums.ePlayerType> r_Player1;
        private readonly KeyValuePair<Enums.eCellType, Enums.ePlayerType> r_Player2;
        private KeyValuePair<Enums.eCellType, Enums.ePlayerType> m_CurrentPlayer;
        private readonly Image r_CoinRedImg = Image.FromFile("CoinRed.png");
        private readonly Image r_CoinYellowImg = Image.FromFile("CoinYellow.png");

        public GameBoardScreen(int i_BoardSize, bool i_IsRobotGame)
        {
            r_BoardSize = i_BoardSize;
            r_GameBoard = new GameBoard(r_BoardSize);
            r_Player1 = new KeyValuePair<Enums.eCellType, Enums.ePlayerType>(Enums.eCellType.Oh, Enums.ePlayerType.Human);

            if (i_IsRobotGame)
            {
                r_Player2 = new KeyValuePair<Enums.eCellType, Enums.ePlayerType>(Enums.eCellType.Ex, Enums.ePlayerType.Computer);
            }
            else
            {
                r_Player2 = new KeyValuePair<Enums.eCellType, Enums.ePlayerType>(Enums.eCellType.Ex, Enums.ePlayerType.Human);
            }
            
            m_CurrentPlayer = r_Player1;
            r_ButtonsMatrix = new PictureBoxWithCoordinates[r_BoardSize, r_BoardSize];
            InitializeComponent();
            initializeButtonMatrix();
            startGame();
        }

        private void startGame()
        {
            GameLogic.InitializeMiddle(r_BoardSize, r_GameBoard);
            updatePictureBoxBoard();
            MoveHandler();
        }

        private void highlightPossibleMoves(List<KeyValuePair<int, int>?> i_ValidMoves)
        {
            Color colorToHighlight = (m_CurrentPlayer.Key == Enums.eCellType.Oh) ? Color.Yellow : Color.Red;

            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    r_ButtonsMatrix[i, j].BackColor = Color.White;
                    r_ButtonsMatrix[i, j].Enabled = false;
                }
            }

            foreach (KeyValuePair<int, int>? possibleMove in i_ValidMoves)
            {
                if (possibleMove.HasValue)
                {
                    r_ButtonsMatrix[possibleMove.Value.Key, possibleMove.Value.Value].BackColor = colorToHighlight;
                    r_ButtonsMatrix[possibleMove.Value.Key, possibleMove.Value.Value].Enabled = true;
                }
            }
        }
    
        private void updatePictureBoxBoard()
        {
            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    switch (r_GameBoard.r_PhysicalBoard[i, j])
                    {
                        case Enums.eCellType.Ex:
                            r_ButtonsMatrix[i, j].Image = r_CoinRedImg; 
                            break;

                        case Enums.eCellType.Oh:
                            r_ButtonsMatrix[i, j].Image = r_CoinYellowImg; 
                            break;

                        case Enums.eCellType.Empty:
                            r_ButtonsMatrix[i, j].Image = null;
                            break;
                    }
                }
            }
        }

        private void initializeButtonMatrix()
        {
            int buttonSize = 50;
            int buttonMarginSize = 10;
            int marginSizeBetweenBodyAndBox = 5;
            this.ClientSize = new Size(buttonSize * r_BoardSize, buttonSize * r_BoardSize);
            int currentRowPlacement = 10;
            int currentCollumnPlacement = 10;

            for (int i = 0; i < r_BoardSize; i++)
            {
                currentRowPlacement = (i * buttonSize) + marginSizeBetweenBodyAndBox;

                for (int j = 0; j < r_BoardSize; j++)
                {
                    currentCollumnPlacement = (j * buttonSize) + marginSizeBetweenBodyAndBox;
                    r_ButtonsMatrix[i, j] = new PictureBoxWithCoordinates();
                    r_ButtonsMatrix[i, j].Size = new Size(buttonSize - buttonMarginSize, buttonSize - buttonMarginSize);
                    r_ButtonsMatrix[i, j].Left = currentRowPlacement;
                    r_ButtonsMatrix[i, j].Top = currentCollumnPlacement;
                    r_ButtonsMatrix[i, j].BorderStyle = BorderStyle.FixedSingle;
                    r_ButtonsMatrix[i, j].BackColor = Color.White;
                    r_ButtonsMatrix[i, j].Click += button_Click;
                    r_ButtonsMatrix[i, j].Enabled = false;
                    r_ButtonsMatrix[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    r_ButtonsMatrix[i, j].m_Coordinates = new KeyValuePair<int, int>(i, j);
                    this.Controls.Add(r_ButtonsMatrix[i, j]);
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.Text = (++m_Turn % 2 == 0) ? string.Format($"Othello - {r_Player1Name}'s m_Turn") : string.Format($"Othello - {r_Player2Name}'s m_Turn");
            PictureBoxWithCoordinates currentBox = sender as PictureBoxWithCoordinates;
            KeyValuePair<int, int>? chosenCoordinates = currentBox.m_Coordinates;
            GameLogic.DoTurn(chosenCoordinates, m_CurrentPlayer.Key, r_GameBoard.r_PhysicalBoard);
            updatePictureBoxBoard();
            changeCurrentPlayer(m_CurrentPlayer);
            MoveHandler();
        }

        private void MoveHandler()
        {
            List<KeyValuePair<int, int>?> validMovesForCurrentPlayer = GameLogic.CheckAllPossibleMoves(m_CurrentPlayer.Key, r_GameBoard.r_PhysicalBoard);

            if (validMovesForCurrentPlayer.Count > 0)
            {
                if (m_CurrentPlayer.Value == Enums.ePlayerType.Computer)
                {
                    makeRobotMove(validMovesForCurrentPlayer);
                }
                else
                {
                    highlightPossibleMoves(validMovesForCurrentPlayer);
                }
            }
            else
            {
                List<KeyValuePair<int, int>?> validMovesForNextPlayer = GameLogic.CheckAllPossibleMoves((m_CurrentPlayer.Equals(r_Player1) ? r_Player2.Key : r_Player1.Key), r_GameBoard.r_PhysicalBoard);

                if (validMovesForNextPlayer.Count == 0)
                {
                    endGame();
                }
                else
                {
                    MessageBox.Show("No Valid Moves for Current Player.\nSkipping m_Turn.");
                    m_Turn++;
                    changeCurrentPlayer(m_CurrentPlayer);
                    MoveHandler();
                }
            }
        }

        private void makeRobotMove(List<KeyValuePair<int, int>?> i_ValidMoves)
        {
            m_Turn++;
            KeyValuePair<int, int>? robotChosenMove = i_ValidMoves[new Random().Next(i_ValidMoves.Count)];

            if (robotChosenMove != null)
            {
                GameLogic.DoTurn(robotChosenMove, m_CurrentPlayer.Key, r_GameBoard.r_PhysicalBoard);
                updatePictureBoxBoard();
                changeCurrentPlayer(m_CurrentPlayer);
                MoveHandler();
            }
        }

        private void endGame()
        {
            int player1Score = GameLogic.GetAmountTypeOnBoard(r_GameBoard.r_PhysicalBoard, r_Player1.Key);
            int player2Score = GameLogic.GetAmountTypeOnBoard(r_GameBoard.r_PhysicalBoard, r_Player2.Key);
            string winnerMessage;

            if (player1Score > player2Score)
            {
                winnerMessage = $"{r_Player1Name} Won!!";
                m_Player1TotalScore++;
            }
            else if (player2Score > player1Score)
            {
                winnerMessage = $"{r_Player2Name} Won!!";
                m_Player2TotalScore++;
            }
            else
            {
                m_Player1TotalScore++;
                m_Player2TotalScore++;
                winnerMessage = "Tie!!";
            }

            string endGameMessage = $"{winnerMessage} ({player1Score}/{player2Score}) ({m_Player1TotalScore}/{m_Player2TotalScore})\nWould you like another round?";
            EndGameScreen endGameScreen = new EndGameScreen(endGameMessage);
            endGameScreen.ShowDialog();
            
            if (endGameScreen.m_PlayAgain)
            {
                resetGame();
                startGame();
            }
            else
            {
                this.Hide();
                this.Close();
            }
            
        }

        private void changeCurrentPlayer(KeyValuePair<Enums.eCellType, Enums.ePlayerType> i_Player)
        {
            if (i_Player.Equals(r_Player1))
            {
                m_CurrentPlayer = r_Player2;
            }
            else
            {
                m_CurrentPlayer = r_Player1;
            }
        }

        private void resetGame()
        {
            GameLogic.ResetBoard(r_BoardSize, r_GameBoard);
            updatePictureBoxBoard();
        }

        private class PictureBoxWithCoordinates : PictureBox
        {
            public KeyValuePair<int,int> m_Coordinates;
        }

        private void GameBoardScreen_Load(object sender, EventArgs e)
        {

        }
    }
}



