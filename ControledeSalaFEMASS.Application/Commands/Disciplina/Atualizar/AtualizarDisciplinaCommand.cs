using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Disciplina.Atualizar;
public class AtualizarDisciplinaCommand : IRequest
{
    public long Id { get; set; }
    public bool NecessitaLaboratorio { get; set; } = false;
    public bool NecessitaArCondicionado { get; set; } = false;
    public bool NecessitaLoucaDigital { get; set; } = false;
}