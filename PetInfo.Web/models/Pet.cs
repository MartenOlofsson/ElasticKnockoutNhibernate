using Newtonsoft.Json;

namespace PetInfo.Web.models
{
    public class Pet
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        [JsonIgnore]
        public virtual Owner Owner { get; set; }
    }
}