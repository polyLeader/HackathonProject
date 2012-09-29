using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    class StreetRepository : IStreetRepository
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

        public Street Update(Street street)
        {
            _databaseContext.Entry(street).State = EntityState.Modified;
            return street;
        }

        public void Delete(Street stret)
        {
            _databaseContext.Entry(stret).State = EntityState.Deleted;
        }

        public IList<Street> GetAll()
        {
            return _databaseContext.Streets.ToArray();
        }

        public Street GetById(int id)
        {
            return _databaseContext.Streets.FirstOrDefault(x => x.Id == id);
        }
    }
}
