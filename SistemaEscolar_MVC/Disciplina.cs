namespace SistemaEscolar_MVC;

public class Disciplina
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public List<Aluno> Alunos { get; set; } = new List<Aluno>();

    public bool MatricularAluno(Aluno aluno)
    {
        if (Alunos.Count >= 15 || aluno.DisciplinasMatriculadas.Count >= 6 || !aluno.PodeMatricular(null))
            return false;

        Alunos.Add(aluno);
        aluno.DisciplinasMatriculadas.Add(this);
        return true;
    }

    public bool DesmatricularAluno(Aluno aluno)
    {
        if (!Alunos.Contains(aluno)) return false;

        Alunos.Remove(aluno);
        aluno.DisciplinasMatriculadas.Remove(this);
        return true;
    }
}