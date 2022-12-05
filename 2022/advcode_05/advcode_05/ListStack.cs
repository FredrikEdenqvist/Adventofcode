namespace advcode_05
{
    internal static class ListStack
    {
        internal static List<Stack<string>> GetStackList()
        {
            List<Stack<string>> stacks = new(9);
            Stack<string> stack1 = new();
            stack1.Push("S");
            stack1.Push("Z");
            stack1.Push("P");
            stack1.Push("D");
            stack1.Push("L");
            stack1.Push("B");
            stack1.Push("F");
            stack1.Push("C");
            stacks.Add(stack1);
            Stack<string> stack2 = new();
            stack2.Push("N");
            stack2.Push("V");
            stack2.Push("G");
            stack2.Push("P");
            stack2.Push("H");
            stack2.Push("W");
            stack2.Push("B");
            stacks.Add(stack2);
            Stack<string> stack3 = new();
            stack3.Push("F");
            stack3.Push("W");
            stack3.Push("B");
            stack3.Push("J");
            stack3.Push("G");
            stacks.Add(stack3);
            Stack<string> stack4 = new();
            stack4.Push("G");
            stack4.Push("J");
            stack4.Push("N");
            stack4.Push("F");
            stack4.Push("L");
            stack4.Push("W");
            stack4.Push("C");
            stack4.Push("S");
            stacks.Add(stack4);
            Stack<string> stack5 = new();
            stack5.Push("W");
            stack5.Push("J");
            stack5.Push("L");
            stack5.Push("T");
            stack5.Push("P");
            stack5.Push("M");
            stack5.Push("S");
            stack5.Push("H");
            stacks.Add(stack5);
            Stack<string> stack6 = new();
            stack6.Push("B");
            stack6.Push("C");
            stack6.Push("W");
            stack6.Push("G");
            stack6.Push("F");
            stack6.Push("S");
            stacks.Add(stack6);
            Stack<string> stack7 = new();
            stack7.Push("H");
            stack7.Push("T");
            stack7.Push("P");
            stack7.Push("M");
            stack7.Push("Q");
            stack7.Push("B");
            stack7.Push("W");
            stacks.Add(stack7);
            Stack<string> stack8 = new();
            stack8.Push("F");
            stack8.Push("S");
            stack8.Push("W");
            stack8.Push("T");
            stacks.Add(stack8);
            Stack<string> stack9 = new();
            stack9.Push("N");
            stack9.Push("C");
            stack9.Push("R");
            stacks.Add(stack9);
            return stacks;
        }
    }
}
