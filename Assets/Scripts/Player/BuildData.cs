


using System.Collections.Generic;

public class BuildData
{
    public List<ComboBuilder> comboBuilders;

    public BuildData(){}

    public BuildData(List<ComboBuilder> comboBuilders)
    {
        this.comboBuilders = comboBuilders;
    }
}

public class ComboBuilder
{
    public int sttBuild;
    public string typeBuild;
    public string statusBuild;
    public ComboBuilder(int sttBuild, string typeBuild, string statusBuild) {
        this.sttBuild = sttBuild;
        this.typeBuild = typeBuild;
        this.statusBuild = statusBuild;
    }
}