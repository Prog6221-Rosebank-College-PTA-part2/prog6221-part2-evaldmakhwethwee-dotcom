using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace SimpleWinFormsChatbot_OneForm
{
    public class Form1 : Form
    {
        // ----------------------------------------------------------
        // SIMPLE WINDOWS FORMS CHATBOT
        // Everything is inside this one form to make it easier.
        // No extra ChatbotEngine class is used.
        // ----------------------------------------------------------

        // Delegate requirement: a delegate stores a method.
        public delegate string BotDelegate(string message);

        // Controls used on the form.
        private RichTextBox chatBox;
        private TextBox inputBox;
        private Button sendButton;
        private Label titleLabel;
        private Label artLabel;

        // Random object for choosing different answers.
        private Random random = new Random();

        // Memory variables.
        private string userName = "";
        private string favouriteTopic = "";
        private string currentTopic = "";

        // Generic collection: Dictionary + List.
        // Dictionary stores the topic.
        // List stores many responses for that topic.
        private Dictionary<string, List<string>> responses = new Dictionary<string, List<string>>()
        {
            {
                "password",
                new List<string>()
                {
                    "Use a strong password with letters, numbers and symbols.",
                    "Do not use your name or birthday as your password.",
                    "Use a different password for every account."
                }
            },
            {
                "scam",
                new List<string>()
                {
                    "Be careful of messages asking for urgent payments.",
                    "Never share banking details with strangers.",
                    "Verify the sender before clicking links."
                }
            },
            {
                "privacy",
                new List<string>()
                {
                    "Check your privacy settings regularly.",
                    "Do not share your home address online.",
                    "Use two-factor authentication on important accounts."
                }
            },
            {
                "phishing",
                new List<string>()
                {
                    "Do not click suspicious links in emails or SMS messages.",
                    "Check the sender's email address before replying.",
                    "Do not enter your password on a link sent by an unknown person."
                }
            }
        };

        public Form1()
        {
            CreateSimpleDesign();
            PlayVoiceGreeting();

            AddBotMessage("Hello! I am your Cybersecurity Awareness Chatbot.");
            AddBotMessage("Ask me about password, scam, privacy, or phishing.");
            AddBotMessage("You can also type: My name is Thabo, I am interested in privacy, Tell me more, or What do you remember?");
        }

        private void CreateSimpleDesign()
        {
            // Form settings.
            this.Text = "Simple Cybersecurity Chatbot";
            this.Size = new Size(850, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            // Title.
            titleLabel = new Label();
            titleLabel.Text = "Cybersecurity Awareness Chatbot";
            titleLabel.Font = new Font("Arial", 18, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.BackColor = Color.DarkSlateBlue;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.SetBounds(20, 15, 790, 45);
            this.Controls.Add(titleLabel);

            // ASCII art translated into the GUI.
            artLabel = new Label();
            artLabel.Text = @"   ____      _                 ____        _   
  / ___|   _| |__   ___ _ __  | __ )  ___ | |_ 
 | |  | | | | '_ \ / _ \ '__| |  _ \ / _ \| __|
 | |__| |_| | |_) |  __/ |    | |_) | (_) | |_ 
  \____\__, |_.__/ \___|_|    |____/ \___/ \__|
       |___/                                    ";
            artLabel.Font = new Font("Consolas", 9, FontStyle.Regular);
            artLabel.ForeColor = Color.DarkSlateBlue;
            artLabel.BackColor = Color.White;
            artLabel.BorderStyle = BorderStyle.FixedSingle;
            artLabel.SetBounds(20, 70, 790, 95);
            this.Controls.Add(artLabel);

            // Chat area.
            chatBox = new RichTextBox();
            chatBox.ReadOnly = true;
            chatBox.Font = new Font("Arial", 11, FontStyle.Regular);
            chatBox.BackColor = Color.White;
            chatBox.SetBounds(20, 180, 790, 330);
            this.Controls.Add(chatBox);

            // Input box.
            inputBox = new TextBox();
            inputBox.Font = new Font("Arial", 12, FontStyle.Regular);
            inputBox.SetBounds(20, 530, 650, 35);
            inputBox.KeyDown += InputBox_KeyDown;
            this.Controls.Add(inputBox);

            // Send button.
            sendButton = new Button();
            sendButton.Text = "Send";
            sendButton.Font = new Font("Arial", 12, FontStyle.Bold);
            sendButton.BackColor = Color.DarkSlateBlue;
            sendButton.ForeColor = Color.White;
            sendButton.SetBounds(690, 528, 120, 38);
            sendButton.Click += SendButton_Click;
            this.Controls.Add(sendButton);
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("greeting.wav");
                player.Play();
            }
            catch
            {
                // If the sound file is missing, the app must not crash.
                AddBotMessage("Voice greeting could not play, but the chatbot is still working.");
            }
        }

        private void SendButton_Click(object? sender, EventArgs e)
        {
            SendMessage();
        }

        private void InputBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
                e.SuppressKeyPress = true;
            }
        }

        private void SendMessage()
        {
            string userMessage = inputBox.Text.Trim();

            if (userMessage == "")
            {
                AddBotMessage("Please type something first.");
                return;
            }

            AddUserMessage(userMessage);
            inputBox.Clear();

            // Delegate usage:
            // botMethod stores the GetBotReply method.
            BotDelegate botMethod = GetBotReply;

            // Calling the method using the delegate.
            string reply = botMethod(userMessage);

            AddBotMessage(reply);
        }

        private string GetBotReply(string message)
        {
            string lowerMessage = message.ToLower();

            // Remember the user's name.
            if (lowerMessage.Contains("my name is"))
            {
                int start = lowerMessage.IndexOf("my name is") + "my name is".Length;
                userName = message.Substring(start).Trim();

                if (userName == "")
                {
                    return "Please type your name like this: My name is Thabo.";
                }

                return "Nice to meet you, " + userName + ". I will remember your name.";
            }

            // Remember the user's favourite cybersecurity topic.
            if (lowerMessage.Contains("interested in"))
            {
                foreach (string topic in responses.Keys)
                {
                    if (lowerMessage.Contains(topic))
                    {
                        favouriteTopic = topic;
                        currentTopic = topic;

                        return "Great! I will remember that you are interested in " + favouriteTopic + ". " + GetRandomResponse(topic);
                    }
                }

                return "Please tell me if you are interested in password, scam, privacy, or phishing.";
            }

            // Recall memory.
            if (lowerMessage.Contains("what do you remember"))
            {
                if (userName == "" && favouriteTopic == "")
                {
                    return "I do not remember anything yet. Tell me your name or favourite cybersecurity topic.";
                }

                string memory = "Here is what I remember: ";

                if (userName != "")
                {
                    memory += "your name is " + userName + ". ";
                }

                if (favouriteTopic != "")
                {
                    memory += "You are interested in " + favouriteTopic + ".";
                }

                return memory;
            }

            // Follow-up conversation.
            if (lowerMessage.Contains("tell me more") ||
                lowerMessage.Contains("another tip") ||
                lowerMessage.Contains("explain more"))
            {
                if (currentTopic != "")
                {
                    return "Here is another tip about " + currentTopic + ": " + GetRandomResponse(currentTopic);
                }

                return "Please first ask me about password, scam, privacy, or phishing.";
            }

            // Sentiment detection.
            string feeling = DetectSentiment(lowerMessage);

            // Keyword recognition.
            foreach (string topic in responses.Keys)
            {
                if (lowerMessage.Contains(topic))
                {
                    currentTopic = topic;
                    string answer = GetRandomResponse(topic);

                    if (userName != "")
                    {
                        answer = userName + ", " + answer;
                    }

                    return feeling + answer;
                }
            }

            // Default response for unknown input.
            return "I am not sure I understand. Please ask me about password, scam, privacy, or phishing.";
        }

        private string GetRandomResponse(string topic)
        {
            List<string> topicResponses = responses[topic];
            int index = random.Next(topicResponses.Count);
            return topicResponses[index];
        }

        private string DetectSentiment(string message)
        {
            if (message.Contains("worried") || message.Contains("scared"))
            {
                return "It is understandable to feel worried. ";
            }

            if (message.Contains("confused") || message.Contains("unsure"))
            {
                return "No problem, let me explain it simply. ";
            }

            if (message.Contains("frustrated") || message.Contains("angry"))
            {
                return "I understand this can be frustrating. ";
            }

            if (message.Contains("curious"))
            {
                return "Great question. ";
            }

            return "";
        }

        private void AddUserMessage(string message)
        {
            chatBox.SelectionColor = Color.Blue;
            chatBox.AppendText("You: " + message + Environment.NewLine);
            chatBox.SelectionColor = Color.Black;
            chatBox.ScrollToCaret();
        }

        private void AddBotMessage(string message)
        {
            chatBox.SelectionColor = Color.DarkGreen;
            chatBox.AppendText("Bot: " + message + Environment.NewLine + Environment.NewLine);
            chatBox.SelectionColor = Color.Black;
            chatBox.ScrollToCaret();
        }
    }
}
