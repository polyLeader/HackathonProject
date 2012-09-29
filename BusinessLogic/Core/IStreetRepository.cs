using System.Collections.Generic;
using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public interface IStreetRepository
    {
        Street Add(Street street);
        Street Update(Street street);
        void Delete(Street stret);
        IList<Street> GetAll();
        Street GetById(int id);
    }
}