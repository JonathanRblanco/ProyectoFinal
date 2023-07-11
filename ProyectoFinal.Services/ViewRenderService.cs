using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.Services
{
    public class ViewRenderService : IViewRenderService
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor contextAccessor;

        public ViewRenderService(IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider,
            IHttpContextAccessor contextAccessor)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            this.contextAccessor = contextAccessor;
        }
        public async Task<string> RenderToString(string viewPathOrName, object model)
        {
            var actionContext = GetActionContext();
            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.FindView(actionContext, viewPathOrName, false);

                if (viewResult.View == null)
                {
                    viewResult = TryGetViewFromWebApplicationFileSystem(viewPathOrName);

                    if (viewResult.View == null)
                    {
                        throw new ArgumentNullException($"{viewPathOrName} does not match any available view");
                    }
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }

        private ViewEngineResult TryGetViewFromWebApplicationFileSystem(string viewPathOrName)
        {
            return _razorViewEngine.GetView(viewPathOrName, viewPathOrName, false);
        }

        private ActionContext GetActionContext()
        {
            if (contextAccessor?.HttpContext != null)
            {
                return new ActionContext(contextAccessor.HttpContext, contextAccessor.HttpContext.GetRouteData(), new ActionDescriptor());
            }

            var httpContext = new DefaultHttpContext
            {
                RequestServices = _serviceProvider
            };

            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }
    }
}
