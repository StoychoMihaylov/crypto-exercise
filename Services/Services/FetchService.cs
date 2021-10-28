using Models.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Services
{
    public class FetchService : IFetchService
    {
        private readonly HttpClient httpClient;

        public FetchService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ICollection<CoinPriceDTO>> RequestGetCoinPrices(string[] coins)
        {
            var listofCoinSymbols = new List<CoinSymbolListDTO>();
            var detailedCointList = new List<CoinPriceDTO>();

            var response = await this.httpClient.GetAsync("https://api.coingecko.com/api/v3/coins/list");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                listofCoinSymbols.AddRange(JsonSerializer.Deserialize<ICollection<CoinSymbolListDTO>>(response.Content.ReadAsStringAsync().Result));
            }
            else
            {
                throw new Exception("bam");
            }

            foreach (var symbol in listofCoinSymbols)
            {
                if (coins.Contains(symbol.Name))
                {
                    detailedCointList.Add(await this.RequestGetCoinDetails(symbol.Id));
                }
            }

            return detailedCointList;
        }

        private async Task<CoinPriceDTO> RequestGetCoinDetails(string id)
        {
            var response = await this.httpClient.GetAsync("https://api.coingecko.com/api/v3/coins/" + id);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.Deserialize<CoinPriceDTO>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                throw new Exception("bam");
            }
        }
    }
}
