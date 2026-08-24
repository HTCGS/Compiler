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
        if (syntaxNode is Function func)
        {
            if (func.Name == "abcde")
            {
                var arg = Parse(func.Body.First());
                var writeLine = Expression.Call(typeof(Console).GetMethod("WriteLine",
                                    new[] { typeof(int) }), arg);
                return writeLine;
            }
            else if (func.Name == "ab")
            {
                var leftCond = Parse(func.Body[0]);
                var rightCond = Parse(func.Body[1]);
                var trueExpr = Parse(func.Body[2]);
                // var trueExpr = Expression.Block(Parse(func.Body[2]));

                var falseExpr = Expression.Call(typeof(Console).GetMethod("WriteLine",
                                    new[] { typeof(string) }), Expression.Constant("false"));
                // var falseExpr = Expression.Block(Expression.Call(typeof(Console).GetMethod("WriteLine",
                //                     new[] { typeof(string) }), Expression.Constant("false")));
                // expr = Expression.Condition(Expression.Equal(leftCond, rightCond),
                //                                 trueExpr, falseExpr);

                expr = Expression.IfThen(Expression.Equal(leftCond, rightCond), trueExpr);
            }
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