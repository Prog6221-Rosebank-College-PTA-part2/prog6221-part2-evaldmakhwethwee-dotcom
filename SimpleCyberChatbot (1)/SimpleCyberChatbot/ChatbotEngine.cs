using System;
using System.Collections.Generic;

namespace SimpleCyberChatbot
{
    public class ChatbotEngine
    {
        // Delegate requirement: this delegate can store any method that receives a string and returns a string.
        public delegate string BotDelegate(string message);

        private readonly Random random = new Random();

        // Memory: the bot remembers simple details shared by the user.
        private string userName = "";
        private string favouriteTopic = "";
        private string currentTopic = "";

        // Generic collection: Dictionary stores a topic, and List stores many answers for that topic.
        private readonly Dictionary<string, List<string>> responses = new Dictionary<string, List<string>>
        {
            {
                "password",
                new List<string>
                {
                    "Use a strong password with letters, numbers and symbols.",
                    "Do not use your name or birthday as a password.",
                    "Use a different password for every account."
                }
            },
            {
                "scam",
                new List<string>
                {
                    "Be careful of messages asking for urgent payments.",
                    "Never share banking details with strangers.",
                    "Verify the sender before clicking links."
                }
            },
            {
                "privacy",
                new List<string>
                {
                    "Check your privacy settings regularly.",
                    "Do not share your home address online.",
                    "Use two-factor authentication on important accounts."
                }
            },
            {
                "phishing",
                new List<string>
                {
                    "Do not click suspicious links in emails or SMS messages.",
                    "Check the sender's email address before responding.",
                    "Do not enter your password on a website opened from a suspicious link."
                }
            }
        };

        public string GetResponse(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "Please type something so I can help you.";
            }

            string lowerMessage = message.ToLower();

            // Remember the user's name.
            if (lowerMessage.Contains("my name is"))
            {
                int startIndex = lowerMessage.IndexOf("my name is") + "my name is".Length;
                userName = message.Substring(startIndex).Trim();

                if (userName == "")
                {
                    return "Please type your name like this: My name is Thabo.";
                }

                return "Nice to meet you, " + userName + ". I will remember your name.";
            }

            // Remember favourite cybersecurity topic.
            if (lowerMessage.Contains("interested in"))
            {
                foreach (string topic in responses.Keys)
                {
                    if (lowerMessage.Contains(topic))
                    {
                        favouriteTopic = topic;
                        currentTopic = topic;

                        return "Great. I will remember that you are interested in " + topic + ". " + GetRandomResponse(topic);
                    }
                }

                return "Please tell me if you are interested in password, scam, privacy, or phishing.";
            }

            // Recall memory.
            if (lowerMessage.Contains("what do you remember") || lowerMessage.Contains("remember me"))
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

            // Follow-up conversation flow.
            if (lowerMessage.Contains("tell me more") ||
                lowerMessage.Contains("another tip") ||
                lowerMessage.Contains("explain more"))
            {
                if (currentTopic != "")
                {
                    return "Here is another tip about " + currentTopic + ": " + GetRandomResponse(currentTopic);
                }

                if (favouriteTopic != "")
                {
                    currentTopic = favouriteTopic;
                    return "Since you are interested in " + favouriteTopic + ", here is a tip: " + GetRandomResponse(favouriteTopic);
                }

                return "Please first ask me about password, scam, privacy, or phishing.";
            }

            // Delegate use: the delegate stores the DetectSentiment method.
            BotDelegate moodChecker = DetectSentiment;
            string moodMessage = moodChecker(message);

            // Keyword recognition.
            foreach (string topic in responses.Keys)
            {
                if (lowerMessage.Contains(topic))
                {
                    currentTopic = topic;
                    return Personalise(moodMessage + GetRandomResponse(topic));
                }
            }

            // Default response for unknown input.
            return "I am not sure I understand. Can you try rephrasing? You can ask about password, scam, privacy, or phishing.";
        }

        private string GetRandomResponse(string topic)
        {
            List<string> possibleAnswers = responses[topic];
            int index = random.Next(possibleAnswers.Count);
            return possibleAnswers[index];
        }

        private string DetectSentiment(string message)
        {
            string lowerMessage = message.ToLower();

            if (lowerMessage.Contains("worried") || lowerMessage.Contains("scared") || lowerMessage.Contains("afraid"))
            {
                return "It is understandable to feel worried. ";
            }

            if (lowerMessage.Contains("confused") || lowerMessage.Contains("unsure"))
            {
                return "No problem, let me explain it simply. ";
            }

            if (lowerMessage.Contains("frustrated") || lowerMessage.Contains("angry"))
            {
                return "I understand this can be frustrating. ";
            }

            if (lowerMessage.Contains("curious"))
            {
                return "Great question. ";
            }

            return "";
        }

        private string Personalise(string response)
        {
            if (userName != "")
            {
                return userName + ", " + response;
            }

            return response;
        }
    }
}
