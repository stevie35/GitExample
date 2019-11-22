using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module20Tp1
{
    class EmployeeWebServiceManager
    {
        private async Task<TItem> HttpClientCaller<TItem>(String url, TItem item)
        {
            using (EmployeeWebServiceManager  client = new HttpClient())
            {
                client.BaseAddress = new Uri(DataConnectionResource);
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);
                item = await HandleResponse(item, response);
            }

            return item;
        }
}
