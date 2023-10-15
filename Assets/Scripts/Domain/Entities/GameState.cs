using System.Collections.Generic;

namespace Highborne.Domain.Entities
{
    public class GameState
    {
        public bool isPaused;
        public List<Player> players;
        public float gameTime;
    }
}