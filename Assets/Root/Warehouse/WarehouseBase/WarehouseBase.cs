using System.Collections.Generic;
using Root.Warehouse.Factory;
using UnityEngine;

namespace Root.Warehouse.WarehouseBase
{
    public abstract class WarehouseBase : MonoBehaviour
    {
        [SerializeField] private bool canPlayerTakeItems;
        [SerializeField] protected Transform houseSpawnPosition;
        
        public bool CanPlayerTakeItems { get; private set; }
        
        public int rows; 
        public int columns;
        
        protected List<Cell> cells = new();
        protected List<Cell> emptyCells = new();
        protected IFactory factory;
        
        public virtual void Initialize(IFactory factory)
        {
            CanPlayerTakeItems = canPlayerTakeItems;
            this.factory = factory;
            FillCells();
        }
        
        private List<Vector3> DividePlane()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            Bounds bounds = meshRenderer.bounds;

            float planeWidth = bounds.size.x;
            float planeHeight = bounds.size.z;
        
            float partWidth = planeWidth / columns;
            float partHeight = planeHeight / rows;
        
            Vector3 startPos = transform.position - new Vector3(planeWidth / 2, 0, planeHeight / 2);
            
            List<Vector3> points = new List<Vector3>();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    float centerX = startPos.x + (col * partWidth) + partWidth / 2;
                    float centerZ = startPos.z + (row * partHeight) + partHeight / 2;

                    Vector3 centerPosition = new Vector3(centerX, startPos.y, centerZ);
                 
                    points.Add(centerPosition);
                }
            }
            return points;
        }
        
        private void FillCells()
        {
            List<Vector3> positions = DividePlane();
            for (int i = 0; i < positions.Count; i++)
            {
                Cell cell = new Cell
                {
                    Position = positions[i],
                    IsFull = false
                };
                cells.Add(cell);
            }
        }
    }
}