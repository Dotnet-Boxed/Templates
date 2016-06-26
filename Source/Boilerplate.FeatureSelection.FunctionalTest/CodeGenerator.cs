namespace Boilerplate.FeatureSelection.FunctionalTest
{
    using System.Linq;
    using Autofac;
    using Boilerplate.FeatureSelection.Features;

    public static class CodeGenerator
    {
        public static void GetInlineData()
        {
            var types = typeof(IFeature).Assembly.GetTypes();
            var binaryChoiceFeatures = string.Join("\r\n", types
                .Where(x => x.IsAssignableTo<IBinaryChoiceFeature>())
                .Where(x => x != typeof(IBinaryChoiceFeature))
                .Where(x => x != typeof(BinaryChoiceFeature))
                .Select(x => x.Name)
                .OrderBy(x => x)
                .Select(x => $"[InlineData(typeof({x}), true)]\r\n[InlineData(typeof({x}), false)]"));
            var multiChoiceFeatures = string.Join("\r\n", types
                .Where(x => x.IsAssignableTo<IMultiChoiceFeature>())
                .Where(x => x != typeof(IMultiChoiceFeature))
                .Where(x => x != typeof(MultiChoiceFeature))
                .Select(x => x.Name)
                .OrderBy(x => x)
                .Select(x => $"[InlineData(typeof({x}), 0)]\r\n[InlineData(typeof({x}), 1)]"));
        }
    }
}
