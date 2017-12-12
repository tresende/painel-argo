using IBM.FCAGroup.FiatApp.Models;
using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace IBM.FCAGroup.FiatApp.Services
{
    public class ConversationService
    {
        public string user = Environment.GetEnvironmentVariable("CONVERSATION_SERVICE_USER");
        public string pass = Environment.GetEnvironmentVariable("CONVERSATION_SERVICE_PASSWORD");
        public string IRRIGAR_INTENT = "pequisar-aprovadores";
        public string workspaceId = Environment.GetEnvironmentVariable("CONVERSATION_SERVICE_WORKSPACE");
        private IBM.WatsonDeveloperCloud.Conversation.v1.ConversationService conversation;

        public ChatParameters StartConversation(string text, dynamic questionContext)
        {
            conversation = new IBM.WatsonDeveloperCloud.Conversation.v1.ConversationService(user, pass, "2017-05-26");
            var list = conversation.ListWorkspaces();
            return this.CallWatson(text, questionContext);
        }

        public ChatParameters CallWatson(string text, dynamic questionContext)
        {
            var messageRequest = CreateMessage(text);
            messageRequest.Context = questionContext ?? messageRequest.Context;
            var result = conversation.Message(workspaceId, messageRequest);
            //recuperar decisão
            var decisao = GetEntity(result, "sim-nao");
            if (decisao != null && decisao.Value == "sim")
            {
                //StartIrrigation(questionContext, messageRequest, decisao);
            }

            questionContext = result.Context;
            return new ChatParameters
            {
                Text = string.Join(" ", result.Output.Text),
                Context = questionContext
            };
        }

        #region Create Responses

        private static MessageRequest CreateMessage(string text)
        {
            return new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = text
                }
            };
        }

        //private MessageResponse StartIrrigation(dynamic questionContext, MessageRequest messageRequest, RuntimeEntity id)
        //{
        //    MeasurementService measurementService = new MeasurementService();
        //    var last = measurementService.GetLastByNode(13);
        //    measurementService.Save(new Measurement()
        //    {
        //        IdNode = last.IdNode,
        //        AirHumidity = 0,
        //        SiolHumidity = 1,
        //        Temperature = 0,

        //    });
        //    MessageResponse result;
        //    var status = this.GetStatusResqust(id.Value);
        //    messageRequest.Entities = new List<RuntimeEntity>();
        //    //Update no banco
        //    messageRequest.Context = questionContext;
        //    result = conversation.Message(workspaceId, messageRequest);
        //    return result;
        //}

        private MessageResponse StopIrrigation(dynamic questionContext, MessageRequest messageRequest, RuntimeEntity id)
        {
            MessageResponse result;
            var approver = this.GetApprover(id.Value);
            messageRequest.Entities = new List<RuntimeEntity>();
            var requestId = SetEntity("sys-number", id.Value);
            var approvers = SetEntity("aprovadores", approver);
            messageRequest.Entities.Add(requestId);
            messageRequest.Entities.Add(approvers);
            messageRequest.Context = questionContext;
            result = conversation.Message(workspaceId, messageRequest);
            return result;
        }

        #endregion

        #region Rest Requests

        private string GetStatusResqust(string id)
        {
            return "Aberto";
        }

        private string GetApprover(string id)
        {
            return "Thiago, Marcelo e Matheus";
        }

        #endregion

        #region Watston API

        private RuntimeEntity SetEntity(string name, string value)
        {
            return new RuntimeEntity
            {
                Entity = name,
                Value = value,
            };
        }

        private RuntimeEntity GetEntity(MessageResponse result, string entity)
        {
            return result.Entities.FirstOrDefault(x => x.Entity == entity);
        }

        private RuntimeIntent GetIntent(MessageResponse result, string intent)
        {
            return result.Intents.FirstOrDefault(x => x.Intent == intent);
        }

        #endregion
    }
}
