public class AbstractUniqueData
{
    private static uint ID = 0;

    public uint uniqueId { private set; get; }

    public AbstractUniqueData()
    {
        uniqueId = ID++;
    }
}