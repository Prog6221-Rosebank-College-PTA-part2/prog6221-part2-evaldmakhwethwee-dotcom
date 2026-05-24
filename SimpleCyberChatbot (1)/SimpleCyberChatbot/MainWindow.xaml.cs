using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SimpleCyberChatbot
{
    public partial class MainWindow : Window
    {
        private readonly ChatbotEngine bot = new ChatbotEngine();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PlayVoiceGreeting();

            AddBotMessage("Hello! I am your Cybersecurity Awareness Chatbot.");
            AddBotMessage("Ask me about password, scam, privacy, or phishing.");
            AddBotMessage("You can also type: My name is Thabo, I am interested in privacy, Tell me more, or What do you remember?");
            AddBotMessage("You can also type: My name is Thabo, I am interested in privacy, Tell me more, or What do you remember?");
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");

                if (File.Exists(filePath))
                {
                    SoundPlayer player = new SoundPlayer(filePath);
                    player.Play();
                }
                else
                {
                    AddBotMessage("Voice greeting file was not found. Add greeting.wav to the project.");
                }
            }
            catch
            {
                AddBotMessage("The voice greeting could not be played.");
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            string message = UserInput.Text.Trim();

            if (message == "")
            {
                return;
            }

            AddUserMessage(message);
            UserInput.Clear();

            string reply = bot.GetResponse(message);
            AddBotMessage(reply);
        }

        private void AddUserMessage(string message)
        {
            AddMessage("You", message, true);
        }

        private void AddBotMessage(string message)
        {
            AddMessage("Bot", message, false);
        }

        private void AddMessage(string sender, string message, bool isUser)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = sender + ": " + message,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 15,
                Foreground = Brushes.Black,
                Margin = new Thickness(8)
            };

            Border bubble = new Border
            {
                Child = textBlock,
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(8),
                Margin = new Thickness(5),
                MaxWidth = 650,
                Background = isUser ? Brushes.LightBlue : Brushes.LightGray,
                HorizontalAlignment = isUser ? HorizontalAlignment.Right : HorizontalAlignment.Left
            };

            ChatPanel.Children.Add(bubble);
            ChatScroll.ScrollToEnd();
        }
    }
}
