using HotelReservations.Model;
using HotelReservations.Repositories;
using System.Collections.Generic;

namespace HotelReservations.Service
{
    public class PriceService
    {
        IPriceRepository priceRepository;

        public PriceService()
        {
            priceRepository = new PriceRepositoryDB();
        }

        public List<Price> GetAllPrices()
        {
            return Hotel.GetInstance().Prices;
        }

        public void SavePrice(Price price)
        {
            if (price.price_id == 0)
            {
                Hotel.GetInstance().Prices.Add(price);
                price.price_id = priceRepository.Insert(price);
            }
            else
            {
                var index = Hotel.GetInstance().Prices.FindIndex(p => p.price_id == price.price_id);
                Hotel.GetInstance().Prices[index] = price;
                priceRepository.Update(price);
            }
        }

        public void MakePriceInactive(Price price)
        {
            var makePriceInactive = Hotel.GetInstance().Prices.Find(p => p.price_id == price.price_id);
            makePriceInactive.price_is_active = false;
            priceRepository.Delete(price.price_id);
        }
    }
}
