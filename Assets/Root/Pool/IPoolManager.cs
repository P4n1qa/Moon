namespace Root.Pool
{
    public interface IPoolManager
    {
        public PoolUse GetPool(PoolEnum poolEnum);
    }
}