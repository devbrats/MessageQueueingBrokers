using Common.Contracts;
using System.Windows;

namespace ConsoleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM _dataContext;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            _dataContext = new MainWindowVM();
            DataContext = _dataContext;
          
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var userDialog = new UserInfoDialog();
            userDialog.Owner = this;
            userDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            userDialog.ShowDialog();
            if (!string.IsNullOrEmpty(User.Name))
            {
                SendMessagesTextBox.Focus();
                this.Title = User.Name;
            }
        }

        private void Send_Message(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void Button_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            if (SendMessagesTextBox.Text != "")
            {
                _dataContext.MessageHandler.Invoke(this, new SendMessageArgs()
                {
                    Message = new Message()
                    {
                        Sender = User.Name,
                        Value = SendMessagesTextBox.Text
                    }
                });
                SendMessagesTextBox.Clear();
            }
        }

        private void ReceiveMessagesGrid_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            if (ReceiveMessagesGrid.Items.Count > 1)
            {
                ReceiveMessagesGrid.ScrollIntoView(ReceiveMessagesGrid.Items[ReceiveMessagesGrid.Items.Count - 1]);
            }
        }
    }
}
