using IBM.FCAGroup.FiatApp.Models;
using IBM.FCAGroup.FiatApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        [HttpPost]
        public ChatParameters Post([FromBody]ChatParameters chatParameters)
        {
            ConversationService conversation = new ConversationService();
            return conversation.StartConversation(chatParameters.Text, chatParameters.Context);
        }
    }
}
