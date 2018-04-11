namespace ApiTemplate.Types
{
    using System;
    using System.Linq;
    using ApiTemplate.Models;
    using GraphQL.Types;

    public class GenderType : EnumerationGraphType
    {
        public GenderType()
        {
            var type = typeof(Gender);
            this.Name = nameof(Gender);
            this.Description = type.GetDescription();
            foreach (var item in Enum
                .GetValues(type)
                .Cast<Gender>()
                .Select(x => (Name: x.ToString(), Value: (int)x)))
            {
                this.AddValue(item.Name, type.GetDescription(item.Name), item.Value);
            }
        }
    }
}
