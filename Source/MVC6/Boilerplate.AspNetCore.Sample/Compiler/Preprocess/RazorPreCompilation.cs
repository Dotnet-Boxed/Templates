#if !DEBUG
namespace MvcBoilerplate.Compiler.Preprocess
{
    using System;
    using Microsoft.AspNetCore.Mvc.Razor.Precompilation;

    /// <summary>
    /// Enable pre-compilation of Razor views, so that errors in your .cshtml files are caught and displayed
    /// in the Visual Studio errors window at compile time, rather than your sites users receiving a runtime 500
    /// internal server error. Pre-compilation may reduce the time it takes to build and launch your project but will
    /// cause the build time to increase. It will also stop edit and continue working for .cshtml files.
    /// </summary>
    public class RazorPreCompilation : RazorPreCompileModule
    {
    }
}
#endif