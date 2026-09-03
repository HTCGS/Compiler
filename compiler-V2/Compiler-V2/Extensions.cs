public static class Extensions
{
    public static void PrintTokens(this List<Token> tokens)
    {
        Console.WriteLine("Tokens:");
        Console.WriteLine("{0,-15} | {1}", "Type", "Lexeme");
        Console.WriteLine(new string('-', 30));

        foreach (var token in tokens)
        {
            Console.WriteLine("{0,-15} | {1}", token.Type, token.Lexeme);
        }
    }

    public static void Print(this SyntaxNode node, int depth = 0)
    {
        if (node is Assign assign)
        {
            System.Console.WriteLine($"Assign:");
            System.Console.Write("|-");
            for (int i = 0; i < depth; i++)
            {
                System.Console.Write("-");
            }
            // System.Console.WriteLine("Variable: ");
            assign.Variable.Print();
            System.Console.Write("|-");
            for (int i = 0; i < depth; i++)
            {
                System.Console.Write("-");
            }
            // System.Console.WriteLine("Expression: ");
            assign.Expression.Print(depth + 1);
        }
        else if (node is Operation operation)
        {
            System.Console.WriteLine($"{operation.Name}:");
            // System.Console.WriteLine("Left: ");
            System.Console.Write("|-");
            for (int i = 0; i < depth; i++)
            {
                System.Console.Write("-");
            }
            operation.Left.Print(depth + 1);
            // System.Console.WriteLine("Right: ");
            System.Console.Write("|-");
            for (int i = 0; i < depth; i++)
            {
                System.Console.Write("-");
            }
            operation.Right.Print(depth + 1);
        }
        else if (node is Variable variable)
        {
            System.Console.WriteLine($"Variable: {variable.Name}");
        }
        else if (node is Constant constant)
        {
            System.Console.WriteLine($"Constant: {constant.Value}");
        }
    }
}