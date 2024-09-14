namespace ERP.Share.Abstractions.Message;
public class IQueryResult
{
}

public class DynamicQueryResult : IQueryResult
{
    public IReadOnlyList<dynamic> Data { get; }

    public DynamicQueryResult(IReadOnlyList<dynamic> data)
    {
        Data = data;
    }
}
