# Veggerby.Algorithm

[![Build Status](https://travis-ci.org/veggerby/Veggerby.Algorithm.svg?branch=master)](https://travis-ci.org/veggerby/Veggerby.Algorithm)

Contains code for:

* Linear Algebra
  * Vector calculations
  * Matrix calculations
* Graph Algorithms
  * Dijkstra shortest path, undirected, weighted graph
  * Bellman-Ford, calculate shortest path in a weighted graph
  * Johnsons's Algorithm for shortest path in a sparse, directed, weighted graph
* Function/arithmetic evaluation
  * Parsing string function
  * Function evaluation

To come:

* Eigen vector
* Matrix determinant calculation
* Various binary tree algorithms
* Function integral and derivative

## Function Grammar

The following grammar is used for defining functions

```ebnf
    digit       = "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" ;

    letter      = "A" | "B" | "C" | "D" | "E" | "F" | "G" | "H" | "I" | "J" | "K" | "L" | "M" | "N" | "O" | "P" | "Q" | "R" | "S" | "T" | "U" | "V" | "W" | "X" | "Y" | "Z" |
                  "a" | "b" | "c" | "d" | "e" | "f" | "g" | "h" | "i" | "j" | "k" | "l" | "m" | "n" | "o" | "p" | "q" | "r" | "s" | "t" | "u" | "v" | "w" | "x" | "y" | "z" ;

    sign        = "+" | "-" ;

    operator    = sign | "*" | "/" | "^" ;

    decimal     = ".", {digit} ;

    exponent    = "E", [sign], {digit} ;

    number      = [sign], {digit}, [decimal], [exponent] ;

    identifier  = {letter}, [{ letter | digit | "_" }] ;

    expression  = operand, {operator, operand} | function | factorial ;

    factorial   = operand, "!" ;

    function    = identifier, "(", operand, [{ ",", operand }], ")" ;

    operand     = number | identifier | expression | "(", operand, ")" | sign, operand ;

    equation    = expression, "=", expression ;
```

## Function Complexity

Constant and NamedConstant will add 1
Variable and Fraction will add 2
Addition and Multiplication will add 1, i.e. x+y will yield Complexity(x) + Complexity(y) + 1
Subtraction and Negative will add 2, i.e. x-y will yield Complexity(x) + Complexity(y) + 2
Division and Power will add 3, i.e. x/y will yield Complexity(x) + Complexity(y) + 3
Sine, Cosine, Logarithm and Exponential functions will add 2, i.e. f(x), Complexity(x) + 2
LogarithmBase, Tangent and Root will add 3
Factorial, Maximum and Minimum will add 4
Function will add 2
FunctionReference will add 2 and sum of parameter, i.e. f(x, y) = Complexity(x) + Complexity(y) + 2

## Ordering

Constant 1
Fraction 2
Named Constant 3
Unspecified Constant 4
Variable 5
Negative 6
Addition 7
Subtraction 8
Multiplication 9
Division 10
Power 11
Function 12
FunctionReference 13
Factorial 14
Exponential 15
Logarithm 16
Sine 17
Cosine 18
Tangent 19
Root 20
LogarithmBase 21
Minimum 22
Maximum 23
