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


var source = "a = 4 + 5";

var lexer = new Lexer(source);
var tokens = lexer.Scan();

Console.ReadLine();