using System;
using BaseballGame.Models;

namespace BaseballGame.Market
{
    [Serializable]
    public class MarketListing
    {
        public string listingId;
        public Player player;
        public string sellerTeamId;
        public int price;
        public bool sold;
    }
}
