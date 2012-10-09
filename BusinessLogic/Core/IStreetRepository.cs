using System.Collections.Generic;
using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public interface IStreetRepository
    {
        Street Add(Street street);
        IEnumerable<Street> GetAll();
        Street GetById(int id);
        int GetIdByName(string streetName);
        string GetNameById(int streetId);
    }
}