namespace SistemaEscolar_MVC;

public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public Curso CursoMatriculado { get; set; }
    public List<Disciplina> DisciplinasMatriculadas { get; set; } = new List<Disciplina>();

    public bool PodeMatricular(Curso curso)
    {
        return CursoMatriculado == null || CursoMatriculado == curso;
    }
}