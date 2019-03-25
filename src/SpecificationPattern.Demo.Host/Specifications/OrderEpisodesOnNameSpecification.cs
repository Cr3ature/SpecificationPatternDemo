using SpecificationPattern.Demo.CrossCutting.Entities;
using SpecificationPattern.Demo.CrossCutting.Enums;

namespace SpecificationPattern.Demo.Host.Specifications
{
    public class OrderEpisodesOnNameSpecification : BaseSpecification<Episode>
    {
        public OrderEpisodesOnNameSpecification()
            : base(null)
        {
            AddOrderBy(o => o.Title, OrderByDirection.Ascending);
        }
    }
}
