using System;

using System.Windows.Forms;

namespace Ex05.UI
{

    public partial class StartScreen : Form
    {
        private int m_CurrentBoardSize = 6;
        private bool m_IsRobotGame;

        public StartScreen()
        {
            InitializeComponent();
        }

        private void boardSizeButton_Click(object sender, EventArgs e)
        {
            m_CurrentBoardSize = (m_CurrentBoardSize + 2 == 14) ? 6 : m_CurrentBoardSize + 2;
            BoardSizeButton.Text = $"Board Size {m_CurrentBoardSize} x {m_CurrentBoardSize}";
        }

        private void boardPlayVsFriendButton_Click(object sender, EventArgs e)
        {
            m_IsRobotGame = false;
            startGame();
        }

        private void boardPlayVsComputerButton_Click(object sender, EventArgs e)
        {
            m_IsRobotGame = true;
            startGame();
        }

        private void startGame()
        {
            GameBoardScreen gameBoard = new GameBoardScreen(m_CurrentBoardSize, m_IsRobotGame);
            gameBoard.Show();
            Hide();
        }
    }
}
