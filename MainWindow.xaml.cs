using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private bool playerX = true; // Переменная для хранения текущего игрока (true - X, false - O)

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.Content == null)
            {
                if (playerX)
                {
                    button.Content = "X";
                }
                else
                {
                    button.Content = "O";
                }

                // Проверка на победу
                if (CheckForWin())
                {
                    MessageBox.Show((playerX ? "X" : "O") + " wins!");
                    ResetBoard();
                }

                // Проверка на ничью
                if (CheckForDraw())
                {
                    MessageBox.Show("It's a draw!");
                    ResetBoard();
                }

                playerX = !playerX;
            }
        }

        private bool CheckForWin()
        {
            // Проверка строк
            for (int i = 0; i < 3; i++)
            {
                if (CheckLine(btn1.Content, btn2.Content, btn3.Content)) return true;
                if (CheckLine(btn4.Content, btn5.Content, btn6.Content)) return true;
                if (CheckLine(btn7.Content, btn8.Content, btn9.Content)) return true;
            }

            // Проверка столбцов
            for (int i = 0; i < 3; i++)
            {
                if (CheckLine(btn1.Content, btn4.Content, btn7.Content)) return true;
                if (CheckLine(btn2.Content, btn5.Content, btn8.Content)) return true;
                if (CheckLine(btn3.Content, btn6.Content, btn9.Content)) return true;
            }

            // Проверка диагоналей
            if (CheckLine(btn1.Content, btn5.Content, btn9.Content)) return true;
            if (CheckLine(btn3.Content, btn5.Content, btn7.Content)) return true;

            return false;
        }

        private bool CheckLine(object content1, object content2, object content3)
        {
            return content1 != null && content1 == content2 && content2 == content3;
        }

        private bool CheckForDraw()
        {
            foreach (var control in gameGrid.Children)
            {
                if (control is Button button && button.Content == null)
                {
                    return false;
                }
            }
            return true;
        }

        private void ResetBoard()
        {
            foreach (var control in gameGrid.Children)
            {
                if (control is Button button)
                {
                    button.Content = null;
                }
            }
        }
    }
}
