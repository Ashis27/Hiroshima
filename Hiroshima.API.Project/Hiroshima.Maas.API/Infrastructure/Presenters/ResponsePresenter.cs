using Hiroshima.Maas.DL.Interfaces;
using Hiroshima.Maas.Services.Interfaces;
using System.Net;
using Web.Api.Serialization;

namespace Hiroshima.Maas.API.Presenters
{
    public sealed class ResponsePresenter : IOutputPort<ResponseMessage>
    {
        public JsonContentResult ContentResult { get; }

        public ResponsePresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(ResponseMessage response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
