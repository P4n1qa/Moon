using Root.Player.Inventory;

namespace Root.Player
{
    public interface IPlayerController
    {
        IPlayerInventory PlayerInventory { get;}
    }
}