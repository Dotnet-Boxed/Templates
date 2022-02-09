namespace ApiTemplate.Constants;

public static class OpenTelemetryHttpFlavour
{
    public const string Http10 = "1.0";
    public const string Http11 = "1.1";
    public const string Http20 = "2.0";
    public const string Http30 = "3.0";

    public static string GetHttpFlavour(string protocol)
    {
        if (HttpProtocol.IsHttp10(protocol))
        {
            return Http10;
        }
        else if (HttpProtocol.IsHttp11(protocol))
        {
            return Http11;
        }
        else if (HttpProtocol.IsHttp2(protocol))
        {
            return Http20;
        }
        else if (HttpProtocol.IsHttp3(protocol))
        {
            return Http30;
        }

        throw new InvalidOperationException($"Protocol {protocol} not recognised.");
    }
}
