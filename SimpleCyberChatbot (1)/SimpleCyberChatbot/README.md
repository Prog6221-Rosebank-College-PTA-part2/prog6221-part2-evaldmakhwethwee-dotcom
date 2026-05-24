# Simple Cybersecurity Awareness Chatbot

This is a beginner-friendly WPF C# chatbot project.

## What this project includes

- GUI using WPF
- Voice greeting using `greeting.wav`
- ASCII art displayed in the GUI
- Keyword recognition: password, scam, privacy, phishing
- Random responses using `Dictionary<string, List<string>>`
- Memory and recall: name and favourite topic
- Sentiment detection: worried, confused, frustrated, curious
- Follow-up questions: tell me more, another tip, explain more
- Delegate example using `BotDelegate`
- Error handling for blank and unknown input

## How to open it

1. Extract the ZIP file.
2. Open Visual Studio on Windows.
3. Click **Open a project or solution**.
4. Open `SimpleCyberChatbot.csproj`.
5. Press **F5** to run.

## Files

| File | Purpose |
|---|---|
| `MainWindow.xaml` | The WPF GUI design |
| `MainWindow.xaml.cs` | Button click, message display, and voice greeting |
| `ChatbotEngine.cs` | Chatbot logic, dictionary, random responses, memory, sentiment, and delegate |
| `greeting.wav` | Voice greeting file |

## Test messages

Try these in the chatbot:

- My name is Thabo
- Tell me about password safety
- I am worried about scams
- I am interested in privacy
- Tell me more
- What do you remember?
- hello

## Delegate explanation

The delegate is in `ChatbotEngine.cs`:

```csharp
public delegate string BotDelegate(string message);
```

It is used here:

```csharp
BotDelegate moodChecker = DetectSentiment;
string moodMessage = moodChecker(message);
```

Simple meaning:

A delegate is a variable that stores a method.
Here, `moodChecker` stores the `DetectSentiment` method.
