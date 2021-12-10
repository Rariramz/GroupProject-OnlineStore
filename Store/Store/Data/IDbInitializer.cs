namespace Store.Data
{
    public interface IDbInitializer
    {
        public Task Seed();
    }
}
