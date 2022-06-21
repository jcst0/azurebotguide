// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.5.0
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.DateTime;
using Microsoft.Recognizers.Text.Number;


namespace Microsoft.BotBuilderSamples.Bots
{
    public class QnABot<T> : ActivityHandler where T : Microsoft.Bot.Builder.Dialogs.Dialog
    {
        protected readonly BotState ConversationState;
        protected readonly Microsoft.Bot.Builder.Dialogs.Dialog Dialog;
        protected readonly BotState UserState;

        public QnABot(ConversationState conversationState, UserState userState, T dialog)
        {
            ConversationState = conversationState;
            UserState = userState;
            Dialog = dialog;
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occurred during the turn.
            await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await UserState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        // // custom code below
         protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            await SendWelcomeMessageAsync(turnContext, cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            // added QnA dialog below
            await Dialog.RunAsync(turnContext, ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
        }
  

        // Greet the user and give them instructions on how to interact with the bot.
        private static async Task SendWelcomeMessageAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in turnContext.Activity.MembersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    IMessageActivity reply = null;
                    reply = MessageFactory.Text("--- Proof of Concept --- Microsoft, Joseph Stephen 2022");
                    reply.Attachments = new List<Attachment>() { GetInlineAttachment() };
                    await turnContext.SendActivityAsync(reply, cancellationToken);
                    await turnContext.SendActivityAsync(
                        $"Welcome to Your Room {member.Name}, a joint initiative by NSW Health and St Vincent's Alcohol and Drug Information Service." +
                        $"(test)",
                        cancellationToken: cancellationToken);
                    await turnContext.SendActivityAsync("Please ask a question to the bot", cancellationToken: cancellationToken);
                    await SendSuggestedActionsAsync(turnContext, cancellationToken);

                    
                    
                }
            }
        }


        // Creates an inline attachment sent from the bot to the user using a base64 string.
        // Using a base64 string to send an attachment will not work on all channels.
        // Additionally, some channels will only allow certain file types to be sent this way.
        // For example a .png file may work but a .pdf file may not on some channels.
        // Please consult the channel documentation for specifics.
        private static Attachment GetInlineAttachment()
        {
            var imagePath = Path.Combine(Environment.CurrentDirectory, @"Resources", "yourroom.jpg");
            var imageData = Convert.ToBase64String(File.ReadAllBytes(imagePath));

            return new Attachment
            {
                Name = @"Resources\yourroom.jpg",
                ContentType = "image/jpg",
                ContentUrl = $"data:image/jpg;base64,{imageData}",
            };
        }

        // Creates and sends an activity with suggested actions to the user. When the user
        // clicks one of the buttons the text value from the "CardAction" will be
        // displayed in the channel just as if the user entered the text. There are multiple
        // "ActionTypes" that may be used for different situations.
        private static async Task SendSuggestedActionsAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("Please choose between speaking to an ADIS agent or the BOT:");

            reply.SuggestedActions = new SuggestedActions()
            {
                // TYPES: 'openUrl', 'imBack', 'postBack','playAudio', 'playVideo', 'showImage', 'downloadFile', 'signin', 'call', 'messageBack
                Actions = new List<CardAction>()
                {
                    new CardAction() { Title = "ADIS", Type = ActionTypes.OpenUrl, Value = "https://yourroom.health.nsw.gov.au/getting-help/Pages/ADIS-Web-Chat.aspx", Image = "https://via.placeholder.com/20/FF0000?text=R", ImageAltText = "ADIS" },
                    new CardAction() { Title = "BOT", Type = ActionTypes.ShowImage, Value = "", Image = "https://via.placeholder.com/20/FFFF00?text=Y", ImageAltText = "BOT" },
                },
            };
            
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

// UNCOMMENT Code below for Prompt users for input sample

    //     protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
    //     {
    //         var conversationStateAccessors = ConversationState.CreateProperty<ConversationFlow>(nameof(ConversationFlow));
    //         var flow = await conversationStateAccessors.GetAsync(turnContext, () => new ConversationFlow(), cancellationToken);

    //         var userStateAccessors = UserState.CreateProperty<UserProfile>(nameof(UserProfile));
    //         var profile = await userStateAccessors.GetAsync(turnContext, () => new UserProfile(), cancellationToken);

    //         await FillOutUserProfileAsync(flow, profile, turnContext, cancellationToken);

    //         // Save changes.
    //         await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
    //         await UserState.SaveChangesAsync(turnContext, false, cancellationToken);
    //     }

    //     private static async Task FillOutUserProfileAsync(ConversationFlow flow, UserProfile profile, ITurnContext turnContext, CancellationToken cancellationToken)
    //     {
    //         var input = turnContext.Activity.Text?.Trim();
    //         string message;

