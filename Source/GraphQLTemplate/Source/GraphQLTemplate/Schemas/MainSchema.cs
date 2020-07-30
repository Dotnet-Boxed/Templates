namespace GraphQLTemplate.Schemas
{
    using System;
    using HotChocolate;
    using HotChocolate.Types;

    public class MainSchema : Schema
    {
        protected override void Configure(ISchemaTypeDescriptor descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            descriptor.Description("This is my schema description that can be accessed by introspection");
        }
    }
}
