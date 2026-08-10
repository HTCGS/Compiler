using System.Linq.Expressions;

public class ASTParser
{
    public Expression Parse(SyntaxNode syntaxNode)
    {
        Expression expr = Expression.Empty();
        // if (syntaxNode is Variable variable)
        // {
        //     var res = VariableManager.GetVariable(variable.Name);
        //     var displayResult = Expression.Call(typeof(Console).GetMethod("WriteLine",
        //                                         new[] { typeof(int) }), res);
        //     expr = displayResult;
        // }
        if (syntaxNode is Assign assign)
        {
            var variable = VariableManager.GetVariable(assign.Variable.Name);
            var expression = Parse(assign.Expression);
            var assignExpr = Expression.Assign(variable, expression);
            expr = assignExpr;
        }
        else if (syntaxNode is Plus plus)
        {
            var left = Parse(plus.Left);
            var right = Parse(plus.Right);
            expr = Expression.Add(left, right);
        }
        else if (syntaxNode is Constant constant)
        {
            expr = Expression.Constant(constant.Value);
        }
        else if (syntaxNode is Variable variable)
        {
            expr = VariableManager.GetVariable(variable.Name);
        }
        return expr;
    }
}


public static class VariableManager
{
    public static Dictionary<string, ParameterExpression> Variables = new Dictionary<string, ParameterExpression>();

    public static ParameterExpression GetVariable(string name)
    {
        if (!Variables.ContainsKey(name))
        {
            Variables[name] = Expression.Variable(typeof(int), name);
        }
        return Variables[name];
    }
}