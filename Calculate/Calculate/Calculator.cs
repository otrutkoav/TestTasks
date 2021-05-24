using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculate
{
    enum Operations
    {
        Multiplication = 0,
        Division = 1,
        Addition = 2,
        Subtraction = 3

    }

    public class Calculator
    {
        static readonly char[] operations = new char[] { '*', '/', '+', '-' };

        string str = string.Empty;

        public Calculator(string input)
        {
            str = input;
            GetOperations(new char[] { operations[(int)Operations.Addition], operations[(int)Operations.Subtraction] },
                 new char[] { operations[(int)Operations.Multiplication], operations[(int)Operations.Division] });
        }

        public void GetResult()
        {
            Console.WriteLine(this.str);
        }

        private void GetOperations(char [] forSkip, char [] forActions)
        {
            List<string> Steps = new List<string>();

            var actions = this.str.Split(forSkip).ToArray();
            if (actions.Length > 0)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    if (actions[i].Contains(forActions[0]) || actions[i].Contains(forActions[1]))
                    {
                        Steps.Add(actions[i]);
                    }
                }
            }

            foreach (var item in Steps)
            {
                CalculateOperation(item);
            }

            if (ContainsSecondOrderActions())
            {
                GetOperations(new char[] {operations[(int)Operations.Addition], operations[(int)Operations.Subtraction] },
                new char[] { operations[(int)Operations.Multiplication], operations[(int)Operations.Division] });
            }

            if (ContainsFirstOrderActions())
            {
                GetOperations(new char[] {operations[(int)Operations.Multiplication], operations[(int)Operations.Division] },
                new char[] { operations[(int)Operations.Addition], operations[(int)Operations.Subtraction] });
            }
        }

        private void CalculateOperation(string item)
        {
            double result = 0;
            int op = -1;
            bool isOut=false;
            bool isNegatively = false;
            List<string> items = new List<string>();

            for (int i = 0; i < item.Length; i++)
            {
                if (i==0 && item[i]=='-')
                {
                    continue;
                }
                for (int j = 0; j < operations.Length; j++)
                {
                    if (item[i]==operations[j])
                    {
                        op = j;
                        isOut = true;
                        break;
                    }
                }

                if (isOut)
                    break;
            }

            string stringForCalculate = string.Empty;

            switch ((Operations)op)
            {
                case Operations.Multiplication:
                    items = item.Split(operations[(int)Operations.Division]).ToList();

                    stringForCalculate = GetStringForCalculate(items, Operations.Multiplication);
                    result = Calculate(stringForCalculate.Split(operations[(int)Operations.Multiplication]).ToList(), Operations.Multiplication);

                    break;

                case Operations.Division:
                    items = item.Split(operations[(int)Operations.Multiplication]).ToList();

                    stringForCalculate = GetStringForCalculate(items, Operations.Division);
                    result = Calculate(stringForCalculate.Split(operations[(int)Operations.Division]).ToList(), Operations.Division);

                    break;

                case Operations.Addition:

                    if (item[0]==operations[(int)Operations.Subtraction])
                    {
                        isNegatively = true;
                    }

                    items = item.Split(operations[(int)Operations.Subtraction]).ToList();

                    stringForCalculate = GetStringForCalculate(items, Operations.Addition);

                    if (isNegatively)
                        stringForCalculate = string.Join("", operations[(int)Operations.Subtraction], stringForCalculate);

                    result = Calculate(stringForCalculate.Split(operations[(int)Operations.Addition]).ToList(), Operations.Addition);

                    break;

                case Operations.Subtraction:

                    if (item[0] == operations[(int)Operations.Subtraction])
                    {
                        isNegatively = true;
                    }

                    items = item.Split(operations[(int)Operations.Addition]).ToList();

                    stringForCalculate = GetStringForCalculate(items, Operations.Subtraction);

                    if (isNegatively)
                        stringForCalculate = string.Join("", operations[(int)Operations.Subtraction], stringForCalculate);

                    result = Calculate(stringForCalculate.Split(operations[(int)Operations.Subtraction]).ToList(), Operations.Subtraction);

                    break;

                default:
                    break;
            }

            this.Replace(stringForCalculate, result.ToString());
        }

        private double Calculate(List<string> items, Operations multiplication)
        {
            double result = 0;
            switch (multiplication)
            {
                case Operations.Multiplication:

                    for (int i = 0; i < items.Count; i++)
                    {
                        if (i == 0)
                        {
                            result = double.Parse(items[i]);
                            continue;
                        }

                        result *= double.Parse(items[i]);
                    }

                    break;

                case Operations.Division:

                    for (int i = 0; i < items.Count; i++)
                    {
                        if (i == 0)
                        {
                            result = double.Parse(items[i]);
                            continue;
                        }

                        result /= double.Parse(items[i]);
                    }

                    break;

                case Operations.Addition:

                    foreach (var item in items)
                    {
                        result += double.Parse(item);
                    }

                    break;

                case Operations.Subtraction:

                    for (int i = 0; i < items.Count; i++)
                    {
                        if (i == 0)
                        {
                            result = double.Parse(items[i]);
                            continue;
                        }

                        result -= double.Parse(items[i]);
                    }

                    break;
                default:
                    break;
            }

            return result;
        }

        private void Replace(string oldValue, string valueForReplace)
        {
            this.str = this.str.Replace(oldValue, valueForReplace);
        }

        private string GetStringForCalculate(List<string> items, Operations operation)
        {
            return items.FirstOrDefault(i => i.Contains(operations[(int)operation]));
        }

        private bool ContainsFirstOrderActions()
        {
            return this.str.Split(operations.Skip(2).ToArray()).Where(i => !String.IsNullOrEmpty(i)).ToArray().Length > 1;
        }

        private bool ContainsSecondOrderActions()
        {
            return this.str.Split(operations.Take(2).ToArray()).Where(i => !String.IsNullOrEmpty(i)).ToArray().Length > 1;
        }

    }
}
