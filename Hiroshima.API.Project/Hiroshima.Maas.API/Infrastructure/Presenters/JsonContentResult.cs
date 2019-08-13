using Microsoft.AspNetCore.Mvc;

namespace Hiroshima.Maas.API.Presenters
{
  public sealed class JsonContentResult : ContentResult
  {
    public JsonContentResult()
    {
      ContentType = "application/json";
    }
  }
}
