using System.Collections.Generic;

public class Produto
{
    public int Codigo { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Subgrupo { get; set; } = string.Empty;
}

public class Classe
{
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public List<Departamento> Departamentos { get; set; } = new List<Departamento>();
}


public class Departamento
{
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public string CodigoVinculo { get; set; }
    public List<Grupo> Grupos { get; set; }
}

public class Grupo
{
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public string CodigoVinculo { get; set; }
    public List<Subgrupo> Subgrupos { get; set; }
}

public class Subgrupo
{
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public string CodigoVinculo { get; set; }
}
