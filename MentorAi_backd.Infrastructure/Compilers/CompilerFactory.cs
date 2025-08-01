using System;
using com.sun.tools.@internal.xjc.api;
using MentorAi_backd.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MentorAi_backd.Infrastructure.Compilers
{
    /// <summary>
    /// Factory class to resolve appropriate ICompiler implementation based on language.
    /// </summary>
    public class CompilerFactory 
    {
        private readonly IServiceProvider _provider;

        public CompilerFactory(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        /// <summary>
        /// Returns the compiler instance for the given language.
        /// </summary>
        public ICompiler GetCompiler(string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                throw new ArgumentException("Language cannot be null or empty.", nameof(language));

            return language.ToLowerInvariant() switch
            {
                "csharp" or "cs" => _provider.GetRequiredService<CSharpCompiler>(),
                "python" => _provider.GetRequiredService<PythonCompiler>(),
                "javascript" or "js" => _provider.GetRequiredService<JavaScriptCompiler>(),
                "typescript" or "ts" => _provider.GetRequiredService<TypeScriptCompiler>(),
                "java" => _provider.GetRequiredService<JavaCompiler>(),
                "php" => _provider.GetRequiredService<PhpCompiler>(),
                "ruby" => _provider.GetRequiredService<RubyCompiler>(),
                "go" => _provider.GetRequiredService<GoCompiler>(),
                "c" => _provider.GetRequiredService<CCompiler>(),
                "cpp" or "c++" => _provider.GetRequiredService<CppCompiler>(),
                _ => throw new NotSupportedException($"Language '{language}' is not supported.")
            };
        }
    }
}