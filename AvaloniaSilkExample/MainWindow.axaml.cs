using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaSilkExample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        
        public void OnButtonClick(object sender, RoutedEventArgs routedEventArgs)
        {
            Console.WriteLine("Click");
        }
    }
}