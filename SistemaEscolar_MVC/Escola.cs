namespace SistemaEscolar_MVC;

public class Escola
{
    public List<Curso> Cursos { get; set; } = new List<Curso>();

    public bool AdicionarCurso(Curso curso)
    {
        if (Cursos.Count >= 5) return false;
        Cursos.Add(curso);
        return true;
    }

    public Curso PesquisarCurso(int id)
    {
        return Cursos.FirstOrDefault(c => c.Id == id);
    }

    public bool RemoverCurso(Curso curso)
    {
        if (curso.Disciplinas.Count > 0) return false;
        return Cursos.Remove(curso);
    }
}