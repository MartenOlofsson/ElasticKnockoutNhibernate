using FluentNHibernate.Mapping;

namespace PetInfo.Web.models
{
    public class PetMap : ClassMap<Pet>
    {
        public PetMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.Owner, "Owner_id").Cascade.None();
        }
    }
}