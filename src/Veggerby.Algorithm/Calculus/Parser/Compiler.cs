using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Parser
{
    public class Compiler
    {
        private Operand CompileFunction(Token token, Operand inner)
        {
            if (token.Type != TokenType.Function)
            {
                throw new ArgumentException("Unexpected token type", nameof(token));
            }

            switch (token.Value.ToLowerInvariant())
            {
                case "sin": return Sine.Create(inner);
                case "cos": return Cosine.Create(inner);
                case "tan": return Tangent.Create(inner);
                case "exp": return Exponential.Create(inner);
                case "ln": return Logarithm.Create(inner);
                case "log": return LogarithmBase.Create(10, inner);
                case "log2": return LogarithmBase.Create(2, inner);
                case "sqrt": return Root.Create(2, inner);
            }

            return null;
        }

        private Operand Compile(UnaryNode node)
        {
            if (node == null)
            {
                return null;
            }

            var inner = Compile(node.Inner);

            switch (node.Token.Type)
            {
                case TokenType.Factorial: return Factorial.Create(inner);
                case TokenType.Function: return CompileFunction(node.Token, inner);
            }

            return null;
        }

        private Operand Compile(BinaryNode node)
        {
            if (node == null)
            {
                return null;
            }

            if (node.Token.Type != TokenType.Sign && node.Token.Type != TokenType.OperatorPriority1 && node.Token.Type != TokenType.Function)
            {
                return null;
            }

            var left = Compile(node.Left);
            var right = Compile(node.Right);

            switch (node.Token.Value)
            {
                case "+": return left != null && right != null ? Addition.Create(left, right) : left ?? right;
                case "-": return left != null && right != null ? Subtraction.Create(left, right) : (left == null ? Negative.Create(right) : left);
                case "*": return Multiplication.Create(left, right);
                case "/": return Division.Create(left, right);
                case "^": return Power.Create(left, right);
                case "root": return Root.Create((int)(left as Constant).Value, right);
                case "max": return Maximum.Create(left, right);
                case "min": return Minimum.Create(left, right);
            }

            return null;
        }

        private Operand CompileIdentifier(Token token)
        {
            if (token.Value == "π" || token.Value == "pi")
            {
                return Constant.Pi;
            }

            if (token.Value == "e")
            {
                return Constant.e;
            }

            return Variable.Create(token.Value);
        }

        private Operand CompileConstant(Token token)
        {
            double result;
            if (double.TryParse(token.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                return Constant.Create(result);
            }

            return null;
        }

        private Operand CompileNode(Node node)
        {
            if (node == null)
            {
                return null;
            }

            switch (node.Token.Type)
            {
                case TokenType.Identifier: return CompileIdentifier(node.Token);
                case TokenType.Number: return CompileConstant(node.Token);
            }

            return null;
        }

        public Operand Compile(Node node)
        {
            return Compile(node as UnaryNode) ?? Compile(node as BinaryNode) ?? CompileNode(node);
        }
    }
}