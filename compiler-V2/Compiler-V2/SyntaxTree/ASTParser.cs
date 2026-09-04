using System.Linq.Expressions;

public class ASTParser
{
    public Expression Parse(SyntaxNode syntaxNode)
    {
        Expression expr = Expression.Empty();
        if (syntaxNode is Function func)
        {
            if (func.Name == "write")
            {
                var arg = Parse(func.Body.First());
                var writeLine = Expression.Call(typeof(Console).GetMethod("WriteLine",
                                    new[] { typeof(int) }), arg);
                return writeLine;
            }
        }
        else if (syntaxNode is IfThenElse ifThenElse)
        {
            var cond = Parse(ifThenElse.Condition);
            var trueExpr = Parse(ifThenElse.TrueExpression);
            if (ifThenElse.FalseExpression is not null)
            {
                var falseExpr = Parse(ifThenElse.FalseExpression);
                expr = Expression.IfThenElse(cond, trueExpr, falseExpr);
                return expr;
            }
            expr = Expression.IfThen(cond, trueExpr);
        }
        else if (syntaxNode is Assign assign)
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
        else if (syntaxNode is Multiply multiply)
        {
            var left = Parse(multiply.Left);
            var right = Parse(multiply.Right);
            expr = Expression.Multiply(left, right);
        }
        else if (syntaxNode is Minus minus)
        {
            var left = Parse(minus.Left);
            var right = Parse(minus.Right);
            expr = Expression.Subtract(left, right);
        }
        else if (syntaxNode is Divide divide)
        {
            var left = Parse(divide.Left);
            var right = Parse(divide.Right);
            expr = Expression.Divide(left, right);
        }
        else if (syntaxNode is IsEqual isEqual)
        {
            var left = Parse(isEqual.Left);
            var right = Parse(isEqual.Right);
            expr = Expression.Equal(left, right);
        }
        else if (syntaxNode is IsGreater isGreater)
        {
            var left = Parse(isGreater.Left);
            var right = Parse(isGreater.Right);
            expr = Expression.GreaterThan(left, right);
        }
        else if (syntaxNode is IsLess isLess)
        {
            var left = Parse(isLess.Left);
            var right = Parse(isLess.Right);
            expr = Expression.LessThan(left, right);
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