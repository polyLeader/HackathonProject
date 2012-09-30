using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BusinessLogic.Core;
using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public class StreetRepository : IStreetRepository
    {
        private readonly DatabaseContext _databaseContext;

        public StreetRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Street Add(Street street)
        {
            _databaseContext.Streets.Add(street);
            _databaseContext.SaveChanges();
            return street;
        }

        public IEnumerable<Street> GetAll()
        {
            return _databaseContext.Streets;
        }

        public Street GetById(int id)
        {
            return _databaseContext.Streets.FirstOrDefault(x => x.Id == id);
        }
    }
}
