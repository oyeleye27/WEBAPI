using AspNetWebApiRest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using System.Web.UI.WebControls;

namespace AspNetWebApiRest.Controllers
{
    public class ListItemsController : ApiController

    {
        private static List<CustomListItem> s_listItems = new List<CustomListItem>();

        // GET api/<controller>
        public IEnumerable<CustomListItem> Get()
        {
            return _listItems;
        }
        public HttpResponseMessage Get(int id)
        {
            var item = _listItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        // GET api/<controller>/5
       // public string Get(int id) => "value";
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]CustomListItem model)
        {
            if (string.IsNullOrEmpty(model?.Text))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var maxId = 0;
            if (_listItems.Count > 0)
            {
                maxId = _listItems.Max(x => x.Id);
            }
            model.Id = maxId + 1;
            _listItems.Add(model);
            return Request.CreateResponse(HttpStatusCode.Created, model);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            var item = _listItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _listItems.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        private static List<Models.CustomListItem> _listItems { get => s_listItems; set => s_listItems = value; }
        public int id { get; private set; }
    }
}