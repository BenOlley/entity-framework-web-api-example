using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;


namespace ClassLibrary.WebApiClient
{
    public static class TemperatureClient
    {

        //This class uses Json.Net 9.0.1
        //Install-Package Newtonsoft.Json -Version 9.0.1
        //JsonConvert.DeserializeObject

        public static HttpClient client = new HttpClient();
        
        static TemperatureClient()
        {
            client.BaseAddress = new Uri("http://localhost:1233/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task <List<TemperatureModel>> GetTemperatures()
        {
            HttpResponseMessage response = await client.GetAsync("api/Data");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var temperatureObjects = JsonConvert.DeserializeObject<List<TemperatureModel>>(result);
                return temperatureObjects;
            }
            return null;
        }

        public static async Task<TemperatureModel> GetTemperature(int id)
        {
            HttpResponseMessage response = await client.GetAsync("api/Data/" + id);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var temperatureObject = JsonConvert.DeserializeObject<TemperatureModel>(result);
                return temperatureObject;
            }
            return null;
        }


        public static async Task<TemperatureModel> PutTemperature(int id, TemperatureModel temperatureModel)
        {

            var jsonObject = JsonConvert.SerializeObject(temperatureModel);
            var httpContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync("api/Data/" + id, httpContent);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var temperatureObject = JsonConvert.DeserializeObject<TemperatureModel>(result);
                return temperatureObject;
            }
            return null;
        }


        public static async Task<TemperatureModel> PostTemperature(TemperatureModel temperatureModel)
        {

            var jsonObject = JsonConvert.SerializeObject(temperatureModel);
            var httpContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Data", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var temperatureObject = JsonConvert.DeserializeObject<TemperatureModel>(result);
                return temperatureObject;
            }
            return null;
        }


        public static async Task<TemperatureModel> DeleteTemperature(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync("api/Data/" + id);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var temperatureObject = JsonConvert.DeserializeObject<TemperatureModel>(result);
                return temperatureObject;
            }
            return null;
        }

        static TemperatureModel DeleteTemperatures(int id)
        {
            var temperatureModel = new TemperatureModel();

            return temperatureModel;
        }

    }
}
