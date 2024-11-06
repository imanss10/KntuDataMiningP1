// See https://aka.ms/new-console-template for more information

using DataMining.FirstProject;

Console.WriteLine("Hello, World!");

void PrintTree(DecisionTreeNode node, string indent = "")
{
    if (node.Result.HasValue)
    {
        Console.WriteLine($"{indent}Leaf: Buys = {node.Result}");
    }
    else
    {
        Console.WriteLine($"{indent}{node.Attribute}");

        foreach (var child in node.Children)
        {
            Console.Write($"{indent} -> {child.Value}: ");
            PrintTree(child, indent + "   ");
        }
    }
}



DecisionTreeNode BuildTree(List<CustomerData> dataset)
{
    // Check if all entries have the same 'Buys' value
    bool allBuy = dataset.All(d => d.Buys);
    bool allNotBuy = dataset.All(d => !d.Buys);

    if (allBuy || allNotBuy)
    {
        return new DecisionTreeNode
        {
            Result = allBuy
        };
    }

    // Find the best attribute to split
    var attributes = new Dictionary<string, Func<CustomerData, object>>
    {
        { "Age", d => d.Age },
        { "Income", d => d.Income },
        { "Married", d => d.Married }
    };

    string bestAttribute = null;
    double bestGain = double.MinValue;

    foreach (var attribute in attributes)
    {
        double gain = DataSets.CalculateInformationGain(dataset, attribute.Value);
        if (gain > bestGain)
        {
            bestGain = gain;
            bestAttribute = attribute.Key;
        }
    }

    // Split the dataset based on the best attribute
    var node = new DecisionTreeNode { Attribute = bestAttribute };
    var bestAttributeSelector = attributes[bestAttribute];

    var groups = dataset.GroupBy(bestAttributeSelector);

    foreach (var group in groups)
    {
        var childNode = BuildTree(group.ToList());
        childNode.Value = group.Key;
        node.Children.Add(childNode);
    }

    return node;
}

