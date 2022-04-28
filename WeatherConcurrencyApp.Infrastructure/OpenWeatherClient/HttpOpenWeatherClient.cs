﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherConcurrencyApp.Common;
using WeatherConcurrentApp.Domain.Entities;
using WeatherConcurrentApp.Domain.Interfaces;
using Newtonsoft.Json;
using System.IO;

namespace WeatherConcurrencyApp.Infrastructure.OpenWeatherClient
{
    public class HttpOpenWeatherClient : IHttpOpenWeatherClient
    {
        public async Task<OpenWeather> GetWeatherByCityNameAsync(string city)
        {
            string url = $"{AppSettings.ApiUrl}{city}&units={AppSettings.units}&lang=sp&appid={AppSettings.Token}";
            string jsonObject = string.Empty;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    jsonObject = await httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();
                    
                }
           
                if (string.IsNullOrEmpty(jsonObject))
                {
                    throw new NullReferenceException("El objeto json no puede ser null.");
                }

                return Newtonsoft.Json.JsonConvert.DeserializeObject<OpenWeather>(jsonObject);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public OpenWeather Extraer()
        {
            string j = @"\WeatherConcurrencyApp.Infrastructure\Cities.json";
            string path = Path.GetFullPath("Cities.json").Replace(@"\WeatherConcurrencyApp\bin\Debug\Cities.json", string.Empty) + j;

            Console.WriteLine(path);
            if (File.Exists(path) == false)
            {
                Console.WriteLine("No existe");
              
            }
            else
                Console.WriteLine("Si existe");

            var jsonString = File.ReadAllText(path);
            OpenWeather openWeather = JsonConvert.DeserializeObject<OpenWeather>(jsonString);
            Console.WriteLine(openWeather.Sys.City);
            return openWeather;
            
        }

        public object PasarDato(object T)
        {
            return T;
        }
    }
}






