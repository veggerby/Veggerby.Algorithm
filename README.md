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

    digit 		= "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9"

    letter 		= "A" | "B" | "C" | "D" | "E" | "F" | "G" | "H" | "I" | "J" | "K" | "L" | "M" | "N" | "O" | "P" | "Q" | "R" | "S" | "T" | "U" | "V" | "W" | "X" | "Y" | "Z" |
                "a" | "b" | "c" | "d" | "e" | "f" | "g" | "h" | "i" | "j" | "k" | "l" | "m" | "n" | "o" | "p" | "q" | "r" | "s" | "t" | "u" | "v" | "w" | "x" | "y" | "z"

    sign   		= "+" | "-"

    operator 	= sign | "*" | "/" | "^"

    decimal 	= "." {digit}

    exponent 	= "E" [sign] {digit}

    number 		= [sign] {digit} [decimal] [exponent]

    identifier	= {letter} [{ letter | digit | "_" }]

    expression	= operand {operator operand} | function | factorial

    factorial	= operand "!"

    function	= identifier "(" operand [{ "," operand }] ")"

    operand 	= number | identifier | expression | "(" operand ")" | sign operand

    equation 	= expression "=" expression
