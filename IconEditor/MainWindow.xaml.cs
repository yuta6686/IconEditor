﻿using System;
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
            rect.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs  e)
        {
            Rectangle rect = (Rectangle)sender;

            if(e.LeftButton == MouseButtonState.Pressed)
            {
                rect.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
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
    }
}
