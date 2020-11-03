namespace MouseMovement.Window
{
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isMouseMoving = false;

        public MainWindow()
        {
            this.InitializeComponent();
            this.SetWindowOnScreen();
        }

        [DllImport("User32.dll")]
        public static extern void SetCursorPos(int x, int y);
        
        private void SetWindowOnScreen()
        {
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var windowWidth = this.Width;
            this.Top = 0;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
        }

        private void CenterMouse()
        {
            this.isMouseMoving = true;
            var centerWidth = (int)SystemParameters.PrimaryScreenWidth / 2;
            var centerHeight = (int)SystemParameters.PrimaryScreenHeight / 2;
            while (this.isMouseMoving)
            {
                Thread.Sleep(350);
                SetCursorPos(centerWidth + 50, centerHeight + 50);
                Thread.Sleep(350);
                SetCursorPos(centerWidth + 50, centerHeight - 50);
                Thread.Sleep(350);
                SetCursorPos(centerWidth - 50, centerHeight - 50);
                Thread.Sleep(350);
                SetCursorPos(centerWidth - 50, centerHeight + 50);
            }
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.isMouseMoving = false;
                Application.Current.Shutdown();
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
            Task.Run(this.CenterMouse);
        }
    }
}