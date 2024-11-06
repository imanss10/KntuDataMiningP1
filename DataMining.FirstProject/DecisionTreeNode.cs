namespace DataMining.FirstProject;

public class DecisionTreeNode
{
    public string Attribute { get; set; }
    public object Value { get; set; }
    public List<DecisionTreeNode> Children { get; set; } = new List<DecisionTreeNode>();
    public bool? Result { get; set; } // True or False if it’s a leaf node
}