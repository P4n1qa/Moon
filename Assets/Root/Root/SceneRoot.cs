using System.Collections.Generic;
using Root.Player;
using Root.Warehouse.Factory;
using UnityEngine;

namespace Root.Root
{
    public class SceneRoot: MonoBehaviour
    {
        [SerializeField] private List<Factory> _factories;
        [SerializeField] private PlayerController _playerController;

        private void Start()
        {
            foreach (var factory in _factories)
            {
                factory.Initialize();
            }

            _playerController.InitializeSystems(Root.Instance.FixedJoystick);
        }
    }
}