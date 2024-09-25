using Root.Player.Body;
using Root.Player.Inventory;
using Root.Player.PlayerMovement;
using UnityEngine;

namespace Root.Player
{
    public class PlayerController: MonoBehaviour,IPlayerController
    {
        [SerializeField] private PlayerMovementSystem playerMovementSystem;
        [SerializeField] private PlayerBody playerBody;
        [SerializeField] private PlayerInventory playerPlayerInventory;
        
        public IPlayerInventory PlayerInventory { get; private set;}

        public void InitializeSystems(FixedJoystick joystick)
        {
            PlayerInventory = playerPlayerInventory;
            
            PlayerInventory.Initialize();
            playerMovementSystem.Initialize(joystick);
            playerBody.Initialize(this);
        }
    }
}