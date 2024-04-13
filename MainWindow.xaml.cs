using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

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
                    DrawX(button);
                    button.Tag = "X";
                }
                else
                {
                    DrawO(button);
                    button.Tag = "O";
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
        private async void DrawX(Button button)
        {
            Grid grid = new Grid();
            grid.Width = button.ActualWidth;
            grid.Height = button.ActualHeight;

            Line line1 = new Line();
            line1.Stroke = Brushes.Red;
            line1.StrokeThickness = 5;
            line1.X1 = 0;
            line1.Y1 = 0;
            line1.X2 = 0;
            line1.Y2 = 0;
            grid.Children.Add(line1);

            Line line2 = new Line();
            line2.Stroke = Brushes.Red;
            line2.StrokeThickness = 5;
            line2.X1 = button.ActualWidth;
            line2.Y1 = 0;
            line2.X2 = button.ActualWidth;
            line2.Y2 = 0;
            grid.Children.Add(line2);

            button.Content = grid;

            // Анимация рисования крестика
            DoubleAnimation line11Animate = new DoubleAnimation(0, button.ActualWidth, TimeSpan.FromSeconds(1));
            line1.BeginAnimation(Line.X2Property, line11Animate);
            DoubleAnimation line12Animate = new DoubleAnimation(0, button.ActualHeight, TimeSpan.FromSeconds(1));
            line1.BeginAnimation(Line.Y2Property, line12Animate);

            DoubleAnimation line21Animate = new DoubleAnimation(button.ActualWidth, 0, TimeSpan.FromSeconds(1));
            line2.BeginAnimation(Line.X1Property, line21Animate);
            DoubleAnimation line22Animate = new DoubleAnimation(0, button.ActualHeight, TimeSpan.FromSeconds(1));
            line2.BeginAnimation(Line.Y1Property, line22Animate);

            await Task.Delay(1000); // Задержка для завершения анимации
        }

        private void DrawO(Button button)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = Brushes.Blue;
            ellipse.Fill = Brushes.White;
            ellipse.Width = button.ActualWidth - 20;
            ellipse.Height = button.ActualHeight - 20;
            ellipse.Margin = new Thickness(5);

            button.Content = ellipse;
        }
    }
}
