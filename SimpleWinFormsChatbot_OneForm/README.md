# Simple Cybersecurity Awareness Chatbot - Windows Forms

This is a very simple Windows Forms version for beginner students.

## Why this version is simpler

- No extra `ChatbotEngine.cs` class.
- All chatbot logic is inside `Form1.cs`.
- The form is created with simple C# controls.
- The code has many comments to help students understand.

## Files

- `Program.cs` - starts the application.
- `Form1.cs` - contains the form design and all chatbot logic.
- `greeting.wav` - simple voice greeting sound file.
- `SimpleWinFormsChatbot_OneForm.csproj` - Visual Studio project file.

## How to run

1. Open the folder in Visual Studio 2022.
2. Open `SimpleWinFormsChatbot_OneForm.csproj`.
3. Press `F5`.

## Test messages

Try these messages:

- `My name is Thabo`
- `Tell me about password safety`
- `I am worried about scams`
- `I am interested in privacy`
- `Tell me more`
- `What do you remember?`
- `hello`

## Requirements covered

| Requirement | Where it is covered |
|---|---|
| GUI application | `CreateSimpleDesign()` |
| Voice greeting | `PlayVoiceGreeting()` |
| ASCII art in GUI | `artLabel` |
| Keyword recognition | password, scam, privacy, phishing |
| Random responses | `Dictionary<string, List<string>>` and `Random` |
| Conversation flow | `Tell me more`, `another tip`, `explain more` |
| Memory and recall | `userName`, `favouriteTopic`, `What do you remember` |
| Sentiment detection | `DetectSentiment()` |
| Error handling | empty input, unknown input, try/catch for sound |
| Generic collection | `Dictionary<string, List<string>>` |
| Delegate | `public delegate string BotDelegate(string message);` |
| OOP | One form class with methods |

## Simple delegate explanation

The delegate is here:

```csharp
public delegate string BotDelegate(string message);
```

The method is stored in the delegate here:

```csharp
BotDelegate botMethod = GetBotReply;
```

The method is called using the delegate here:

```csharp
string reply = botMethod(userMessage);
```

This is enough for beginners because it shows that a delegate can store and call a method.
