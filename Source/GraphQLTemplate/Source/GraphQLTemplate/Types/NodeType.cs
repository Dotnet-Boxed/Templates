namespace GraphQLTemplate.Types
{
    using System;
    using GraphQLTemplate.Models;
    using HotChocolate.Types;

    public class NodeType : InterfaceType<INode>
    {
        protected override void Configure(IInterfaceTypeDescriptor<INode> descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            descriptor.Name("Node");
            descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
        }
    }
}