    //         switch (flow.LastQuestionAsked)
    //         {
    //             case ConversationFlow.Question.None:
    //                 await turnContext.SendActivityAsync("Let's get started. What is your name?", null, null, cancellationToken);
    //                 flow.LastQuestionAsked = ConversationFlow.Question.Name;
    //                 break;
    //             case ConversationFlow.Question.Name:
    //                 if (ValidateName(input, out var name, out message))
    //                 {
    //                     profile.Name = name;
    //                     await turnContext.SendActivityAsync($"Hi {profile.Name}.", null, null, cancellationToken);
    //                     await turnContext.SendActivityAsync("How old are you?", null, null, cancellationToken);
    //                     flow.LastQuestionAsked = ConversationFlow.Question.Age;
    //                     break;
    //                 }
    //                 else
    //                 {
    //                     await turnContext.SendActivityAsync(message ?? "I'm sorry, I didn't understand that.", null, null, cancellationToken);
    //                     break;
    //                 }

    //             case ConversationFlow.Question.Age:
    //                 if (ValidateAge(input, out var age, out message))
    //                 {
    //                     profile.Age = age;
    //                     await turnContext.SendActivityAsync($"I have your age as {profile.Age}.", null, null, cancellationToken);
    //                     await turnContext.SendActivityAsync("When is your flight?", null, null, cancellationToken);
    //                     flow.LastQuestionAsked = ConversationFlow.Question.Date;
    //                     break;
    //                 }
    //                 else
    //                 {
    //                     await turnContext.SendActivityAsync(message ?? "I'm sorry, I didn't understand that.", null, null, cancellationToken);
    //                     break;
    //                 }

    //             case ConversationFlow.Question.Date:
    //                 if (ValidateDate(input, out var date, out message))
    //                 {
    //                     profile.Date = date;
    //                     await turnContext.SendActivityAsync($"Your cab ride to the airport is scheduled for {profile.Date}.");
    //                     await turnContext.SendActivityAsync($"Thanks for completing the booking {profile.Name}.");
    //                     await turnContext.SendActivityAsync($"Type anything to run the bot again.");
    //                     flow.LastQuestionAsked = ConversationFlow.Question.None;
    //                     profile = new UserProfile();
    //                     break;
    //                 }
    //                 else
    //                 {
    //                     await turnContext.SendActivityAsync(message ?? "I'm sorry, I didn't understand that.", null, null, cancellationToken);
    //                     break;
    //                 }
    //         }
    //     }

    //     private static bool ValidateName(string input, out string name, out string message)
    //     {
    //         name = null;
    //         message = null;

    //         if (string.IsNullOrWhiteSpace(input))
    //         {
    //             message = "Please enter a name that contains at least one character.";
    //         }
    //         else
    //         {
    //             name = input.Trim();
    //         }

    //         return message is null;
    //     }

    //     private static bool ValidateAge(string input, out int age, out string message)
    //     {
    //         age = 0;
    //         message = null;

    //         // Try to recognize the input as a number. This works for responses such as "twelve" as well as "12".
    //         try
    //         {
    //             // Attempt to convert the Recognizer result to an integer. This works for "a dozen", "twelve", "12", and so on.
    //             // The recognizer returns a list of potential recognition results, if any.

    //             var results = NumberRecognizer.RecognizeNumber(input, Culture.English);

    //             foreach (var result in results)
    //             {
    //                 // The result resolution is a dictionary, where the "value" entry contains the processed string.
    //                 if (result.Resolution.TryGetValue("value", out var value))
    //                 {
    //                     age = Convert.ToInt32(value);
    //                     if (age >= 18 && age <= 120)
    //                     {
    //                         return true;
    //                     }
    //                 }
    //             }

    //             message = "Please enter an age between 18 and 120.";
    //         }
    //         catch
    //         {
    //             message = "I'm sorry, I could not interpret that as an age. Please enter an age between 18 and 120.";
    //         }

    //         return message is null;
    //     }

    //     private static bool ValidateDate(string input, out string date, out string message)
    //     {
    //         date = null;
    //         message = null;

    //         // Try to recognize the input as a date-time. This works for responses such as "11/14/2018", "9pm", "tomorrow", "Sunday at 5pm", and so on.
    //         // The recognizer returns a list of potential recognition results, if any.
    //         try
    //         {
    //             var results = DateTimeRecognizer.RecognizeDateTime(input, Culture.English);

    //             // Check whether any of the recognized date-times are appropriate,
    //             // and if so, return the first appropriate date-time. We're checking for a value at least an hour in the future.
    //             var earliest = DateTime.Now.AddHours(1.0);

    //             foreach (var result in results)
    //             {
    //                 // The result resolution is a dictionary, where the "values" entry contains the processed input.
    //                 var resolutions = result.Resolution["values"] as List<Dictionary<string, string>>;

    //                 foreach (var resolution in resolutions)
    //                 {
    //                     // The processed input contains a "value" entry if it is a date-time value, or "start" and
    //                     // "end" entries if it is a date-time range.
    //                     if (resolution.TryGetValue("value", out var dateString)
    //                         || resolution.TryGetValue("start", out dateString))
    //                     {
    //                         if (DateTime.TryParse(dateString, out var candidate)
    //                             && earliest < candidate)
    //                         {
    //                             date = candidate.ToShortDateString();
    //                             return true;
    //                         }
    //                     }
    //                 }
    //             }

    //             message = "I'm sorry, please enter a date at least an hour out.";
    //         }
    //         catch
    //         {
    //             message = "I'm sorry, I could not interpret that as an appropriate date. Please enter a date at least an hour out.";
    //         }

    //         return false;
    //     }
    // }
}
}