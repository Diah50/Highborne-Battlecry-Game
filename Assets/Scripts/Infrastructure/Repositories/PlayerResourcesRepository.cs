using Highborne.Domain.Entities;
using Highborne.Domain.Repository;

namespace Highborne.Infrastructure.Repositories
{
    public class PlayerResourcesRepository : IPlayerResourcesRepository
    {
        private readonly PlayerResources _playerResources = new();

        public PlayerResources GetPlayerResources() => _playerResources;

        public void SetGold(int value) => _playerResources.Gold = value;
        public void AddGold(int value) => _playerResources.Gold += value;
        public void SubtractGold(int value) => _playerResources.Gold -= value;

        public void SetFood(int value) => _playerResources.Food = value;
        public void AddFood(int value) => _playerResources.Food += value;
        public void SubtractFood(int value) => _playerResources.Food -= value;

        public void SetWood(int value) => _playerResources.Wood = value;
        public void AddWood(int value) => _playerResources.Wood += value;
        public void SubtractWood(int value) => _playerResources.Wood -= value;

        public void SetStone(int value) => _playerResources.Stone = value;
        public void AddStone(int value) => _playerResources.Stone += value;
        public void SubtractStone(int value) => _playerResources.Stone -= value;

        public void SetMetal(int value) => _playerResources.Metal = value;
        public void AddMetal(int value) => _playerResources.Metal += value;
        public void SubtractMetal(int value) => _playerResources.Metal -= value;

        public void SetCrystal(int value) => _playerResources.Crystal = value;
        public void AddCrystal(int value) => _playerResources.Crystal += value;
        public void SubtractCrystal(int value) => _playerResources.Crystal -= value;
    }
}