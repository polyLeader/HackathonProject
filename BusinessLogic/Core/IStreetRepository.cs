using System.Collections.Generic;
using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public interface IStreetRepository
    {
        Street Add(Street street);
        IList<Street> GetAll();
        Street GetById(int id);
    }
}