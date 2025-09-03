namespace SistemaEscolar_MVC;

public class Curso
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public List<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();

    public bool AdicionarDisciplina(Disciplina disciplina)
    {
        if (Disciplinas.Count >= 12) return false;
        Disciplinas.Add(disciplina);
        return true;
    }

    public Disciplina PesquisarDisciplina(int id)
    {
        return Disciplinas.FirstOrDefault(d => d.Id == id);
    }

    public bool RemoverDisciplina(Disciplina disciplina)
    {
        if (disciplina.Alunos.Count > 0) return false;
        return Disciplinas.Remove(disciplina);
    }
}