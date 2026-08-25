using System.Linq.Expressions;

var res = Expression.Variable(typeof(int), "result");

var init = Expression.Assign(res, Expression.Constant(4));

// var add = Expression.AddAssign(res, Expression.Constant(5));
var add = Expression.Add(res,
    Expression.Add(res, Expression.Constant(5))
);
var assign = Expression.Assign(res,
                Expression.Add(add, add));

var displayResult = Expression.Call(
    typeof(Console).GetMethod("WriteLine", new[] { typeof(int) }),
    res
);

var lambda = Expression.Lambda<Func<int>>(
    Expression.Block(
        new[] { res },
        // init,
        assign,
        displayResult,
        // add,
        // displayResult,
        res
    )
);

var compiled = lambda.Compile();

// var result = compiled();
// Console.WriteLine(result);


// var source = "a = 1+2+3";
// var source = "a = 1*2*3+4";
// var source = "a = 2+2*2";
// var source = "a = 2*2+2";
// var source = "a = 2*2*2";
// var source = "a = 1+2*3+4";
// var source = "a = 1*2*3+4";
// var source = "a = 2*3*4+5";
// var source = "a = 1+2*3+4*5";
// var source = "a = =1+2=3 ";
// var source = "a = (1)";
// var source = "a = (2+2)*2";
// var source = "a = 1+(2*3)+4";
// var source = "a = 1+2*3*4+5";
// var source = "a = 1+(2*3)*4+5";
// var source = "a = 1+(2*3+4)*5+6";
// var source = "a = (2+2)*2";
// var source = "a = (1*2)*3+4";
// var source = "a = (1+2)+(3*4+5)";
// var source = "a = (1+2)+(3+4+5)";
// var source = "a = (1+2)+(3*4*5)";
// var source = "a = (1+2)*3+(4*5)";
// var source = "a = (1*2)*(3*(4*5))";
// var source = "a = (1+2)*(3+(4*5))";
// var source = "a = (1+2)+(3+(4+(5+6)))";
// var source = "a = (1+2)+(3+(4+(5*6+7)))";

// var source = "a = -1 -1-1 -1-(-2+3) -(1+2)";
// var source = "a = -1";
var source = "a = -1-1 ";
// var source = "a = -(1+2)";
// var source = "a = -1-(-2+3)";

// var source = "write((123))";
// var source = "write((1+2)*3)";
// var source = "if(1=0+1)(if(2=0+1+1)(if(3=0+1+2)(write(111))))";
// var source = "if(6+2=(2+2)*2)(a = 55)";

var lexer = new Lexer(source);
lexer.Keywords = new List<string> { "if", "write" };
var tokens = lexer.Scan();

Console.WriteLine("Tokens:");
foreach (var token in tokens)
{
    Console.WriteLine($"  {token.Type}: {token.Lexeme}");
}

var parser = new TokensToASTParser();
var syntaxTree = parser.Parse(tokens);

var astParser = new ASTParser();
var expression = astParser.Parse(syntaxTree);

lexer.Source = "b=2";
tokens = lexer.Scan();

syntaxTree = parser.Parse(tokens);
var expression2 = astParser.Parse(syntaxTree);

lexer.Source = "write(a)";
tokens = lexer.Scan();

syntaxTree = parser.Parse(tokens);
var expression3 = astParser.Parse(syntaxTree);
// if (expression3 is ParameterExpression parameterExpression)
// {
//     expression3 = Expression.Call(typeof(Console).GetMethod("WriteLine",
//                                     new[] { typeof(int) }), expression3);
// }


var allVariables = VariableManager.Variables.Select(kvp => kvp.Value).ToList();

// var varA = VariableManager.GetVariable("a");
// var varB = VariableManager.GetVariable("b");
// var displayA = Expression.Call(typeof(Console).GetMethod("WriteLine",
//                                     new[] { typeof(int) }), varA);
// var displayB = Expression.Call(typeof(Console).GetMethod("WriteLine",
//                                     new[] { typeof(int) }), varB);

System.Console.WriteLine(expression);
System.Console.WriteLine(expression2);
System.Console.WriteLine(expression3);
System.Console.WriteLine("=======");

var program = Expression.Block(allVariables, expression, expression2, expression3);
var compiledExpression = Expression.Lambda<Action>(program).Compile();
compiledExpression();

// Console.ReadLine();