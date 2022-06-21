# yourroom_poc
Proof of Concept for NSW Health. Azure Bot Service implementation for <i>Your Room</i> Knowledge Base integration.

# Architecture Diagram
![Architecture](https://docs.microsoft.com/en-us/gaming/azure/reference-architectures/media/cognitive/cognitive-customer-service-bot.png)
Note: Bot Source code, including QnA Knowledge Base, was developed and published from [<b>Azure Cognitive Service for Language</b>](https://azure.microsoft.com/en-us/services/cognitive-services/language-service/)  
##### (HLA sourced from https://docs.microsoft.com/en-us/gaming/azure/reference-architectures/cognitive-css-bot)

# Usage
#### It is recommended to use this code as a sample resource only. Development and creation of bot must be done through [<b>Azure Cognitive Service for Language</b>](https://azure.microsoft.com/en-us/services/cognitive-services/language-service/). Main logic for bot is located at `bot\Bots\QnABot.cs`

## Embed Bot (Web channel)
```html
<iframe src="https://webchat.botframework.com/embed/YOUR_BOT_NAME_HERE?s=YOUR_SECRET_HERE" style="height: 502px; max-height: 502px;"></iframe>
```

## Modify appsettings.json
#### For local development and testing, modify `bot\appsettings.json`
```json
{"MicrosoftAppId": "Don't need for local",
"MicrosoftAppPassword": "Don't need for local",
"QnAKnowledgebaseId": "This is the name of your Custom question answering project",
"QnAEndpointKey": "Find your QnA Key in either App Service environment variables or Languages Services Prediction URL (Ocp-Apim-Subscription-Key)",
"QnAEndpointHostName": "https://YOUR_BOT_NAME_HERE.cognitiveservices.azure.com/",
"QnAServiceType": "language",
"EnablePreciseAnswer": "true",
"DisplayPreciseAnswerOnly": "false"}
```
## Testing
Use [Bot Framework Emulator](https://aka.ms/bot-framework-F5-download-emulator) for testing local endpoint

You can test your bot in the Bot Framework Emulator by connecting to http://localhost:3978/api/messages

# General Guide for Building a Bot
1. Plan:
    - Review the bot [design guidelines](https://aka.ms/bot-framework-emulator-design-guidelines)
2. Build:
    - Create a bot from [<b>QUICKSTART QnA Bot in Azure Cognitive Service for Language</b>](https://docs.microsoft.com/en-us/azure/cognitive-services/language-service/question-answering/quickstart/sdk?pivots=studio) or [locally](https://aka.ms/bot-framework-emulator-create-bot-locally)
    - Download [Command-line tools](https://aka.ms/bot-framework-emulator-tools)
    - Add more Language Services such as [LUIS](https://aka.ms/bot-framework-emulator-LUIS-docs-home) to further extend functionality
3. Test:
    - Test using the [Emulator](https://aka.ms/bot-framework-emulator-debug-with-emulator)
    - Test online in [Web Chat](https://aka.ms/bot-framework-emulator-debug-with-web-chat)
4. Publish:
    - Publish [directly to Azure](https://aka.ms/bot-framework-emulator-publish-Azure) or
    - Use [Continuous Deployment](https://aka.ms/bot-framework-emulator-publish-continuous-deployment)
5. Connect:
    - Connect to [channels](https://aka.ms/bot-framework-emulator-connect-channels)
6. Evaluate:
    - [View analytics](https://aka.ms/bot-framework-emulator-bot-analytics)

# Resources
- [Question Answering Docs](https://docs.microsoft.com/en-us/azure/cognitive-services/language-service/question-answering/overview)
- [Question Answering Projects API](https://docs.microsoft.com/en-us/rest/api/cognitiveservices/questionanswering/question-answering-projects)
- [Question Answering API](https://docs.microsoft.com/en-us/rest/api/cognitiveservices/questionanswering/question-answering)
- [C# Sample Code](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore)
- [Bot Service Docs guides (based on Sample Code above)](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-send-messages?view=azure-bot-service-4.0&tabs=csharp)
- [Question Answering GA Blog Post](https://techcommunity.microsoft.com/t5/ai-cognitive-services-blog/question-answering-feature-is-generally-available/ba-p/2899497)

# Extra Links
- [Expire a conversation](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-expire-conversation?view=azure-bot-service-4.0&tabs=csharp)
- [Add telemetry to your bot](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-telemetry?view=azure-bot-service-4.0&tabs=csharp)
- [Add telemetry to your QnA bot](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-telemetry-qnamaker?view=azure-bot-service-4.0)
- [Analyse your bot's telemetry data](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-telemetry-analytics-queries?view=azure-bot-service-4.0)
