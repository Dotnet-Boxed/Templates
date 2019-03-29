namespace ApiTemplate.ViewModelSchemaFilters
{
    using System.Collections.Generic;
    using ApiTemplate.ViewModels;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class PageResultCarSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var pageResult = new PageResult<Car>()
            {
                Count = 2,
                Items = new List<Car>()
                {
                    new Car()
                    {
                        CarId = 1,
                        Cylinders = 6,
                        Make = "Honda",
                        Model = "Civic",
                    },
                    new Car()
                    {
                        CarId = 2,
                        Cylinders = 8,
                        Make = "Lambourghini",
                        Model = "Countach",
                    },
                },
                Page = 1,
                TotalCount = 50,
                TotalPages = 10,
            };
            model.Default = pageResult;
            model.Example = pageResult;
        }
    }
}
