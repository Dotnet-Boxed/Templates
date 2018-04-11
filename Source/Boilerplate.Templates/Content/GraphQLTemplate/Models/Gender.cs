namespace ApiTemplate.Models
{
#if (GraphQL)
    using System.ComponentModel;

#endif
    public enum Gender
    {
#if (GraphQL)

        [Description("The male gender.")]
#endif
        Male,

#if (GraphQL)
        [Description("The female gender.")]
#endif
        Female
    }
}
