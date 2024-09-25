using Root.Pool;
using UnityEngine;

namespace Root.Root
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private PoolManager poolManager;
        [SerializeField] private FixedJoystick fixedJoystick;
        
        public static Root Instance;
        
        public IPoolManager PoolManager => poolManager;
        public FixedJoystick FixedJoystick => fixedJoystick;
        
        private void Awake()
        {
            InitializeSingleton();
        }

        private void InitializeSingleton()
        {
            if (Instance == null) 
            { 
                Instance = this; 
            } 
            else if(Instance == this)
            { 
                Destroy(gameObject); 
            }
            
            DontDestroyOnLoad(gameObject);
        }
    }
}