using System;
using System.Collections.Generic;

namespace Veggerby.Algorithm.Arithmetic
{
    public class VariableExtractorVisitor : IOperandVisitor
    {
        private readonly IList<Variable> _variables = new List<Variable>();
        
        public IEnumerable<Variable> Variables => _variables;

        private void VisitBinaryOperation(BinaryOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }

        public void Visit(Subtraction operand)
        {
            VisitBinaryOperation(operand);
        }

        public void Visit(Division operand)
        {
            VisitBinaryOperation(operand);
        }

        public void Visit(Multiplication operand)
        {
            VisitBinaryOperation(operand);
        }

        public void Visit(Addition operand)
        {
            VisitBinaryOperation(operand);
        }

        public void Visit(Variable operand)
        {
            if (!_variables.Contains(operand))
            {
                _variables.Add(operand);
            }
        }

        public void Visit(Constant operand)
        {
        }
    }
}