using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Business.Interface
{
    public interface IRazorPartialToStringRenderer
    {
        Task<string> RenderPartialToStringAsync<TModel>(string partialName, TModel model);
    }
}
