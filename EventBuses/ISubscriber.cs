namespace Valossy.EventBuses;

public interface ISubscriber
{
    public ulong GetInstanceId();

    public virtual int Priority => 0;
}