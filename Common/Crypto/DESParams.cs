namespace Common.Crypto
{
    public class DESParams
    {
        public byte[] IV;
        public byte[] Key;

        public DESParams(byte[] IV, byte[] Key)
        {
            this.IV = IV;
            this.Key = Key;
        }
    }
}