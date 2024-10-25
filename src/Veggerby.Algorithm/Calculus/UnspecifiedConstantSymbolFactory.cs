namespace Veggerby.Algorithm.Calculus;

public class UnspecifiedConstantSymbolFactory
{
    private const string _unspecifiedConstantNames = "CABDEFGHIJKLMNOPQRSTUVW";

    private readonly IDictionary<Guid, string> _allocatedNamed = new Dictionary<Guid, string>();

    internal string Next()
    {
        var name = _unspecifiedConstantNames[_allocatedNamed.Count % _unspecifiedConstantNames.Length];

        var ixc = _allocatedNamed.Count / _unspecifiedConstantNames.Length;

        if (ixc > 0)
        {
            return $"{name}{ixc}";
        }

        return name.ToString();
    }

    public string Get(Guid instanceId)
    {
        if (!_allocatedNamed.ContainsKey(instanceId))
        {
            _allocatedNamed.Add(instanceId, Next());
        }

        return _allocatedNamed[instanceId];
    }

    internal static int Compare(string a, string b)
    {
        try
        {
            var aIxc = _unspecifiedConstantNames.IndexOf(a[0]);
            var bIxc = _unspecifiedConstantNames.IndexOf(b[0]);

            if (aIxc == bIxc)
            {
                var aNumber = int.Parse(a.Substring(1));
                var bNumber = int.Parse(b.Substring(1));

                return aNumber.CompareTo(bNumber);
            }

            return aIxc.CompareTo(bIxc);
        }
        catch (Exception)
        {
            throw new Exception("Undefined Constant name format invalid");
        }
    }
}