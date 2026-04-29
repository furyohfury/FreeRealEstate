using Unity.Entities;

namespace Game.GameEngine
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class GameplayGroup : ComponentSystemGroup
    {
        public GameplayGroup()
        {
            EnableSystemSorting = false;
        }
    }
}
