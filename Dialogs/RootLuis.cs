using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Security;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using QnABot;
using System.Linq;

namespace Microsoft.Bot.Sample.QnABot
{
    [LuisModel("a0de6018-1a36-4ffb-813f-3dc921e44f", "8f8d6831f3634aa3b99c468d248536ee")]
    [Serializable]
    public class RootLuis : LuisDialog<object>
    {

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, IAwaitable<IMessageActivity> message, LuisResult result)
        {

            var messageToForward = await message as Activity;
            await context.Forward(new RootDialog(), this.AfterQnA, messageToForward, CancellationToken.None);
        }

        private async Task AfterQnA(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(this.MessageReceived);
        }

      

        [LuisIntent("DeviceDetail")]
        public async Task DeviceData(IDialogContext context, LuisResult result)
        {
            
            var message = Convert.ToString(result.Query);
            StringBuilder sb = new StringBuilder();
            using (DeviceDataEntities deviceDataEntities = new DeviceDataEntities())
            {
                var query = from d in deviceDataEntities.tbl_DeviceDetails
                            orderby d.DeviceId
                            select d;
                foreach (var item in query.ToList())
                {
                    sb.AppendLine("invertory has " + item.DeviceCount.ToString() + " " + item.DeviceType.ToString() + "\n");
                }
            }
            
            await context.PostAsync(sb.ToString());
        }

    }
}