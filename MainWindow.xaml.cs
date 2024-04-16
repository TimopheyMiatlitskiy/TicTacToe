using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private bool player = true; // Переменная для хранения текущего игрока (true - X, false - O)

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.Content == null)
            {
                if (player)
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
                    MessageBox.Show((player ? "X" : "O") + " wins!");
                    ResetBoard();
                }

                // Проверка на ничью
                if (CheckForDraw())
                {
                    MessageBox.Show("It's a draw!");
                    ResetBoard();
                }

                player = !player;
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
                if (control is Button button && button.Tag == null)
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
                    button.Tag = null;
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
            line1.X1 = 10;
            line1.Y1 = 10;
            line1.X2 = 10;
            line1.Y2 = 10;
            grid.Children.Add(line1);

            Line line2 = new Line();
            line2.Stroke = Brushes.Red;
            line2.StrokeThickness = 5;
            line2.X1 = button.ActualWidth-10;
            line2.Y1 = 10;
            line2.X2 = button.ActualWidth-10;
            line2.Y2 = 10;
            grid.Children.Add(line2);

            button.Content = grid;

            // Анимация рисования крестика
            AnimateLines(line1, 10, button.ActualWidth-10, Line.X2Property);
            AnimateLines(line1, 10, button.ActualHeight-10, Line.Y2Property);
            await Task.Delay(1000); // Задержка для завершения анимации
            AnimateLines(line2, button.ActualWidth - 10, 10, Line.X1Property);
            AnimateLines(line2, 10, button.ActualHeight-10, Line.Y1Property);
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
            ellipse.StrokeThickness = 5;
            ellipse.Width = button.ActualWidth;
            ellipse.Height = button.ActualHeight;
            ellipse.Margin = new Thickness(5);

            button.Content = ellipse;

            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.From = 0;
            widthAnimation.To = button.ActualWidth - 20;
            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));

            DoubleAnimation heightAnimation = new DoubleAnimation();
            heightAnimation.From = 0;
            heightAnimation.To = button.ActualHeight - 15;
            heightAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));

            ellipse.BeginAnimation(Ellipse.WidthProperty, widthAnimation);
            ellipse.BeginAnimation(Ellipse.HeightProperty, heightAnimation);

            //DoubleAnimation rotationAnimation = new DoubleAnimation();
            //rotationAnimation.From = 0;
            //rotationAnimation.To = 360;
            //rotationAnimation.RepeatBehavior = RepeatBehavior.Forever;
            //rotationAnimation.Duration = new Duration(TimeSpan.FromSeconds(2));

            //RotateTransform transform = new RotateTransform();
            //ellipse.RenderTransform = transform;

            //transform.BeginAnimation(RotateTransform.AngleProperty, rotationAnimation);
        }

    }
}
