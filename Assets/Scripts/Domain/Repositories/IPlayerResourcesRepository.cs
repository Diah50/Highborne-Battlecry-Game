using Highborne.Domain.Entities;

namespace Highborne.Domain.Repository
{
    public interface IPlayerResourcesRepository
    {
        PlayerResources GetPlayerResources();
        
        void SetGold(int value);
        void AddGold(int value);
        void SubtractGold(int value);

        void SetFood(int value);
        void AddFood(int value);
        void SubtractFood(int value);

        void SetWood(int value);
        void AddWood(int value);
        void SubtractWood(int value);

        void SetStone(int value);
        void AddStone(int value);
        void SubtractStone(int value);

        void SetMetal(int value);
        void AddMetal(int value);
        void SubtractMetal(int value);

        void SetCrystal(int value);
        void AddCrystal(int value);
        void SubtractCrystal(int value);
    }
}
