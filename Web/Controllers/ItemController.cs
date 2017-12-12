using IBM.FCAGroup.FiatApp.Models;
using Microsoft.AspNetCore.Mvc;
using IBM.FCAGroup.FiatApp.Services;
using System.Collections.Generic;

namespace IBM.FCAGroup.FiatApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : BaseController
    {
        #region Protected Properties

        protected ItemService Service = new ItemService();

        #endregion

        #region Public Methods

        
        [HttpGet("{id}")]
        public Item Get(int id)
        {
            return Service.Get(id);
        }

        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return this.Service.All();
        }
        
        [HttpPost]
        public Item Post([FromBody]Item model)
        {
            this.Service.Save(model);
            return model;
        }
        
        [HttpPut]
        public Item Put([FromBody]Item model)
        {
            this.Service.Save(model);
            return model;
        }
        
        [HttpDelete]
        public void Delete(long id)
        {
            this.Service.Delete(id);
        }

        #endregion
    }
}