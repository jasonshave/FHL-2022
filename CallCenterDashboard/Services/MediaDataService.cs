using CallingDashboard.Interfaces;
using CallingDashboard.Models;

namespace CallingDashboard.Services;

public class MediaDataService : IMediaDataService
{
    private readonly IRepository<MediaData> _mediaRepository;

    public MediaDataService(IRepository<MediaData> mediaRepository)
    {
        _mediaRepository = mediaRepository;
    }

    public void Upsert(MediaData mediaData) => _mediaRepository.Update(mediaData.Id, mediaData);

    public MediaData? Get(string id) => _mediaRepository.Find(id);

    public IEnumerable<MediaData> List() => _mediaRepository.List();

    public void Remove(string id) => _mediaRepository.Remove(id);
}