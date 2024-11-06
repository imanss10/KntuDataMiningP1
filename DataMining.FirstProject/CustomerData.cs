namespace DataMining.FirstProject;

public class CustomerData
{
    public int Age { get; set; }
    public IncomeLevel Income { get; set; }
    public bool Married { get; set; }
    public bool Buys { get; set; }
}

public enum IncomeLevel
{
    High,
    Medium,
    Low
}


