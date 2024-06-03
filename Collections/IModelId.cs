namespace Valossy.Collections;

public interface IModelId : IHaveModelKey
{
    public object Id { get; set; }
}