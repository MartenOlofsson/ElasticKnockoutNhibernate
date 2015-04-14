using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace PetInfo.Web.models
{
    public class OwnerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Owner
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
        public virtual IList<Pet> Pets { get; set; }

        public Owner()
        {
            Pets = new List<Pet>();
        }
    }

    public class OwnerMap : ClassMap<Owner>
    {
        public OwnerMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Age);
            HasMany(x => x.Pets)
                .KeyColumn("Owner_id")
                .Inverse()
                .Cascade.All();
        }
    }
}