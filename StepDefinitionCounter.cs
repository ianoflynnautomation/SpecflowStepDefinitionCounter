using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Specflow.Acceptance.Tests;

public class StepDefinitionCounter
{
    [Test]
    public void StepDefinitionCountTest()
    {
        string[] featureFiles = {
            // Replace with your feature files
        }; 

        var StepDefinitions = GetStepDefinitions(featureFiles);
        var groupedSteps = GroupByCount(StepDefinitions);ß

        // Print the result
        System.Diagnostics.Debug.WriteLine($"System Tests");
        foreach (var kvp in groupedSteps.OrderByDescending(x => x.Key))
        {
            foreach (string step in kvp.Value)
            {
                System.Diagnostics.Debug.WriteLine($"Step: {step} Count: {kvp.Key}");
            }
            Console.WriteLine();
        }
    }

    public Dictionary<string, int> GetStepDefinitions(string[] featureFiles)
    {
        var stepDefinitions = new Dictionary<string, int>();

        // Regular expression pattern to match step definitions
        string pattern = @"\b(Given|When|Then|And|But)\b";

        foreach (string featureFile in featureFiles)
        {
            string[] lines = File.ReadAllLines(featureFile);
            foreach (string line in lines)
            {
                Match match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    string step = match.Value;
                    if(step == "Given" || step == "When" || step == "Then" || step == "And" || step == "But")
                    {
                        if(stepDefinitions.ContainsKey(line.ToString().TrimStart()))
                        {
                            stepDefinitions[line.ToString().TrimStart()] += 1;
                        }
                        else{
                                stepDefinitions.Add(line.ToString().TrimStart(), 1);
                        }
                    
                    }
                    else
                    {
                        stepDefinitions[step] = 1;
                    }
                }
            }
        }

        return stepDefinitions;
    }

    public Dictionary<int, List<string>> GroupByCount(Dictionary<string, int> stepDefinitions)
    {
        var groupedSteps = new Dictionary<int, List<string>>();

        foreach (var kvp in stepDefinitions)
        {
            int count = kvp.Value;
            string step = kvp.Key;

            if (groupedSteps.ContainsKey(count))
            {
                groupedSteps[count].Add(step);
            }
            else
            {
                groupedSteps[count] = new List<string> { step };
            }
        }

        return groupedSteps;
    }
}
