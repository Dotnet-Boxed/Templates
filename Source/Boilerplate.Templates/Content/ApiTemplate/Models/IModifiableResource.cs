namespace ApiTemplate
{
    using System;

    public interface IModifiableResource
    {
        string ETag { get; }

        DateTimeOffset? LastModified { get; }
    }
}
