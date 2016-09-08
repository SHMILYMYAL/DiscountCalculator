namespace DiscountCalculator.Repository
{
    public interface IRoleRepository
    {
        bool IsInRole(int userId, string role);
    }
}
