using AutoMapper;
using ControledeSalaFEMASS.Domain.Dtos;
using ControledeSalaFEMASS.Domain.Exceptions;
using ControledeSalaFEMASS.Domain.Repositories;
using MediatR;
using OfficeOpenXml;

namespace ControledeSalaFEMASS.Application.Commands.Importacao.Turma;
public class ImportarTurmasCommandHandler : IRequestHandler<ImportarTurmasCommand>
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ImportarTurmasCommandHandler(
        ITurmaRepository turmaRepository, 
        IDisciplinaRepository disciplinaRepository, 
        IUnitOfWork unitOfWork)
    {
        _turmaRepository = turmaRepository;
        _disciplinaRepository = disciplinaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ImportarTurmasCommand request, CancellationToken cancellationToken)
    {
        var turmasExcel = await Validate(request);

        var turmasBanco = await _turmaRepository.GetAll();
        var disciplinas = await _disciplinaRepository.GetAll();

        var turmasASeremImportadas = new List<Domain.Entities.Turma>();

        foreach (var turmaExcel in turmasExcel)
        {
            int idTurma = int.TryParse(turmaExcel.CodigoTurma, out int temp) ? temp : 0;

            if(idTurma == 0)
            {
                throw new ErrorOnValidationException("Há turmas com código de turma inválido. Por favor, verifique.");
            }

            var disciplina = disciplinas.FirstOrDefault(d => d.Nome.Equals(turmaExcel.Disciplina, StringComparison.CurrentCultureIgnoreCase));

            if (disciplina is null)
            {
                disciplina = new Domain.Entities.Disciplina { Nome = turmaExcel.Disciplina };
                await _disciplinaRepository.Add(disciplina);
            }

            var turmaBanco = turmasBanco.FirstOrDefault(p => p.Id == idTurma);

            if(turmaBanco is null)
            {
                var turma = new Domain.Entities.Turma
                {
                    Id = idTurma,
                    Professor = turmaExcel.Professor,
                    Disciplina = disciplina,
                    QuantidadeAlunos = turmaExcel.QuantidadeAlunos,
                    CodigoHorario = turmaExcel.CodigoHorario
                };

                turmasASeremImportadas.Add(turma);
            } else
            {
                turmaBanco.Professor = turmaExcel.Professor;
                turmaBanco.QuantidadeAlunos = turmaExcel.QuantidadeAlunos;
                turmaBanco.CodigoHorario = turmaExcel.CodigoHorario;

                _turmaRepository.Update(turmaBanco);
            }
        }

        if(turmasASeremImportadas.Any())
        {
            await _turmaRepository.AddRange(turmasASeremImportadas);
        }

        await _unitOfWork.Commit();

        await ValidarTurmasAgrupadas();
    }

    private async Task<List<TurmaImportadaDto>> Validate(ImportarTurmasCommand request)
    {
        if (request.FileExcel == null || request.FileExcel.Length == 0)
            throw new OperationInvalidException("Arquivo não fornecido ou vazio");

        using var stream = new MemoryStream();
        await request.FileExcel.CopyToAsync(stream);
        stream.Position = 0;

        using var package = new ExcelPackage(stream);
        var worksheet = package.Workbook.Worksheets[0];

        var turmas = new List<TurmaImportadaDto>();

        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
        {
            string codigoTurma = worksheet.Cells[row, 1].Text;

            if(!string.IsNullOrEmpty(codigoTurma))
            {
				turmas.Add(new TurmaImportadaDto
				{
					CodigoTurma = codigoTurma,
					Professor = worksheet.Cells[row, 2].Text,
					Disciplina = worksheet.Cells[row, 3].Text,
					QuantidadeAlunos = int.TryParse(worksheet.Cells[row, 4].Text, out int temp) ? temp : 0,
					CodigoHorario = int.TryParse(worksheet.Cells[row, 5].Text, out int temp2) ? temp2 : 0,
				});
			}
        }

        return turmas;
    }

    private async Task ValidarTurmasAgrupadas()
    {
        var existTurmaAgrupada = await _turmaRepository.ExistsTurmaAgrupada();

        if (existTurmaAgrupada)
        {
            var turmasBanco = await _turmaRepository.GetTurmasAgrupadas();

            var turmasAgrupadas = turmasBanco
                .GroupBy(t => new { t.Professor, t.CodigoHorario })
                .Where(g => g.Count() > 1)
                .Select(g => new
                {
                    TurmaBase = g.OrderByDescending(t => t.QuantidadeAlunos).First(),
                    TurmasAgrupadas = g.OrderByDescending(t => t.QuantidadeAlunos).Skip(1).ToList()
                })
                .ToList();

            foreach (var grupo in turmasAgrupadas)
            {
                grupo.TurmaBase.TotalQuantidadeAlunosAgrupados = grupo.TurmasAgrupadas.Sum(t => t.QuantidadeAlunos) + grupo.TurmaBase.QuantidadeAlunos;
                grupo.TurmaBase.TurmasGradeAntiga = grupo.TurmasAgrupadas;
            }

            await _unitOfWork.Commit();
        }
    }
}