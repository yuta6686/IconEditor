using System;
using System.Collections.Generic;
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



namespace IconEditor
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Create grid-like picture on h:32 * w:32
        private void MainCanvas2_Initialized(object sender, EventArgs e)
        {
            Canvas canvas = (Canvas)sender;

            for(int y = 0; y < 32; y++)
            {
                for(int x = 0; x < 32; x++)
                {
                    Rectangle rect;
                    rect = new Rectangle();
                    rect.Fill = new SolidColorBrush(Colors.White);
                    rect.Width = 19;
                    rect.Height = 19;
                    rect.MouseDown += Rectangle_MouseDown;
                    rect.MouseMove += Rectangle_MouseMove;


                    Canvas.SetLeft(rect, x * 20);
                    Canvas.SetTop(rect, y * 20);

                    canvas.Children.Add(rect);
                }
            }

            
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            //rect.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
            SolidColorBrush puletteBrush = (SolidColorBrush)ColorPalette.Fill;
            rect.Fill = new SolidColorBrush(puletteBrush.Color);
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs  e)
        {
            Rectangle rect = (Rectangle)sender;

            String text;

            int x = (int)Canvas.GetLeft(rect) / 20;
            int y = (int)Canvas.GetTop(rect) / 20;

            text = "列:" + x.ToString() +"、"+ "行" + y.ToString();

            StatusBarLabel.Content = text;

            if(e.LeftButton == MouseButtonState.Pressed)
            {
                //rect.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
                SolidColorBrush puletteBrush = (SolidColorBrush)ColorPalette.Fill;
                rect.Fill = new SolidColorBrush(puletteBrush.Color);
            }
            
        }

        
        private void MenuItem_Finalize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_New_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Version_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("☆スター・バースト・アイコン・エディター☆\nVersion 0.0.1",
                "☆スター・バースト・アイコン・エディター☆　のバージョン情報",
                MessageBoxButton.OK,
                MessageBoxImage.Information,
                MessageBoxResult.Yes);
        }

        private void MenuItem_ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            int index = Slider_Zoom.Ticks.IndexOf(Slider_Zoom.Value);
            index--;

            if (0 > index)
                return;

            Slider_Zoom.Value = Slider_Zoom.Ticks[index];

            //ステータスバーの拡大率を変更
            StatusBarLabel_Scale.Content = "拡大率：" + Slider_Zoom.Value.ToString() + "%";
        }

        private void MenuItem_ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            int index = Slider_Zoom.Ticks.IndexOf(Slider_Zoom.Value);
            index++;

            if (Slider_Zoom.Ticks.Count <= index)
                return;

            Slider_Zoom.Value = Slider_Zoom.Ticks[index];

            //ステータスバーの拡大率を変更
            StatusBarLabel_Scale.Content = "拡大率：" + Slider_Zoom.Value.ToString() + "%";
        }

        private void Slider_Zoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainCanvas2 == null) return;

            //Matrixを使うと拡縮とか使える
            Matrix matrix = new Matrix();

            //*0.01にしているのはパーセントを0.1とかに直している
            matrix.Scale(Slider_Zoom.Value * 0.01, Slider_Zoom.Value * 0.01);
            matrixTransform.Matrix = matrix;

            //%の表示を変える
            ZoomLavel.Content = Slider_Zoom.Value + "%";

            //ステータスバーの拡大率を変更
            StatusBarLabel_Scale.Content = "拡大率：" + Slider_Zoom.Value.ToString()+"%";


            //メインキャンバスの大きさを変える
            MainCanvas2.Width = 640 * Slider_Zoom.Value * 0.01;
            MainCanvas2.Height = 640 * Slider_Zoom.Value * 0.01;
        }




        //  CTRL+マウスホイールで拡縮
        private void MainCanvas2_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if ((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) == KeyStates.Down ||
                (Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) == KeyStates.Down)
            {
                if (e.Delta > 0)
                {
                    MenuItem_ZoomIn_Click(sender, e);
                }
                else
                {
                    MenuItem_ZoomOut_Click(sender, e);
                }
            }
        }

        private void ColorPalette_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //  カラーダイアログを呼び出す
            System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();

            cd.FullOpen = true;

            //  カラーダイアログの色を設定
            SolidColorBrush paletteBrush = (SolidColorBrush)ColorPalette.Fill;
            cd.Color = System.Drawing.Color.FromArgb(paletteBrush.Color.A,
                paletteBrush.Color.R, paletteBrush.Color.G, paletteBrush.Color.B);

            //  ウィンドウが表示され、OKが押されたら
            if(cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //  RectAngleを塗る色として設定
                Color color = Color.FromArgb(cd.Color.A, cd.Color.R, cd.Color.G, cd.Color.B);
                ColorPalette.Fill = new SolidColorBrush(color);
            }

        }
    }
}
