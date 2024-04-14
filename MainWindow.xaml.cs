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
                    //button.Content = "X";
                }
                else
                {
                    DrawO(button);
                    button.Tag = "O";
                    //button.Content = "O";
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
            // Проверка столбцов
            // Проверка диагоналей
            if (CheckLine(btn1.Tag, btn2.Tag, btn3.Tag)
                || CheckLine(btn4.Tag, btn5.Tag, btn6.Tag)
                || CheckLine(btn7.Tag, btn8.Tag, btn9.Tag)
                || CheckLine(btn1.Tag, btn4.Tag, btn7.Tag)
                || CheckLine(btn2.Tag, btn5.Tag, btn8.Tag)
                || CheckLine(btn3.Tag, btn6.Tag, btn9.Tag)
                || CheckLine(btn1.Tag, btn5.Tag, btn9.Tag)
                || CheckLine(btn3.Tag, btn5.Tag, btn7.Tag))
                return true;

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
            grid.Margin = new Thickness(-5);

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
            AnimateLines(line1, 0, button.ActualWidth, Line.X2Property);
            AnimateLines(line1, 0, button.ActualHeight, Line.Y2Property);
            await Task.Delay(1000); // Задержка для завершения анимации
            AnimateLines(line2, button.ActualWidth, 0, Line.X1Property);
            AnimateLines(line2, 0, button.ActualHeight, Line.Y1Property);
        }

        public void AnimateLines(Line line, double corner1, double corner2, DependencyProperty property)
        {
            DoubleAnimation lineAnimate = new DoubleAnimation(corner1, corner2, TimeSpan.FromSeconds(1));
            line.BeginAnimation(property, lineAnimate);
        }

        private void DrawO(Button button)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = Brushes.Blue;
            ellipse.Fill = Brushes.Transparent;
            ellipse.StrokeThickness = 5;
            ellipse.Width = button.ActualWidth - 20;
            ellipse.Height = button.ActualHeight - 20;
            ellipse.Margin = new Thickness(5);

            button.Content = ellipse;
        }
    }
}
