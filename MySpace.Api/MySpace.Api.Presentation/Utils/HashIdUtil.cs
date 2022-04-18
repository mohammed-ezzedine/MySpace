using HashidsNet;

namespace MySpace.Api.Presentation.Utils;

public class HashIdUtil
{
    private readonly Hashids _hasher;

    public HashIdUtil(string salt)
    {
        _hasher = new Hashids(salt, 11);
    }

    public string EncodeId(int id)
    {
        return _hasher.Encode(id);
    }

    public int DecodeId(string encodedId)
    {
        return _hasher.DecodeSingle(encodedId);
    }
}