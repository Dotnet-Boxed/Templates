namespace GraphQLTemplate.Directives
{
    using HotChocolate.Types;

    public class LowerDirectiveType : DirectiveType
    {
        protected override void Configure(IDirectiveTypeDescriptor descriptor) =>
            descriptor
                .Name("lower")
                .Location(DirectiveLocation.Field)
                .Use(next => async context =>
                {
                    await next.Invoke(context).ConfigureAwait(false);

                    if (context.Result is string value)
                    {
                        context.Result = value.ToLowerInvariant();
                    }
                });
    }
}
