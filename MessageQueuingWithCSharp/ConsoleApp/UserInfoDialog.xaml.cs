using System.Windows;

namespace ConsoleApp
{
    /// <summary>
    /// Interaction logic for UserInfoDialog.xaml
    /// </summary>
    public partial class UserInfoDialog : Window
    {
        public UserInfoDialog()
        {
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (!string.IsNullOrEmpty(UserName.Text))
                {
                    User.Name = UserName.Text;
                    this.Close();
                }
            }
        }
    }

    public class User
    {
        public static string Name { get; set; }
    }
}
