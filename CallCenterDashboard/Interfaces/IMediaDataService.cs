using CallingDashboard.Models;

namespace CallingDashboard.Interfaces;

public interface IMediaDataService
{
    void Upsert(MediaData mediaData);
    MediaData? Get(string id);

    IEnumerable<MediaData> List();

    void Remove(string id);
}