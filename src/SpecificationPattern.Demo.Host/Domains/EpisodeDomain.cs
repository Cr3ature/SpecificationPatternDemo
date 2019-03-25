using System.Threading.Tasks;
using SpecificationPattern.Demo.CrossCutting.Entities;
using SpecificationPattern.Demo.Host.Specifications;
using SpecificationPattern.Demo.Infrastructure.Repositories;

namespace SpecificationPattern.Demo.Host.Domains
{
    public class EpisodeDomain : IEpisodeDomain
    {
        private readonly IReadAsyncRepository<Episode> _episodeRepository;

        public EpisodeDomain(IReadAsyncRepository<Episode> episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        public Task<Episode[]> GetAllEpisodes()
            => _episodeRepository.GetAllAsync();

        public Task<Episode[]> GetAllEpisodesOrderedOnTitle()
            => _episodeRepository.GetAllAsync(new OrderEpisodesOnNameSpecification());
        public Task<Episode[]> GetPagedEpisodes(int pageIndex, int pageSize)
            => _episodeRepository.GetAllAsync(new PagedEpisodesSpecification(pageIndex, pageSize));
    }
}
