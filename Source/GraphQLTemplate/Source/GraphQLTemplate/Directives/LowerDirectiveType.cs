namespace GraphQLTemplate.Directives
{
    using System;
    using HotChocolate.Types;

    public class LowerDirectiveType : DirectiveType
    {
        protected override void Configure(IDirectiveTypeDescriptor descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            descriptor
                .Name("lower")
                .Location(DirectiveLocation.Field)
                .Use(next => async context =>
                {
                    await next.Invoke(context).ConfigureAwait(false);

                    if (context.Result is string value)
                    {
#pragma warning disable CA1308 // Normalize strings to uppercase
                        context.Result = value.ToLowerInvariant();
#pragma warning restore CA1308 // Normalize strings to uppercase
                    }
                });
        }
    }
}
