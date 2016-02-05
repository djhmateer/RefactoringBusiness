namespace RefactoringCSharp.Controllers
{
    public static class MathFunctions
    {
        // Extension method off int
        public static int CircularIncrement(this int i, int max)
        {
            return (i + 1)%max;
        }
    }
}