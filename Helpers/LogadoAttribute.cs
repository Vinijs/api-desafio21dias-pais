using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace web_renderizacao_server_side.Helpers
{
  public class LogadoAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var authorization = filterContext.HttpContext.Request.Headers["Authorization"].ToString();
        
        if(string.IsNullOrEmpty(authorization)){
          filterContext.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
          return;
        }

        using (var http = new HttpClient())
        {
            http.BaseAddress = new Uri(Config.AdministradorAPI);
            http.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            http.DefaultRequestHeaders.Add("Authorization", authorization);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, "/token");
            var response = http.Send(request);

            if(response.StatusCode != HttpStatusCode.NoContent){
                filterContext.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
            }
        }

        base.OnActionExecuting(filterContext);
    }
  }
}