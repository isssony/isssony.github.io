using Xunit;
using AvaloniaApp;
using System;

namespace AvaloniaApp.Tests
{
    public class QuadraticSolverTests
    {
        [Fact]
        public void Solve_TwoRoots()
        {
            var result = QuadraticSolver.Solve(1, -3, 2);

            Assert.True(result.hasRoots);
            Assert.Equal(2, result.x1, 5);
            Assert.Equal(1, result.x2, 5);
        }

        [Fact]
        public void Solve_OneRoot()
        {
            var result = QuadraticSolver.Solve(1, 2, 1);

            Assert.True(result.hasRoots);
            Assert.Equal(-1, result.x1, 5);
            Assert.Equal(-1, result.x2, 5);
        }

        [Fact]
        public void Solve_NoRoots()
        {
            var result = QuadraticSolver.Solve(1, 0, 1);

            Assert.False(result.hasRoots);
        }

        [Fact]
        public void Solve_AEqualsZero_ShouldThrow()
        {
            Assert.Throws<DivideByZeroException>(() =>
                QuadraticSolver.Solve(0, 2, 1));
        }

        [Fact]
        public void Parse_Standard()
        {
            var result = QuadraticSolver.ParseEquation("x^2+2x+1=0");

            Assert.Equal(1, result.a);
            Assert.Equal(2, result.b);
            Assert.Equal(1, result.c);
        }

        [Fact]
        public void Parse_InvalidInput_ShouldThrow()
        {
            Assert.Throws<FormatException>(() =>
                QuadraticSolver.ParseEquation("abc"));
        }
    }
}
