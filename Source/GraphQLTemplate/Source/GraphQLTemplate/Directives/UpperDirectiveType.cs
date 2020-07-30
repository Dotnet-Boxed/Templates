namespace GraphQLTemplate.Directives
{
    using System;
    using HotChocolate.Types;

    public class UpperDirectiveType : DirectiveType
    {
        protected override void Configure(IDirectiveTypeDescriptor descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            descriptor
                .Name("upper")
                .Location(DirectiveLocation.Field)
                .Use(next => async context =>
                {
                    await next.Invoke(context).ConfigureAwait(false);

                    if (context.Result is string value)
                    {
                        context.Result = value.ToUpperInvariant();
                    }
                });
        }
    }
}
