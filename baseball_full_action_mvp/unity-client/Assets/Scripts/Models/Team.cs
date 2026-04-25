using System;
using System.Collections.Generic;

namespace BaseballGame.Models
{
    [Serializable]
    public class Team
    {
        public string id;
        public string name;
        public int budget;
        public List<Player> roster = new List<Player>();

        public bool CanAfford(int price)
        {
            return budget >= price;
        }

        public void AddPlayer(Player player)
        {
            roster.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            roster.Remove(player);
        }
    }
}
