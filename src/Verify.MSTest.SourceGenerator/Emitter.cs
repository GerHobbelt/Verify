class Emitter
{
    static readonly string AutoGenerationHeader = """
        //-----------------------------------------------------
        // This code was generated by a tool.
        //
        // Changes to this file may cause incorrect behavior
        // and will be lost when the code is regenerated.
        // <auto-generated />
        //-----------------------------------------------------
        """;

    static readonly string GeneratedCodeAttribute =
        $"[global::System.CodeDom.Compiler.GeneratedCodeAttribute(\"{typeof(Emitter).Assembly.GetName().Name}\", \"{typeof(Emitter).Assembly.GetName().Version}\")]";

    readonly IndentedStringBuilder builder = new();

    void WriteNamespace(ClassToGenerate classToGenerate)
    {
        if (classToGenerate.Namespace is not null)
        {
            builder.Append("namespace ").AppendLine(classToGenerate.Namespace)
              .AppendLine("{")
              .IncreaseIndent();
        }

        WriteParentTypes(classToGenerate);

        if (classToGenerate.Namespace is not null)
        {
            builder.DecreaseIndent()
              .AppendLine("}");
        }
    }

    void WriteParentTypes(ClassToGenerate classToGenerate)
    {
        foreach (var parentClass in classToGenerate.ParentClasses)
        {
            builder.Append("partial ").Append(parentClass.Keyword).Append(" ").AppendLine(parentClass.Name)
              .AppendLine("{");

            builder.IncreaseIndent();
        }

        WriteClass(classToGenerate);

        foreach (var _ in classToGenerate.ParentClasses)
        {
            builder.DecreaseIndent()
              .AppendLine("}");
        }
    }

    void WriteClass(ClassToGenerate classToGenerate) =>
        builder.AppendLine(GeneratedCodeAttribute)
          .Append("partial class ").AppendLine(classToGenerate.ClassName)
          .AppendLine("{")
          .AppendLine("    public TestContext TestContext")
          .AppendLine("    {")
          .AppendLine("        get => Verifier.CurrentTestContext.Value!.TestContext;")
          .AppendLine("        set => Verifier.CurrentTestContext.Value = new TestExecutionContext(value, GetType());")
          .AppendLine("    }")
          .AppendLine("}");

    public string GenerateExtensionClasses(IReadOnlyCollection<ClassToGenerate> classesToGenerate, Cancel cancel)
    {
        builder.AppendLine(AutoGenerationHeader);

        foreach (var classToGenerate in classesToGenerate)
        {
            cancel.ThrowIfCancellationRequested();

            builder.AppendLine();
            WriteNamespace(classToGenerate);
        }

        return builder.ToString();
    }
}
