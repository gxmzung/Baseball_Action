using System;
using System.Collections.Generic;
using UnityEngine;
using BaseballGame.Models;

namespace BaseballGame.Market
{
    public class MarketManager : MonoBehaviour
    {
        public List<MarketListing> listings = new List<MarketListing>();

        public MarketListing ListPlayer(Player player, string sellerTeamId, int price)
        {
            var listing = new MarketListing
            {
                listingId = Guid.NewGuid().ToString(),
                player = player,
                sellerTeamId = sellerTeamId,
                price = price,
                sold = false
            };

            listings.Add(listing);
            return listing;
        }

        public bool BuyPlayer(Team buyer, MarketListing listing)
        {
            if (listing == null || listing.sold) return false;
            if (!buyer.CanAfford(listing.price)) return false;

            buyer.budget -= listing.price;
            buyer.AddPlayer(listing.player);
            listing.sold = true;
            return true;
        }
    }
}
