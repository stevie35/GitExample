using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2
{
    private async Task<TItem> HttpClientSender<TItem>(String url, TItem item, TItem result)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(DataConnectionResource);
            client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(item),
                Encoding.UTF8, "application/json"));

            result = await HandleResponse(item, response);
        }

        return result;
    }

    HttpResponseMessage response = await client.GetAsync("api/products/1");
 if (response.IsSuccessStatusCode){
 Product product = await response.Content.ReadAsAsync<Product>();
    Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price,
product.Category);

}