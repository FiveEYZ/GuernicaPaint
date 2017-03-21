using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GuernicaPaint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private enum SelectedShape
        { None, Circle, Rectangle, Line, Free }
        private enum SelectedBorder
        { None, Stroke }

        private bool border;
        private bool _press;
        private bool _isDrawing;

        private Line _line;
        private Polyline _polyLine;

        private Brush _selectedColor;
        private Brush _selectedStrokeColor;

        private Brush SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
                SelectPaintingColor.Background = _selectedColor;
            }
        }

        private Brush SelectedStrokeColor
        {
            get { return _selectedStrokeColor; }
            set
            {
                _selectedStrokeColor = value;
                SelectStrokeColorCanvas.Background = _selectedStrokeColor;
            }
        }
        private Shape _rendershape;

        private Point _currentPoint;

        private int StrokeSize { get; set; }
        private SelectedBorder _border = SelectedBorder.None;
        private SelectedShape _shape = SelectedShape.None;

        private bool BorderBool
        {
            get
            {
                return border;
            }
            set
            {
                border = value;
                _border = border ? SelectedBorder.Stroke : SelectedBorder.None;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region ShapeButtons
        private void btnCircle_Click(object sender, RoutedEventArgs e)
        {
            _isDrawing = false;
            _shape = SelectedShape.Circle;
        }

        private void btnSquare_Click(object sender, RoutedEventArgs e)
        {
            _isDrawing = false;
            _shape = SelectedShape.Rectangle;
        }

        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            _isDrawing = false;
            _shape = SelectedShape.Line;
        }

        private void btnFree_Click(object sender, RoutedEventArgs e)
        {
            _shape = SelectedShape.Free;
            _isDrawing = true;
        }
        #endregion

        private void btnStroke_Click(object sender, RoutedEventArgs e)
        {
            BorderBool = !BorderBool;
        }

        private void StrokeSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            StrokeSize = (int)StrokeSizeSlider.Value;
        }

        #region ColorButtons
        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Brushes.Red;
        }

        private void btnRed_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedStrokeColor = Brushes.Red;
        }

        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Brushes.Blue;
        }

        private void btnBlue_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedStrokeColor = Brushes.Blue;
        }


        private void btnGreen_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Brushes.Green;
        }

        private void btnGreen_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedStrokeColor = Brushes.Green;
        }

        private void btnYellow_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Brushes.Yellow;
        }

        private void btnYellow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedStrokeColor = Brushes.Yellow;
        }

        private void btnOrange_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Brushes.Orange;
        }

        private void btnOrange_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedStrokeColor = Brushes.Orange;
        }

        private void btnWhite_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Brushes.White;
        }

        private void btnWhite_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedStrokeColor = Brushes.White;
        }

        private void btnBlack_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Brushes.Black;
        }

        private void btnBlack_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedStrokeColor = Brushes.Black;
        }
        #endregion

        #region CanvasMouseEvents
        private void canvasArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_shape == SelectedShape.Line)
            {
                if (_press)
                {
                    _line = new Line();

                    _line.X1 = _currentPoint.X;
                    _line.Y1 = _currentPoint.Y;
                    _line.X2 = e.GetPosition(canvasArea).X;
                    _line.Y2 = e.GetPosition(canvasArea).Y;
                    _line.Stroke = SelectedColor;
                    _line.StrokeThickness = StrokeSize;
                    canvasArea.Children.Add(_line);
                }
            }
            else if (_shape == SelectedShape.Free)
            {
                _currentPoint = e.GetPosition(canvasArea);
                _polyLine = new Polyline();
                _polyLine.Stroke = SelectedColor;
                _polyLine.StrokeThickness = StrokeSize;

                canvasArea.Children.Add(_polyLine);
            }
            else
            {
                switch (_shape)
                {
                    case SelectedShape.Circle:
                        if (_border == SelectedBorder.Stroke)
                        {
                            _rendershape = new Ellipse() { Height = 40, Width = 40 };
                            _rendershape.Stroke = SelectedStrokeColor;
                            _rendershape.Fill = SelectedColor;
                            _rendershape.StrokeThickness = StrokeSize;
                            break;
                        }
                        else
                        {
                            _rendershape = new Ellipse() { Height = 40, Width = 40 };
                            _rendershape.Fill = SelectedColor;

                            break;
                        }
                    case SelectedShape.Rectangle:
                        if (border)
                        {
                            _rendershape = new Rectangle() { Height = 45, Width = 45 };
                            _rendershape.Fill = SelectedColor;
                            _rendershape.Stroke = SelectedStrokeColor;
                            _rendershape.StrokeThickness = StrokeSize;
                            break;
                        }
                        else
                        {
                            _rendershape = new Rectangle() { Height = 45, Width = 45 };
                            _rendershape.Fill = SelectedColor;
                            break;
                        }
                    default:
                        return;
                }
                Canvas.SetLeft(_rendershape, e.GetPosition(canvasArea).X);
                Canvas.SetTop(_rendershape, e.GetPosition(canvasArea).Y);

                canvasArea.Children.Add(_rendershape);

            }
        }

        private void canvasArea_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition((Canvas)sender);
            HitTestResult result = VisualTreeHelper.HitTest(canvasArea, pt);

            if (result != null)
            {
                canvasArea.Children.Remove(result.VisualHit as Shape);
            }
        }

        private void canvasArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                _currentPoint = e.GetPosition(canvasArea);
        }

        private void canvasArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _isDrawing)
            {
                Point endPoint = e.GetPosition(canvasArea);
                if (_currentPoint != endPoint)
                {
                    _polyLine.Points.Add(endPoint);
                }
            }
        }

        private void canvasArea_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_press)
            {
                _press = false;
                _line = new Line();
            }
            else
            {
                _currentPoint = e.GetPosition(canvasArea);
                _press = true;
            }
        }
        #endregion

    }
}
