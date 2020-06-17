namespace ModelsAndProps
{
    public interface IWriter
    {
        void SendToDumpingBuffer();
        void SendToHistorical();
    }
}
