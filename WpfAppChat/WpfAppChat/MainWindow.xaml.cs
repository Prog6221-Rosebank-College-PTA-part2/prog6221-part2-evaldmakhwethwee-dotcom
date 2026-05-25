using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            lstChat.Items.Add("Bot: Hello, I am a cybersecurity chatbot. ");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         string message =txtMessage.Text.ToLower();
            lstChat.Items.Add("You:"+ txtMessage.Text);
            if (message.Contains("hello")) {
                lstChat.Items.Add($"Bot: Hello to you too");
            }
            else
            {
                lstChat.Items.Add("bot: i do not understand you");
            }
            txtMessage.Clear();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string message = txtMessage.Text.ToLower();
                lstChat.Items.Add("You:" + txtMessage.Text);
                if (message.Contains("hello"))
                {
                    lstChat.Items.Add($"Bot: Hello to you too");
                }
                else
                {
                    lstChat.Items.Add("bot: i do not understand you");
                }
                txtMessage.Clear();
        }
    }
}