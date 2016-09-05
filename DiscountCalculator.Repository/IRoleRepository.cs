using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountCalculator.Repository
{
    public interface IRoleRepository
    {
        bool IsInRole(int userId, string role);
    }
}
