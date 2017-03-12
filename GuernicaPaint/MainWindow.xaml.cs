using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GuernicaPaint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum SelectedShape
        { None, Circle, Rectangle, Line, Free }
        private enum SelectedBorder
        { None, Stroke }

        private bool border;
        private bool Press;
        private bool isDrawing;

        private Line _line;
        private Polyline _polyLine;

        private Brush SelectedColor { get; set; }
        private Brush SelectedStrokeColor { get; set; }
        private Shape Rendershape = null;

        private Point currentPoint;
        private Point _startPoint;

        private int StrokeSize { get; set; }
        private SelectedBorder _border = SelectedBorder.None;
        private SelectedShape _shape = SelectedShape.None;

        private bool borderBool
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
        }

        #region ShapeButtons
        private void btnCircle_Click(object sender, RoutedEventArgs e)
        {
            isDrawing = false;
            _shape = SelectedShape.Circle;
        }

        private void btnSquare_Click(object sender, RoutedEventArgs e)
        {
            isDrawing = false;
            _shape = SelectedShape.Rectangle;
        }

        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            isDrawing = false;
            _shape = SelectedShape.Line;
        }

        private void btnFree_Click(object sender, RoutedEventArgs e)
        {
            _shape = SelectedShape.Free;
            isDrawing = true;
        }
        #endregion

        private void btnStroke_Click(object sender, RoutedEventArgs e)
        {
            borderBool = !borderBool;
        }

        private void StrokeSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            StrokeSize = (int)StrokeSizeSlider.Value;
        }

        #region ColorButtons
        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Brushes.Red;
            SelectPaintingColor.Background = Brushes.Red;
        }

        private void btnRed_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedStrokeColor = Brushes.Red;
            SelectStrokeColorCanvas.Background = Brushes.Red;
        }

        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Brushes.Blue;
            SelectPaintingColor.Background = Brushes.Blue;
        }

        private void btnBlue_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedStrokeColor = Brushes.Blue;
            SelectStrokeColorCanvas.Background = Brushes.Blue;
        }


        private void btnGreen_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Brushes.Green;
            SelectPaintingColor.Background = Brushes.Green;
        }

        private void btnGreen_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedStrokeColor = Brushes.Green;
            SelectStrokeColorCanvas.Background = Brushes.Green;
        }

        private void btnYellow_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Brushes.Yellow;
            SelectPaintingColor.Background = Brushes.Yellow;
        }

        private void btnYellow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedStrokeColor = Brushes.Yellow;
            SelectStrokeColorCanvas.Background = Brushes.Yellow;
        }

        private void btnWhite_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Brushes.White;
            SelectPaintingColor.Background = Brushes.White;
        }

        private void btnWhite_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedStrokeColor = Brushes.White;
            SelectStrokeColorCanvas.Background = Brushes.White;
        }

        private void btnBlack_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Brushes.Black;
            SelectPaintingColor.Background = Brushes.Black;
        }

        private void btnBlack_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedStrokeColor = Brushes.Black;
            SelectStrokeColorCanvas.Background = Brushes.Black;
        }

        private void btnOrange_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Brushes.Orange;
            SelectPaintingColor.Background = Brushes.Orange;
        }

        private void btnOrange_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedStrokeColor = Brushes.Orange;
            SelectStrokeColorCanvas.Background = Brushes.Orange;
        }
        #endregion

        #region CanvasMouseEvents
        private void canvasArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_shape == SelectedShape.Line)
            {
                if (Press)
                {
                    _line = new Line();

                    _line.X1 = currentPoint.X;
                    _line.Y1 = currentPoint.Y;
                    _line.X2 = e.GetPosition(canvasArea).X;
                    _line.Y2 = e.GetPosition(canvasArea).Y;
                    _line.Stroke = SelectedColor;
                    _line.StrokeThickness = StrokeSize;
                    canvasArea.Children.Add(_line);
                }
            }
            else if (_shape == SelectedShape.Free)
            {
                _startPoint = e.GetPosition(canvasArea);
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
                            Rendershape = new Ellipse() { Height = 40, Width = 40 };
                            Rendershape.Stroke = SelectedStrokeColor;
                            Rendershape.Fill = SelectedColor;
                            Rendershape.StrokeThickness = StrokeSize;
                            break;
                        }
                        else
                        {
                            Rendershape = new Ellipse() { Height = 40, Width = 40 };
                            Rendershape.Fill = SelectedColor;

                            break;
                        }
                    case SelectedShape.Rectangle:
                        if (border)
                        {
                            Rendershape = new Rectangle() { Height = 45, Width = 45 };
                            Rendershape.Fill = SelectedColor;
                            Rendershape.Stroke = SelectedStrokeColor;
                            Rendershape.StrokeThickness = StrokeSize;
                            break;
                        }
                        else
                        {
                            Rendershape = new Rectangle() { Height = 45, Width = 45 };
                            Rendershape.Fill = SelectedColor;
                            break;
                        }
                    default:
                        return;
                }
                Canvas.SetLeft(Rendershape, e.GetPosition(canvasArea).X);
                Canvas.SetTop(Rendershape, e.GetPosition(canvasArea).Y);

                canvasArea.Children.Add(Rendershape);

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
                currentPoint = e.GetPosition(canvasArea);
        }

        private void canvasArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && isDrawing)
            {
                Point currentPoint = e.GetPosition(canvasArea);
                if (_startPoint != currentPoint)
                {
                    _polyLine.Points.Add(currentPoint);
                }
            }
        }

        private void canvasArea_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Press)
            {
                Press = false;
                _line = new Line();
            }
            else
            {
                currentPoint = e.GetPosition(canvasArea);
                Press = true;
            }
        }
        #endregion
    }
}
