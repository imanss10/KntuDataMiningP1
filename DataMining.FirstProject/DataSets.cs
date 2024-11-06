namespace DataMining.FirstProject;

public static class DataSets
{
    public static List<CustomerData> GetDataset()
    {
        return new List<CustomerData>
        {
            new CustomerData { Age = 22, Income = IncomeLevel.High, Married = false, Buys = false },
            new CustomerData { Age = 35, Income = IncomeLevel.Low, Married = true, Buys = true },
            // Add remaining data entries here...
        };
    }
    
    public static double CalculateEntropy(List<CustomerData> dataset)
    {
        int total = dataset.Count;
        int buysCount = dataset.Count(d => d.Buys);
        int notBuysCount = total - buysCount;

        double pBuys = (double)buysCount / total;
        double pNotBuys = (double)notBuysCount / total;

        double entropy = 0;
        if (pBuys > 0)
            entropy -= pBuys * Math.Log2(pBuys);
        if (pNotBuys > 0)
            entropy -= pNotBuys * Math.Log2(pNotBuys);

        return entropy;
    }

    public static double CalculateInformationGain(List<CustomerData> dataset, Func<CustomerData, object> attributeSelector)
    {
        double entropyBeforeSplit = CalculateEntropy(dataset);

        var groups = dataset.GroupBy(attributeSelector).ToList();
        double weightedEntropyAfterSplit = 0;

        foreach (var group in groups)
        {
            double groupEntropy = CalculateEntropy(group.ToList());
            weightedEntropyAfterSplit += (double)group.Count() / dataset.Count * groupEntropy;
        }

        return entropyBeforeSplit - weightedEntropyAfterSplit;
    }

    
    
}