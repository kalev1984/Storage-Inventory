namespace Base.Contracts.DAL;

public interface IBaseMapper<TLeftObject, TRightObject>
{
    TLeftObject? Map(TRightObject? inObject);
    TRightObject? Map(TLeftObject? inObject);
}