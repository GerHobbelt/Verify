namespace VerifyMSTest.SourceGenerator;

[Generator]
public class UsesVerifyGenerator : IIncrementalGenerator
{
    static string MarkerAttributeName => "VerifyMSTest.UsesVerifyAttribute";
    static string TestClassAttributeName => "Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var markerAttributes = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                fullyQualifiedMetadataName: MarkerAttributeName,
                predicate: IsSyntaxEligibleForGeneration,
                transform: TransformClass)
            .WhereNotNull()
            .WithTrackingName(TrackingNames.MarkerAttributeInitialTransform)
            .Collect();

        var assemblyAttributes = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: IsSyntaxEligibleForGeneration,
                transform: TransformAssembly)
            .WhereNotNull()
            .WithTrackingName(TrackingNames.AssemblyAttributeInitialTransform)
            .Collect();

        // Collect the classes to generate into a single collection so that we can write them to a single file and
        // avoid the issues of ambiguous hint names discussed in https://github.com/dotnet/roslyn/discussions/60272.
        var toGenerate = markerAttributes.Combine(assemblyAttributes)
            .SelectMany((classes, _) => classes.Left.AddRange(classes.Right))
            .WithTrackingName(TrackingNames.Merge)
            .Collect()
            .WithTrackingName(TrackingNames.Complete);

        context.RegisterSourceOutput(toGenerate, Execute);
    }

    static ClassToGenerate? TransformClass(GeneratorAttributeSyntaxContext context, Cancel cancel)
    {
        if (context.TargetSymbol is not INamedTypeSymbol symbol)
        {
            return null;
        }

        if (context.TargetNode is not TypeDeclarationSyntax syntax)
        {
            return null;
        }

        cancel.ThrowIfCancellationRequested();

        // Only run generator for classes when the parent won't _also_ have generation.
        // Otherwise the generator will hide the base member.
        if (HasParentWithMarkerAttribute(symbol))
        {
            return null;
        }

        return Parser.Parse(symbol, syntax, cancel);
    }

    static ClassToGenerate? TransformAssembly(GeneratorSyntaxContext context, Cancel cancel)
    {
        if (context.Node is not TypeDeclarationSyntax syntax)
        {
            return null;
        }

        var model = context.SemanticModel;
        if (!IsAssemblyEligibleForGeneration(model.Compilation.Assembly))
        {
            return null;
        }

        if (model.GetDeclaredSymbol(syntax, cancel) is not INamedTypeSymbol symbol)
        {
            return null;
        }

        if (HasTestClassAttribute(symbol))
        {
            return null;
        }

        // Only run generator for classes when the parent won't _also_ have generation.
        // Otherwise the generator will hide the base member.
        if (HasParentWithTestClassAttribute(symbol) ||
            HasParentWithMarkerAttribute(symbol))
        {
            return null;
        }

        return Parser.Parse(symbol, syntax, cancel);
    }

    static bool HasTestClassAttribute(INamedTypeSymbol symbol) => !symbol.HasAttributeOfType(TestClassAttributeName, includeDerived: true);

    static bool IsSyntaxEligibleForGeneration(SyntaxNode node, Cancel _) =>
        node is ClassDeclarationSyntax;

    static bool IsAssemblyEligibleForGeneration(IAssemblySymbol assembly) =>
        assembly.HasAttributeOfType(MarkerAttributeName, includeDerived: false);

    static bool HasParentWithMarkerAttribute(INamedTypeSymbol symbol) =>
        symbol
        .GetBaseTypes()
        .Any(_ => _.HasAttributeOfType(MarkerAttributeName, includeDerived: false));

    static bool HasParentWithTestClassAttribute(INamedTypeSymbol symbol) =>
        symbol
        .GetBaseTypes()
        .Any(_ => _.HasAttributeOfType(TestClassAttributeName, includeDerived: true));

    static void Execute(SourceProductionContext context, ImmutableArray<ClassToGenerate> toGenerate)
    {
        if (toGenerate.IsDefaultOrEmpty)
        {
            return;
        }

        var classes = toGenerate.Distinct();

        var emitter = new Emitter();
        var sourceCode = emitter.GenerateExtensionClasses(classes, context.CancellationToken);
        context.AddSource("UsesVerify.g.cs", SourceText.From(sourceCode, Encoding.UTF8));
    }
}