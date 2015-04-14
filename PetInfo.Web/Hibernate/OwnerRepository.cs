using System.Collections.Generic;
using System.Linq;
using NHibernate;
using PetInfo.Web.models;

namespace PetInfo.Web.Hibernate
{
    public class OwnerRepository
    {
        private readonly ISessionFactory _sessionFactory;

        public OwnerRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IEnumerable<T> GetAll<T>() where T : Owner
        {
            IList<T> owners;

            using (var session = _sessionFactory.OpenSession())
            {
                owners = session.QueryOver<T>()
                    .Fetch(x => x.Pets).Eager.List();
            }

            return owners;
        }

        public T GetOwner<T>(int id)
        {
            T obj;
            using (var session = _sessionFactory.OpenSession())
            {
                obj = session.Get<T>(id);
            }
            return obj;
        }

        public void AddOwners()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var pet = new Pet
                    {
                        Name = "Lilla gubben",

                    };

                    var owner = new Owner
                    {
                        Age = 123,
                        Name = "Mårten",
                    };
                    owner.Pets.Add(pet);
                    pet.Owner = owner;
                    session.Save(pet.Owner);
                    session.Save(pet);
                    tx.Commit();
                }
            }
        }

        public void Save<T>(T obj)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(obj);
                    tx.Commit();
                }
            }
        }
    }
}