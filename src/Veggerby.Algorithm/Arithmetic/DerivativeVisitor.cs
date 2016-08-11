using System;

namespace Veggerby.Algorithm.Arithmetic
{
    public class DerivativeVisitor : IOperandVisitor
    {
        public IOperand Result { get; private set; }

        public void Visit(Subtraction operand)
        {
            throw new NotImplementedException();
        }

        public void Visit(Division operand)
        {
            throw new NotImplementedException();
        }

        public void Visit(Multiplication operand)
        {
            throw new NotImplementedException();
        }

        public void Visit(Addition operand)
        {
            throw new NotImplementedException();
        }

        public void Visit(Variable operand)
        {
            throw new NotImplementedException();
        }

        public void Visit(Constant operand)
        {
            throw new NotImplementedException();
        }
    }
}