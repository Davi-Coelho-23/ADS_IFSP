namespace SistemaEscolar_MVC;

class Program
{
    static Escola escola = new Escola();
    static List<Aluno> alunos = new List<Aluno>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n--- MENU PRINCIPAL ---");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar curso");
            Console.WriteLine("2. Pesquisar curso");
            Console.WriteLine("3. Remover curso");
            Console.WriteLine("4. Adicionar disciplina no curso");
            Console.WriteLine("5. Pesquisar disciplina");
            Console.WriteLine("6. Remover disciplina do curso");
            Console.WriteLine("7. Matricular aluno na disciplina");
            Console.WriteLine("8. Remover aluno da disciplina");
            Console.WriteLine("9. Pesquisar aluno");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out int opcao)) continue;
            if (opcao == 0) break;

            switch (opcao)
            {
                case 1: AdicionarCurso(); break;
                case 2: PesquisarCurso(); break;
                case 3: RemoverCurso(); break;
                case 4: AdicionarDisciplina(); break;
                case 5: PesquisarDisciplina(); break;
                case 6: RemoverDisciplina(); break;
                case 7: MatricularAluno(); break;
                case 8: RemoverAluno(); break;
                case 9: PesquisarAluno(); break;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }

        Console.WriteLine("Encerrando...");
    }

    static Curso ObterCurso()
    {
        Console.Write("ID do curso: ");
        int id = int.Parse(Console.ReadLine());
        var curso = escola.PesquisarCurso(id);
        if (curso == null) Console.WriteLine("Curso não encontrado.");
        return curso;
    }

    static Disciplina ObterDisciplina(Curso curso)
    {
        Console.Write("ID da disciplina: ");
        int id = int.Parse(Console.ReadLine());
        var disciplina = curso?.PesquisarDisciplina(id);
        if (disciplina == null) Console.WriteLine("Disciplina não encontrada.");
        return disciplina;
    }

    static void AdicionarCurso()
    {
        Console.Write("ID do curso: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Descrição do curso: ");
        string descricao = Console.ReadLine();

        var curso = new Curso { Id = id, Descricao = descricao };
        Console.WriteLine(escola.AdicionarCurso(curso)
            ? "Curso adicionado com sucesso!"
            : "Não foi possível adicionar o curso. Limite de 5 cursos atingido.");
    }

    static void PesquisarCurso()
    {
        var curso = ObterCurso();
        if (curso == null) return;

        Console.WriteLine($"\nCurso encontrado: {curso.Descricao}");
        Console.WriteLine("Disciplinas associadas:");
        if (curso.Disciplinas.Count == 0)
            Console.WriteLine(" - Nenhuma disciplina cadastrada.");
        else
            curso.Disciplinas.ForEach(d => Console.WriteLine($" - {d.Descricao} (ID: {d.Id})"));
    }

    static void RemoverCurso()
    {
        var curso = ObterCurso();
        if (curso == null) return;

        Console.WriteLine(escola.RemoverCurso(curso)
            ? "Curso removido com sucesso!"
            : "Não é possível remover o curso. Ele possui disciplinas associadas.");
    }

    static void AdicionarDisciplina()
    {
        var curso = ObterCurso();
        if (curso == null) return;

        Console.Write("ID da disciplina: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Descrição da disciplina: ");
        string descricao = Console.ReadLine();

        var disciplina = new Disciplina { Id = id, Descricao = descricao };
        Console.WriteLine(curso.AdicionarDisciplina(disciplina)
            ? "Disciplina adicionada com sucesso!"
            : "Não foi possível adicionar. Limite de 12 disciplinas atingido.");
    }

    static void PesquisarDisciplina()
    {
        var curso = ObterCurso();
        var disciplina = ObterDisciplina(curso);
        if (disciplina == null) return;

        Console.WriteLine($"\nDisciplina encontrada: {disciplina.Descricao}");
        Console.WriteLine("Alunos matriculados:");
        if (disciplina.Alunos.Count == 0)
            Console.WriteLine(" - Nenhum aluno matriculado.");
        else
            disciplina.Alunos.ForEach(a => Console.WriteLine($" - {a.Nome} (ID: {a.Id})"));
    }

    static void RemoverDisciplina()
    {
        var curso = ObterCurso();
        var disciplina = ObterDisciplina(curso);
        if (disciplina == null) return;

        Console.WriteLine(curso.RemoverDisciplina(disciplina)
            ? "Disciplina removida com sucesso!"
            : "Não é possível remover. A disciplina possui alunos matriculados.");
    }

    static void MatricularAluno()
    {
        var curso = ObterCurso();
        var disciplina = ObterDisciplina(curso);
        if (disciplina == null) return;

        Console.Write("ID do aluno: ");
        int alunoId = int.Parse(Console.ReadLine());
        Console.Write("Nome do aluno: ");
        string nome = Console.ReadLine();

        var aluno = alunos.FirstOrDefault(a => a.Id == alunoId);
        if (aluno == null)
        {
            aluno = new Aluno { Id = alunoId, Nome = nome, CursoMatriculado = curso };
            alunos.Add(aluno);
        }

        Console.WriteLine(disciplina.MatricularAluno(aluno)
            ? "Aluno matriculado com sucesso!"
            : "Não foi possível matricular. Verifique os limites de alunos e disciplinas.");
    }

    static void RemoverAluno()
    {
        var curso = ObterCurso();
        var disciplina = ObterDisciplina(curso);
        if (disciplina == null) return;

        Console.Write("ID do aluno: ");
        int alunoId = int.Parse(Console.ReadLine());
        var aluno = alunos.FirstOrDefault(a => a.Id == alunoId);

        if (aluno == null)
        {
            Console.WriteLine("Aluno não encontrado.");
            return;
        }

        Console.WriteLine(disciplina.DesmatricularAluno(aluno)
            ? "Aluno removido da disciplina com sucesso!"
            : "O aluno não está matriculado nesta disciplina.");
    }

    static void PesquisarAluno()
    {
        Console.Write("Nome do aluno: ");
        string nome = Console.ReadLine();

        var aluno = alunos.FirstOrDefault(a => a.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        if (aluno == null)
        {
            Console.WriteLine("Aluno não encontrado.");
            return;
        }

        Console.WriteLine($"\nAluno encontrado: {aluno.Nome} (ID: {aluno.Id})");
        Console.WriteLine("Disciplinas matriculadas:");
        if (aluno.DisciplinasMatriculadas.Count == 0)
            Console.WriteLine(" - Nenhuma disciplina.");
        else
            aluno.DisciplinasMatriculadas.ForEach(d => Console.WriteLine($" - {d.Descricao} (ID: {d.Id})"));
    }
}