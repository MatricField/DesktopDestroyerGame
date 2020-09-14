using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopDestroyer
{
    /// <summary>
    /// Interaction logic for PlayfieldPage.xaml
    /// </summary>
    public partial class PlayfieldPage : Page
    {
        public BitmapSource BackgroundImage
        {
            get { return (BitmapSource)GetValue(BackgroundImageProperty); }
            set { SetValue(BackgroundImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundImageProperty =
            DependencyProperty.Register(
                "BackgroundImage",
                typeof(BitmapSource),
                typeof(PlayfieldPage),
                new PropertyMetadata(null));


        public PlayfieldPage()
        {
            InitializeComponent();
            LoadScreen();
        }

        private void LoadScreen()
        {
            using (var bitmap = ImageHelper.CaptureScreen())
            using (var newBitmap = (System.Drawing.Bitmap)ImageHelper.To8bpp(bitmap))
            {
                    BackgroundImage = Imaging.CreateBitmapSourceFromHBitmap(
                        newBitmap.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());    
            }
        }

        private void DrawingBoard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = Mouse.GetPosition(DrawingBoard);
            var shape = new Ellipse()
            {
                Fill = new SolidColorBrush(Colors.Black),
                Width = 50,
                Height = 50,
            };
            Canvas.SetLeft(shape, mousePos.X);
            Canvas.SetTop(shape, mousePos.Y);
            DrawingBoard.Children.Add(shape);
        }
    }
}
