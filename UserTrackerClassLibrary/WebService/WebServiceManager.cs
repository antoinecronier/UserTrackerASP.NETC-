using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UsersTracker.EnumManager;

namespace UsersTracker.WebService
{
    public class WebServiceManager<T> where T : class
    {
        private const string APIFormat = "sAPI/";

        public String DataConnectionResource { get; set; }

        public WebServiceManager(DataConnectionResource resource)
        {
            DataConnectionResource = EnumString.GetStringValue(resource);
        }

        public async Task<T> Get(Int32 id)
        {
            T item = default(T);
            String url = typeof(T).Name + APIFormat + id + "/";
            item = await HttpClientCaller<T>(url, item);
            return item;
        }

        public async Task<List<T>> Get()
        {
            List<T> item = default(List<T>);
            String url = typeof(T).Name + APIFormat;
            item = await HttpClientCaller<List<T>>(url, item);
            return item;
        }

        public async Task<T> Post(T item)
        {
            T result = default(T);
            String url = typeof(T).Name + APIFormat;
            result = await HttpClientSender<T>(url, item, result);

            return result;
        }

        public async Task<List<T>> Post(List<T> items)
        {
            List<T> result = default(List<T>);
            String url = typeof(T).Name + "s/";
            result = await HttpClientSender<List<T>>(url, items, result);

            return result;
        }

        public async Task Put(T item)
        {
            String url = typeof(T).Name + APIFormat;
            await HttpClientPuter<T>(url, item);
        }

        public async Task Put(List<T> items)
        {
            String url = typeof(T).Name + APIFormat;
            foreach (var item in items)
            {
                await HttpClientPuter<T>(url, item);
            }
        }

        public async Task<Int32> Delete(T item)
        {
            Int32 result = default(Int32);
            String url = typeof(T).Name + APIFormat;
            result = await HttpClientDeleter<T, Int32>(url, item, result);

            return result;
        }

        public async Task<Int32> Delete(List<T> items)
        {
            Int32 result = default(Int32);
            String url = typeof(T).Name + "s/";
            result = await HttpClientDeleter<List<T>, Int32>(url, items, result);

            return result;
        }

        private async Task<TItem> HttpClientCaller<TItem>(String url, TItem item)
        {
            using (HttpClient client = new HttpClient())
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

        private async Task HttpClientPuter<TItem>(string url, TItem item)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(DataConnectionResource);
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsync(url,
                    new StringContent(JsonConvert.SerializeObject(item),
                    Encoding.UTF8, "application/json"));

                await HandleResponse(item, response);
            }
        }

        private async Task<TResult> HttpClientDeleter<TItem, TResult>(string url, TItem item, TResult result)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(DataConnectionResource);
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, url))
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(item),
                    Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.SendAsync(message);

                    result = await HandleResponse(result, response);
                }
            }

            return result;
        }

        private async Task<TItem> HandleResponse<TItem>(TItem item, HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                String result = await response.Content.ReadAsStringAsync();
                item = JsonConvert.DeserializeObject<TItem>(result);
            }

            return item;
        }
    }
}

