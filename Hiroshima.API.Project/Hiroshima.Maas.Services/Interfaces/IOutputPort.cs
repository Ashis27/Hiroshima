

namespace Hiroshima.Maas.Services.Interfaces
{
    public interface IOutputPort<in TResponse>
    {
        void Handle(TResponse response);
    }
}
